using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUI : MonoBehaviour {

	public UnityEngine.UI.InputField account;
	public UnityEngine.UI.InputField password;
	public UnityEngine.UI.Button login;
	public UnityEngine.UI.Button register;
	// Use this for initialization
	void Start () {
		login.onClick.AddListener(LoginFunction);
		register.onClick.AddListener(RegisterFunction);
	}
	void RegisterFunction(){
		SwitchScene.NextScene("Register");
	}
	void LoginFunction(){
		if(account.text.Equals("") || password.text.Equals(""))
		{
			ErrorInfo.CreateUI("Pls Check Input.");
		}
		else
			Login.In(account.text,password.text,(data,error) =>{
				if(error!=null&&!error.Equals(""))
				ErrorInfo.CreateUI(error,()=>{
					account.text = "";
					password.text = "";
					SwitchScene.NextScene("Init");
				});
				else
				{
					SwitchScene.NextScene("BigMap");
				}
			});

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
