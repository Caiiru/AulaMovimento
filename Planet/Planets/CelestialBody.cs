using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass {get;private set;}
    public float surfaceGravity;
    public float radius;
    public Vector3 initialVelocity;
    public string bodyName  ="Unnamed";
    public Vector3 currentVelocity{get; private set;}
    [SerializeField]Transform spriteHolder;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        currentVelocity = initialVelocity;

    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep)
    {
        foreach (var otherbody in allBodies)
        {
            if (otherbody != this)
            {
                float sqrDst = (otherbody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (otherbody.rb.position - rb.position).normalized;
                Vector3 force = forceDir * Universe.gravitationalConstant * mass * otherbody.mass / sqrDst;
                Vector3 acceleration = force / mass;
                currentVelocity += acceleration * timeStep;
            }
        }
    }
    public void UpdateVelocity(Vector3 acceleration, float timeStep){
        currentVelocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep)
    {
        rb.MovePosition(rb.position + new Vector2(currentVelocity.x, currentVelocity.y) * timeStep);
    }

    private void OnValidate() {
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
        spriteHolder=transform.GetChild(0);

        spriteHolder.localScale = Vector2.one * radius;
        gameObject.name = bodyName;
    }

    public Vector3 Position {
        get{
            return rb.position;
        }
    }
}
