using UnityEngine;
using System.Collections;



public class BootonPlay : MonoBehaviour {

	string userName = "";
	string pass= "";
	public   StateGame stateGame;
	public GameObject pos;
	private bool onlineUser= false;
	public GUISkin guiSkin;
	public GameObject textPass;
	Connector connector;
	bool selectUser = true;




	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if(Application.loadedLevelName.Equals ("PortadaScene")) {

			GUIStyle style = new GUIStyle ();
			char charStyle = '*';
			style.fontSize = 40;
			style.normal.textColor = Color.white;
			style.normal.background = MakeTex(600, 1, new Color(3.0f, 3.0f, 4.0f, 0.4f));
			this.guiSkin.customStyles [0].fontSize = Screen.height * 4 / 100;
			this.guiSkin.customStyles [0].padding.left = Screen.height * 5 / 100;
			userName = GUI.TextField (new Rect(Screen.width * 35/100 , Screen.height  * 55/100 , Screen.height * 50/100,Screen.height* 10/100), userName, style);
			onlineUser = GUI.Toggle(new Rect(Screen.width * 70/100 , Screen.height  * 70/100 ,  Screen.height * 5/100,Screen.height* 5/100), onlineUser, "Online User", this.guiSkin.customStyles[0]);;
			if(onlineUser){
				pass = GUI.PasswordField (new Rect(Screen.width * 35/100 , Screen.height  * 67/100 , Screen.height * 50/100,Screen.height* 10/100),pass	 ,charStyle , style);
				textPass.SetActive(true);
			}else {
				textPass.SetActive(false);
			}


		}
	}


	void OnMouseDown() {
	
		GetComponent<AudioSource>().Play ();
		if (Application.loadedLevelName.Equals ("PortadaScene") && !userName.Equals("")) {
			if(onlineUser){
				User userOnline = stateGame.getUserByLogin (userName);
				if(userOnline == null){
					Hashtable data = new Hashtable();
					data.Add( "username",  userName);
					data.Add( "password", pass );
					//192.168.0.104
					HTTP.Request someRequest = new HTTP.Request( "post", "https://boiling-sea-3589.herokuapp.com/api/auth/sign_in", data );

					//HTTP.Request someRequest = new HTTP.Request( "get", "https://www.google.com");
					someRequest.Send( ( request ) => {
						if(request.response != null) {
							Debug.LogWarning(request.response.GetHeader("Access-Token"));
							Debug.LogWarning(request.response.GetHeader("Client"));
							Debug.LogWarning(request.response.GetHeader("Expiry"));
							Debug.LogWarning(request.response.GetHeader("Token-Type"));
							Debug.LogWarning(request.response.GetHeader("Uid"));
							Debug.LogWarning(request.response.Text	);
							Debug.LogWarning(someRequest.isDone);
							if(request.response.status.Equals(200)){
								saveUserLocally();
								NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "Saving User, one moment please...");
								Invoke("LoadScene", 3f);

							}else {
								NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "User not found!!!!");
							}
						}
						
					});
		
				}else{
					userOnline = stateGame.getUserByLogin (userName);
					if(userOnline != null){
						if (userOnline.getOnline()) {
							if(userOnline.getPass().Equals(pass)){
								NotificationCenter.DefaultCenter().PostNotification(this, "selectUser", userName);
								Invoke ("LoadScene", GetComponent<AudioSource> ().clip.length);
							}else{
								NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "Password incorrect !!!!");
							}
						}else {
							NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "This user is not online !!!!");
						}
					}else {
						NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "User not registered !!!!");
					}
				}

			}else {
				User userOffLine = stateGame.getUserByLogin (userName);
				if (userOffLine != null) {
					if(!userOffLine.getOnline()) {
	
						Invoke ("LoadScene", GetComponent<AudioSource> ().clip.length);
					}else {
						NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "User registered Online !!!!");
					}
				}else{
					NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "User not registered !!!!");
				}
			}

		} else {
			if(Application.loadedLevelName.Equals ("PortadaScene")) {
				NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "Fields must be completed");
			}else {
				selectUser = false;
				Invoke ("LoadScene", GetComponent<AudioSource> ().clip.length);

			}
		}
	}

	void LoadScene() {
		if (selectUser) {
			NotificationCenter.DefaultCenter().PostNotification(this, "selectUser", userName);
		}
		selectUser = true;
		NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "Done!!!");
		Application.LoadLevel("GeometricSurvivor");
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

	public void saveUserLocally(){
		User newUser = new User ();
		newUser.setLogin (userName);
		newUser.setPass (pass);
		newUser.setOnline (true);
		//add more atributes

		NotificationCenter.DefaultCenter().PostNotification(this, "saveUser", newUser);

	}


}
