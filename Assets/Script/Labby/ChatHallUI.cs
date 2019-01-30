﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;
using MsgPack.Serialization;
using DataModel;
public class ChatHallUI : MonoBehaviour
{
    public UnityEngine.UI.Button _createRoom;
    public UnityEngine.UI.Button _exit;
    public UnityEngine.UI.Button next;
    public UnityEngine.UI.Button before;
    public UnityEngine.UI.Image [] roomMapList;
    public UnityEngine.UI.Text [] roomNameList;
    public UnityEngine.UI.Text sum;
    public UnityEngine.GameObject createRoomUI;
    private static float timer = 0;
    private int cur_page = 1;
    private int sum_page = 1;
    private IList<MsgPack.MessagePackObject> list;
    private ArrayList rooms_info = new ArrayList();
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
                    rooms_info.Add((new Room()).UnPack(item));
                    cur_i = cur_i + 1;
                }
                FlushHall();
            }else{
                data.TryGetValue("error", out tmp);
                string error = tmp.AsStringUtf8();
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
        for(int i = 0;i<rooms_info.Count-1;i++)
        {
            Room cur = rooms_info[i] as Room;
            int id = Map.GetMapIndex(cur.map_name);
            string name = cur.title;
            UnityEngine.Sprite sprite  = UnityEngine.Resources.Load(Map.MapSmallPath(id), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
			roomMapList[i].sprite = sprite;
            roomMapList[i].gameObject.SetActive(true);
            roomNameList[i].text = name;
        }
        for(int i = rooms_info.Count;i<6;i++){
            roomMapList[i].gameObject.SetActive(false);
            roomNameList[i].text = "空";
        }
    }
    void Start()
    {
        _createRoom.onClick.AddListener(OpenCreateRoomUI);
        _exit.onClick.AddListener(ExitToBigMap);
        next.onClick.AddListener(NextPage);
        before.onClick.AddListener(Before);
    }
    void OpenCreateRoomUI()
    {
        createRoomUI.gameObject.SetActive(true);
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
        timer += Time.deltaTime;
		if(timer>=10){
			timer = 0;
             GetFromServer();
		}
    }
}
