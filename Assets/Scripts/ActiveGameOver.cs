using UnityEngine;
using System.Collections;

public class ActiveGameOver : MonoBehaviour {


	public GameObject ScreenGameOver;


	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "GameOver");
	}

	void GameOver(Notification noti){
		if(ScreenGameOver != null)
		ScreenGameOver.SetActive (true);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
