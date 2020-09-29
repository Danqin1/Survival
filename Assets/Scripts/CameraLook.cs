using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    #region variables

    [SerializeField] private FixedTouchField fixedTouch;
    private float mouseSensitivity;

    private float currentYrot;
    private float currentXrot;
    private float wantedYrot;
    private float wantedXrot;
    private float yRotSpeed;
    private float xRotSpeed;

    private float rotationYVelocity, cameraXVelocity;

    #endregion

    #region Unity methods

    private void Start()
    {
        mouseSensitivity = .2f;
    }

    private void Update()
    {
        wantedYrot += fixedTouch.TouchDistance.x * mouseSensitivity;
        wantedXrot -= fixedTouch.TouchDistance.y * mouseSensitivity;
        wantedXrot = Mathf.Clamp(wantedXrot, -30, 30);
        transform.rotation = Quaternion.Euler(currentXrot, currentYrot,0 );
    }

    void FixedUpdate()
    {
        currentYrot = Mathf.SmoothDamp(currentYrot, wantedYrot, ref rotationYVelocity, yRotSpeed);
        currentXrot = Mathf.SmoothDamp(currentXrot, wantedXrot, ref cameraXVelocity, xRotSpeed);
    }

    #endregion
}
