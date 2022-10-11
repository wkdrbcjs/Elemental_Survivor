using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;
    Collider2D _Collider2D;

    void ApplyDamage(float damage)
    {
        print(damage);
    }    
 
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _Collider2D = GetComponent<CircleCollider2D>();
    }
    private void FixedUpdate()
    {      
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rigidbody2D.velocity = movement * moveSpeed;
    }    

    void OnTriggerEnter2D(Collider2D coll)
    {       
        moveSpeed = 0.0f;       
    }
}
