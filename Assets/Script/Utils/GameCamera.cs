using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils2D;
public class GameCamera : MonoBehaviour {
    void Awake() {
        View.Init(GetComponent<Camera>());
    }
    void Update () {

    }
}
