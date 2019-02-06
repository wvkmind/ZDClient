using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAni : MonoBehaviour
{
    Animator _ani;
    public float m_speed = 0.5f;
    void Awake() {
        _ani = transform.GetComponent<Animator>();
        _ani.speed = m_speed;
        // * this is defaults
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
