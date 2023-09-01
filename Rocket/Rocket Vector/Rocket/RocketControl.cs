using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    #region Other Objects
    public GameObject circleMarker;
    #endregion

    #region Movement
    [SerializeField] private Vector3 clickedPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 velocityVector;
    [SerializeField] private float movementTime = 2f;
    [SerializeField] private float currentTimer;
    [SerializeField] private bool isMoving;

    bool wasClicked;
    #endregion

    #region Rotate
    private bool isRotating = false;
    [SerializeField] private float rotationTimer = 0f;
    float angleVector;
    [SerializeField] float currentAngle;

    private float angleVelocity;
    private float angle;
    #endregion


    private void Start()
    {
        circleMarker.SetActive(false);
        currentTimer = 0;
        wasClicked = false;
    }

    private void Update()
    {
        checkClick();
        Rotate();
        Movement();
    }

    private void checkClick()
    {
        if (Input.GetMouseButtonDown(0) && !wasClicked)
        {
            clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // pego a posição do mouse
            clickedPosition.z = 0;
            setMarker(clickedPosition); // coloco o marcador na posição clickada
            targetPosition = clickedPosition - transform.position; // Calculo o ΔS(ΔS = Pos final - pos inicial)
            wasClicked = true;
        }
    }

    private void Rotate()
    {
        if (!isRotating && wasClicked)
        {
            isRotating = true;
            float newAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg - 90f;
            angle = transform.rotation.eulerAngles.z;
            float angularDistance = newAngle - angle;
            if (angularDistance > 180f)
            {
                angularDistance -= 360f;
            }
            else if (angularDistance < -180f)
            {
                angularDistance += 360f;
            }
            angleVelocity = angularDistance / movementTime;
        }
        else if(isRotating && wasClicked && !isMoving)
        {
            rotationTimer += Time.deltaTime;
            if (angleVelocity != 0f)
            {
                angle += angleVelocity * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, 0, angle);
                if (rotationTimer >= movementTime)
                {
                    angleVelocity = 0f;
                    rotationTimer = 0f;
                    isMoving=true;
                }
            }
        }




    }

    private void Movement()
    {
        if (isMoving)
        {
            if (currentTimer < movementTime)
            {
                currentTimer += 1 * Time.deltaTime;
                velocityVector = targetPosition / movementTime; // Calculo o ΔV (ΔV = ΔS/T)
                transform.position += velocityVector * Time.deltaTime; // Adiciono o vetor de velocidade à posição 
            }
            else
            {
                FinishMovement();
            }

        }
    }
    void FinishMovement()
    {
        currentTimer = 0;
        velocityVector = Vector3.zero;
        setMarker();
        isMoving = false;
        isRotating = false;
        wasClicked=false;
        rotationTimer = 0;

    }

    #region Click Marker Set Active or Not Active
    private void setMarker(Vector3 targetPosition)
    {
        if (isMoving)
        {
            circleMarker.SetActive(false);
        }
        else
        {
            circleMarker.SetActive(true);
            circleMarker.transform.position = targetPosition;
        }
    }
    private void setMarker()
    {
        if (isMoving)
        {
            circleMarker.SetActive(false);
        }
        else
        {
            circleMarker.SetActive(true);
        }
    }

    #endregion
}
