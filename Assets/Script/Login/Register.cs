using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register  {
	public static void In(string account,string password,System.Action<bool > f){
		string accountHash = Md5.GetMd5Hash(account);
		string passwrodHash = Md5.GetMd5Hash(password);
        NetEventDispatch.RegisterEvent("register",data =>{
			NetEventDispatch.UnRegisterEvent("register");
			Registered(data,f);
		});
		Dictionary<string, object> dic = NetWork.getSendStart();
		dic.Add("account",accountHash);
		dic.Add("password",passwrodHash);
		dic.Add("name", "register");
		NetWork.Push(dic);
	}
	private static void Registered(Dictionary<string, MsgPack.MessagePackObject> dic,System.Action<bool > f){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("status", out tmp);
		int status = tmp.AsInt32();
		if(f!=null)
		f.Invoke(status == 0);
	}
}
