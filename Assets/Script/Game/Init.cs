using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataModel;
public class Init : MonoBehaviour {
	public static Init instance = null;
	//User信息
	public static User userInfo = null;//自己的信息
	public static ArrayList  otherUsersInCurMap = new ArrayList(); //房间里别人的信息只在大厅进入的时候用
	public static UnityEngine.GameObject me = null;//房间里我的角色
	public static Dictionary<int, UnityEngine.GameObject> other = new Dictionary<int, UnityEngine.GameObject>();//房间里别人的角色
	public static UnityEngine.GameObject map = null;
	public static Dictionary<string, MsgPack.MessagePackObject> temp_data = new Dictionary<string, MsgPack.MessagePackObject>();//切换场景的时候的临时数据 (这是个傻逼方案)
	void Start () {
		instance = this;
		NetWork.ConnectGate();
		DontDestroyOnLoad(this);
	}
	void Update () {
		NetWork.Update();
		NetEventDispatch.Update();
	}
	void OnDestory() {
		NetWork.OnDestory();
	}
	public static void PutRoleObjectWithId(int id,UnityEngine.GameObject obj){
		other.Remove(id);
		other.Add(id,obj);
	}
	public static UnityEngine.GameObject GetRoleObjecWithId(int id){
		UnityEngine.GameObject tmp;
		other.TryGetValue(id,out tmp);
		return tmp;
	}
	public static void RemoveRoleObjectWithId(int id){
		other.Remove(id);
	}
	public static void RemoveAllRoleObject(){
		otherUsersInCurMap.Clear();
		temp_data.Clear();
		other.Clear();
	}

	public static void PushData(string key,MsgPack.MessagePackObject value){
		temp_data.Remove(key);
		temp_data.Add(key,value);
	}
	public static MsgPack.MessagePackObject GetData(string key)
	{
		MsgPack.MessagePackObject ret;
		temp_data.TryGetValue(key,out ret);
		return ret;
	}
}