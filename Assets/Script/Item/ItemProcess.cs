using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProcess : MonoBehaviour
{
    public UnityEngine.UI.Button button ;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(()=>{
            Debug.Log("aslkdfakljdfklas");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        RoleData role = col.gameObject.GetComponent<RoleData>();
        if(role.data.id==Init.userInfo.id)
            Debug.Log("GameObject2 enter collided with " + col.name);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        RoleData role = col.gameObject.GetComponent<RoleData>();
        if(role.data.id==Init.userInfo.id)
            Debug.Log("GameObject2 exit collided with " + col.name);
    }
     
}
