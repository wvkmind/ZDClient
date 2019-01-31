using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChatBox : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.GameObject bk;
    public UnityEngine.UI.Button close;
    public UnityEngine.UI.Button open;
    void Start()
    {
        open.onClick.AddListener(()=>{
            bk.gameObject.SetActive(true);
            close.gameObject.SetActive(true);
            open.gameObject.SetActive(false);
        });
        close.onClick.AddListener(()=>{
            close.gameObject.SetActive(false);
            open.gameObject.SetActive(true);
            bk.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
