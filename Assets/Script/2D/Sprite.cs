using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils2D;
public class Sprite : MonoBehaviour {

	[RangeAttribute(0,1)]
    public float anchorX;
    [RangeAttribute(0,1)]
    public float anchorY;
    public void InitPosition(){
		transform.position= new Vector3(transform.position.x+(2*(0.5f-anchorX)*View.wdiff),transform.position.y,transform.position.z);
    }
	void Start(){
		InitPosition();
	}
	void Update() {

	}
}
