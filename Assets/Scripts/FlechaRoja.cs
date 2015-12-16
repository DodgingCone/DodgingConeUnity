using UnityEngine;
using System.Collections;

public class FlechaRoja : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D objectss) {

		if (objectss.gameObject.name.Equals("Personaje")) {
			NotificationCenter.DefaultCenter().PostNotification(this, "FlechaRoja");
			NotificationCenter.DefaultCenter().PostNotification(this, "Increment", 2);
			Destroy(gameObject);
		}
	}


}
