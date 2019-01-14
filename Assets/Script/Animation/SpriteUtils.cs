using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;

public class SpriteUtils : MonoBehaviour {

	Animator _ani;
    public float m_speed = 0.5f;
    private string cur_role = "BanGye";
    private int _ani_layer_index = 0;
    private int action_id = 0;
    private int direction = 0;//面向前后左右
    public void SetAction(int i){
        action_id = direction*Role.RHAL+i;
        _ani.Play(Role.Actions[i],_ani_layer_index);
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
    public void SetIdle(){
        _ani.SetInteger("next_action",0);
        SetAction(0);
    }
    public void SetSnoring(){
        _ani.SetInteger("next_action",0);
        SetAction(7);
    }
    public void SetWalk(){
        _ani.SetInteger("next_action",1);
        SetAction(1);
    }
    public void SetExp(int i){
        SetAction(8+i);
         _ani.SetInteger("next_action",0);
    }
    // Use this for initialization
    void Awake() {
        _ani = transform.GetComponent<Animator>();
        _ani.speed = m_speed;
        //this is defaults
        SetRoleType("BanGye");
        SetAction(0);
    }
	// Update is called once per frame
	void Update () {
        
        Touch[] touches = Input.touches;
        // if(Input.GetKeyUp(KeyCode.D)||(touches.Length==1&&touches[0].phase==TouchPhase.Ended)){
        //     if(cur_role=="BanGye")cur_role="Doobu";
        //     else cur_role="BanGye";
        //     SetRoleType(cur_role);
        //     SetAction(action_id);
        // }
        if(Input.GetKeyUp(KeyCode.F)||(touches.Length==2&&touches[0].phase==TouchPhase.Ended&&touches[1].phase==TouchPhase.Ended)){
            SetWalk();
        }
        if(Input.GetKeyUp(KeyCode.S)){
            SetExp(1);
        }
	}
}
