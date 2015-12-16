using UnityEngine;
using System.Collections;

public class UrlSignUp : MonoBehaviour {

	//PublicTextMesh txtSignUp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Application.OpenURL("https://boiling-sea-3589.herokuapp.com");
	}
}
