using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class Target : MonoBehaviour
{
    [SerializeField] protected int hitsRemaining;
    public float timeToDespawn, moveSpeed, speedMax, timeToDespawnMin;
    public float maxScaleFactor, scaleFactor;
    protected Vector2 direction;
    protected Rigidbody2D rb;

    [HideInInspector] public bool wasClicked;

    [SerializeField] protected AudioClip deathSound, hitSound, despawnSound, spawnSound;

    protected bool isType3;

    [SerializeField] private GameObject ExplosionEffect;

    protected virtual void Awake()
    {
        isType3 = false;
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        GameManager.instance.audioSource.PlayOneShot(spawnSound);
        StartCoroutine(DespawnRoutine());

        moveSpeed += SpawnManager.instance.currentBonusSpeed;
        if (moveSpeed > speedMax)
        {
            moveSpeed = speedMax;
        }

        
        timeToDespawn -= SpawnManager.instance.currentDespawnReduction;
        if (timeToDespawn < timeToDespawnMin)
        {
            timeToDespawn = timeToDespawnMin;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnMouseDown()
    { 
        ProcessClick();
        GameManager.instance.targetWasClicked = true;
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
            GameManager.instance.audioSource.PlayOneShot(deathSound);
            Destroy(gameObject);

            if ( ExplosionEffect != null )
            {
                Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            }

        }
        else
        {
            GameManager.instance.audioSource.PlayOneShot(hitSound);
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
    }

    IEnumerator DespawnRoutine()
    {
        yield return new WaitForSeconds(timeToDespawn);
        if (!GameManager.instance.gameOver && !isType3)
        {
            GameManager.instance.health--;
            GameManager.instance.ActualizeHealth();
        }
        GameManager.instance.audioSource.PlayOneShot(despawnSound);
        Destroy(gameObject);
    }
}
