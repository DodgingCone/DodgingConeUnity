using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	bool start;
	float speed =0f;
	float time = 0.0000000001f;
	bool move= true;

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "ChangeSpeed");
		NotificationCenter.DefaultCenter().AddObserver(this, "GameOver");
		NotificationCenter.DefaultCenter().AddObserver(this, "FlechaRoja");
		NotificationCenter.DefaultCenter().AddObserver(this, "FlechaAzul");

	}
	
	// Update is called once per frame
	void Update () {
		if(/*speed != 0f && */start)
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (Time.time * speed, 0);
	}

	void ChangeSpeed (Notification notification) {
		start = true;
		if (move)
		speed = 0.335f;

	}

	public void speed1() {
		move = true;
	}
	void GameOver() {

		start = false;
	}
	void FlechaRoja() {
		speed = 0f;
		move = false;
		//renderer.material.mainTextureOffset = new Vector2 (Time.time * speed, 0);
		Invoke ("speed1", time);
	}
	void FlechaAzul() {
		speed = 0f;
		move = false;
		//renderer.material.mainTextureOffset = new Vector2 (Time.time * speed, 0);
		Invoke ("speed1", time);
	}
}
