using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class RolePos : MonoBehaviour
{
    private Vector3 start_position;
    private Vector3 end_position;
    private bool move_flag = false;
    private float before_x;
    private float before_y;
    public float speed;
    private RoleRender roleRender;
    float UpdateZ(float y)
    {
        return -1-(y+3)/20.0f;
    }
    void Awake() {
        transform.position = new Vector3(transform.position.x,transform.position.y,UpdateZ(transform.position.y));
        Clear();
    }
    void Clear()
    {
        before_x = transform.position.x;
        before_y = transform.position.y;
        move_flag = false;
    }
    public void WorkTo(float x,float y){
        move_flag = true;
        end_position = new Vector3(x,y,UpdateZ(y));
        roleRender.SetWalk();
    }
    public void ToPosImmediately(float x,float y){
        transform.position = new Vector3(x,y,UpdateZ(y));
    }
    void Start()
    {
        roleRender = gameObject.GetComponent<RoleRender>();
    }
    void Update()
    {
        
        
        if(move_flag&& (transform.localPosition.x!=end_position.x||transform.localPosition.y!=end_position.y))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,end_position,speed*Time.deltaTime);
            
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
        if(Mathf.Abs(before_x-transform.localPosition.x)<0.005&&Mathf.Abs(before_y-transform.localPosition.y)<0.005){
            Clear();
            roleRender.SetIdle(true);
        }
        before_x = transform.localPosition.x;
        before_y = transform.localPosition.y;
    }
}
