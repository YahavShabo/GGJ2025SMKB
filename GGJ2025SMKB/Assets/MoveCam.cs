//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{

    public Transform player;
    public float Yoffset;
    public float leftX = -102.9f;
    public float rightX = 103.6f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float clampedX = Mathf.Clamp(player.position.x, leftX, rightX);
        transform.position = new Vector3(clampedX, player.position.y + Yoffset,-10);
    }
}
