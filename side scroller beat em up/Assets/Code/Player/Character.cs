using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int lives = 3;
    [SerializeField] private int score;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator animator;
    private bool canAct = true;
    private bool isJumping;
    private float comboTime;
    private Rigidbody rb;
    Vector2 moveVector;
    private float leftEdge, rightEdge;
    [SerializeField] private List<AttackType> currentCombo = new List<AttackType>();
    [SerializeField] private ComboAction[] comboActions;

    public void SetMoveEdges(float leftEdge, float rightEdge)
    {
        this.leftEdge = leftEdge;
        this.rightEdge = rightEdge;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (canAct)
        {
            rb.MovePosition(new Vector3(Mathf.Clamp(transform.position.x + moveVector.x * movementSpeed * Time.deltaTime, leftEdge, rightEdge), transform.position.y, Mathf.Clamp(transform.position.z + moveVector.y * movementSpeed * Time.deltaTime, -3f, 3f)));
        }
    }

    public void TakeDamage(int damageToDo)
    {
        health -= damageToDo;
        if (health <= 0) OnDeath();
    }

    private void OnDeath()
    {
        if (lives > 0)
        {
            lives--;
            health = 100;
        }
        Debug.Log("Game over");
    }

    public void Move(Vector2 moveDirection)
    {
        if (moveDirection.x > 0)
            transform.LookAt(transform.position + Vector3.right);
        else if (moveDirection.x < 0)
            transform.LookAt(transform.position + Vector3.left);
        moveVector = moveDirection;
    }
    public void Attack(AttackType attackType)
    {
        if (canAct)
        {
            ComboAction activeCombo = null;
            if (isJumping)
            {

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
                Debug.Log($"player deals {activeCombo.Damage} damage");
                StartCoroutine(WaitToFinishAttack(activeCombo.AttackTime));
                currentCombo.Add(attackType);
            }
        }
    }
    IEnumerator WaitToFinishAttack(float attackTime)
    {
        canAct = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(attackTime);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        canAct = true;
    }
    public void Jump()
    {
        if (!canAct || isJumping) return;
        rb.AddForce(transform.up * 5, ForceMode.VelocityChange);
        isJumping = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Floor")
            isJumping = false;
    }
}

[System.Serializable]
public class ComboAction
{
    public AttackType ComboUnit;
    public int Damage;
    public float AttackTime;
    public ComboAction[] FollowUpActions;
}

public enum AttackType
{
    Light,
    Heavy
}