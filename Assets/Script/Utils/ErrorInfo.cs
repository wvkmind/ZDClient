using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorInfo : MonoBehaviour {

	public UnityEngine.UI.Button ok;
	public UnityEngine.UI.Text error_text;
	public UnityEngine.GameObject obj;
	public static string error_info = null;
	// Use this for initialization
	void Start () {
		ok.onClick.AddListener(Exit);
	}
	void Exit(){
		error_info = null;
		obj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(error_info!=null){
			obj.SetActive(true);
			error_text.text = error_info;
		}
	}
}
