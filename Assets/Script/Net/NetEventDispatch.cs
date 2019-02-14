using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MsgPack.Serialization;
using System.Threading;
public class NetEventDispatch {
	public static Dictionary<string,float> TTL = new  Dictionary<string,float>();
	private static Dictionary<string, List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>>> events = new Dictionary<string, List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>>>();
	public static void Clear(){
		events.Clear();
		TTL.Clear();
		is_timeout = false;
	}
	private static bool is_timeout = false;
	public static void RegisterEvent(string name,System.Action<Dictionary<string, MsgPack.MessagePackObject>> m){
		List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>> a; 
        if(!events.TryGetValue(name, out a))
		{
			List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>> mlist = new List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>>(); 
			mlist.Add(m);
			events.Add(name,mlist);
			if (events.ContainsKey(name))
			{
				events[name] = mlist;
			}
			else
			{
				events.Add(name, mlist);
			}
		}
		else
			a.Add(m);
	}
	public static void UnRegisterEvent(string name){
		events.Remove(name);
	}
	private static void CheckTTL(){

		Dictionary<string,float> temp = new  Dictionary<string,float>();
		if(!is_timeout)
		foreach(var key in TTL.Keys){
			float ttl ;
			if(TTL.TryGetValue(key,out ttl))
			{
				ttl = ttl + Time.deltaTime;
				if(ttl>10){
					is_timeout = true;
					ErrorInfo.CreateUI("跟村子丢失连接啦"+key,()=>{
						Login.ReLoginOut();
					});
				}
				temp.Add(key,ttl);
			}
		}
		TTL = temp;
	}
	private static void Loop(){
		CheckTTL();
		Byte[] d = NetWork.Get();
		while(!is_timeout&&d!=null){
			NetWork.PrepareType();
			var serializer = MessagePackSerializer.Get<Dictionary<string, MsgPack.MessagePackObject>>();
			Dictionary<string, MsgPack.MessagePackObject> dic= serializer.UnpackSingleObject(d);
			MsgPack.MessagePackObject a;
			dic.TryGetValue("event_name", out a);
			
			List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>> mlist;
			string name = a.ToString();
			if(events.TryGetValue(name, out mlist))
			{
				TTL.Remove(name);
				mlist.ForEach(delegate(System.Action<Dictionary<string, MsgPack.MessagePackObject>> e)
				{
					e.Invoke(dic);
				});
			}
			d = NetWork.Get();
		}
	}
	public static void Update () {
		Loop();
	}
}
