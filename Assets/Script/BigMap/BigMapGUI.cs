using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BigMapGUI : MonoBehaviour
{
    public UnityEngine.UI.Button [] btnEntrancies;
    public UnityEngine.Animator [] aniDynamics;
    public UnityEngine.UI.Button btnExit;
    private int beforeClick = -1;
    public UnityEngine.UI.Image imgBigMap;
    private Vector3 touchPositon;
    private bool touchEnd=true;
    private bool moved = false;
    private float limit;
    private float sc ;
    private float unit;
    void Start()
    {
        for(int i =0;i<btnEntrancies.Length;i++){
            int _i = i;
            btnEntrancies[_i].onClick.AddListener(()=>{
                Entry(_i);
            });
            aniDynamics[_i].enabled = false;
        }
        btnExit.onClick.AddListener(()=>{
            SwitchScene.NextScene("Login");
        });
        unit = Screen.height/2.0f/Camera.main.orthographicSize;
        sc = unit;
        limit = (400.0f-Screen.width/(Screen.height*Camera.main.rect.height/600.0f)/2.0f)*3.2f/100.0f;
    }
    void Entry(int i){
        Debug.Log(i);
        if(beforeClick!=i&&aniDynamics[i].enabled==false){
            if(beforeClick!=-1)
            {
                aniDynamics[beforeClick].enabled = false;
            }
            aniDynamics[i].enabled = true;
        }
        else if(beforeClick==i&&aniDynamics[i].enabled==true)
        {
            switch (i)
            {
                case 0 : SwitchScene.NextScene("Shop");break;
                case 1 : SwitchScene.NextScene("ChatHall");break;
                case 2 : SwitchScene.NextScene("Landfill");break;
                case 3 : SwitchScene.NextScene("Qixingtang");break;
                case 4 : SwitchScene.NextScene("Youlechang");break;
                case 5 : SwitchScene.NextScene("Police");break;
                case 6 : SwitchScene.NextScene("HuntingChat");break;
                default: return ;
            }
        }
        beforeClick = i;
    }
    void Update()
    {
        //for ios and android
        if(Input.touchCount == 1 )
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    
                }
                else{
                    touchPositon = Input.touches[0].position;
                    touchEnd = false;
                }
            }else if(Input.touches[0].phase == TouchPhase.Moved){
                if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    
                }
                else
                {
                    if(Mathf.Abs(touchPositon.x-Input.touches[0].position.x)>15&&Mathf.Abs(touchPositon.y-Input.touches[0].position.y)>15||moved)
                    {
                        moved = true;
                        Vector3 delta_pos =  Input.touches[0].deltaPosition;
                        if(Mathf.Abs(imgBigMap.transform.position.x+delta_pos.x/sc)<limit)
                            imgBigMap.transform.position = new Vector3(imgBigMap.transform.position.x+delta_pos.x/sc,imgBigMap.transform.position.y,imgBigMap.transform.position.z);
                    }
                }
            }
            else if(Input.touches[0].phase == TouchPhase.Ended||Input.touches[0].phase == TouchPhase.Canceled){
                touchEnd = true;
                moved = false;
            }
            
        }
    }
}
