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
    public void SetIdle(){
        SetAction(0);
    }
    public void SetWalk(){
        SetAction(1);
    }

    // Use this for initialization
    void Awake() {
        _ani = transform.GetComponent<Animator>();
        _ani.speed = m_speed;
        SetRoleType("Doobu");//this is defaults
        SetAction(0);
    }
	// Update is called once per frame
	void Update () {
        Touch[] touches = Input.touches;
        if(Input.GetKeyUp(KeyCode.D)||(touches.Length==1&&touches[0].phase==TouchPhase.Ended)){
            if(cur_role=="BanGye")cur_role="Doobu";
            else cur_role="BanGye";
            SetRoleType(cur_role);
            SetAction(action_id);
        }
        if(Input.GetKeyUp(KeyCode.F)||(touches.Length==2&&touches[0].phase==TouchPhase.Ended&&touches[1].phase==TouchPhase.Ended)){
            if(action_id==0)SetAction(1);
            else SetAction(0);
        }
        if(Input.GetKeyUp(KeyCode.S)){
            direction = direction + 1;
            if(direction==4)direction = 3;
        }
	}
}
