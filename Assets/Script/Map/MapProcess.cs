using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils2D;
public class MapProcess : MonoBehaviour
{
    private bool touchEnd=true;
    private float sc ;
    private float limit;
    void Awake() {
        
    }
    void Start()
    {
        sc = View.unit;
        limit = (800*3.2f/2.0f-Screen.width/2.0f)/View.unit;
    }
    void Update()
    {
        if(Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            if(touchEnd&&Input.touches[0].tapCount == 2)//双击走路
            {
                Vector3 pos =  PositionTransform.ScreenToWorld(Input.touches[0].position,transform);
                Init.me.GetComponent<UserInput>().WorkTo(pos.x,pos.y);
                touchEnd = false;
            }
        }else if(touchEnd&&Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved){
            Vector3 delta_pos =  Input.touches[0].deltaPosition;
            Debug.Log("----------------------------");
            Debug.Log("x:"+delta_pos.x);
            Debug.Log("----------------------------");
            if(Mathf.Abs(transform.position.x+delta_pos.x/sc)<limit)
                transform.position = new Vector3(transform.position.x+delta_pos.x/sc,transform.position.y,transform.position.z);
        }
        else if(Input.touchCount == 1 && (Input.touches[0].phase == TouchPhase.Ended||Input.touches[0].phase == TouchPhase.Canceled)){
            touchEnd = true;
        }
    }
}
