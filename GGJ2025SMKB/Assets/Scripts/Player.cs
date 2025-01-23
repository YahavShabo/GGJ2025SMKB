using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, GameControls.IControlsActions
{
    GameControls controls;
    public float defaultGS; // Gravity scale
    public float currentGS;
    public float moveSpeed = 4f;
    public float fireRate = 0.5f;
    public Transform rot;
    public Transform point;
    public GameObject bubble;
    public int life;
    public int maxLife = 1;
    public int angle;
    public int moveDir = 1;
    public float resetDelay = 1;
    public float aimDir = 1;
    public float add = 0;
    private float moveX = 0f;
    private Vector2 aim = new Vector2(0, 0);
    private float lastfire;
    private bool holdingFire = false;
    private float rotZ;
    void Awake() 
    {
        life = maxLife;
        controls = new GameControls();
        controls.Enable();
        controls.Controls.SetCallbacks(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        lastfire = -fireRate;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(moveX * moveSpeed * Time.deltaTime,0,0);
        Aim();
        Fire();
        if (Input.GetKeyDown(KeyCode.R)) // Example: Press "R" to trigger the event
        {
            EventManager.RevertPhase?.Invoke();
            Debug.Log("RevertPhase event invoked.");
        }
    }

    public void OnMovment(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveX = context.ReadValue<float>();
        }
        else
        {
            moveX = 0;
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }
    public void Aim()
    {
        // Check if the player is aiming
        bool isAiming = aim != Vector2.zero;

        if (isAiming)
        {
            // Calculate rotation and angle
            rotZ = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            rot.transform.rotation = Quaternion.Euler(0, 0, rotZ + 180 * add);
            angle = Mathf.RoundToInt((Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg) + 360) % 360;

            // Determine direction based on angle
            if ((angle >= 0 && angle < 90) || (angle > 270 && angle <= 360)) // Right rotation
            {
                add = 0;
                aimDir = 1;
            }
            else if (angle > 90 && angle < 270) // Left rotation
            {
                add = 1;
                aimDir = -1;
            }

            // Set object scale for aiming direction
            transform.localScale = new Vector3(aimDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Set object scale for movement direction
            transform.localScale = new Vector3(moveDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Update movement direction based on input
        if (moveX > 0)
        {
            moveDir = 1;
        }
        else if (moveX < 0)
        {
            moveDir = -1;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) 
        { 
            holdingFire = true;
        }
        else
        {
            holdingFire = false;
        }

    }
    public void Fire()
    {
        if (holdingFire && Time.timeSinceLevelLoad >= lastfire + fireRate)
        {
            GameObject lastBubble;
            //add fire anim of somekind
            lastfire = Time.timeSinceLevelLoad;
            lastBubble = Instantiate(bubble, point.transform.position , Quaternion.identity);
            if(aim.x == 0)
            {
                lastBubble.GetComponent<Bubble>().transVec = new Vector3(transform.localScale.x, aim.y, 0).normalized;
            }
            else
            {
                lastBubble.GetComponent<Bubble>().transVec = new Vector3(aim.x, aim.y, 0).normalized;
            }
            Debug.Log(lastBubble.GetComponent<Bubble>().transVec);
        }
    }
    public void RevertStats()
    {
        life = GetComponent<Player>().maxLife;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        moveDir = 1;
        aimDir = 1;
        angle = 0;
        rot.transform.rotation = Quaternion.Euler(0, 0, 0);
        controls.Disable();
        Invoke("TurnOnInputSystem", resetDelay);//one sec delay
        //disable and enable input system
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy" || other.tag == "spike")
        {
            if (life <= 0)
            {
                EventManager.RevertPhase?.Invoke();
            }
        }
    }
    public void TurnOnInputSystem()
    {
        controls.Enable();
    }
}
