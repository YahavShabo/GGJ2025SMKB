//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float attackRate=1f;
    public float attackDelay = 0.3f;
    Animator anim;
    float resetTime;
    float attackTime;
    public bool Rising = false;
    // Start is called before the first frame update
    void Start()
    {
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && Time.timeSinceLevelLoad - attackTime >=attackRate)
        {
            Invoke(nameof(Attack), attackDelay);
        }
    }
    public void Attack()
    {
        attackTime= Time.timeSinceLevelLoad;
        anim.Play("Hand");
    }
}
