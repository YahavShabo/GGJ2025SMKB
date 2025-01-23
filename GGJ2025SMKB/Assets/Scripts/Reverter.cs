using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Reverter : MonoBehaviour
{
    public Transform originalSp;
    public int phase;
    // Start is called before the first frame update
    void Start()
    {
        originalSp = transform;//gets the original spawn point
        EventManager.RevertPhase += revert;
    }
    public void revert()
    {
        transform.position = originalSp.position;
        if (GetComponent<Life>() != null)
        {
            GetComponent<Life>().RevertToOriginal();
        }
    }
}
