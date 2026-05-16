using UnityEngine;

public class EnemyType2 : Target
{
    private void Awake()
    {
        hitsRemaining = 3;
        moveSpeed = 10;
        timeToDespawn = 3;
    }
    protected override void ProcessClick()
    {
        base.ProcessClick();

        if (hitsRemaining <= 0)
        {
            GameManager.instance.score += 3;
        }
    }
}
