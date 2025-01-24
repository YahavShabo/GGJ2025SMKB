//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UIElements;

public class PlayerBubble : MonoBehaviour
{
    public Animator anim;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Deflate()
    {
        anim.Play("Deflate");
    }
    public void Pop()
    {
        anim.Play("PlayerBubblePop");
    }
    public void Deactivate()
    {
        anim.Play("Deactivated");
    }
}
