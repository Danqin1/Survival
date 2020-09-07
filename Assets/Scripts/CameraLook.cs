using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public FixedTouchField fixedTouch;
    private float mouseSensitivity;
    //Camera cam;
    float currentYrot;
    float currentXrot;
    //float zRot;

    float wantedYrot;
    float wantedXrot;

    public float yRotSpeed;
    public float xRotSpeed;

    private float rotationYVelocity, cameraXVelocity;
    private void Start()
    {
        //zRot = 0;
        mouseSensitivity = .2f;
        //cam = GameObject.Find("Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        wantedYrot += fixedTouch.TouchDistance.x * mouseSensitivity;
        wantedXrot -= fixedTouch.TouchDistance.y * mouseSensitivity;
        wantedXrot = Mathf.Clamp(wantedXrot, -30, 30);
        transform.rotation = Quaternion.Euler(currentXrot, currentYrot,0 );
        /*if(cam)
        {
            cam.transform.localRotation = Quaternion.Euler(currentXrot, 0, zRot);
        }*/
    }
    void FixedUpdate()
    {
        currentYrot = Mathf.SmoothDamp(currentYrot, wantedYrot, ref rotationYVelocity, yRotSpeed);
        currentXrot = Mathf.SmoothDamp(currentXrot, wantedXrot, ref cameraXVelocity, xRotSpeed);
    }

}
