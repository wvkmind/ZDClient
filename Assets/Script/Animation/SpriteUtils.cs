using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUtils : MonoBehaviour {

	Animator _ani;
    public float m_speed = 0.5f;
    // Use this for initialization
    void Start () {
        _ani = transform.GetComponent<Animator>();
        _ani.speed = m_speed;
        _ani.SetInteger("Ani0",1);
    }
	
	// Update is called once per frame
	void Update () {
        Touch[] touches = Input.touches;
        if(touches.Length==1&&touches[0].phase==TouchPhase.Ended){
            int i = _ani.GetInteger("Ani0");
            _ani.SetInteger("Ani0",1-i);
        }
	}
}
