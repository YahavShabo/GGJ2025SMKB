using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float defaultGS = 1;// gravity scale
    public float currentGS;
    public float hitTime;
    public float recoverDelay = 3;
    public float recoverRate = 0.05f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGS = rb.gravityScale;
        currentGS = defaultGS;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad - hitTime >= recoverDelay && currentGS < defaultGS)
        {
            currentGS += recoverRate;
        }
        else if (currentGS >= defaultGS)
        {
            currentGS = defaultGS;
            rb.gravityScale = defaultGS;
        }
    }

    public void Damage(float damage)
    {
        rb.gravityScale -= damage;
        currentGS = rb.gravityScale;
        hitTime = Time.timeSinceLevelLoad;
    }
    public void RevertToOriginal()
    {
        rb.gravityScale = defaultGS;
    }
}
