using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpy : Enemy
{
    public float XVelocity=2;
    public float YVelocity=5;
    public void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Calculate the intended velocity
        Vector2 jumpVelocity = new Vector2(XVelocity * faceDirection, YVelocity);
        rb.velocity = jumpVelocity;

        // Ensure the position stays within leftX and rightX
        float clampedX = Mathf.Clamp(transform.position.x, leftX, rightX);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}
