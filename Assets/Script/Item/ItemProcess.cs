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
    private RolePos mePos ;
    bool _in = false;
    public int owner = -1;
    private RoleRender user_r = null;
    private RoleData user_d = null;
    private float timer =0.0f;
    public bool is_clear = false;
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
    void SendPick(int _type){
        string name = "pick";
        if(_type == 1)name = "eat";
        Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("pos",pos);
		dic.Add("name", name);
		NetWork.Push(dic);
    }
    public void SetNull(){
        this.pos = -1;
        this.type = -1;
        this.owner = -1;
        if(user_r!=null)
            user_r.CancelEat();
            user_r = null;
            user_d =null;
    }
    public void Set(int _pos,int _type,int _owner){
        if(_pos==this.pos&&_type==this.type&&_owner==this.owner)
        {
            return;
        }
        this.pos = _pos;
        this.type = _type;
        this.owner = _owner;
        is_clear = false;
        if(_owner!=-1)
        {
            UnityEngine.GameObject user =  User.GetUser(_owner);
            user_r = user.GetComponent<RoleRender>();
            user_d = user.GetComponent<RoleData>();
        }
    }
    void Update()
    {
        if(is_clear){
            gameObject.GetComponent<ItemRender>().SetNull();
        }
        if(!is_show&&_in&&Init.me.GetComponent<RolePos>().IsStop()&&owner==-1)
        {
            is_show = true;
            button.gameObject.SetActive(true);
        }else if(owner!=-1&&user_r!=null)
        {
            
            float d_x = 0.0f;
            float d_y = 0.0f;
            float d_z = 0.0f;
            if(user_r.IsEatting())
            {
                d_y = 0.0f;
                
                    timer += Time.deltaTime;
                    if(timer>=1){
                        timer = 0;
                        if(this.owner==Init.userInfo.id)
                        {
                            Dictionary<string, object> dic = NetWork.getSendStart();
                            dic.Add("pos",pos);
                            dic.Add("name", "eat");
                            NetWork.Push(dic);
                        }
                        int step = (int)((float)user_d.data.phy_str_rate/1.0f+1.0f)*10*gameObject.GetComponent<ItemRender>().id;
                        if(step<1)
		                    step = 1;
                        user_d.AddTL(step);
                    }
                if(user_r.GetDirection()==0)
                {
                    d_x = 0.0f;
                    d_z = -0.2f;
                }else if(user_r.GetDirection()==1)
                {
                    d_x = 0.0f;
                    d_z =  0.2f;
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
