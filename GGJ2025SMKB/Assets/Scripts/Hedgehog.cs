using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : Enemy
{
    spike childScript;

    public void ShootSpikes()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Pivot")
            {
                childScript = child.GetComponent<spike>();
                childScript.isMoving = true;
                childScript.DestroyInvoke();
            }
        }
    }
}
