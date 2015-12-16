using UnityEngine;
using System.Collections;

public class Limite : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D pelota) {
		if(pelota.gameObject.name.Equals("Personaje"))
		NotificationCenter.DefaultCenter().PostNotification(this, "Rebote");
	}

}
