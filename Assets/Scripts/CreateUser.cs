using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class CreateUser : MonoBehaviour {

	private User user;
	private string login = "";
	private string gender = "";
	private string country= "";
	GUIStyle title = new GUIStyle();




	// Use this for initialization
	void Start () {
		user = new User ();
		title.fontSize = 40;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnGUI(){
		GUIStyle style = new GUIStyle ();
		style.fontSize = 40;
		style.normal.textColor = Color.white;
		style.normal.background = MakeTex(600, 1, new Color(3.0f, 3.0f, 4.0f, 0.4f));


		login = (GUI.TextField (new Rect (Screen.width * 50/100 , Screen.height  * 26/100 , Screen.height * 50/100,Screen.height* 10/100),login, style));
		gender = (GUI.TextField (new Rect (Screen.width * 50/100 , Screen.height  * 38/100 , Screen.height * 50/100,Screen.height* 10/100),gender, style));
		country = (GUI.TextField (new Rect (Screen.width * 50/100 , Screen.height  * 50/100 , Screen.height * 50/100,Screen.height* 10/100),country, style));

		if (GUI.Button (new Rect (Screen.width * 30/100 , Screen.height  * 65/100 , Screen.height * 50/100,Screen.height* 10/100), "Save")) {
			if(!login.Equals("")) {
					if(!StateGame.stateGame.isExistsUserByLogin(login)) {
						user.setLogin(login);
						user.setGender(gender);
						user.setCountry(country);
						NotificationCenter.DefaultCenter().PostNotification(this, "saveUser", user);
						Invoke("saveUser", 1f);
					}else {
						NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "Username already registered");
					}


			}else {
				NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "You must complete all fields!!!!");
				Debug.LogWarning("no guarda");
			}
		}

	}

	void saveUser(){

		Application.LoadLevel ("PortadaScene");
	}

	public Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width*height];
		
		for(int i = 0; i < pix.Length; i++)
			pix[i] = col;
		
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		
		return result;
	}
}
