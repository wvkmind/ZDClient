using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;

public class RoleData : MonoBehaviour
{
    public User data;
    public bool isMe(){return data.id==Init.userInfo.id;}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
