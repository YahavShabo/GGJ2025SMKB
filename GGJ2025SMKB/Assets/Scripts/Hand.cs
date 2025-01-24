//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float attackDelay=0.5f;
    Animator anim;
    float resetTime;
    public bool Rising = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        resetTime = -attackDelay;
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
        if(other.tag == "Player")
        {
            Debug.Log("Player");
            anim.Play("Hand");
        }
    }
}
