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
    public void Attack()
    {
        if (canAct)
        {
            if (isJumping)
            {

            }
            else
            {

            }
        }
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
