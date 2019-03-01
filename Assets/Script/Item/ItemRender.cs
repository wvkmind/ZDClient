using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRender : MonoBehaviour
{
    private SpriteRenderer item_pic;
    public UnityEngine.UI.Button button ;
    public UnityEngine.GameObject food_lave_bk;
    public UnityEngine.GameObject food_lave;
    public int type;
    public int id;
    public bool pick_action = false;
    void Awake(){
        item_pic = GetComponent<SpriteRenderer>();
    }
    public void SetNull(){
        type = -1;
        id = -1;
        gameObject.SetActive(false);
    }
    public void SetType(int n){
        type = n;
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
        if(i==this.id&&t==this.type)return;
        SetId(i);
        SetType(t);
        Fresh();
        string str = "Pick";
        if(t==1) str = "Eat";
        UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("GUI/Map/"+str, typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
		button.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        button.gameObject.SetActive(false);
        gameObject.SetActive(true);
        food_lave_bk.gameObject.SetActive(t==1);
        food_lave.gameObject.SetActive(t==1);
    }
    public void SetEnergy(float scale){
        food_lave.gameObject.transform.localScale = new Vector3(scale,1,1);
    }
    void Update()
    {
        if(pick_action){
            PickAction();
            pick_action = false;
        }
    }

    public void PickAction()
    {
        gameObject.transform.localPosition  = Vector3.MoveTowards(gameObject.transform.localPosition,new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y+1,gameObject.transform.localPosition.z),Time.deltaTime);
    }
}
