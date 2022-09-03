using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public float lookSensitivity;
    public float minXLook;
    public float maxXLook;
    public Transform camAnchor;

    public bool invertXRotation;
 
    private float _curXRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.eulerAngles += Vector3.up * x * lookSensitivity;

        if (invertXRotation)
            _curXRot += y * lookSensitivity;
        else
            _curXRot -= y * lookSensitivity;
        _curXRot = Mathf.Clamp(_curXRot, minXLook, maxXLook);

        Vector3 clampedAngle = camAnchor.eulerAngles;
        clampedAngle.x = _curXRot;
        camAnchor.eulerAngles = clampedAngle; 
    }
}
