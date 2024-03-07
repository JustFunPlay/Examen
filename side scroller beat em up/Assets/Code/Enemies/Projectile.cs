using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 targetPosition;
    int damage;
    float splashArea;
    Rigidbody rb;
    
    public void StartProjectile(Vector3 targetPosition, int damage, float splashArea)
    {
        this.targetPosition = targetPosition;
        this.damage = damage;
        this.splashArea = splashArea;

        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        Vector3 dir = targetPosition - startPosition;
        Vector3 groundDir = new Vector3(dir.x, 0, dir.z);
        targetPosition = new Vector3(groundDir.magnitude, dir.y, 0);
        float initialVelocity;
        float time;
        float angle;
        float height = targetPosition.y + targetPosition.magnitude / 2f;
        height = Mathf.Max(0.001f, height);
        CalculatePathWithHeight(targetPosition, height, out initialVelocity, out angle, out time);
        StartCoroutine(ProjectileInAir(dir, initialVelocity, angle, time));
    }

    float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }
    void CalculatePathWithHeight(Vector3 targetPos, float h, out float initialVelocity, out float angle, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float a = (-0.5f * g);
        float b = Mathf.Sqrt(2 * g * h);
        float c = -yt;

        float tPlus = QuadraticEquation(a, b, c, 1);
        float tMin = QuadraticEquation(a, b, c, -1);

        time = tPlus > tMin ? tPlus : tMin;
        angle = Mathf.Atan(b * time / xt);
        initialVelocity = b / Mathf.Sin(angle);
    }
    private IEnumerator ProjectileInAir(Vector3 dir, float initialVelocity, float angle, float time)
    {
        float airTime = 0;
        while (airTime < time)
        {
            float x = initialVelocity * airTime * Mathf.Cos(angle);
            float y = initialVelocity * airTime * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(airTime, 2);
            rb.MovePosition(startPosition + dir * x + Vector3.up * y);

            airTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Collider[] colliders = Physics.OverlapSphere(targetPosition, splashArea);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Character>(out Character character))
            {
                character.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
