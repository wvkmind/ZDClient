using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatHall : MonoBehaviour
{
    public UnityEngine.UI.Button _createRoom;
    public UnityEngine.UI.Button _exit;
    public UnityEngine.UI.Button _sendLobby;

    // Start is called before the first frame update
    void Start()
    {
        _exit.onClick.AddListener(ExitToBigMap);
    }
    void ExitToBigMap()
    {
        SwitchScene.NextScene("BigMap");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
