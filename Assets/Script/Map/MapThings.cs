using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;
public class MapThings : MonoBehaviour
{
    public UnityEngine.GameObject UserSpwanPos;
    public UnityEngine.GameObject prefab;
    void InitRoles(){
        for(int i =0;i<Init.otherUsersInCurMap.Count-1;i++){
            User u = Init.otherUsersInCurMap[i] as User;
            if(u!=null&&u.status==0){
                UnityEngine.GameObject user = (UnityEngine.GameObject) Instantiate(prefab, new Vector3(u.x, u.y, 0), Quaternion.identity,this.transform);
                user.transform.localScale = new Vector3(1,1,1);
                user.GetComponent<RoleData>().data = u;
                user.GetComponent<RoleRender>().SetName(u.name);
                user.GetComponent<RoleRender>().SetLevel(u.level);
                Init.PutRoleObjectWithId(u.id,user);
            }else
            {
                break;
            }
        }
        Init.me = (UnityEngine.GameObject)Instantiate(prefab, new Vector3(UserSpwanPos.transform.position.x,UserSpwanPos.transform.position.y , 0), Quaternion.identity,this.transform);
        Init.me.transform.localScale = new Vector3(1,1,1);
        Init.me.GetComponent<RoleData>().data = Init.userInfo;
        Init.me.GetComponent<RoleRender>().SetName(Init.userInfo.name);
        Init.me.GetComponent<RoleRender>().SetLevel(Init.userInfo.level);
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
