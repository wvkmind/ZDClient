using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;

public class Adaptation : MonoBehaviour
{
    private Camera camera;
    // Start is called before the first frame update
    void Awake()
    {
        camera = gameObject.GetComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = 9.6f;
        camera.backgroundColor = Color.black;
        PhoneDevice phone = new PhoneDevice();
        camera.rect = new Rect(0.0f, phone.notch.bottom/Screen.height, 1, (Screen.height-(phone.notch.bottom+phone.notch.top))/Screen.height);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
