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
    private Vector2 end_position;
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
        sc = Screen.width/375.0f;
        limit = 400.0f*0.9566666f-375.0f/2.0f;
        end_position = imgBigMap.GetComponent<RectTransform>().anchoredPosition;
    }
    void Entry(int i){
        if(moved)return;
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
                touchPositon = Input.touches[0].position;
                touchEnd = false;
            }else if(Input.touches[0].phase == TouchPhase.Moved){
                if(Mathf.Abs(touchPositon.x-Input.touches[0].position.x)>15&&Mathf.Abs(touchPositon.y-Input.touches[0].position.y)>15||moved)
                {
                    moved = true;
                    Vector3 delta_pos =  Input.touches[0].deltaPosition;
                    float diff = imgBigMap.GetComponent<RectTransform>().anchoredPosition.x+delta_pos.x/sc*3;
                    if(Mathf.Abs(diff)<limit)
                    {
                        end_position = new Vector2(diff,imgBigMap.GetComponent<RectTransform>().anchoredPosition.y);
                    }
                    else
                    {
                        end_position = new Vector2((diff/Mathf.Abs(diff))*limit,imgBigMap.GetComponent<RectTransform>().anchoredPosition.y);
                    }
                    Debug.Log("imgBigMap.GetComponent<RectTransform>().anchoredPositin"+imgBigMap.GetComponent<RectTransform>().anchoredPosition);
                    Debug.Log("end_position"+end_position);
                }
            }
            else if(Input.touches[0].phase == TouchPhase.Ended||Input.touches[0].phase == TouchPhase.Canceled){
                touchEnd = true;
                moved = false;
            }
            
        }
        float dx = Mathf.Abs(imgBigMap.GetComponent<RectTransform>().anchoredPosition.x-end_position.x);
        if(dx<0.001f)
        {

        }
        else
        {
            float t = dx*50.0f*Time.deltaTime;
            imgBigMap.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(imgBigMap.GetComponent<RectTransform>().anchoredPosition,end_position,t);
        }
        
    }
}
