using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected Character targetCharacter;
    [SerializeField] protected int health = 50;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected float attackDelay;

    [SerializeField] int scoreOnHit;
    [SerializeField] int scoreOnKill;

    protected NavMeshAgent agent;
    [SerializeField] protected bool isActive;
    protected Animator animator;

    public AudioSource[] HitSound;
    public GameObject HitEffect;

    [SerializeField] protected bool canAct;
    public bool IsAlive {  get; protected set; }
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        animator = GetComponent<Animator>();
        IsAlive = true;
        //ActivateSelf();
    }
    protected virtual void Update()
    {
        if (animator != null && agent != null && agent.velocity.magnitude > 0)
        {
            animator.SetBool("Walk", true);
        }
        else if (animator != null)
        {
            animator.SetBool("Walk", false);
        }
        if (canAct && isActive)
        {
            if (agent != null && targetCharacter != null)
            {
                agent.SetDestination(targetCharacter.transform.position + (Vector3.right * (transform.position.x > targetCharacter.transform.position.x ? attackRange : -attackRange) ));
            }
            if (transform.position.x > targetCharacter.transform.position.x)
                transform.LookAt(transform.position + Vector3.left);
            else transform.LookAt(transform.position + Vector3.right);

        }
    }

    public void TakeDamage(int damageToDo, Character attacker)
    {
        health -= damageToDo;
        Debug.Log($"{gameObject.name} took {damageToDo} damage");
        attacker.AddScore(scoreOnHit);
        
        //play random hit sound
        if (HitSound.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, HitSound.Length - 1);
        HitSound[index].Play();
        HitSound[index].pitch = Random.Range(0.8f, 1.3f);

        //instantiate hit effect particle
        GameObject.Instantiate(HitEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
            attacker.AddScore(scoreOnKill);
            if (animator != null)
            {
                animator.SetTrigger("Defeat");
            }
            if (agent != null)
                IsAlive = false;
            agent.enabled = false;
            Invoke("OnDeath", 3);
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    public void ActivateSelf()
    {
        if (isActive) return;
        gameObject.SetActive(true);
        GetClosestCharacter();
        isActive = true;
        canAct = true;
    }
    protected void GetClosestCharacter()
    {
        Character[] characters = FindObjectsOfType<Character>();
        for (int i = 0; i < characters.Length; i++)
        {
            if (targetCharacter == null || (Vector3.Distance(transform.position, characters[i].transform.position) < Vector3.Distance(transform.position, targetCharacter.transform.position) && characters[i].IsAlive))
                targetCharacter = characters[i];
        }
    }
    protected void GetRandomCharacter()
    {
        Character[] characters = FindObjectsOfType<Character>();
        int rand = Random.Range(0, characters.Length);
        for (int i = 0; i < 10; i++)
        {
            if (characters[rand].IsAlive)
                break;
            else
                rand = Random.Range(0, characters.Length);
        }
        targetCharacter = characters[rand];
    }
}
