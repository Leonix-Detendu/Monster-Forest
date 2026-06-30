using UnityEngine;

public class EnemyType3 : Target
{
    protected override void Awake()
    {
        base.Awake();
        isType3 = true;
    }

    protected override void ProcessClick()
    {
        base.ProcessClick();

        if (hitsRemaining <= 0)
        {
            GameManager.instance.health += 1;
            GameManager.instance.ActualizeHealth();
        }
    }
}
