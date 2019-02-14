using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;
using Utils;
public class MapThings : MonoBehaviour
{
    public UnityEngine.GameObject UserSpwanPos;
    public UnityEngine.GameObject prefab; //role
    public UnityEngine.GameObject item_prefab;
    public UnityEngine.GameObject [] ItemPos;
    private UnityEngine.GameObject [] item_entities = new UnityEngine.GameObject[10];
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
        NewMe();
    }
    void InitItems(){
        for(var i = 0;i<10;i++){
            item_entities[i] = (UnityEngine.GameObject)Instantiate(item_prefab, new Vector3(
                    ItemPos[i].gameObject.transform.position.x,
                    ItemPos[i].gameObject.transform.position.y,
                    PositionTransform.UpdateZ(ItemPos[i].gameObject.transform.position.y)
                ), 
                Quaternion.identity,
                this.transform
            );
        }
        FlushItem(Init.GetData("item_list"));
    }
    void NewMe(){
        Init.me = (UnityEngine.GameObject)Instantiate(prefab, new Vector3(UserSpwanPos.transform.localRotation.x,UserSpwanPos.transform.localRotation.y , 0), Quaternion.identity,this.transform);
        Init.me.transform.localScale = new Vector3(1,1,1);
        Init.me.GetComponent<RoleData>().data = Init.userInfo;
        Init.me.GetComponent<RoleRender>().SetName(Init.userInfo.name);
        Init.me.GetComponent<RoleRender>().SetLevel(Init.userInfo.level);
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
    void FlushOther(MsgPack.MessagePackObject tmp){
        ArrayList have_ids = new ArrayList();
        foreach (var item in tmp.AsList())
        {
            User other_user = (new User()).UnPack(item);
            if(Init.GetRoleObjecWithId(other_user.id)==null)
            {
                NewOhter(other_user);
            }
            have_ids.Add(other_user.id);
        }
        ArrayList del_ids = new ArrayList();
        foreach (int id in Init.other.Keys)
        {
            if(have_ids.IndexOf(id)==-1)
            {
                del_ids.Add(id);
            }
        }
        foreach (int id in del_ids)
        {
            Init.RemoveRoleObjectWithId(id);
        }
    }
    void FlushItem(MsgPack.MessagePackObject tmp){
        for(var i = 0;i<10;i++){
            item_entities[i].GetComponent<ItemRender>().SetNull();
        }
        if(!tmp.IsNil)
        foreach (var item in tmp.AsList())
        {
            if(!item.IsNil)
            {
                Item cur_item = (new Item()).UnPack(item);
                ItemRender item_entity = item_entities[cur_item.pos].GetComponent<ItemRender>();
                item_entity.Set(cur_item.id,cur_item.type);
            }
        }
    }
    void FlushRoom(){
        NetEventDispatch.RegisterEvent("flush_room",data =>{
            NetEventDispatch.UnRegisterEvent("flush_room");
			MsgPack.MessagePackObject tmp;
            data.TryGetValue("status", out tmp);
            int status = tmp.AsInt32();
            if(status == 0){
                data.TryGetValue("item_list", out tmp);
                FlushItem(tmp);
                data.TryGetValue("other_user", out tmp);
                FlushOther(tmp);
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
        InitItems();//初始化物品
        InitRoles();//初始化玩家
    }    
    
    void Start()
    {
        NetEventDispatch.RegisterEvent("new_one",data =>{
			NewOne(data);
		});
        NetEventDispatch.RegisterEvent("out_one",data =>{
			OutOne(data);
		});
        NetEventDispatch.RegisterEvent("change_item",data =>{
            MsgPack.MessagePackObject tmp;
            data.TryGetValue("item_list", out tmp);
			FlushItem(tmp);
		});
        
    }
    void NewOne(Dictionary<string, MsgPack.MessagePackObject> dic){
        MsgPack.MessagePackObject tmp;
        dic.TryGetValue("new_one", out tmp);
        User newone = (new User()).UnPack(tmp);
        if(Init.GetRoleObjecWithId(newone.id)==null)
        {
            NewOhter(newone);
        }
    }
    void OutOne(Dictionary<string, MsgPack.MessagePackObject> dic){
        MsgPack.MessagePackObject tmp;
        dic.TryGetValue("out_one", out tmp);
        User newone = (new User()).UnPack(tmp);
        UnityEngine.GameObject del_obj = Init.GetRoleObjecWithId(newone.id);
        if(del_obj!=null)
        {
            Destroy(del_obj);
        }
        Init.RemoveRoleObjectWithId(newone.id);
    }
    void Update()
    {
        timer += Time.deltaTime;
		if(timer>=20){
			timer = 0;
			FlushRoom();
		}
    }

    private void OnDestroy() {
        NetEventDispatch.UnRegisterEvent("new_one");
        NetEventDispatch.UnRegisterEvent("out_one");
        NetEventDispatch.UnRegisterEvent("change_item");
    }
}
