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
    void OnApplicationFocus( bool  isFocus )
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

}
