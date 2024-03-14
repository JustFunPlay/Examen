using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private float moveDistance;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Vector3 throwPosition;

    private void FixedUpdate()
    {
        if (canAct)
        {
            if (Vector3.Distance(transform.position, agent.destination) <= 1.5f * agent.baseOffset + 0.5f)
            {
                canAct = false;
                StartCoroutine(PrepareToThrow());
            }
        }
    }
    private IEnumerator PrepareToThrow()
    {
        agent.SetDestination(transform.position);
        GetRandomCharacter();

        Vector3 targetPosition = targetCharacter.transform.position;
        Debug.Log($"{gameObject.name} performs attack");
        if (animator != null)
        {
            animator.SetBool("UseVariant", Random.Range(0, 2) == 0);
            animator.SetTrigger("WindUp");
        }
        yield return new WaitForSeconds(attackDelay);
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        Projectile newProjectile = Instantiate(projectile, transform.TransformPoint(throwPosition), Quaternion.identity);
        newProjectile.StartProjectile(targetPosition, damage, 0.75f);
        yield return new WaitForSeconds(attackDelay);
        StartCoroutine(MoveToNewPosition());
    }

    private IEnumerator MoveToNewPosition()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        Vector3 dir = new Vector3 (Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
        agent.SetDestination(transform.position + dir * Random.Range(1, moveDistance));
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        StartCoroutine(PrepareToThrow());
    }
}
