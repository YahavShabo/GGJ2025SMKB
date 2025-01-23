using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float dmg = 0.3f;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="enemy")
        {
            other.GetComponent<Rigidbody2D>().gravityScale -= dmg;
            other.GetComponent<Life>().Damage(dmg);
        }
    }
    public void Pop()
    { 
        //use bubble pop animation
        Destroy(gameObject);
    }
}
