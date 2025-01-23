using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float dmg = 0.3f;
    public float speed = 5f;
    public Vector3 transVec;
    public float destroyTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Pop", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transVec * speed * Time.deltaTime);
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
