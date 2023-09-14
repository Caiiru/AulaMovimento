using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float mass {get; private set;}
    public float surfaceGravity;
    public float radius;
    public float gravityRadius;
    public string bodyName;
    Transform spriteHolder;
    Rigidbody2D rb;
    CircleCollider2D collider2D;

    public GameObject player;
    Rigidbody2D playerRb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = Universe.gravitationalConstant;
        collider2D = transform.GetComponent<CircleCollider2D>();
        collider2D.radius = radius/2;
    }

    void CalculateAcceleration(){
        //primeiro descobrir a força que o planeta gera
        float gravityForce = Universe.gravitationalConstant * mass/radius*radius;
    }

    private void FixedUpdate() { 
        if(player.transform.position.magnitude < ((player.transform.position - transform.position).normalized * gravityRadius).magnitude ){
            Debug.Log("Inside"); 
            float dst = Vector3.Distance(rb.position, playerRb.position);
            Vector3 target = rb.position - playerRb.position;
            float newAngle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            Vector3 force = Vector3.zero;
            float forceIntensity = (mass * Universe.gravitationalConstant * playerRb.mass) / (dst*dst);

            force = target.normalized * forceIntensity;
            playerRb.AddForce(force);
        }
    }

    
    private void OnValidate() {
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
        spriteHolder = transform.GetChild(0);

        spriteHolder.localScale = Vector2.one * radius;
        gameObject.name = bodyName;
    }

    public Vector3 Position {
        get{
            return rb.position;
        }
    }
 

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, player.transform.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(((player.transform.position - transform.position).normalized * gravityRadius), player.transform.position);
        }

}
