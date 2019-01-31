using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private RolePos rolePos;
    private RoleRender roleRender;
    // Start is called before the first frame update
    void Awake()
    {
        rolePos = gameObject.GetComponent<RolePos>();
        roleRender = gameObject.GetComponent<RoleRender>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //这个WorkTo是外部输入调的WorkTo
    public void WorkTo(float x,float y)
    {
        rolePos.WorkTo(x,y);
    }
    //这个Action是外部输入调的Action
    public void Action(int i){
        roleRender.SetAction(i);
    }
    //这个WorkTo是外部输入调的WorkTo
    public void WorkTo(float x,float y,int direction,float tox,float toy)
    {
        roleRender.SetDirection(direction);
        rolePos.ToPosImmediately(x,y);
        rolePos.WorkTo(tox,toy);
    }
    public void Exp(int i){
        roleRender.SetExp(i);
    }
    //这个Action是网络进来调用
    public void Action(float x,float y,int direction,int i)
    {
        roleRender.SetDirection(direction);
        rolePos.ToPosImmediately(x,y);
        roleRender.SetAction(i);
    }
}
