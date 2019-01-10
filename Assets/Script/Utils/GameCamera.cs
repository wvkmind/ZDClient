using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils2D;
public class GameCamera : MonoBehaviour {
    private int screen_width = 0;
    private int screen_height = 0;
    void Awake() {
        View.Init(GetComponent<Camera>());
    }
    void Update () {

    }
}
