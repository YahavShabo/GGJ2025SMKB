using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : Enemy
{
    spike childScript;

    public override void Attack()
    {
        Debug.Log("hedgehog");
        base.Attack();
        foreach (Transform child in transform)
        {
            if (child.name == "Pivot")
            {
                childScript = child.GetComponent<spike>();
                childScript.ShootSpike();
                childScript.DestroyInvoke();
            }
        }
    }
}
