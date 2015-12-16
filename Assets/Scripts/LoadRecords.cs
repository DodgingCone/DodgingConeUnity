using UnityEngine;
using System.Collections;

public class LoadRecords : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		GetComponent<AudioSource>().Play ();
		Invoke ("LoadScene", GetComponent<AudioSource> ().clip.length);
	}
	void LoadScene() {
		Application.LoadLevel ("Records");
	}
}
