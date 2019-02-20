﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProcess : MonoBehaviour
{
    public UnityEngine.UI.Button button ;
    private int pos;
    private int type;
    private bool is_show = false;
    public bool delete = false;
    private RolePos mePos ;
    bool _in = false;
    void Awake() {
        button.onClick.AddListener(()=>{
            SendPick(type);
        });
        button.gameObject.SetActive(false);
    }
    void Start()
    {
        mePos = Init.me.GetComponent<RolePos>();
    }
    public void Delete(){
        Destroy(this.gameObject);
    }
    void SendPick(int _type){
        string name = "pick";
        if(_type == 1)name = "eat";
        Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("pos",pos);
		dic.Add("name", name);
		NetWork.Push(dic);
    }
    public void Set(int _pos,int _type){
        this.pos = _pos;
        this.type = _type;
    }
    void Update()
    {
        if(delete)Delete();
        if(!is_show&&_in&&Init.me.GetComponent<RolePos>().IsStop())
        {
            is_show = true;
            button.gameObject.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!is_show&&col.gameObject.tag == "Role"){
            RoleData role = col.gameObject.GetComponent<RoleData>();
            if(role.isMe())
            {
                RolePos pos = col.gameObject.GetComponent<RolePos>();
                if(pos.FrontMyFace(gameObject.transform.localPosition))
                {
                    _in= true;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(is_show&&col.gameObject.tag == "Role"){
            RoleData role = col.gameObject.GetComponent<RoleData>();
            if(role.isMe())
            {
                _in= false;
                is_show = false;
                button.gameObject.SetActive(false);
            }
        }
    }
     
}