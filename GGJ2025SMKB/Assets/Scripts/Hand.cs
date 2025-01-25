//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float attackRate=5f;
    Animator anim;
    float resetTime;
    float attackTime;
    public bool Rising = false;
    public AudioSource attack;
    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        attackTime = -attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - resetTime >= (double)1/15 && Rising)
        {
            PolygonCollider2D newCollider = gameObject.AddComponent<PolygonCollider2D>();
            newCollider.isTrigger = true;
            Destroy(GetComponent<PolygonCollider2D>());
            resetTime = Time.timeSinceLevelLoad;
        }
        if(Time.timeSinceLevelLoad - attackTime >= attackRate)
        {
            Attack();
        }
    }
    public void Attack()
    {
        attack.Play();
        attackTime= Time.timeSinceLevelLoad;
        anim.Play("Hand");
    }
}
