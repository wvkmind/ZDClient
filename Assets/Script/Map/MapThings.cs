using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;
public class MapThings : MonoBehaviour
{
    public UnityEngine.GameObject UserSpwanPos;
    public UnityEngine.GameObject prefab;
    private static float timer = 0;
    void InitRoles(){
        for(int i =0;i<Init.otherUsersInCurMap.Count;i++){
            User u = Init.otherUsersInCurMap[i] as User;
            if(u!=null&&u.status==0){
                NewOhter(u);
            }else
            {
                break;
            }
        }
        Init.me = (UnityEngine.GameObject)Instantiate(prefab, new Vector3(UserSpwanPos.transform.localRotation.x,UserSpwanPos.transform.localRotation.y , 0), Quaternion.identity,this.transform);
        Init.me.transform.localScale = new Vector3(1,1,1);
        Init.me.GetComponent<RoleData>().data = Init.userInfo;
        Init.me.GetComponent<RoleRender>().SetName(Init.userInfo.name);
        Init.me.GetComponent<RoleRender>().SetLevel(Init.userInfo.level);

        Dictionary<string, object> dic = NetWork.getSendStart();
        float [] cp_data = {UserSpwanPos.transform.localRotation.x,UserSpwanPos.transform.localRotation.y,0,UserSpwanPos.transform.localRotation.x,UserSpwanPos.transform.localRotation.y};
		dic.Add("cp_data",cp_data);
		dic.Add("name", "cp");
		NetWork.Push(dic);

    }
    void NewOhter(User u){
        UnityEngine.GameObject user = (UnityEngine.GameObject) Instantiate(prefab, new Vector3(u.cur_x, u.cur_y, 0), Quaternion.identity,this.transform);
        user.transform.localScale = new Vector3(1,1,1);
        user.GetComponent<RoleData>().data = u;
        user.GetComponent<RoleRender>().SetName(u.name);
        user.GetComponent<RoleRender>().SetLevel(u.level);
        user.GetComponent<UserInput>().WorkTo(u.cur_x,u.cur_y,u.direction,u.target_x,u.target_y);
        Init.PutRoleObjectWithId(u.id,user);
    }
    void FlushOther(){
        NetEventDispatch.RegisterEvent("flush_room",data =>{
            NetEventDispatch.UnRegisterEvent("flush_room");
			MsgPack.MessagePackObject tmp;
            data.TryGetValue("status", out tmp);
            int status = tmp.AsInt32();
            if(status == 0){
                Init.otherUsersInCurMap.Clear();
                Init.otherIdsInCurMap.Clear();
                data.TryGetValue("other_user", out tmp);
                foreach (var item in tmp.AsList())
                {
                    User other_user = (new User()).UnPack(item);
                    if(Init.GetRoleObjecWithId(other_user.id)==null)
                    {
                        NewOhter(other_user);
                    }
                    Init.otherUsersInCurMap.Add(other_user);
                    Init.otherIdsInCurMap.Add(other_user.id);
                }
                ArrayList del_ids = new ArrayList();
                foreach (int id in Init.other.Keys)
                {
                    if(Init.otherIdsInCurMap.IndexOf(id)==-1)
                    {
                        del_ids.Add(id);
                    }
                }
                foreach (int id in del_ids)
                {
                    Init.RemoveRoleObjectWithId(id);
                }
            }else{
                data.TryGetValue("error", out tmp);
                string error = tmp.AsStringUtf8();
                ErrorInfo.CreateUI("刷新房间信息失败:"+error);
            }
		});
		Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("name", "flush_room");
		NetWork.Push(dic);
    }
    void Awake() {
        InitRoles();
    }    
    void Start()
    {
        
    }
    void Update()
    {
        timer += Time.deltaTime;
		if(timer>=20){
			timer = 0;
			FlushOther();
		}
    }
}
