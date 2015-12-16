using UnityEngine;
using System.Collections;

public class Destructor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D cono) {
		Destroy (cono.gameObject);
		if (cono.tag == "Personaje") {

			NotificationCenter.DefaultCenter().PostNotification(this, "GameOver");

		
		}
	}
}
