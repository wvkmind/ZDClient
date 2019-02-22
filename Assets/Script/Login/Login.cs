using MsgPack.Serialization;
using System.Collections.Generic;
using UnityEngine;
using DataModel;
public class Login {
	public static string _account ;
	public static string _password ;
	public static void In(string account,string password,System.Action<bool,string > f){
		string accountHash = Md5.GetMd5Hash(account);
		string passwrodHash = Md5.GetMd5Hash(password);
		_account = account;
		_password = password;
		PlayerPrefs.SetString("user.account",_account);
		PlayerPrefs.SetString("user.password",_password);
        NetEventDispatch.RegisterEvent("login",data =>{
			NetEventDispatch.UnRegisterEvent("login");
			Logined(data,f);
		});
		Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("account",accountHash);
		dic.Add("password",passwrodHash);
		dic.Add("name", "login");
		NetWork.Push(dic);
	}
	public static void LoginOut(){
		NetEventDispatch.Clear();
		NetWork.ClosePing();
		NetWork.ClearQueue();
		NetWork.ConnectGate();
	}
	public static void ReLoginOut(){
		LoginOut();
		Login.In(_account,_password,(data,error) =>{
			if(error!=null&&!error.Equals(""))
			ErrorInfo.CreateUI(error,()=>{
				SwitchScene.NextScene("Login");
			});
			else
			{
				SwitchScene.NextScene("BigMap");
			}
		});
	}
	private static void Logined(Dictionary<string, MsgPack.MessagePackObject> dic,System.Action<bool,string > f){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("status", out tmp);
		int status = tmp.AsInt32();
		if(status == 0){
			
			dic.TryGetValue("ip", out tmp);
			string ip = tmp.AsStringUtf8();
			dic.TryGetValue("port", out tmp);
			int port = tmp.AsInt32();
			NetWork.ConnectNode(ip,port);
			dic.TryGetValue("token", out tmp);
			string token = tmp.AsStringUtf8();
			dic.TryGetValue("user", out tmp);
			Init.userInfo = (new User()).UnPack(tmp);
			NetWork.token = token;
			NetWork.StartPing();
			f.Invoke(true,"");
		}
		else{
			dic.TryGetValue("error", out tmp);
			string error = tmp.AsStringUtf8();
			f.Invoke(false,error);
		}
	}

}
