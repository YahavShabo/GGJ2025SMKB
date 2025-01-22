using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, GameControls.IControlsActions
{
    GameControls controls;
    public float defaultGS;// gravity scale
    float moveX = 0f;
    Vector2 aim = new Vector2 (0,0);
    float currentGS;
    public float moveSpeed = 4f;
    float rotZ;
    public Transform rot;
    void Awake() 
    {
        controls = new GameControls();
        controls.Enable();
        controls.Controls.SetCallbacks(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (aim != Vector2.zero)
        {
            rotZ = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            rot.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            //angle = Mathf.FloorToInt((Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg) + 360) % 360;
            //if needed to mirror the weapon take from GHOSTSZ
        }
    }
}
