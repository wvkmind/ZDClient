using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;
public class CreateRoomUI : MonoBehaviour
{
    public UnityEngine.UI.Button _createRoom;
    public UnityEngine.UI.Button _exit;
    public UnityEngine.UI.Button [] select_list;
    private int sellect_index = -1;
    // Start is called before the first frame update
    void Start()
    {
       // _createRoom.onClick.AddListener();
        _exit.onClick.AddListener(Cancel);
        for(int i=0;i<19;i++){
            int a = i;
            select_list[a].onClick.AddListener(()=>{
                select(a);
            });
        }
    }
    void select(int i ){
        if(sellect_index!=-1)
        {
            UnityEngine.Sprite _sprite  = UnityEngine.Resources.Load("GUI/Lobby/CRoomBack_4", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
            select_list[sellect_index].GetComponent<UnityEngine.UI.Image>().sprite = _sprite;
        }
        UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("GUI/Lobby/CRoomBack_2", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
        select_list[i].GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        sellect_index = i;
    }
    void Cancel()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
