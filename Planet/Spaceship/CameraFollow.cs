using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region camera movement

    Transform player;
    Camera cam;

    #endregion
    #region zoom settings
    public float cameraZoom = 10f;
    private float lerpIntensity = 0.0008f;
    float finalCameraSize;
    #endregion

    public Vector3 offset;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();

        transform.position = new Vector3(player.position.x, player.position.y, -1f);
    }


    void Update()
    {

        Zoom();
    }

    void Zoom()
    {
        finalCameraSize = Mathf.Lerp(cam.orthographicSize, cameraZoom, lerpIntensity);
    }
    private void LateUpdate()
    {
        cam.orthographicSize = finalCameraSize;
    }

}
