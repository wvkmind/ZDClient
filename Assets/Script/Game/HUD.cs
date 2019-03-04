﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;
public class HUD : MonoBehaviour
{
    public UnityEngine.UI.Image tilizhi;
    public UnityEngine.UI.Image exp;
    private float tilizhi_img_width = 198.0f;
    private float exp_img_width = 198.0f;
    float oldt=-1;
    float olde=-1;
    void Awake()
    {
        tilizhi_img_width = tilizhi.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        exp_img_width = exp.gameObject.GetComponent<RectTransform>().sizeDelta.x;
    }
    public void FreshHUD()
    {
        if(Init.me.GetComponent<RoleData>().data!=null)
        {
            if(oldt!=Init.me.GetComponent<RoleData>().data.tilizhi)
            {
                oldt = Init.me.GetComponent<RoleData>().data.tilizhi;
                RectTransform tilizhi_rect = tilizhi.gameObject.GetComponent<RectTransform>();
                tilizhi_rect.sizeDelta = new Vector2((Init.me.GetComponent<RoleData>().data.tilizhi/100.0f)*tilizhi_img_width,tilizhi_rect.rect.height);
            }
            if(olde!=Init.me.GetComponent<RoleData>().data.exp)
            {
                olde = Init.me.GetComponent<RoleData>().data.exp;
                RectTransform exp_rect = exp.gameObject.GetComponent<RectTransform>();
                float e = ( (float)(Init.me.GetComponent<RoleData>().data.exp-Role.LevelExp[Init.me.GetComponent<RoleData>().data.level-1])/Role.LevelExp[Init.me.GetComponent<RoleData>().data.level+1]-Role.LevelExp[Init.me.GetComponent<RoleData>().data.level-1]);
                exp_rect.sizeDelta = new Vector2(e*exp_img_width,exp_rect.rect.height);
            }
        }
    }
}
