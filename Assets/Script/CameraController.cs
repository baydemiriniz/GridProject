using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void CalculatorCamera(float gridBound)
    {
        float horizontal=gridBound*_camera.pixelHeight/_camera.pixelWidth;
        _camera.orthographicSize = horizontal*0.5f;
    }
}
