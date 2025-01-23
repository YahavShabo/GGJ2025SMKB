using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    Transform spikeChild;
    public GameObject spikeObject;
    public bool isMoving = false;
    public float speed = 10;
    public float DestoryDelay = 3;
    public Vector2 start;
    // Start is called before the first frame update
    void Start()
    {
        spikeChild = transform.GetChild(0);
        start = spikeChild.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            spikeChild.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }

    public void ShootSpike()
    {
        isMoving = true;
    }
    public void DestroyInvoke()
    {
        Invoke(nameof(Destroy),DestoryDelay);
        spikeChild.SetParent(null);
    }

    public void Destroy()
    {
        if(spikeChild != null)
        {
            isMoving = false;
            Destroy(spikeChild.gameObject);
            GameObject currentSpike = Instantiate(spikeObject, transform);
            spikeChild = currentSpike.transform;
            spikeChild.position = transform.position;
        }
    }

}
