using UnityEngine;
using System.Collections;

public class Generador : MonoBehaviour {

	public GameObject obj;
	public float timeMin = 2f;
	public float timeMax = 6f;
	bool start = true;


	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x, transform.position.y,0f);
		NotificationCenter.DefaultCenter().AddObserver(this, "Move");
		NotificationCenter.DefaultCenter().AddObserver(this, "GameOver");
	}

	void Move(Notification notification)  {
		Generar();
	}

	// Update is called once per frame
	void Update () {

	}

	void Generar() {
		if (start) {
			timeMin = Random.Range (2, 5);
			Instantiate (obj, transform.position, Quaternion.identity);
			Invoke ("Generar", Random.Range(timeMin, timeMax));
		}
	}

	void GameOver() {
		start = false;
	}

}
