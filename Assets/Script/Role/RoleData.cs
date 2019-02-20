using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;

public class RoleData : MonoBehaviour
{
    public User data;
    public bool isMe(){return data.id==Init.userInfo.id;}
    private static float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        AddTiLiZhi();
    }
    void AddTiLiZhi()
    {
        if(Init.me.GetComponent<RolePos>().IsStop())
        {
            timer += Time.deltaTime;
            if(timer>=1){
                timer = 0;
                if(data.tilizhi!=100)
                {
                    int n = data.phy_str_rate/10;
                    if(n<1)n=1;
                        data.tilizhi = data.tilizhi + n;
                    if(isMe()){
                        Dictionary<string, object> dic = NetWork.getSendStart();
                        dic.Add("name", "atlz");
                        dic.Add("n", data.tilizhi);
                        NetWork.Push(dic,false);
                    }
                }
            }
        }
    }
}
