using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float dmg = 0.3f;
    public float speed = 5f;
    public Vector3 transVec;
    public float destroyTime = 5;
    public float hitTime;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent == null)
        {
            Invoke("Pop", destroyTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transVec * speed * Time.deltaTime);
        if (transform.parent != null && Time.timeSinceLevelLoad - hitTime >= destroyTime)
        {
            Pop();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="enemy" && other.GetComponent<Life>()!=null)
        {
            other.GetComponent<Life>().Damage(dmg);
        }
        else if( transform.parent == null && (other.tag == "Structure" || other.tag == "spike"))
        {
            Pop();
        }
    }
    public void Pop()
    {
        GetComponent<Animator>().Play("Pop");
    }
    public void ResetGravity()
    {
        if (transform.parent != null)
        {
            float defaultScale = GetComponentInParent<Life>().defaultGS;
            float scale =GetComponentInParent<Life>().rb.gravityScale = defaultScale;
        }
    }
    public void DestroyBubble()
    { 
        Destroy(gameObject);
    }
}
