using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;
public class CreateRoomUI : MonoBehaviour
{
    public UnityEngine.UI.Button _createRoom;
    public UnityEngine.UI.Button _exit;
    public UnityEngine.UI.Button [] select_list;
    public UnityEngine.UI.InputField room_name;
    public UnityEngine.UI.InputField password;
    private int sellect_index = -1;
    // Start is called before the first frame update
    void Start()
    {
        _createRoom.onClick.AddListener(()=>{
            string room_name_str = room_name.text;
            if(room_name_str == ""){
                ErrorInfo.CreateUI("请输入房间名字");
                return;
            }
            string passrod_str = password.text;
            string map_name = Map.MapName[sellect_index];
            NetEventDispatch.RegisterEvent("create_room",data =>{
                NetEventDispatch.UnRegisterEvent("create_room");
                MsgPack.MessagePackObject tmp;
                data.TryGetValue("status", out tmp);
                int status = tmp.AsInt32();
                if(status == 0){
                    //TODO 进入房间
                    ErrorInfo.CreateUI("创建房间成功");
                }else{
                    data.TryGetValue("error", out tmp);
                    string error = tmp.AsString();
                    ErrorInfo.CreateUI("房间创建失败:"+error);
                }
            });
            Dictionary<string, object> dic = NetWork.getSendStart();
            dic.Add("map_name",map_name);
            dic.Add("room_name",room_name_str);
            if(passrod_str=="")
                dic.Add("password",passrod_str);
            dic.Add("name", "create_room");
            NetWork.Push(dic);
        });
        _exit.onClick.AddListener(Cancel);
        for(int i=0;i<19;i++){
            int a = i;
            select_list[a].onClick.AddListener(()=>{
                select(a);
            });
        }
    }
    void select(int i ){
        if(sellect_index!=-1)
        {
            UnityEngine.Sprite _sprite  = UnityEngine.Resources.Load("GUI/Lobby/CRoomBack_4", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
            select_list[sellect_index].GetComponent<UnityEngine.UI.Image>().sprite = _sprite;
        }
        UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("GUI/Lobby/CRoomBack_2", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
        select_list[i].GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        sellect_index = i;
    }
    void Cancel()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
