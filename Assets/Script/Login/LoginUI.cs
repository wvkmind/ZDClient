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
		//UnityEngine.SceneManagement.SceneManager.
	}
	void LoginFunction(){
		Login.In(account.text,password.text,(data,error) =>{
			if(account.text.Equals("") || account.text.Equals(""))
			{
				ErrorInfo.CreateUI("Pls Check Input.");
			}
			else if(error!=null&&!error.Equals(""))
				ErrorInfo.CreateUI(error,()=>{
					account.text = "";
					password.text = "";
				});
			else
			{
				//TODO: 登陆成功
			}
		});

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
