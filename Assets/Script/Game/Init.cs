using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataModel;
public class Init : MonoBehaviour {
	public static Init instance = null;
	//User信息
	public static User userInfo = null;//自己的信息
	public static User [] otherUsersInCurMap = new User[9]; //房间里别人的信息
	public static UnityEngine.GameObject me = null;//房间里我的角色
	public static Dictionary<int, UnityEngine.GameObject> other = new Dictionary<int, UnityEngine.GameObject>();//房间里别人的角色
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
		other.Clear();
	}
}