using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpaceshipControl : MonoBehaviour
{
    public float mass = 10f;
    public float forceIntensity=11f;

    Vector3 velocity;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        Vector3 target = newPos - transform.position;
        float newAngle = Mathf.Atan2(target.y,target.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0,0, newAngle);
        Vector3 force = Vector3.zero;
        if(Input.GetMouseButton(0)){
            force = target.normalized * forceIntensity; 
        }
        rb.AddForce(force);
        rb.gravityScale = Universe.gravitationalConstant;

        Vector3 aceleration = force/mass;
        velocity += aceleration * Universe.physicsTimeStep;

        transform.position += new Vector3(velocity.x, velocity.y,0) * Universe.physicsTimeStep;
    }

}
