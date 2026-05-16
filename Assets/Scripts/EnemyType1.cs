using UnityEngine;

public class EnemyType1 : Target
{
    private void Awake()
    {
        hitsRemaining = 1;
        moveSpeed = 10;
        timeToDespawn = 2;
    }

    protected override void ProcessClick()
    {
        base.ProcessClick();

        if (hitsRemaining <= 0)
        {
            GameManager.instance.score += 1;
        }
    }
}
