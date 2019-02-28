using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;
public class ItemProcess : MonoBehaviour
{
    public UnityEngine.UI.Button button ;
    private int pos;
    private int type;
    private bool is_show = false;
    public bool delete = false;
    private RolePos mePos ;
    bool _in = false;
    public int owner = -1;
    private RoleRender user_r = null;
    private float timer =0.0f;
    void Awake() {
        button.onClick.AddListener(()=>{
            SendPick(type);
        });
        button.gameObject.SetActive(false);
    }
    void Start()
    {
        mePos = Init.me.GetComponent<RolePos>();
    }
    public void Delete(){
        if(user_r!=null)user_r.CancelEat();
        Destroy(this.gameObject);
    }
    void SendPick(int _type){
        string name = "pick";
        if(_type == 1)name = "eat";
        Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("pos",pos);
		dic.Add("name", name);
		NetWork.Push(dic);
    }
    public void Set(int _pos,int _type,int _owner){
        this.pos = _pos;
        this.type = _type;
        this.owner = _owner;
        if(_owner!=-1)
        {
            UnityEngine.GameObject user =  User.GetUser(_owner);
            user_r = user.GetComponent<RoleRender>();
        }
    }
    void Update()
    {
        if(delete){Delete();return;}
        if(!is_show&&_in&&Init.me.GetComponent<RolePos>().IsStop()&&owner==-1)
        {
            is_show = true;
            button.gameObject.SetActive(true);
        }else if(owner!=-1&&user_r!=null)
        {
            
            float d_x = 0.0f;
            float d_y = 0.0f;
            float d_z = 0.0f;
            Debug.Log("user_r.IsEatting()"+user_r.real_action_id);
            if(User.GetUser(owner).GetComponent<RoleRender>().IsEatting())
            {
                d_y = -0.6f;
                if(this.owner==Init.userInfo.id)
                {
                    timer += Time.deltaTime;
                    if(timer>=1){
                        timer = 0;
                        Dictionary<string, object> dic = NetWork.getSendStart();
                        dic.Add("pos",pos);
                        dic.Add("name", "eat");
                        NetWork.Push(dic);
                    }
                }
                if(user_r.GetDirection()==0)
                {
                    d_x = 0.0f;
                    d_z = -0.1f;
                }else if(user_r.GetDirection()==1)
                {
                    d_x = 0.0f;
                    d_z =  0.1f;
                }else if(user_r.GetDirection()==2)
                {
                    d_x = -0.3f;
                }else if(user_r.GetDirection()==3)
                {
                    d_x = 0.3f;
                }
            }
            else
            {
                d_y = 0.3f;
            }
            transform.localPosition = new Vector3(user_r.transform.localPosition.x+d_x,user_r.transform.localPosition.y+d_y,user_r.transform.localPosition.z+d_z);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!is_show&&col.gameObject.tag == "Role"){
            RoleData role = col.gameObject.GetComponent<RoleData>();
            if(role.isMe())
            {
                RolePos pos = col.gameObject.GetComponent<RolePos>();
                if(pos.FrontMyFace(gameObject.transform.localPosition))
                {
                    _in= true;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(is_show&&col.gameObject.tag == "Role"){
            RoleData role = col.gameObject.GetComponent<RoleData>();
            if(role.isMe())
            {
                _in= false;
                is_show = false;
                button.gameObject.SetActive(false);
            }
        }
    }
     
}
