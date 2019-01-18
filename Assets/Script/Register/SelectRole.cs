using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRole : MonoBehaviour {
	public UnityEngine.UI.Button role1;
	public SpriteUtils role_show;
	public AudioSource audio_source;
	private int [] flag = {1,0,0,0,0,0,0,0,0,0,0,0};
	// Use this for initialization
	void Start () {
		role1.onClick.AddListener(OnSelect1);
	}
	void OnSelect1(){
		show1();
		playAudio("7");
	}
	void playAudio(string index){
		UnityEngine.AudioClip clip =(UnityEngine.AudioClip)Resources.Load("Sound/AvatarSelect/"+index, typeof(UnityEngine.AudioClip));
		audio_source.clip = clip;
		audio_source.Play();
	}
	void show1(){
		UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("Image/RegisterUI/Role/Koongya_15", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
		role1.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
		flag[0] = 1;
		role_show.SetRoleType("BanGye");
		role_show.SetExp(0);
	}
	void hide1(){
		UnityEngine.Sprite sprite  = UnityEngine.Resources.Load("Image/RegisterUI/Role/Koongya_14", typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
		role1.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
		flag[0] = 0;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
