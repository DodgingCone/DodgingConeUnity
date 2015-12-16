using UnityEngine;
using System.Collections;

public class ConoPunto : MonoBehaviour {
	public GameObject ScreenGameOver;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D Pelota) {


		if (Pelota.gameObject.name.Equals ("Personaje")) {

			NotificationCenter.DefaultCenter ().PostNotification (this, "Increment", 1);
		} else {
			if(Pelota.gameObject.name.Equals ("triangle(Clone)")) {
				Destroy(gameObject);
			}
		}

	}
}
