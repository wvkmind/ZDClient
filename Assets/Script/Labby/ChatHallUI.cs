using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;
using MsgPack.Serialization;
public class ChatHallUI : MonoBehaviour
{
    public UnityEngine.UI.Button _createRoom;
    public UnityEngine.UI.Button _exit;
    public UnityEngine.UI.Button next;
    public UnityEngine.UI.Button before;
    public UnityEngine.UI.Image [] roomMapList;
    public UnityEngine.UI.Text [] roomNameList;
    public UnityEngine.UI.Text sum;
    private int [] maps_ids = {-1,-1,-1,-1,-1,-1};
    private string [] rooms_title = {"空","空","空","空","空","空"};
    private int cur_page = 1;
    private int sum_page = 1;
    private IList<MsgPack.MessagePackObject> list;
    // Start is called before the first frame update
    void Awake() {
       GetFromServer();
    }
    void GetFromServer()
    {
        NetEventDispatch.RegisterEvent("hall_list",data =>{
			NetEventDispatch.UnRegisterEvent("hall_list");
            MsgPack.MessagePackObject tmp;
            data.TryGetValue("status", out tmp);
            int status = tmp.AsInt32();
            if(status == 0){
                data.TryGetValue("page_sum", out tmp);
                sum_page = tmp.AsInt32();
                data.TryGetValue("cur_page", out tmp);
                cur_page = tmp.AsInt32();
                data.TryGetValue("list", out tmp);
                list = tmp.AsList();
                int cur_i =0;
                foreach (MsgPack.MessagePackObject item in list)
                {
                    if(cur_i>5)break;
                    var serializer = MessagePackSerializer.Get<Dictionary<string, MsgPack.MessagePackObject>>();
			        MsgPack.MessagePackObjectDictionary room_info_dic= item.AsDictionary();
                    MsgPack.MessagePackObject room_info;
                    room_info_dic.TryGetValue("title",out room_info);
                    rooms_title[cur_i]=room_info.AsString();
                    room_info_dic.TryGetValue("map_name",out room_info);
                    maps_ids[cur_i]=Map.GetMapIndex(room_info.AsString());
                    cur_i = cur_i + 1;
                }
                FlushHall();
                Debug.Log("大厅刷新成功");
            }else{
                data.TryGetValue("error", out tmp);
                string error = tmp.AsString();
                ErrorInfo.CreateUI("大厅刷新失败:"+error);
            }
		});
		Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("page",cur_page);
		dic.Add("name", "hall_list");
		NetWork.Push(dic);
    }
    void FlushHall()
    {
        sum.text = cur_page+"/"+sum_page;
        for(int i = 0;i<6;i++)
        {
            int id = maps_ids[i];
            string name = rooms_title[i];
            if(id==-1)
            {
                roomMapList[i].gameObject.SetActive(false);
                roomNameList[i].text = "空";
            }
            else
            {
                Debug.Log(Map.MapSmallPath(id));
                UnityEngine.Sprite sprite  = UnityEngine.Resources.Load(Map.MapSmallPath(id), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
			    roomMapList[i].sprite = sprite;
                roomMapList[i].gameObject.SetActive(true);
                roomNameList[i].text = name;
            }
        }
    }
    void Start()
    {
        _exit.onClick.AddListener(ExitToBigMap);
        next.onClick.AddListener(NextPage);
        before.onClick.AddListener(Before);
    }
    void NextPage()
    {
        if(cur_page!=sum_page) cur_page = cur_page +1;
        GetFromServer();
    }
    void Before()
    {
        if(cur_page!=1) cur_page = cur_page -1;
        GetFromServer();
    }
    void ExitToBigMap()
    {
        SwitchScene.NextScene("BigMap");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
