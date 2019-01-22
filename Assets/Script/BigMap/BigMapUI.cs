using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMapUI : MonoBehaviour
{
    public UnityEngine.GameObject node;
    private bool is_begen_touch = false;
    private bool is_move = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                is_begen_touch = true;
            }
            else
            {
                if(Input.touches[0].phase == TouchPhase.Moved)
                {
                    
                    if(is_begen_touch)
                    {
                        float ch = node.transform.position.x+Input.touches[0].deltaPosition.x;
                        if(ch<0&&ch>-(800*3.2*Screen.height/1920-Screen.width))
                            node.transform.position = new Vector3(ch,node.transform.position.y,node.transform.position.z);
                    }
                    is_move = true;
                }
                else if(Input.touches[0].phase ==TouchPhase.Ended)
                {
                    is_move = false;
                    is_begen_touch = false;
                }
            }
        }
    }
}
