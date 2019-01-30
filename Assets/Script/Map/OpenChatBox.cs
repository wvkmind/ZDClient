using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChatBox : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image bk;
    public UnityEngine.UI.Button close;
    void Start()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
            bk.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        });
        close.onClick.AddListener(()=>{
            bk.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
