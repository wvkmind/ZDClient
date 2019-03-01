using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils2D;
using UnityEngine.EventSystems;
public class MapProcess : MonoBehaviour
{
    private bool touchEnd=true;
    private bool moved = false;
    private float sc ;
    private float limit;
    private float unit; 
    private float proportion;
    public MapThings mapThings;
    private Vector3 touchPositon;
    public HUD hUD;
    void Awake() {
        
    }
    void Start()
    {
        proportion = Screen.height/600.0f;
        unit = Screen.height/2.0f/Camera.main.orthographicSize;
        sc = unit;
        limit = (400.0f-Screen.width/(Screen.height*Camera.main.rect.height/600.0f)/2.0f)*3.2f/100.0f;
        NetEventDispatch.RegisterEvent("cp",data =>{
			UpdatePos(data);
		});
        NetEventDispatch.RegisterEvent("exp",data =>{
			UpdateExp(data);
		});
        NetEventDispatch.RegisterEvent("pick",data =>{
			UpdatePick(data);
		});
        NetEventDispatch.RegisterEvent("eat",data =>{
			UpdateEat(data);
		});
        NetEventDispatch.RegisterEvent("ptlz",data =>{
			UpdateTilizhi(data);
		});
        NetEventDispatch.RegisterEvent("plu",data =>{
			UpdateLevel(data);
		});
        NetEventDispatch.RegisterEvent("ce",data =>{
			UpdateUexp(data);//经验值
		});
        hUD.FreshHUD();
    }
    
    private void UpdateUexp(Dictionary<string, MsgPack.MessagePackObject> dic){
        MsgPack.MessagePackObject tmp;
		dic.TryGetValue("exp",out tmp);
        int exp = tmp.AsInt32();
        Init.me.GetComponent<RoleData>().data.exp = exp;
    }
    private void UpdateLevel(Dictionary<string, MsgPack.MessagePackObject> dic){
        //TODO 升级特效
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("id",out tmp);
        int id = tmp.AsInt32();
        dic.TryGetValue("level",out tmp);
        int level = tmp.AsInt32();
        if(id==Init.userInfo.id)
        {    
            Init.me.GetComponent<RoleData>().data.level = level;
            Init.me.GetComponent<RoleRender>().SetLevel(level);
        }
        else
        {
            UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
            if(other!=null)
            {    
                other.GetComponent<RoleData>().data.level = level;
                other.GetComponent<RoleRender>().SetLevel(level);
            }
        }
	}
    private void UpdateTilizhi(Dictionary<string, MsgPack.MessagePackObject> dic){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("id",out tmp);
        int id = tmp.AsInt32();
        dic.TryGetValue("tilizhi",out tmp);
        int tilizhi = tmp.AsInt32();
        if(id==Init.userInfo.id)
        {    
            Init.me.GetComponent<RoleData>().data.tilizhi = tilizhi;
            hUD.FreshHUD();
        }
        else
        {
            UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
            if(other!=null)
                other.GetComponent<RoleData>().data.tilizhi = tilizhi;
        }
	}
    private void UpdateExp(Dictionary<string, MsgPack.MessagePackObject> dic){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("id",out tmp);
        int id = tmp.AsInt32();
        dic.TryGetValue("ac_data",out tmp);
        float cur_x = (float)tmp.AsList()[0].AsDouble();
        float cur_y = (float)tmp.AsList()[1].AsDouble();
        int direction = (int)tmp.AsList()[2].AsDouble();
        int action = (int)tmp.AsList()[3].AsDouble();
        if(id==Init.userInfo.id)
            Init.me.GetComponent<UserInput>().Exp(action);
        else
        {
            UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
            if(other!=null)
                other.GetComponent<UserInput>().Exp(cur_x,cur_y,direction,action);
        }
	}
    private void UpdatePos(Dictionary<string, MsgPack.MessagePackObject> dic){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("id",out tmp);
        int id = tmp.AsInt32();
        dic.TryGetValue("tl",out tmp);
        int tilizhi = tmp.AsInt32();
        dic.TryGetValue("cp_data",out tmp);
        float cur_x = (float)tmp.AsList()[0].AsDouble();
        float cur_y = (float)tmp.AsList()[1].AsDouble();
        int direction = (int)tmp.AsList()[2].AsDouble();
        float target_x = (float)tmp.AsList()[3].AsDouble();
        float target_y = (float)tmp.AsList()[4].AsDouble();
        if(id==Init.userInfo.id)
        {
            Init.me.GetComponent<UserInput>().WorkTo(target_x,target_y);
            Init.me.GetComponent<RoleData>().data.tilizhi = tilizhi;
            hUD.FreshHUD();
        }
        else
        {
            UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
            if(other!=null)
            {
                other.GetComponent<UserInput>().WorkTo(cur_x,cur_y,direction,target_x,target_y);
                other.GetComponent<RoleData>().data.tilizhi = tilizhi;
            }
        }
	}
    void Update()
    {
        //for ios and android
        if(Input.touchCount == 1 )
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    
                }
                else{
                    touchPositon = Input.touches[0].position;
                    touchEnd = false;
                }
            }else if(Input.touches[0].phase == TouchPhase.Moved){
                if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    
                }
                else
                {
                    if(Mathf.Abs(touchPositon.x-Input.touches[0].position.x)>15&&Mathf.Abs(touchPositon.y-Input.touches[0].position.y)>15||moved)
                    {
                        moved = true;
                        Vector3 delta_pos =  Input.touches[0].deltaPosition;
                        if(Mathf.Abs(transform.position.x+delta_pos.x/sc)<limit)
                            transform.position = new Vector3(transform.position.x+delta_pos.x/sc,transform.position.y,transform.position.z);
                    }
                }
            }
            else if(Input.touches[0].phase == TouchPhase.Ended||Input.touches[0].phase == TouchPhase.Canceled){
                if(!touchEnd&&!moved)
                {   
                    float real_screen_x = Input.touches[0].position.x;
                    float real_screen_y = Input.touches[0].position.y;
                    float design_x = (real_screen_x-Screen.width/2.0f)/proportion/100.0f;
                    float design_y = (real_screen_y-Screen.height/2.0f)/proportion/100.0f;
                    Vector3 pos = new Vector3(design_x,design_y,1.0f) - gameObject.transform.localPosition/3.2f;
                    SendMyTouch(Init.me.transform.localPosition.x,Init.me.transform.localPosition.y,Init.me.GetComponent<RoleRender>().GetDirection(),pos.x,pos.y);
                }
                touchEnd = true;
                moved = false;
            }
            
        }
        hUD.FreshHUD();
    }
    public static void SendMyTouch(float cur_x,float cur_y,int direction,float target_x,float tartge_y){
        Dictionary<string, object> dic = NetWork.getSendStart();
        float [] cp_data = {cur_x,cur_y,direction,target_x,tartge_y};
		dic.Add("cp_data",cp_data);
        dic.Add("tl",Init.me.GetComponent<RoleData>().data.tilizhi);
		dic.Add("name", "cp");
		NetWork.Push(dic);
    }
    private void UpdatePick(Dictionary<string, MsgPack.MessagePackObject> dic){
        MsgPack.MessagePackObject tmp;
        dic.TryGetValue("status", out tmp);
		int status = tmp.AsInt32();
		if(status == 0){
            dic.TryGetValue("items",out tmp);
            mapThings.FlushItem(tmp);
            dic.TryGetValue("user_id",out tmp);
            int id = tmp.AsInt32();
            if(id==Init.userInfo.id)
                Init.me.GetComponent<RoleRender>().SetPick();
            else
            {
                UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
                if(other!=null)
                    other.GetComponent<RoleRender>().SetPick();
            }
            dic.TryGetValue("pick_pos",out tmp);
            int pos = tmp.AsInt32();
            dic.TryGetValue("id",out tmp);
            int item_id = tmp.AsInt32();
            dic.TryGetValue("type",out tmp);
            int item_type = tmp.AsInt32();
            mapThings.FakeItemUp(item_id,item_type,pos);
        }
    }
    private void UpdateEat(Dictionary<string, MsgPack.MessagePackObject> dic){
        MsgPack.MessagePackObject tmp;
        dic.TryGetValue("status", out tmp);
		int status = tmp.AsInt32();
		if(status == 0||status == 2){
            dic.TryGetValue("items",out tmp);
            mapThings.FlushItem(tmp);
            dic.TryGetValue("user_id",out tmp);
            int id = tmp.AsInt32();
            if(id==Init.userInfo.id)
            {
                if(status == 0)
                    Init.me.GetComponent<RoleRender>().SetEat();
                else
                    Init.me.GetComponent<RoleRender>().CancelEat();
            }   
            else
            {
                UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
                if(other!=null)
                    if(status == 0)
                        other.GetComponent<RoleRender>().SetEat();
                    else
                        other.GetComponent<RoleRender>().CancelEat();
            }
        }
    }
    private void OnDestroy() {
        NetEventDispatch.UnRegisterEvent("cp");
        NetEventDispatch.UnRegisterEvent("exp");
        NetEventDispatch.UnRegisterEvent("pick");
        NetEventDispatch.UnRegisterEvent("eat");
    }
}
