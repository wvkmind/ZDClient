using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register  {
	public static void In(string account,string password,int type,int tra,int phy,int exp,System.Action<bool ,string> f){
		string accountHash = Md5.GetMd5Hash(account);
		string passwrodHash = Md5.GetMd5Hash(password);
        NetEventDispatch.RegisterEvent("register",data =>{
			NetEventDispatch.UnRegisterEvent("register");
			Registered(data,f);
		});
		Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("account",accountHash);
		dic.Add("password",passwrodHash);
		dic.Add("type",type);
		dic.Add("tra_rate",tra);
		dic.Add("phy_str_rate",phy);
		dic.Add("exp_rate",exp);
		dic.Add("name", "register");
		NetWork.Push(dic);
	}
	private static void Registered(Dictionary<string, MsgPack.MessagePackObject> dic,System.Action<bool,string > f){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("status", out tmp);
		int status = tmp.AsInt32();
		if(f!=null)
		if(status == 0){
			f.Invoke(true,"");
		}else{
			dic.TryGetValue("error", out tmp);
			string error = tmp.AsString();
			f.Invoke(false,error);
		}
	}
}
