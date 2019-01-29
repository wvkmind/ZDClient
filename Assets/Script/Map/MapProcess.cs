﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils2D;
using UnityEngine.EventSystems;
public class MapProcess : MonoBehaviour
{
    private bool touchEnd=true;
    private float sc ;
    private float limit;
    private float unit; 
    private float proportion;
    void Awake() {
        
    }
    void Start()
    {
        proportion = Screen.height/600.0f;
        unit = Screen.height/2.0f/Camera.main.orthographicSize;
        sc = unit;
        limit = (400.0f-Screen.width/(Screen.height/600.0f)/2.0f)*3.2f/100.0f;
    }
    void Update()
    {
        if(Input.touchCount == 1 )
        {
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("UI");
            }else{
                if(Input.touches[0].phase == TouchPhase.Began)
                {
                    if(touchEnd&&Input.touches[0].tapCount == 2)//双击走路
                    {
                        touchEnd = false;
                        float real_screen_x = Input.touches[0].position.x;
                        float real_screen_y = Input.touches[0].position.y;
                        float design_x = (real_screen_x-Screen.width/2.0f)/proportion/100.0f;
                        float design_y = (real_screen_y-Screen.height/2.0f)/proportion/100.0f;
                        Vector3 pos = new Vector3(design_x,design_y,1.0f) - gameObject.transform.localPosition/3.2f;
                        Init.me.GetComponent<UserInput>().WorkTo(pos.x,pos.y);
                    }
                }else if(touchEnd && Input.touches[0].phase == TouchPhase.Moved){
                    Vector3 delta_pos =  Input.touches[0].deltaPosition;
                    if(Mathf.Abs(transform.position.x+delta_pos.x/sc)<limit)
                        transform.position = new Vector3(transform.position.x+delta_pos.x/sc,transform.position.y,transform.position.z);
                }
                else if(Input.touches[0].phase == TouchPhase.Ended||Input.touches[0].phase == TouchPhase.Canceled){
                    touchEnd = true;
                }
            }
        }
    }
}
