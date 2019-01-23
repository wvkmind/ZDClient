using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;
public class ChatHall : MonoBehaviour
{
    public UnityEngine.UI.Button _createRoom;
    public UnityEngine.UI.Button _exit;
    public UnityEngine.UI.Button next;
    public UnityEngine.UI.Button before;
    public UnityEngine.UI.Image [] roomMapList;
    public UnityEngine.UI.Text [] roomNameList;
    public UnityEngine.UI.Text sum;
    private string sum_str = "1/1";
    private int [] maps_ids = {-1,-1,-1,-1,-1,-1};
    private string [] rooms_title = {"空","空","空","空","空","空"};
    // Start is called before the first frame update
    void Awake() {
       FlushHall();
    }
    void FlushHall()
    {
        sum.text = sum_str;
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
