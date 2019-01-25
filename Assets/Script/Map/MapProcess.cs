using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class MapProcess : MonoBehaviour
{
    void Awake() {
        
    }
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("----------------------------");
            Debug.Log("tapCount:"+Input.touches[0].tapCount);
            if(Input.touches[0].tapCount == 2)//双击走路
            {
                Debug.Log("position:"+Input.touches[0].position);
                Vector3 pos =  PositionTransform.ScreenToWorld(Input.touches[0].position,transform);
                Debug.Log("x:"+pos.x);
                Debug.Log("y:"+pos.y);
                Init.me.GetComponent<UserInput>().WorkTo(pos.x,pos.y);
            }
            Debug.Log("----------------------------");
        }
    }
}
