using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAction : MonoBehaviour
{
    public UnityEngine.UI.Button up;
    public UnityEngine.UI.Button down;
    public UnityEngine.UI.Button ac1;
    public UnityEngine.UI.Button ac2;
    public UnityEngine.UI.Button ac3;
    public UnityEngine.UI.Button ac4;
    private int page = 0;
    private int [] actions_related = {7,2,3,4,5,14,8,9,10,1,15,16,17,18,19};
    void Start()
    {
        up.onClick.AddListener(()=>{
            if(page<3)
            {   
                page = page + 1;
                FlushActionIcon();
            }
        });
        down.onClick.AddListener(()=>{
            if(page>0)
            {
                page = page - 1;
                FlushActionIcon();
            }
        });
        ac1.onClick.AddListener(()=>{
            SendActionToOther(0);
        });
        ac2.onClick.AddListener(()=>{
            SendActionToOther(1);
        });
        ac3.onClick.AddListener(()=>{
            SendActionToOther(2);
        });
        ac4.onClick.AddListener(()=>{
            SendActionToOther(3);
        });
    }
    void SendActionToOther(int i ){
        Dictionary<string, object> dic = NetWork.getSendStart();
        int ac_data = page*4+i;
		dic.Add("ac_data",actions_related[ac_data]);
		dic.Add("name", "ac");
		NetWork.Push(dic);
    }
    void FlushActionIcon(){
        UnityEngine.Sprite sprite1  = UnityEngine.Resources.Load("GUI/Map/ActionInventoryIcon_"+(page*4), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
        if(sprite1!=null){
            ac1.GetComponent<UnityEngine.UI.Image>().sprite = sprite1;
            ac1.gameObject.SetActive(true);
        }
        else
        {
            ac1.gameObject.SetActive(false);
        }
        UnityEngine.Sprite sprite2  = UnityEngine.Resources.Load("GUI/Map/ActionInventoryIcon_"+(page*4+1), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
        if(sprite2!=null){
            ac2.GetComponent<UnityEngine.UI.Image>().sprite = sprite2;
            ac2.gameObject.SetActive(true);
        }
        else
        {
            ac2.gameObject.SetActive(false);
        }
        UnityEngine.Sprite sprite3  = UnityEngine.Resources.Load("GUI/Map/ActionInventoryIcon_"+(page*4+2), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
        if(sprite3!=null){
            ac3.GetComponent<UnityEngine.UI.Image>().sprite = sprite3;
            ac3.gameObject.SetActive(true);
        }
        else
        {
            ac1.gameObject.SetActive(false);
        }
        UnityEngine.Sprite sprite4  = UnityEngine.Resources.Load("GUI/Map/ActionInventoryIcon_"+(page*4+3), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
        if(sprite4!=null){
            ac4.GetComponent<UnityEngine.UI.Image>().sprite = sprite4;
            ac4.gameObject.SetActive(true);
        }
        else
        {
            ac4.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
