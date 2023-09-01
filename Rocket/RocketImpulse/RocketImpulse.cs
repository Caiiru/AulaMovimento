using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketImpulse : MonoBehaviour
{


    #region Components
    private Rigidbody2D rigidbody2D;

    #endregion
    #region Circle Marker

    public GameObject circleMarker;
    #endregion
    #region Rotate
    bool isRotating;
    float currentAngle;
    private float angleVelocity;
    private Vector3 mousePosition; 
    float angularDistance;
    float finalAngle;

    #endregion
    #region Movement
    private Vector3 targetPosition;
    #endregion
     void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleMarker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        checkMousePosition();
        checkClick();
    } 

    void checkMousePosition(){
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        targetPosition = mousePosition-transform.position;
        Rotate();
    }
    void checkClick(){
        if(Input.GetMouseButtonDown(0)){
            rigidbody2D.AddForce(targetPosition,ForceMode2D.Impulse);
        }
    }
    
    void Rotate(){
        currentAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg - 90f;
        rigidbody2D.rotation = currentAngle;
    }
}
