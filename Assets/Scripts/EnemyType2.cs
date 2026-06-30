using UnityEngine;

public class EnemyType2 : Target
{
    protected override void ProcessClick()
    {
        base.ProcessClick();

        if (hitsRemaining <= 0)
        {
            GameManager.instance.score += 3;
            GameManager.instance.ActualizeScore();
        }
    }
}
