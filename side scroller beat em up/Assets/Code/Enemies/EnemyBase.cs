using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected Character closestCharacter;
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

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        animator = GetComponent<Animator>();
        ActivateSelf();
    }
    protected virtual void Update()
    {
        if (canAct && isActive)
        {
            if (agent != null && closestCharacter != null)
            {
                agent.SetDestination(closestCharacter.transform.position + (Vector3.right * (transform.position.x > closestCharacter.transform.position.x ? attackRange : -attackRange) ));
            }
            if (transform.position.x > closestCharacter.transform.position.x)
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
        closestCharacter = FindObjectOfType<Character>();
        isActive = true;
        canAct = true;
    }
}
