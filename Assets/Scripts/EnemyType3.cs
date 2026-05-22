using UnityEngine;

public class EnemyType3 : Target
{

    protected override void ProcessClick()
    {
        base.ProcessClick();

        if (hitsRemaining <= 0)
        {
            GameManager.instance.health += 1;
        }
    }
}
