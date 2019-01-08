using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    private int screen_width = 0;
    private int screen_height = 0;
    void Start () {
        Debug.Log(Screen.width+"x"+Screen.height);
    }
    void Update () {
        if(screen_width!=Screen.width||screen_height!=Screen.height){
            screen_width = Screen.width;
            screen_height = Screen.height;
        }
    }
}
