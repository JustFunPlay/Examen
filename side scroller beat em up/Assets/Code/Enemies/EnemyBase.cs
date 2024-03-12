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

    [SerializeField] protected bool canAct;
    public bool IsAlive { get { return health > 0; } }
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        animator = GetComponent<Animator>();
        //ActivateSelf();
    }
    protected virtual void Update()
    {
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
        if (health <= 0)
        {
            attacker.AddScore(scoreOnKill);
            OnDeath();
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
            if (targetCharacter == null || Vector3.Distance(transform.position, characters[i].transform.position) < Vector3.Distance(transform.position, targetCharacter.transform.position))
                targetCharacter = characters[i];
        }
    }
    protected void GetRandomCharacter()
    {
        Character[] characters = FindObjectsOfType<Character>();
        targetCharacter = characters[Random.Range(0, characters.Length)];
    }
}
