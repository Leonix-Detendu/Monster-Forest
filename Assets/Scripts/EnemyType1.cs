using UnityEngine;

public class EnemyType1 : Target
{
    protected override void ProcessClick()
    {
        base.ProcessClick();

        if (hitsRemaining <= 0)
        {
            GameManager.instance.score += 1;
        }
    }
}
