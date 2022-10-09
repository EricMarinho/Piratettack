using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public void Accelerate(Rigidbody2D rb, float speed)
    {
        rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void Rotate(Rigidbody2D rb, float speed)
    {
        rb.AddTorque(speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void Stop(Rigidbody2D rb, float speed)
    {
        rb.velocity -= new Vector2(rb.velocity.x, rb.velocity.y) * speed * Time.deltaTime;
    }
}
