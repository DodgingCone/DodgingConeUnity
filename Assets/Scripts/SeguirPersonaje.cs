 using UnityEngine;
using System.Collections;

public class SeguirPersonaje : MonoBehaviour {

	public Transform Personaje;
	public float separacion = 3f;
	bool stop = false;

	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "GameOver");
		NotificationCenter.DefaultCenter().AddObserver(this, "FlechaRoja");
		NotificationCenter.DefaultCenter().AddObserver(this, "FlechaAzul");
	}
	// Update is called once per frame
	void Update () {
		if (!stop) {
			transform.position = new Vector3 (Personaje.position.x + separacion, transform.position.y, transform.position.z);
		} else {
			transform.position = transform.position;
		}

	}

	void FlechaRoja () {
		separacion --;
	}

	void FlechaAzul(){
		if (separacion < 9) {
			separacion  ++;
		}
	}

	void GameOver(){
		stop = true;
	}

}
