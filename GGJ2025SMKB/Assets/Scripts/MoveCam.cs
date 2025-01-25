//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{

    public Transform player;
    public float Yoffset;
    public float leftX = -102.9f;
    public float rightX = 103.6f;
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float clampedX = Mathf.Clamp(player.position.x, leftX, rightX);
        if(transform.position.x<clampedX)
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(clampedX, player.position.y + Yoffset, -10),3f);
        }
        transform.position = new Vector3(clampedX, player.position.y + Yoffset,-10);
    }
}
