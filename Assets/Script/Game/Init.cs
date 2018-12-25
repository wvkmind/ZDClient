using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Init : MonoBehaviour {
	public UnityEngine.Object game_data;
	public UnityEngine.Object switch_scene;
	void Start () {
		GameObject.DontDestroyOnLoad(game_data);
		NetWork.ConnectGate();
	}
	void Update () {
		NetWork.Update();
		NetEventDispatch.Update();
	}
	void OnDestory() {
		NetWork.OnDestory();
	}
}