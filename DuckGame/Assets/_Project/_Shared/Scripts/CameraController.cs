using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float DesignOrthographicSize = 7.5f;
    private float DesignAspect = 1920f / 1080f;

    
    private void Awake()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        GetComponent<Camera>().orthographicSize = DesignOrthographicSize * (DesignAspect / currentAspect);
    }
}
