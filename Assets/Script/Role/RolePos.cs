using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using DataModel;
public class RolePos : MonoBehaviour
{
    private Vector3 start_position;
    private Vector3 end_position;
    private bool move_flag = false;
    private bool aready_routing = false;
    private float before_x;
    private float before_y;
    public float speed;
    private RoleRender roleRender;
    private bool me = false;
    float UpdateZ(float y)
    {
        return -2+(y+3)/20.0f;
    }
    void Awake() {
        roleRender = gameObject.GetComponent<RoleRender>();
        transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,UpdateZ(transform.localPosition.y));
        Clear();
    }
    public void Clear(bool is_exp = false)
    {
        before_x = transform.localPosition.x;
        before_y = transform.localPosition.y;
        move_flag = false;
        aready_routing = false;
        if(me)
        MapProcess.SendMyTouch(before_x,before_y,roleRender.GetDirection(),before_x,before_y);
    }
    public void WorkTo(float x,float y){
        
        if(Mathf.Abs(before_x-x)<0.0005&&Mathf.Abs(before_y-y)<0.0005)
        {
            
        }
        else
        {
            move_flag = true;
            end_position = new Vector3(x,y,UpdateZ(y));
            roleRender.SetWalk();
        }
    }
    public void ToPosImmediately(float x,float y){
        transform.localPosition = new Vector3(x,y,UpdateZ(y));
    }
    void Start()
    {
        User data = gameObject.GetComponent<RoleData>().data;
        me = data.id == Init.userInfo.id;
    }
    void Update()
    {
        if(move_flag&& (transform.localPosition.x!=end_position.x||transform.localPosition.y!=end_position.y))
        {
            float t = speed*Time.deltaTime;
            Vector3 pos = Vector3.MoveTowards(transform.localPosition,end_position,t);

            transform.localPosition = new Vector3(pos.x,pos.y,pos.z);
            
            if(!aready_routing)
            {
                if(Mathf.Abs(end_position.x-transform.localPosition.x)>Mathf.Abs(end_position.y-transform.localPosition.y))
                {
                    if(end_position.x>transform.localPosition.x)
                    {
                        roleRender.SetRight();
                    }
                    else
                    {
                        roleRender.SetLeft();
                    }
                }else
                {
                    if(end_position.y<=transform.localPosition.y)
                    {
                        roleRender.SetFront();
                    }
                    else
                    {
                        roleRender.SetBack();
                    }
                }
            }
            aready_routing = true;
        }
        if(move_flag && Mathf.Abs(before_x-transform.localPosition.x)<0.0005&&Mathf.Abs(before_y-transform.localPosition.y)<0.0005){
            Clear();
            roleRender.SetIdle(true);
        }
        before_x = transform.localPosition.x;
        before_y = transform.localPosition.y;
    }
}
