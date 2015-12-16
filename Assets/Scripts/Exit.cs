using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown() {
		Camera.main.GetComponent<AudioSource>().Stop ();
		GetComponent<AudioSource>().Play ();
		Invoke ("ExitGame", GetComponent<AudioSource>().clip.length);
	}
	void ExitGame() {
		Application.Quit ();
	}
}
