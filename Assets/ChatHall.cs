using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatHall : MonoBehaviour
{
    public UnityEngine.UI.Button createRoom;
    public UnityEngine.UI.Button Exit;
    public UnityEngine.UI.Button SendLobby;

    // Start is called before the first frame update
    void Start()
    {
        Exit.onClick.AddListener(ExitToBigMap);
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
