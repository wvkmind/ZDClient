using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Utils2D;
using UnityEngine.EventSystems;
public class MapProcess : MonoBehaviour
{
    private bool touchEnd=true;
    private float sc ;
    private float limit;
    private float unit; 
    private float proportion;
    void Awake() {
        
    }
    void Start()
    {
        proportion = Screen.height/600.0f;
        unit = Screen.height/2.0f/Camera.main.orthographicSize;
        sc = unit;
        limit = (400.0f-Screen.width/(Screen.height/600.0f)/2.0f)*3.2f/100.0f;
        NetEventDispatch.RegisterEvent("cp",data =>{
			UpdatePos(data);
		});
        NetEventDispatch.RegisterEvent("exp",data =>{
			UpdateExp(data);
		});
    }
    private static void UpdateExp(Dictionary<string, MsgPack.MessagePackObject> dic){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("id",out tmp);
        int id = tmp.AsInt32();
        dic.TryGetValue("ac_data",out tmp);
        int action = tmp.AsInt32();
        if(id==Init.userInfo.id)
            Init.me.GetComponent<UserInput>().Exp(action);
        else
        {
            UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
            if(other!=null)
                other.GetComponent<UserInput>().Exp(action);
        }
	}
    private static void UpdatePos(Dictionary<string, MsgPack.MessagePackObject> dic){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("id",out tmp);
        int id = tmp.AsInt32();
        dic.TryGetValue("cp_data",out tmp);
        float cur_x = (float)tmp.AsList()[0].AsDouble();
        float cur_y = (float)tmp.AsList()[1].AsDouble();
        int direction = (int)tmp.AsList()[2].AsDouble();
        float target_x = (float)tmp.AsList()[3].AsDouble();
        float target_y = (float)tmp.AsList()[4].AsDouble();
        if(id==Init.userInfo.id)
            Init.me.GetComponent<UserInput>().WorkTo(target_x,target_y);
        else
        {
            UnityEngine.GameObject other = Init.GetRoleObjecWithId(id);
            if(other!=null)
                other.GetComponent<UserInput>().WorkTo(cur_x,cur_y,direction,target_x,target_y);
        }
	}
    void Update()
    {
        //for ios and android
        if(Input.touchCount == 1 )
        {
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
            }else{
                if(Input.touches[0].phase == TouchPhase.Began)
                {
                    if(touchEnd&&Input.touches[0].tapCount == 2)//双击走路
                    {
                        touchEnd = false;
                        float real_screen_x = Input.touches[0].position.x;
                        float real_screen_y = Input.touches[0].position.y;
                        float design_x = (real_screen_x-Screen.width/2.0f)/proportion/100.0f;
                        float design_y = (real_screen_y-Screen.height/2.0f)/proportion/100.0f;
                        Vector3 pos = new Vector3(design_x,design_y,1.0f) - gameObject.transform.localPosition/3.2f;
                        SendMyTouch(Init.me.transform.localPosition.x,Init.me.transform.localPosition.y,Init.me.GetComponent<RoleRender>().GetDirection(),pos.x,pos.y);
                    }
                }else if(touchEnd && Input.touches[0].phase == TouchPhase.Moved){
                    Vector3 delta_pos =  Input.touches[0].deltaPosition;
                    if(Mathf.Abs(transform.position.x+delta_pos.x/sc)<limit)
                        transform.position = new Vector3(transform.position.x+delta_pos.x/sc,transform.position.y,transform.position.z);
                }
                else if(Input.touches[0].phase == TouchPhase.Ended||Input.touches[0].phase == TouchPhase.Canceled){
                    touchEnd = true;
                }
            }
        }
        //for pc test
        // if(Input.GetMouseButtonDown(0)){
        //     float real_screen_x = Input.mousePosition.x;
        //     float real_screen_y = Input.mousePosition.y;
        //     float design_x = (real_screen_x-Screen.width/2.0f)/proportion/100.0f;
        //     float design_y = (real_screen_y-Screen.height/2.0f)/proportion/100.0f;
        //     Vector3 pos = new Vector3(design_x,design_y,1.0f) - gameObject.transform.localPosition/3.2f;
        //     SendMyTouch(Init.me.transform.localPosition.x,Init.me.transform.localPosition.y,Init.me.GetComponent<RoleRender>().GetDirection(),pos.x,pos.y);
        // }
    }
    public static void SendMyTouch(float cur_x,float cur_y,int direction,float target_x,float tartge_y){
        Dictionary<string, object> dic = NetWork.getSendStart();
        float [] cp_data = {cur_x,cur_y,direction,target_x,tartge_y};
		dic.Add("cp_data",cp_data);
		dic.Add("name", "cp");
		NetWork.Push(dic);
    }
    private void OnDestroy() {
        NetEventDispatch.UnRegisterEvent("cp");
        NetEventDispatch.UnRegisterEvent("exp");
    }
}
