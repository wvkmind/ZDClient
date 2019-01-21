using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Init : MonoBehaviour {
	public static Init instance = null;
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