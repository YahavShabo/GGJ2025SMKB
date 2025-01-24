using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    public float speed = 3;
    public float attactkRate=2;
    public float LastAttackTime=0;
    public int faceDirection;
    //x positions to turn around on when moving freely
    public float leftX=0;
    public float rightX=0;
    bool playerClose;
    // Start is called before the first frame update
    void Start()
    {
        if(rightX == leftX)
        {
            leftX = transform.position.x - 5;
            rightX= transform.position.x + 5;
        }
        LastAttackTime = -attactkRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerClose)
        {
            FacePlayer();
        }
        else
        {
            MoveFreely();
        }
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        // Ensure the position stays within leftX and rightX
        float clampedX = Mathf.Clamp(transform.position.x, leftX, rightX);
        transform.position = new Vector2(clampedX, transform.position.y);
        if(Time.timeSinceLevelLoad - LastAttackTime >= attactkRate && playerClose)
        {
            Attack();
        }
    }
    protected void FacePlayer()
    {
        //face the player and attack him
        if (transform.position.x < player.position.x)
        {
            //face right
            faceDirection = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            //face left
            faceDirection = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Time.timeSinceLevelLoad - LastAttackTime >= attactkRate && playerClose)
        {
            Attack();
        }
    }
    protected void MoveFreely()
    {
        if (transform.position.x <= leftX)//passed left border
        {
            //face right
            faceDirection = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.x >= rightX)//passed right border
        {
            //face left
            faceDirection = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerClose = true;
            player = other.transform;
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerClose = false;
        }
    }
    public virtual void Attack()
    {
        //play animation
        Debug.Log("enemy");
        LastAttackTime = Time.timeSinceLevelLoad;
    }
}
