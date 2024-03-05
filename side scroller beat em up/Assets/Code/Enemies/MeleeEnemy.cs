using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    void FixedUpdate()
    {
        if (canAct)
        {
            if (Vector3.Distance(transform.position, agent.destination) <= 0.1f + agent.baseOffset)
            {
                int r = Random.Range(0, 100);
                Debug.Log($"r = {r}");
                if (r <= 5) StartCoroutine(Attack());
            }
        }
    }
    protected IEnumerator Attack()
    {
        canAct = false;
        Debug.Log($"{gameObject.name} performs attack");
        yield return new WaitForSeconds(attackDelay);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange))
        {
            if (hit.collider.TryGetComponent<Character>(out Character character))
            {
                character.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(attackDelay);
        canAct = true;
    }
}
