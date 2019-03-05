using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public UnityEngine.Canvas a ;
    public UnityEngine.Canvas b ;
    public UnityEngine.UI.Button ba;
    public UnityEngine.UI.Button bb;
    public UnityEngine.UI.Button c;
    public UnityEngine.UI.InputField d;
    public UnityEngine.UI.Image e;
    // Start is called before the first frame update
    void Start()
    {
        ba.onClick.AddListener(()=>{
            a.gameObject.SetActive(false);
            b.gameObject.SetActive(true);
        });
        bb.onClick.AddListener(()=>{
            a.gameObject.SetActive(true);
            b.gameObject.SetActive(false);
        });
        c.onClick.AddListener(()=>{
            e.GetComponent<RectTransform>().sizeDelta = new Vector2(float.Parse(d.text), 20.0f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
