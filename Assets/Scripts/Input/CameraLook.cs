using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    #region variables

    [SerializeField] private FixedTouchField fixedTouch;
    [SerializeField] private Transform pivot;
    [SerializeField] private float fieldVerticalViewMin;
    [SerializeField] private float fieldVerticalViewMax;

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
        wantedXrot -= fixedTouch.TouchDistance.y * mouseSensitivity/100;
        wantedXrot = Mathf.Clamp(wantedXrot, fieldVerticalViewMin, fieldVerticalViewMax);
        Player.instance.gameObject.transform.rotation = Quaternion.Euler(0, currentYrot, 0);
        pivot.transform.position = new Vector3(pivot.transform.position.x, -currentXrot, pivot.transform.position.z);
        transform.LookAt(pivot);
    }

    void FixedUpdate()
    {
        currentYrot = Mathf.SmoothDamp(currentYrot, wantedYrot, ref rotationYVelocity, yRotSpeed);
        currentXrot = Mathf.SmoothDamp(currentXrot, wantedXrot, ref cameraXVelocity, xRotSpeed);
    }

    #endregion
}
