using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Weapon HeldWeapon;
    private Player player;
    private LevelManager levelManager;
    [SerializeField] private int health = 100;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator animator;
    private bool canAct;
    public bool IsAlive { get; private set; }
    private bool isJumping;
    private float comboTime;
    private Rigidbody rb;
    Vector2 moveVector;
    [SerializeField] private float leftEdge, rightEdge, frontEdge, backEdge;
    private List<AttackType> currentCombo = new List<AttackType>();
    [SerializeField] private ComboAction[] comboActions;
    [SerializeField] private ComboAction[] aerialCombos;
    public Action<int> OnChangeHealth;

    public void InitializeCharacter(Player player, float frontWorldEdge,  float backWorldEdge, LevelManager levelManager)
    {
        this.player = player;
        frontEdge = frontWorldEdge;
        backEdge = backWorldEdge;
        this.levelManager = levelManager;
        rb = GetComponent<Rigidbody>();
        IsAlive = true;
        canAct = true;
    }

    public void SetMoveEdges(float leftEdge, float rightEdge)
    {
        this.leftEdge = leftEdge;
        this.rightEdge = rightEdge;
    }

    private void Update()
    {
        if (!IsAlive) return;
        if (canAct)
        {
            rb.MovePosition(new Vector3(Mathf.Clamp(transform.position.x + moveVector.x * movementSpeed * Time.deltaTime, leftEdge, rightEdge), transform.position.y, Mathf.Clamp(transform.position.z + moveVector.y * movementSpeed * Time.deltaTime, frontEdge, backEdge)));
        }
        if (moveVector != Vector2.zero && canAct) animator.SetBool("Walk", true);
        else animator.SetBool("Walk", false);
        if (comboTime > 0)
        {
            comboTime -= Time.deltaTime;
        }
        else if (comboTime < 0)
        {
            comboTime = 0;
            currentCombo.Clear();
        }
    }

    public void TakeDamage(int damageToDo)
    {
        health -= damageToDo;
        OnChangeHealth.Invoke(health);
        if (health <= 0 && IsAlive) OnDeath();
    }

    private void OnDeath()
    {
        animator.SetTrigger("Death");
        IsAlive = false;
        canAct = false;
        StartCoroutine(WaitToRevive());
        Debug.Log("Game over");
    }
    IEnumerator WaitToRevive()
    {
        canAct = false;
        rb.useGravity = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        //yield return new WaitForSeconds(4f);
        if (GameManager.CheckForRevive(player))
        {
            //levelManager.DenyDefeat();
            animator.SetTrigger("Revive");
            yield return new WaitForSeconds(0.5f);
            IsAlive = true;
            canAct = true;
            health = 100;
            OnChangeHealth.Invoke(health);
            GetComponent<Collider>().enabled = true;
            rb.useGravity = true;
        }
        else
        {
            levelManager.ConfirmDefeat();
            levelManager.RemoveFromCamera(this);
        }
    }

    public void Move(Vector2 moveDirection)
    {
        if (!IsAlive) return;
        if (moveDirection.x > 0)
            transform.LookAt(transform.position + Vector3.right);
        else if (moveDirection.x < 0)
            transform.LookAt(transform.position + Vector3.left);
        moveVector = moveDirection;
    }
    public void Attack(AttackType attackType)
    {
        if (canAct && IsAlive)
        {
            ComboAction activeCombo = null;
            if (isJumping)
            {
                for (int i = 0; i < aerialCombos.Length; i++)
                {
                    if (aerialCombos[i].ComboUnit == attackType)
                    {
                        activeCombo = aerialCombos[i];
                        break;
                    }
                }
            }
            else
            {
                if (currentCombo.Count > 0)
                {
                    int comboDepth = 0;
                    for (int i = 0; i < comboActions.Length; i++)
                    {
                        if (comboActions[i].ComboUnit == currentCombo[0])
                        {
                            activeCombo = comboActions[i];
                            comboDepth++;
                            break;
                        }
                    }
                    while (comboDepth < currentCombo.Count && activeCombo != null && activeCombo.FollowUpActions.Length > 0)
                    {
                        bool hasFollowUp = false;
                        for (int i = 0; i < activeCombo.FollowUpActions.Length; i++)
                        {
                            if (activeCombo.FollowUpActions[i].ComboUnit == currentCombo[comboDepth])
                            {
                                activeCombo = activeCombo.FollowUpActions[i];
                                comboDepth++;
                                hasFollowUp = true;
                                break;
                            }
                        }
                        if (!hasFollowUp)
                        {
                            activeCombo = null;
                        }
                    }
                    if (activeCombo == null || activeCombo.FollowUpActions.Length == 0)
                    {
                        currentCombo.Clear();
                    }
                    else
                    {
                        bool hasFollowUp = false;
                        for (int i = 0; i < activeCombo.FollowUpActions.Length; i++)
                        {
                            if (activeCombo.FollowUpActions[i].ComboUnit == attackType)
                            {
                                hasFollowUp = true;
                                activeCombo = activeCombo.FollowUpActions[i];
                                comboDepth++;
                                break;
                            }
                        }
                        if (!hasFollowUp)
                        {
                            activeCombo = null;
                            currentCombo.Clear();
                        }
                    }

                }
                if (currentCombo.Count == 0)
                {
                    for (int i = 0; i < comboActions.Length; i++)
                    {
                        if (comboActions[i].ComboUnit == attackType)
                        {
                            activeCombo = comboActions[i];
                            break;
                        }
                    }
                }
            }
            if (activeCombo != null)
            {
                Collider[] enemyColliders = Physics.OverlapCapsule(transform.TransformPoint(activeCombo.HitboxStart), transform.TransformPoint(activeCombo.HitboxEnd), activeCombo.HitboxRadius);
                Debug.DrawLine(transform.TransformPoint(activeCombo.HitboxStart), transform.TransformPoint(activeCombo.HitboxEnd), Color.red, activeCombo.AttackTime);
                if (enemyColliders.Length > 0)
                {
                    int damageToBeDealt = activeCombo.Damage;
                    if (HeldWeapon != null)
                    {
                        damageToBeDealt = (int)(damageToBeDealt * (1f + HeldWeapon.damageBoost));
                        HeldWeapon.durability--;
                        if (HeldWeapon.durability <= 0)
                            Destroy(HeldWeapon.gameObject);
                    }
                    foreach (Collider collider in enemyColliders)
                    {
                        if (collider.TryGetComponent<EnemyBase>(out EnemyBase enemy))
                        {
                            enemy.TakeDamage(damageToBeDealt, this);
                        }
                        else if (collider.TryGetComponent<DestructableObject>(out DestructableObject destructableObject))
                        {
                            destructableObject.TakeDamage(damageToBeDealt);
                        }
                    }

                    Debug.Log($"player deals {damageToBeDealt} damage");
                }
                if (animator != null) animator.SetTrigger(activeCombo.AnimationCall);
                StartCoroutine(WaitToFinishAttack(activeCombo.AttackTime));
                if (!isJumping) currentCombo.Add(attackType);
            }
        }
    }
    IEnumerator WaitToFinishAttack(float attackTime)
    {
        canAct = false;
        comboTime = attackTime * 1.1f;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(attackTime * 0.75f);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        yield return new WaitForSeconds(attackTime * 0.25f);
        comboTime = 1f;
        if (IsAlive)
        canAct = true;
    }
    public void Jump()
    {
        if (!canAct || isJumping || !IsAlive) return;
        rb.AddForce(transform.up * 5, ForceMode.VelocityChange);
        if (animator != null) animator.SetBool("Jump", true);
        isJumping = true;
    }

    public void AddScore(int scoreToAdd)
    {
        int score = GameManager.AddScoreToPlayer(player, scoreToAdd);
        Debug.Log($"{gameObject.name} earned {scoreToAdd} points, for a total of {score}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Floor")
        {
            isJumping = false;
            if (animator != null) animator.SetBool("Jump", false);
        }
    }
}

[System.Serializable]
public class ComboAction
{
    public AttackType ComboUnit;
    public string AnimationCall;
    public int Damage;
    public float AttackTime;
    public Vector3 HitboxStart, HitboxEnd;
    public float HitboxRadius = 0.5f;
    public ComboAction[] FollowUpActions;
}

public enum AttackType
{
    Light,
    Heavy
}