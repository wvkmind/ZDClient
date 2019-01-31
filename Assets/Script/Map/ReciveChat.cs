using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveChat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.UI.Text text = this.GetComponent<UnityEngine.UI.Text>();
        NetEventDispatch.RegisterEvent("talk",data =>{
			MsgPack.MessagePackObject tmp;
            data.TryGetValue("talk", out tmp);
            text.text = text.text+"\n"+tmp.AsStringUtf8();
		});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        NetEventDispatch.UnRegisterEvent("talk");
    }
}
