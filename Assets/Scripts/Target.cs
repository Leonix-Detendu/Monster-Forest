using UnityEngine;

public class Target : MonoBehaviour
{
    protected int hitsRemaining;
    protected float timeToDespawn;
    protected float moveSpeed;


    private void OnMouseDown()
    {
        ProcessClick();
    }

    protected virtual void ProcessClick()
    {
        hitsRemaining--;

        if (hitsRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }
}
