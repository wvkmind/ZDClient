using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnApplicationFocus( bool  isFocus )
	{
 
 
		if( isFocus )
		{
			 
			Debug.Log("返回到游戏 刷新用户数据");  //  返回游戏的时候触发     执行顺序 2      
 
		}
		else
		{
			 
			Debug.Log("离开游戏 激活推送");  //  返回游戏的时候触发     执行顺序 1
		}
	}

    public void OnApplicationPause(bool isPause)
	{ 
 
		if( isPause)
		{
			//将玩家游戏切后台的运行时间检测到
 
 
			Debug.Log("游戏暂停 一切停止 ");  // 缩到桌面的时候触发
		}else
		{
			//回到前台我们需要将后台的倒计时方法关闭掉
			//将游戏的运行总时间检测到   使用总的游戏时间-玩家的切入后台的时间  就是玩家在切后台的总时间
		
 
			Debug.Log("游戏开始  万物生机 ");  //回到游戏的时候触发 最晚
		}
	}

}
