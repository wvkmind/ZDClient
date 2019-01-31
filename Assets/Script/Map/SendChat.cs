using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendChat : MonoBehaviour
{
    public UnityEngine.UI.InputField input;
    
    void Start()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
            if(input.text!="")
            {
                Dictionary<string, object> dic = NetWork.getSendStart();
                dic.Add("talk",Init.userInfo.name+":"+input.text);
                input.text = "";
                dic.Add("name", "talk");
                NetWork.Push(dic);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
