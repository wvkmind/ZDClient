using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRender : MonoBehaviour
{
    private SpriteRenderer item_pic;
    private int type;
    private int id;
    void Awake(){
        item_pic = GetComponent<SpriteRenderer>();
    }
    public void SetNull(){
        gameObject.SetActive(false);
    }
    public void SetType(int n){
        type = id;
    }
    public bool IsFood(){
        return type == 1;
    }
    public void SetId(int n){
        id = n;
    }
    public void Fresh(){
        string path ;
        if(type == 0)
        {
            path = "Image/Item/Laji/"+id;
        }else{
            path = "Image/Item/Food/EatItem.spr/"+id;
        }
        UnityEngine.Sprite _sprite  = UnityEngine.Resources.Load(path, typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
        item_pic.sprite = _sprite;
    }
    public void Set(int i,int t){
        SetId(i);
        SetType(t);
        Fresh();
        gameObject.SetActive(true);
    }
}
