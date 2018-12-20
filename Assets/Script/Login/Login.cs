using MsgPack.Serialization;
using System.Collections.Generic;
public class Login {
	public static void In(string account,string password,System.Action<bool,string > f){
		string accountHash = Md5.GetMd5Hash(account);
		string passwrodHash = Md5.GetMd5Hash(password);
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
	private static void Logined(Dictionary<string, MsgPack.MessagePackObject> dic,System.Action<bool,string > f){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("status", out tmp);
		int status = tmp.AsInt32();
		if(status == 0){
			dic.TryGetValue("ip", out tmp);
			string ip = tmp.AsString();
			dic.TryGetValue("port", out tmp);
			int port = tmp.AsInt32();
			NetWork.ConnectNode(ip,port);
			dic.TryGetValue("token", out tmp);
			string token = tmp.AsString();
			NetWork.token = token;
			NetWork.StartPing();
			f.Invoke(true,"");
		}
		else{
			dic.TryGetValue("error", out tmp);
			string error = tmp.AsString();
			f.Invoke(false,error);
		}
	}

}
