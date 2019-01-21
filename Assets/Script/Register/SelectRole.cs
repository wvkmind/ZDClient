using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRole : MonoBehaviour {
	public UnityEngine.UI.Button role1;
	public SpriteUtils role_show;
	public AudioSource audio_source;
	private int [] flag = {0,0,0,0,0,0,0,0,0,0,0,0};
	private int role_type = 7;
	// Use this for initialization
	public int RoleType(){
		return role_type;
	}
	void Start () {
		role1.onClick.AddListener(OnSelect1);
	}
	void OnSelect1(){
		role_type = 7;
		show7();
		playAudio("7");
	}
	void playAudio(string index){
		UnityEngine.AudioClip clip =(UnityEngine.AudioClip)Resources.Load("Sound/AvatarSelect/"+index, typeof(UnityEngine.AudioClip));
		audio_source.clip = clip;
		audio_source.Play();
	}
	void show7(){
		UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("Image/RegisterUI/Role/Koongya_15", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
		role1.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
		flag[7] = 1;
		role_show.SetRoleType("BanGye");
		role_show.SetExp(0);
	}
	void hide7(){
		UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("Image/RegisterUI/Role/Koongya_14", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
		role1.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
		flag[7] = 0;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
