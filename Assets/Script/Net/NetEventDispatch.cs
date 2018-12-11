using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MsgPack.Serialization;
using System.Threading;
public class NetEventDispatch {
	private static Dictionary<string, List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>>> events = new Dictionary<string, List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>>>();
	public static void Clear(){
		events.Clear();
	}
	public static void RegisterEvent(string name,System.Action<Dictionary<string, MsgPack.MessagePackObject>> m){
		List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>> a; 
        if(!events.TryGetValue(name, out a))
		{
			List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>> mlist = new List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>>(); 
			mlist.Add(m);
			events.Add(name,mlist);
		}
		else
			a.Add(m);
	}
	public static void UnRegisterEvent(string name){
		events.Remove(name);
	}
	private static void Loop(){
		Byte[] d = NetWork.Get();
		while(d!=null){
			NetWork.PrepareType();
			var serializer = MessagePackSerializer.Get<Dictionary<string, MsgPack.MessagePackObject>>();
			Dictionary<string, MsgPack.MessagePackObject> dic= serializer.UnpackSingleObject(d);
			MsgPack.MessagePackObject a;
			dic.TryGetValue("event_name", out a);
			
			List<System.Action<Dictionary<string, MsgPack.MessagePackObject>>> mlist;
			string name = a.ToString();
			if(events.TryGetValue(name, out mlist))
			{
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
