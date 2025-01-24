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
    public float currentSpeed=4f;
    public float moveSpeed = 4f;
    public float dashSpeed = 15f;
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
    private Vector2 aim = new Vector2(0, 0);
    private float lastfire;
    private bool holdingFire = false;
    private float rotZ;
    public Vector2 move;
    public bool inBubble = false;//if inside of a bubble gs = 0
    public Animator anim;
    public float jumpForce = 3f;
    public bool canJump = true;
    public bool grounded = true;
    public bool jumping = false;
    public bool canShoot;
    public bool isDashing=false;
    float lastGround;
    float jumpCD = 1f;
    void Awake() 
    {
        anim = GetComponent<Animator>();
        life = maxLife;
        controls = new GameControls();
        controls.Enable();
        controls.Controls.SetCallbacks(this);
        defaultGS = GetComponent<Rigidbody2D>().gravityScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        lastfire = -fireRate;
        lastGround = -jumpCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (inBubble)
        {
            transform.Translate(move * currentSpeed * Time.deltaTime);
            anim.SetBool("walking", false);
        }
        else
        {
            transform.Translate(move.x * currentSpeed * Time.deltaTime, 0, 0);
        }
        if(isDashing)
        {
            transform.Translate(moveDir * dashSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            currentSpeed = moveSpeed;
        }
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
        if (context.performed && !isDashing)
        {
            move = context.ReadValue<Vector2>();
            anim.SetBool("walking", true);
        }
        else
        {
            move = Vector2.zero;
            anim.SetBool("walking", false);
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
        if (move.x > 0)
        {
            moveDir = 1;
        }
        else if (move.x < 0)
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
        if (holdingFire && Time.timeSinceLevelLoad >= lastfire + fireRate && canShoot && !isDashing)
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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        jumping = false;
        grounded = true;
        lastfire = -fireRate;
        lastGround = -jumpCD;
        canJump = true;
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
            else
            {
                anim.Play("Hit React");
                //prevent from movement
            }
        }
    }
    public void TurnOnInputSystem()
    {
        controls.Enable();
    }

    public void OnFlyingBubble(InputAction.CallbackContext context)
    {
        //depends on what phase
        Debug.Log("launch");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            anim.Play("Jump");
            jumping = true;
            canJump = false;
            Debug.Log("jumpCalled");
        }
    }
    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
        Debug.Log("jump");
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        currentSpeed = 0;
        anim.Play("Dash");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
            lastGround = Time.timeSinceLevelLoad;
            canJump = true; // Allow jumping when grounded
            if(!jumping && grounded)
            {
                anim.Play("Idle");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = false;
            canJump = false; // Disable jumping when not grounded
        }
    }

}
