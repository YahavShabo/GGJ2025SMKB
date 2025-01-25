//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int ID = 0;
    public GameObject blockWall;
    public GameObject cam;
    public GameObject reaper;
    bool enterd=false;//entered the checkpoint

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ID > 0 && !enterd)
            {
                enterd = true;
                Player player = other.GetComponentInParent<Player>();
                player.maxLife++;
                player.life = player.maxLife;
                other.GetComponentInParent<Reverter>().originalSp = other.transform.position;
                blockWall.SetActive(true);
                if(ID ==1)
                {
                    cam.GetComponent<MoveCam>().leftX = -30.6f;
                    player.BubbleUnlock = true;
                }
                if(ID ==2)
                {
                    cam.GetComponent<MoveCam>().leftX = 35.07f;
                    player.BubbleUnlock = true;
                    player.DashUnlock = true;
                    player.CanFly = true;
                    reaper.SetActive(true);
                }
            }
        }
    }
}
