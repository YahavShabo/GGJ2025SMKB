//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float defaultGS = 1;// gravity scale
    public float currentGS;
    public float hitTime;
    public float recoverDelay = 3;
    public float recoverRate = 0.05f;
    public Vector3 bubbleIncrease = new Vector3(0.1f,0.1f,0.1f);
    public GameObject Bubble;
    public GameObject currentBubble=null;
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
        if(currentBubble != null)
        {
            currentBubble.transform.position = transform.position;
        }
    }

    public void Damage(float damage)
    {
        Debug.Log("bubble hit");
        rb.gravityScale -= damage;
        currentGS = rb.gravityScale;
        hitTime = Time.timeSinceLevelLoad;
        if(currentBubble == null)
        {
            currentBubble = Instantiate(Bubble, transform) as GameObject;
            currentBubble.transform.localScale = Vector3.zero;
            currentBubble.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        currentBubble.transform.localScale += bubbleIncrease;
        currentBubble.GetComponent<Bubble>().hitTime = Time.timeSinceLevelLoad;
    }
    public void RevertToOriginal()
    {
        rb.gravityScale = defaultGS;
    }
}
