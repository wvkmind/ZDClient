using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;

public class MapInit : MonoBehaviour
{   
    void Start()
    {
        Init.map = this.gameObject;
    }
    void Update()
    {
        
    }
    void OnDestroy() {
        Init.map = null;
    }
}
