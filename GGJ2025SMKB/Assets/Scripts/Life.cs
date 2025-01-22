using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float defaultGS;// gravity scale
    float currentGS;
    // Start is called before the first frame update
    void Start()
    {
        defaultGS = GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
