using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;
/**
 * *  这个是只负责角色的动画部分
 */
public class RoleRender : MonoBehaviour {

	Animator _ani;
    public float m_speed = 0.5f;
    private string cur_role = "BanGye";
    private int _ani_layer_index = 0;
    private int action_id = 0;
    public int real_action_id = 0;
    private int direction = 0;// * 面向前后左右
    public void SetFront(){direction=0;_ani.SetInteger("direction",direction);SetAction(real_action_id);}
    public void SetBack(){direction=1;_ani.SetInteger("direction",direction);SetAction(real_action_id);}
    public void SetLeft(){direction=2;_ani.SetInteger("direction",direction);SetAction(real_action_id);}
    public void SetRight(){direction=3;_ani.SetInteger("direction",direction);SetAction(real_action_id);}
    public void SetDirection(int d){direction = d;}
    public int GetDirection(){return direction;}
    public TextMesh user_name;
    public SpriteRenderer user_level;
    private RoleData role_data;
    
    public void SetAction(int i){
        action_id = direction*Role.RHAL+i;
        real_action_id = i;
        _ani.Play(Role.Actions[action_id],_ani_layer_index);
    }
    public void SetRoleType(string type){
        cur_role = type;
        _ani_layer_index = _ani.GetLayerIndex(cur_role);
        _ani.SetLayerWeight(_ani_layer_index,1);
        for(int i = 0;i<_ani.layerCount;i++){
            if(_ani_layer_index!=i){
                _ani.SetLayerWeight(i,0);
            }
        }
    }  
    //!next_action == 0的时候是idle状态，不过累的的时候idle是Snoring
    private void SetIdleAction(){
        _ani.SetInteger("next_action",0);
        _ani.SetBool("sleep",false);
        SetAction(0);
    }
    private void SetSnoring(){
        _ani.SetInteger("next_action",0);
        _ani.SetBool("sleep",false);
        SetAction(7);
    }
    void Start(){
        role_data = gameObject.GetComponent<RoleData>();
    }
    public void SetGo(){
        if(_ani.GetBool("have_food"))
        {
            SetRaiseHands();
        }
        else
        {
            if(gameObject.GetComponent<RoleData>().data.tilizhi <= 10)
            {
                SetWalk();
            }
            else
            {
                SetRun();
            }
        }
    }
    public void SetWalk(){
        _ani.SetInteger("next_action",1);
        _ani.SetBool("sleep",false);
        SetAction(1);
    }
    public void SetTalk(){
        _ani.SetInteger("next_action",0);
        _ani.SetBool("sleep",false);
        SetAction(2);
    }
    public void SetRun(){
        _ani.SetInteger("next_action",3);
        _ani.SetBool("sleep",false);
        SetAction(3);
    }
    public void SetEat(){
        _ani.SetBool("have_food",true);
        SetEatAction();
    }
    public void SetEatAction(){
        _ani.SetInteger("next_action",4);
        _ani.SetBool("sleep",false);
        SetAction(4);
    }
    public void SetRaiseHands(){
        _ani.SetInteger("next_action",5);
        _ani.SetBool("sleep",false);
        SetAction(5);
    }
    public void SetPick(){
        _ani.SetInteger("next_action",0);
        _ani.SetBool("sleep",false);
        SetAction(6);
    }
    public void SetCheer(){
        _ani.SetInteger("next_action",0);
        _ani.SetBool("sleep",false);
        SetAction(8);
    }
    public void SetExp(int i){
        _ani.SetInteger("next_action",0);
        if(i==19)_ani.SetBool("sleep",true);
        else _ani.SetBool("sleep",false);
        SetAction(8+i);
    }
    // * Use this for initialization
    void Awake() {
        _ani = transform.GetComponent<Animator>();
        _ani.speed = m_speed;
        // * this is defaults
        //SetRoleType("BanGye");
        //SetAction(0);
    }
    public void SetName(string name)
    {
       user_name.text = name;
    }
    public void SetLevel(int i)
    {
        Object [] t =  UnityEngine.Resources.LoadAll("Image/Level/Level_0");
        user_level.sprite = t[i]  as  UnityEngine.Sprite;
    }
    public void SetIdle(){
        if(_ani.GetBool("have_food"))
        {
            SetEatAction();
        }
        else
        {
            if(gameObject.GetComponent<RoleData>().data.tilizhi > 10)
                this.SetIdleAction();
            else 
                this.SetSnoring();
            _ani.SetBool("is_tired",gameObject.GetComponent<RoleData>().data.tilizhi < 10);
        }
    }
	void Update () {

	}
    public bool IsSetGoing()
    {
        return action_id == direction*Role.RHAL+1 || action_id == direction*Role.RHAL+3;
    }
    public bool IsEatting()
    {
        return _ani.GetBool("have_food")&&!_ani.GetBool("move");
    }

    public void SetMove(bool f)
    {
        _ani.SetBool("move",f);
    }
    public void CancelEat(){
        _ani.SetBool("have_food",false);
        SetIdle();
    }
}
