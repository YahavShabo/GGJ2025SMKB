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

    public int angle;
    public int moveDir = 1;

    public float aimDir = 1;
    public float add = 0;
    private float moveX = 0f;
    private Vector2 aim = new Vector2(0, 0);
    private float lastfire;
    private bool holdingFire = false;
    private float rotZ;
    void Awake() 
    {
        controls = new GameControls();
        controls.Enable();
        controls.Controls.SetCallbacks(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        lastfire = -fireRate;
    }
    private void OnDestroy()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(moveX * moveSpeed * Time.deltaTime,0,0);
        Aim();
        Fire();
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
            //add fire anim of somekind
            Debug.Log("fire");
            lastfire = Time.timeSinceLevelLoad;
        }
    }
    //public void ChangeSide()
}
