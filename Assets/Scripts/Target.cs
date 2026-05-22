using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    [SerializeField] protected int hitsRemaining;
    [SerializeField] protected float timeToDespawn;
    [SerializeField] protected float moveSpeed;
    protected Vector2 direction;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    private void OnMouseDown()
    {
        ProcessClick();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision);
    }

    private void Bounce(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];

        Vector2 newDirection = Vector2.Reflect(direction, contact.normal);

        direction = newDirection.normalized;
    }

    protected virtual void ProcessClick()
    {
        hitsRemaining--;

        if (hitsRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
    }
}
