using UnityEngine;

public class HPCapsule : Target
{
    private void Awake()
    {
        hitsRemaining = 1;
        moveSpeed = 0;
        timeToDespawn = 1;
    }
    protected override void ProcessClick()
    {
        base.ProcessClick();

        if (hitsRemaining <= 0)
        {
            GameManager.instance.health += 1;
        }
    }
}
