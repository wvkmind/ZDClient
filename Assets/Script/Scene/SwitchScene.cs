using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour {

	private static string next_scene = null;
	private AsyncOperation async = null;
	public UnityEngine.UI.Image image; 
	public UnityEngine.UI.Image loading; 
	private static float timer = 0;
	public static SwitchScene instance = null;
	public static void NextScene(string str){
		next_scene = str;
		instance.gameObject.SetActive(true);
	}
	void Start() {
		if(instance == null)
			instance = this;
		DontDestroyOnLoad(this);
		instance.gameObject.SetActive(false);
	}
	IEnumerator LoadScene()
    {
        async =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(next_scene);
        yield return async;
    }
	void Update() {
		if(next_scene!=null&&async==null)
		{
			image.gameObject.SetActive(true);
			loading.gameObject.SetActive(true);
			timer += Time.deltaTime;
			if(timer>=2){
				timer = 0;
				StartCoroutine("LoadScene");
				next_scene = null;
			}
		}
		else if (async != null && async.isDone == true)
		{
			async = null;
			instance.gameObject.SetActive(false);
		}
	}
}
