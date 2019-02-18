using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProcess : MonoBehaviour
{
    public UnityEngine.UI.Button button ;
    private int pos;
    private int type;
    private bool is_show = false;
    public bool delete = false;
    void Awake() {
        button.onClick.AddListener(()=>{
            SendPick();
        });
        button.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }
    public void Delete(){
        Destroy(this);
    }
    void SendPick(){
        string name = "pick";
        if(type == 1)name = "eat";
        Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("pos",pos);
		dic.Add("name", name);
		NetWork.Push(dic);
    }
    public void Set(int _pos,int _type){
        this.pos = _pos;
        this.type = _type;
    }
    void Update()
    {
        if(delete)Delete();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!is_show&&col.gameObject.name == "RoleShow"){
            RoleData role = col.gameObject.GetComponent<RoleData>();
            if(role.isMe())
            {
                RolePos pos = col.gameObject.GetComponent<RolePos>();
                if(pos.FrontMyFace(gameObject.transform.localPosition))
                {
                    is_show = true;
                    button.gameObject.SetActive(true);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(is_show&&col.gameObject.name == "RoleShow"){
            RoleData role = col.gameObject.GetComponent<RoleData>();
            if(role.isMe())
            {
                is_show = false;
                button.gameObject.SetActive(false);
            }
        }
    }
     
}
