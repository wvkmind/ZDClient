using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePos : MonoBehaviour
{
    float UpdateZ(float y)
    {
        return -1-(y+3)/20.0f;
    }
    public void SetPosition(float x,float y)
    {
        this.transform.position = new Vector3(x,y,UpdateZ(y));
    }
    void Awake() {
        SetPosition(this.transform.position.x,this.transform.position.y);
    }
    void Update()
    {
        
    }
}
