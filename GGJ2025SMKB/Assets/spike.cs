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
    // Start is called before the first frame update
    void Start()
    {
        spikeChild = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            spikeChild.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    public void ShootSpike()
    {
        isMoving = true;
    }
    public void DestroyInvoke()
    {
        Invoke(nameof(Destroy),DestoryDelay);
    }

    public void Destroy()
    {
        if(spikeChild != null)
        {
            Destroy(spikeChild.gameObject);
            isMoving = false;
            GameObject currentSpike = Instantiate(spikeObject, transform) as GameObject;
            spikeChild = currentSpike.transform;
        }
    }

}
