using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Reverter : MonoBehaviour
{
    public Vector2 originalSp;
    // Start is called before the first frame update
    void Start()
    {
        originalSp = transform.position;//gets the original spawn point
        EventManager.RevertPhase += revert;
    }
    public void revert()
    {
        Debug.Log("revert");
        transform.position = originalSp;
        if (GetComponent<Life>() != null)
        {
            GetComponent<Life>().RevertToOriginal();
        }
        if (gameObject.tag == "Player")
        {
            GetComponent<Player>().RevertStats();
        }
    }
}
