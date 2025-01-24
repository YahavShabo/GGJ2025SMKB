using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int ID = 0;
    public GameObject blockWall;
    public GameObject cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ID > 0)
            {
                other.GetComponent<Player>().maxLife = ID;
                other.GetComponent<Player>().life = ID;
                other.GetComponent<Reverter>().originalSp = other.transform.position;
                other.GetComponent<Player>().currentPhase = ID;
                blockWall.SetActive(true);
                if(ID ==1)
                {
                    cam.GetComponent<MoveCam>().leftX = -30.6f;
                }
                if(ID ==2)
                {
                    cam.GetComponent<MoveCam>().leftX = 35.07f;
                }
            }
        }
    }
}
