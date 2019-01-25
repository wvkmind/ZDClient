using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataModel;
public class Init : MonoBehaviour {
	public static Init instance = null;
	//User信息
	public static User userInfo = null;
	public static User [] otherUsersInCurMap = new User[9]; 
	void Start () {
		instance = this;
		NetWork.ConnectGate();
		DontDestroyOnLoad(this);
	}
	void Update () {
		NetWork.Update();
		NetEventDispatch.Update();
	}
	void OnDestory() {
		NetWork.OnDestory();
	}
}