//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    Transform Player;
    public float speed = 2.5f;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveAndEnabled)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed*Time.deltaTime);
        }
    }
}
