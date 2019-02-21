using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "你的设备信息是："+UnityEngine.SystemInfo.deviceModel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
