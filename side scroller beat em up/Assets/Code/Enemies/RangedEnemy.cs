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
            if (Vector3.Distance(transform.position, agent.destination) <= 0.1f + agent.baseOffset)
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
        yield return new WaitForSeconds(attackDelay);
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
