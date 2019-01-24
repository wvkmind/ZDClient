using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterUI : MonoBehaviour {
	public UnityEngine.UI.Button bt1;
	public UnityEngine.UI.Button bt2;
	public UnityEngine.UI.Button bt3;
	public UnityEngine.UI.Button bt4;
	public UnityEngine.UI.Button bt5;
	public UnityEngine.UI.Button bt6;
	public UnityEngine.UI.Image number;
	public UnityEngine.UI.Image line1;
	public UnityEngine.UI.Image line2;
	public UnityEngine.UI.Image line3;
	public UnityEngine.UI.Button bTOpenPropertyUI;
	public UnityEngine.Canvas propertyUI;
	public UnityEngine.UI.Button btSettingOk;
	public UnityEngine.UI.InputField account;
	public UnityEngine.UI.InputField password;
	public UnityEngine.UI.InputField name;
	public UnityEngine.UI.Button btOk;
	public SelectRole role;
	private int num = 5;
	private int tra_rate = 0;
	private int phy_str_rate = 0;
	private int exp_rate = 0;
	private float line1_w = 0f;
	private float line2_w = 0f;
	private float line3_w = 0f;
	// Use this for initialization
	void Start () {
		FlashNumber();
		line1_w = line1.gameObject.GetComponent<RectTransform>().sizeDelta.x;
		line2_w = line2.gameObject.GetComponent<RectTransform>().sizeDelta.x;
		line3_w = line3.gameObject.GetComponent<RectTransform>().sizeDelta.x;
		bt1.onClick.AddListener(Bt1Up);
		bt2.onClick.AddListener(Bt1Down);
		bt3.onClick.AddListener(Bt2Up);
		bt4.onClick.AddListener(Bt2Down);
		bt5.onClick.AddListener(Bt3Up);
		bt6.onClick.AddListener(Bt3Down);
		bTOpenPropertyUI.onClick.AddListener(OpenProperty);
		btSettingOk.onClick.AddListener(CloseProperty);
		btOk.onClick.AddListener(onRegister);
		FlashLine();
	}
	void onRegister(){
		if(account.text.Equals("") || password.text.Equals("")||name.text.Equals(""))
		{
			ErrorInfo.CreateUI("你是否输入的信息有问题呢");
		}
		else
			Register.In(account.text,password.text,name.text,role.RoleType(),tra_rate,phy_str_rate,exp_rate,(status,error) =>{
				if(!status)
					ErrorInfo.CreateUI(error,()=>{
						account.text = "";
						password.text = "";
					});
				else
					ErrorInfo.CreateUI("注册成功，用户名密码已加密无法找回，请牢记。",()=>{
						SwitchScene.NextScene("Init");
					});
			});
	}
	void CloseProperty(){
		propertyUI.gameObject.SetActive(false);
	}
	void OpenProperty(){
		propertyUI.gameObject.SetActive(true);
	}
	void Bt1Up(){
		ChangeTra(true);
	}
	void Bt1Down(){
		ChangeTra(false);
	}
	void Bt2Up(){
		ChangePhy(true);
	}
	void Bt2Down(){
		ChangePhy(false);
	}
	void Bt3Up(){
		ChangeExp(true);
	}
	void Bt3Down(){
		ChangeExp(false);
	}
	void ChangeTra(bool up){
		if((tra_rate>0&&!up)||tra_rate<5&&up)
		{
			tra_rate = tra_rate+ChangeNumber(up);
			FlashLine();
		}
	}
	void ChangePhy(bool up){
		if((phy_str_rate>0&&!up)||phy_str_rate<5&&up)
		{
			phy_str_rate = phy_str_rate+ChangeNumber(up);
			FlashLine();
		}
	}
	void ChangeExp(bool up){
		if((exp_rate>0&&!up)||exp_rate<5&&up)
		{
			exp_rate = exp_rate+ChangeNumber(up);
			FlashLine();
		}
	}
	int ChangeNumber(bool up){
		int step = 0;
		if(up&&num>0){
			num = num - 1;
			step = 1;
		}
		else if(!up&&num<5)
		{
			num = num + 1;
			step = -1;
		}
		if(step!=0)
		{
			FlashNumber();
		}
		return step;
	}
	void FlashLine(){
		RectTransform line1_rect = line1.gameObject.GetComponent<RectTransform>();
		line1_rect.sizeDelta = new Vector2((tra_rate/5.0f)*line1_w,line1_rect.rect.height);
		RectTransform line2_rect = line2.gameObject.GetComponent<RectTransform>();
		line2_rect.sizeDelta = new Vector2((phy_str_rate/5.0f)*line2_w,line2_rect.rect.height);
		RectTransform line3_rect = line3.gameObject.GetComponent<RectTransform>();
		line3_rect.sizeDelta = new Vector2((exp_rate/5.0f)*line3_w,line3_rect.rect.height);
	}
	void FlashNumber(){
		if(num == 0)
		{
			number.gameObject.SetActive(false);
		}
		else
		{
			number.gameObject.SetActive(true);
			UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("Image/RegisterUI/Number_"+(num-1), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
			number.sprite = sprite;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
