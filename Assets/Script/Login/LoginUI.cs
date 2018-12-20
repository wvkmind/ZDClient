using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUI : MonoBehaviour {

	public UnityEngine.UI.InputField account;
	public UnityEngine.UI.InputField password;
	public UnityEngine.UI.Button login;
	// Use this for initialization
	void Start () {
		login.onClick.AddListener(LoginFunction);
	}

	void LoginFunction(){
		Login.In(account.text,password.text,(data,error) =>{
			if(error!=null&&!error.Equals(""))
				ErrorInfo.error_info = error;
			else
				ErrorInfo.error_info = "登陆成功";
		});

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
