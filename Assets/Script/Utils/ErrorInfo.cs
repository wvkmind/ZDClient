using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ErrorInfo : MonoBehaviour {
	public UnityEngine.UI.Button ok;
	public UnityEngine.UI.Text error_text;
	private System.Action call_back;
	public void SetText(string error_info){
		this.gameObject.SetActive(true);
		error_text.text = error_info;
	}
	public void SetCallBack(System.Action fun){
		call_back = fun;
	}
	// Use this for initialization
	void Start () {
		ok.onClick.AddListener(Exit);
	}
	void Exit(){
		if(call_back!=null)
			call_back.Invoke();
		Destroy(this.gameObject);
	}
	public static void CreateUI(string error_info,System.Action fun=null){
		GameObject obj = Instantiate(UnityEngine.Resources.Load("Prefab/UI/ErrorUI")) as GameObject;
		ErrorInfo ui = obj.gameObject.GetComponent("ErrorInfo") as ErrorInfo;
		if(fun!=null)
		ui.SetCallBack(fun);
		ui.SetText(error_info);
	}
}
