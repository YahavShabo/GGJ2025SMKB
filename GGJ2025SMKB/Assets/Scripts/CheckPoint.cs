using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int ID = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ID > 0)
            {
                other.GetComponent<Player>().maxLife = ID;
                other.GetComponent<Player>().life = ID;
                other.GetComponent<Reverter>().originalSp = other.transform.position;
            }
        }
    }
}
