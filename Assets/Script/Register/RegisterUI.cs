using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseData;

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
	public UnityEngine.UI.InputField account;
	public UnityEngine.UI.InputField password;
	public UnityEngine.UI.InputField userName;
	public UnityEngine.Canvas selectRoleUI;
	public UnityEngine.Canvas infoUI;
	public UnityEngine.UI.Button BACK1;
	public UnityEngine.UI.Button NEXT1;
	public UnityEngine.UI.Button BACK2;
	public UnityEngine.UI.Button NEXT2;
	public UnityEngine.UI.Text text_tra_rate;
	public UnityEngine.UI.Text text_phy_str_rate;
	public UnityEngine.UI.Text text_exp_rate;
	private int num = 5;
	private float tra_rate = 0;
	private float phy_str_rate = 0;
	private float exp_rate = 0;
	private float rate1 = 0;
	private float rate2 = 0;
	private float rate3 = 0;
	private float line1_w = 0f;
	private float line2_w = 0f;
	private float line3_w = 0f;
	public UnityEngine.UI.Button [] roles;
	public UnityEngine.UI.Image [] roles_sel;
	public Animator  role_show;
	public AudioSource audio_source;
	private int role_type = 0;
	private int before_role_type = 0;
	public int RoleType(){
		return role_type;
	}
	void InitDefaultProperty(){
		tra_rate = Role.PropertyDefault[role_type*3];
		phy_str_rate = Role.PropertyDefault[role_type*3+1];
		exp_rate = Role.PropertyDefault[role_type*3+2];
		FreshDefaultProperty();
	}
	void FreshDefaultProperty(){
		text_tra_rate.text = (tra_rate+rate1*0.2).ToString("#0.0");
		text_phy_str_rate.text = (phy_str_rate+rate2*0.2).ToString("#0.0");
		text_exp_rate.text = (exp_rate+rate3*0.2).ToString("#0.0");
	}
	void OnSelect(int i){
		role_type = i;
		show(i);
		playAudio(i.ToString());
		hide(before_role_type);
		before_role_type = i;
	}
	void playAudio(string index){
		UnityEngine.AudioClip clip =(UnityEngine.AudioClip)Resources.Load("Sound/AvatarSelect/"+index, typeof(UnityEngine.AudioClip));
		audio_source.clip = clip;
		audio_source.Play();
	}
	void show(int i){
		UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("Image/RegisterUI/Role/Koongya_"+(i*2+1), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
		roles[i].GetComponent<UnityEngine.UI.Image>().sprite = sprite;
		roles_sel[i].gameObject.SetActive(true);
		int _ani_layer_index = role_show.GetLayerIndex(i.ToString());
        role_show.SetLayerWeight(_ani_layer_index,1);
        for(int a = 0;a<role_show.layerCount;a++){
            if(_ani_layer_index!=a){
                role_show.SetLayerWeight(a,0);
            }
        }
        role_show.SetLayerWeight(_ani_layer_index,1);
        role_show.Play(Role.Actions[0],_ani_layer_index);
	}
	void hide(int i){
		UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("Image/RegisterUI/Role/Koongya_"+(i*2), typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
		roles[i].GetComponent<UnityEngine.UI.Image>().sprite = sprite;
		roles_sel[i].gameObject.SetActive(false);
	}
	void InitBtn(){
		for (int i = 0;i<roles.Length;i++)
		{
			int temp_i = i;
			UnityEngine.UI.Button btn = roles[i];
			btn.onClick.AddListener(()=>{	
				OnSelect(temp_i);
			});
		}
	}
	
	// Use this for initialization
	void Start () {
		InitBtn();
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
		BACK1.onClick.AddListener(onBack1);
		NEXT1.onClick.AddListener(onNext1);
		BACK2.onClick.AddListener(onBack2);
		NEXT2.onClick.AddListener(onNext2);
		FlashLine();
		InitDefaultProperty();
	}
	void onNext1(){
		selectRoleUI.gameObject.SetActive(false);
		infoUI.gameObject.SetActive(true);
	}
	void onBack1(){
		SwitchScene.NextScene("Login");
	}
	void onNext2(){
		BACK2.enabled = false;
		NEXT2.enabled = false;
		onRegister();
	}
	void onBack2(){
		selectRoleUI.gameObject.SetActive(true);
		infoUI.gameObject.SetActive(false);
	}
	void onRegister(){
		if(account.text.Equals("") || password.text.Equals("")||userName.text.Equals(""))
		{
			ErrorInfo.CreateUI("你是否输入的信息有问题呢");
		}
		else
		{
			if(account.text.Length<2)
			{
				ErrorInfo.CreateUI("账号少于两个字符");
			}else if(userName.text.Length<2)
			{
				ErrorInfo.CreateUI("角色名少于两个字符");
			}else if(password.text.Length<2)
			{	
				ErrorInfo.CreateUI("密码少于两个字符");
			}else
			{
				Register.In(account.text,password.text,userName.text,role_type,tra_rate,phy_str_rate,exp_rate,(status,error) =>{
					if(!status)
						ErrorInfo.CreateUI(error,()=>{
							account.text = "";
							password.text = "";
							BACK2.enabled = true;
							NEXT2.enabled = true;
						});
					else
					{
						PlayerPrefs.SetString("user.account",account.text);
						PlayerPrefs.SetString("user.password",password.text);
						ErrorInfo.CreateUI("注册成功",()=>{
							SwitchScene.NextScene("Login");
						});
					}
				});
			}	
		}
	}
	void Bt1Up(){
		ChangeTra(true);
		FreshDefaultProperty();
	}
	void Bt1Down(){
		ChangeTra(false);
		FreshDefaultProperty();
	}
	void Bt2Up(){
		ChangePhy(true);
		FreshDefaultProperty();
	}
	void Bt2Down(){
		ChangePhy(false);
		FreshDefaultProperty();
	}
	void Bt3Up(){
		ChangeExp(true);
		FreshDefaultProperty();
	}
	void Bt3Down(){
		ChangeExp(false);
		FreshDefaultProperty();
	}
	void ChangeTra(bool up){
		if((rate1>0&&!up)||rate1<5&&up)
		{
			rate1 = rate1+ChangeNumber(up);
			FlashLine();
		}
	}
	void ChangePhy(bool up){
		if((rate2>0&&!up)||rate2<5&&up)
		{
			rate2 = rate2+ChangeNumber(up);
			FlashLine();
		}
	}
	void ChangeExp(bool up){
		if((rate3>0&&!up)||rate3<5&&up)
		{
			rate3 = rate3+ChangeNumber(up);
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
		line1_rect.sizeDelta = new Vector2((rate1/5.0f)*line1_w,line1_rect.rect.height);
		RectTransform line2_rect = line2.gameObject.GetComponent<RectTransform>();
		line2_rect.sizeDelta = new Vector2((rate2/5.0f)*line2_w,line2_rect.rect.height);
		RectTransform line3_rect = line3.gameObject.GetComponent<RectTransform>();
		line3_rect.sizeDelta = new Vector2((rate3/5.0f)*line3_w,line3_rect.rect.height);
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
}
