using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init : MonoBehaviour {

	void Start () {
		NetWork.ConnectGate();
		//Register.In("test2","aklsdjfkla",data =>{Debug.Log("Register:"+data.ToString());});
		Login.In("test2","aklsdjfkla",(data,error) =>{Debug.Log("Login:"+data.ToString()+":"+error);});
	}
	private void OnClick(){
        if(start==false)
		{
			button_text.text = "停止";
			start = true;
		}
		else
		{
			button_text.text = "开始";
			start = false;
		}
    }
	void Update () {
		NetWork.Update();
		NetEventDispatch.Update();
	}
	void OnDestory() {
		//[TODO] 暂时放这里，真正的退出是游戏退出
		NetWork.OnDestory();
	}
}