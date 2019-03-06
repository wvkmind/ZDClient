using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public UnityEngine.Canvas a ;
    public UnityEngine.Canvas b ;
    public UnityEngine.Canvas c ;
    public UnityEngine.UI.Button ba;
    public UnityEngine.UI.Button bb;
    public UnityEngine.UI.Button bc;
    // Start is called before the first frame update
    void Start()
    {
        ba.onClick.AddListener(()=>{
            a.gameObject.SetActive(true);
            b.gameObject.SetActive(false);
            c.gameObject.SetActive(false);
        });
        bb.onClick.AddListener(()=>{
            a.gameObject.SetActive(false);
            b.gameObject.SetActive(true);
            c.gameObject.SetActive(false);
        });
        bc.onClick.AddListener(()=>{
            a.gameObject.SetActive(false);
            b.gameObject.SetActive(false);
            c.gameObject.SetActive(true);
        });
    }
}
