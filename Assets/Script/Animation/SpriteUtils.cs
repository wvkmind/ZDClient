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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
