﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUI : MonoBehaviour {
	// Use this for initialization
	public UnityEngine.UI.Text CURRENTACCOUNT;
	public UnityEngine.UI.Text ACCOUNT;
	public UnityEngine.UI.Button BTN_LOGIN;
	public UnityEngine.UI.Button BTN_CHANGEACCOUNT;
	public UnityEngine.UI.Text I;
	public UnityEngine.UI.Button BTN_CREATEACCOUNT;
	public UnityEngine.UI.Button BNT_REPORT;
	public UnityEngine.UI.Button BTN_CREATEACCOUNT2;
	public UnityEngine.UI.Text ACCOUNT2;
	public UnityEngine.UI.Text PASSWORD;
	public UnityEngine.UI.InputField AccountInputField;
	public UnityEngine.UI.InputField PasswordInputField;
	public UnityEngine.UI.Toggle Volume;
	public UnityEngine.UI.Button BTN_TEST;
	private bool directLoginFlag = false;
	void PageChangAccount(){
		BTN_CREATEACCOUNT2.gameObject.SetActive(true);
		ACCOUNT2.gameObject.SetActive(true);
		PASSWORD.gameObject.SetActive(true);
		AccountInputField.gameObject.SetActive(true);
		PasswordInputField.gameObject.SetActive(true);

		CURRENTACCOUNT.gameObject.SetActive(false);
		ACCOUNT.gameObject.SetActive(false);
		BTN_CHANGEACCOUNT.gameObject.SetActive(false);
		I.gameObject.SetActive(false);
		BTN_CREATEACCOUNT.gameObject.SetActive(false);

		directLoginFlag = false;
	}
	void PageLogin(){
		CURRENTACCOUNT.gameObject.SetActive(true);
		ACCOUNT.gameObject.SetActive(true);
		BTN_CHANGEACCOUNT.gameObject.SetActive(true);
		I.gameObject.SetActive(true);
		BTN_CREATEACCOUNT.gameObject.SetActive(true);

		BTN_CREATEACCOUNT2.gameObject.SetActive(false);
		ACCOUNT2.gameObject.SetActive(false);
		PASSWORD.gameObject.SetActive(false);
		AccountInputField.gameObject.SetActive(false);
		PasswordInputField.gameObject.SetActive(false);

		directLoginFlag = true;
	}
	void Start () {

		string account = PlayerPrefs.GetString("user.account","");
		if(account.Equals(""))
		{
			PageChangAccount();
		}
		else
		{
			ACCOUNT.text = account;
			PageLogin();
		}
		BTN_CHANGEACCOUNT.onClick.AddListener(()=>{
			PageChangAccount();
		});
		BTN_CREATEACCOUNT.onClick.AddListener(()=>{
			RegisterFunction();
		});
		BTN_CREATEACCOUNT2.onClick.AddListener(()=>{
			RegisterFunction();
		});
		BTN_TEST.onClick.AddListener(()=>{
			SwitchScene.NextScene("Test1");
		});
		BTN_LOGIN.onClick.AddListener(LoginFunction);
		Volume.isOn = PlayerPrefs.GetInt("volume",1)==0;
		Volume.onValueChanged.AddListener(delegate {
            ToggleValueChanged(Volume);
        });
		AudioListener.volume = PlayerPrefs.GetInt("volume",1);
	}
	 void ToggleValueChanged(UnityEngine.UI.Toggle change)
    {
        PlayerPrefs.SetInt("volume",change.isOn?0:1);
		AudioListener.volume = PlayerPrefs.GetInt("volume",1);
    }
	void RegisterFunction(){
		SwitchScene.NextScene("Register");
	}
	void LoginFunction(){
		string a ;
		string p ;
		if(directLoginFlag)
		{
			a = PlayerPrefs.GetString("user.account","");
			p = PlayerPrefs.GetString("user.password","");
		}
		else
		{
			a = AccountInputField.text;
			p = PasswordInputField.text;
		}
		BTN_LOGIN.enabled = false;
		BTN_CREATEACCOUNT.enabled = false;
		BTN_CREATEACCOUNT2.enabled = false;
		BTN_CHANGEACCOUNT.enabled = false;
		Login.In(a,p,(data,error) =>{
			if(error!=null&&!error.Equals(""))
				ErrorInfo.CreateUI(error,()=>{
					AccountInputField.text = "";
					PasswordInputField.text = "";
					SwitchScene.NextScene("Login");
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
