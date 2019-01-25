using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;
public class MapThings : MonoBehaviour
{
    public UnityEngine.GameObject UserSpwanPos;
    public UnityEngine.GameObject prefab;
    private UnityEngine.GameObject me;
    private ArrayList other;
    void InitRoles(){
        for(int i =0;i<9;i++){
            User u = Init.otherUsersInCurMap[i];
            if(u!=null&&u.status==0){
                UnityEngine.GameObject user = (UnityEngine.GameObject) Instantiate(prefab, new Vector3(u.x, u.y, 0), Quaternion.identity,this.transform);
                user.transform.localScale = new Vector3(1,1,1);
                user.GetComponent<RoleData>().data = u;
                other.Add(user);
            }else
            {
                break;
            }
        }
        me = (UnityEngine.GameObject)Instantiate(prefab, new Vector3(UserSpwanPos.transform.position.x,UserSpwanPos.transform.position.y , 0), Quaternion.identity,this.transform);
        me.transform.localScale = new Vector3(1,1,1);
        me.GetComponent<RoleData>().data = Init.userInfo;
    }
    void Awake() {
        InitRoles();
    }    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
