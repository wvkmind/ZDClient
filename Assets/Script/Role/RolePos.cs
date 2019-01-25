using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePos : MonoBehaviour
{
    private Vector3 start_position;
    private Vector3 end_position;
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
        end_position.x = transform.position.x;
        end_position.y = transform.position.y;
    }
    public void WorkTo(float x,float y){
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
        if(transform.position.x!=end_position.x||transform.position.y!=end_position.y)
        {
            transform.position=Vector3.MoveTowards(transform.position,end_position,speed*Time.deltaTime);
            if(Mathf.Abs(end_position.x-transform.position.x)>Mathf.Abs(end_position.y-transform.position.y))
            {
                if(end_position.x>transform.position.x)
                {
                    roleRender.SetRight();
                }
                else
                {
                    roleRender.SetLeft();
                }
            }else
            {
                if(end_position.y<=transform.position.y)
                {
                    roleRender.SetFront();
                }
                else
                {
                    roleRender.SetBack();
                }
            }
        }
        if(Mathf.Abs(before_x-transform.position.x)<0.001&&Mathf.Abs(before_y-transform.position.y)<0.001){
            Clear();
            roleRender.SetIdle(true);
        }
        before_x = transform.position.x;
        before_y = transform.position.y;
    }
}
