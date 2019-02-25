using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMapUI : MonoBehaviour
{
    public UnityEngine.UI.Button nextPage;
    public UnityEngine.UI.Button beforePage;
    public UnityEngine.UI.Button exit;
    public UnityEngine.UI.Button chatMap;
    public UnityEngine.GameObject node;
    private int page = 0;
    private float time = 0.0f;
    private float per_page = 0.0f;
    private bool flag = false;
    void Start()
    {
        exit.onClick.AddListener(ExitToInit);
        chatMap.onClick.AddListener(OpenChatHall);
        nextPage.onClick.AddListener(_NextPage);
        beforePage.onClick.AddListener(_BeforePage);
        node.transform.localScale = node.transform.localScale*Camera.main.rect.height;
        per_page =  -(800.0f/3.0f*Camera.main.rect.height);
        FlushPage();
    }
    void FlushPage()
    {
        beforePage.gameObject.SetActive(page!=0);
        nextPage.gameObject.SetActive(page!=2);
        
    }
    void _NextPage()
    {
        if(page!=2)page = page + 1;
        FlushPage();
        flag = true;
    }
    void _BeforePage()
    {
        if(page!=0)page = page - 1;
        FlushPage();
        flag = true;
    }
    void OpenChatHall()
    {
        SwitchScene.NextScene("ChatHall");
    }
    void ExitToInit()
    {
        SwitchScene.NextScene("Login");
    }
    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            time = time + Time.deltaTime;
            float s = page*per_page-node.GetComponent<RectTransform>().anchoredPosition.x;
            if(s == 0 )time = 0.0f;
            float cur_speed = 0.0f;
            if(s != 0 )
                cur_speed = s / time*(0.1f);
            node.GetComponent<RectTransform>().anchoredPosition = new Vector2(node.GetComponent<RectTransform>().anchoredPosition.x+time*cur_speed,node.GetComponent<RectTransform>().anchoredPosition.y);
            if(Mathf.Abs(node.GetComponent<RectTransform>().anchoredPosition.x - node.GetComponent<RectTransform>().anchoredPosition.x+time*cur_speed)<0.001f)flag=false;
        }
        
    }
}
