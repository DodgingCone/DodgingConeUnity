using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ListRecords = System.Collections.Generic.KeyValuePair<int, string>;

public class LoadListRecods : MonoBehaviour {

	public GUISkin guiSkin;
	public TextMesh txtName1; 
	public TextMesh txtName2;
	public TextMesh txtName3;
	public TextMesh txtName4;
	public TextMesh txtName5;

	public TextMesh txtPoint1;
	public TextMesh txtPoint2;
	public TextMesh txtPoint3;
	public TextMesh txtPoint4;
	public TextMesh txtPoint5;

	public bool point1Online = false;
	public bool point2Online = false;
	public bool point3Online = false;
	public bool point4Online = false;
	public bool point5Online = false;






	// Use this for initialization
	void Start () {
		int sizeList = StateGame.stateGame.getListRecords ().Count;
		int posRecord = 1;
		while(posRecord < 6 && sizeList > 0) {
			if(posRecord == 1) {
				txtName1.text = StateGame.stateGame.getListRecords ()[sizeList-1].nameUser;
				txtPoint1.text = StateGame.stateGame.getListRecords ()[sizeList-1].point.ToString() + " Points";
				if(StateGame.stateGame.getUserByLogin(txtName1.text).getOnline()){
					point1Online = true;
				}

			}
			if(posRecord == 2) {
				txtName2.text = StateGame.stateGame.getListRecords ()[sizeList-1].nameUser;
				txtPoint2.text = StateGame.stateGame.getListRecords ()[sizeList-1].point.ToString() + " Points";
				if(StateGame.stateGame.getUserByLogin(txtName2.text).getOnline()){
					point2Online = true;
				}
			}
			if(posRecord == 3) {
				txtName3.text = StateGame.stateGame.getListRecords ()[sizeList-1].nameUser;
				txtPoint3.text = StateGame.stateGame.getListRecords ()[sizeList-1].point.ToString() + " Points";
				if(StateGame.stateGame.getUserByLogin(txtName3.text).getOnline()){
					point3Online = true;
				}
			}
			if(posRecord == 4) {
				txtName4.text = StateGame.stateGame.getListRecords ()[sizeList-1].nameUser;
				txtPoint4.text = StateGame.stateGame.getListRecords ()[sizeList-1].point.ToString() + " Points";
				if(StateGame.stateGame.getUserByLogin(txtName4.text).getOnline()){
					point4Online = true;
				}
			}
			if(posRecord == 5) {
				txtName5.text = StateGame.stateGame.getListRecords ()[sizeList-1].nameUser;
				txtPoint5.text = StateGame.stateGame.getListRecords ()[sizeList-1].point.ToString() + " Points";
				if(StateGame.stateGame.getUserByLogin(txtName5.text).getOnline()){
					point5Online = true;
				}
			}
			posRecord++;
			sizeList--;
		}

	}
	void OnGUI() {
		GUIStyle style = new GUIStyle ();
		style.fontSize = 40;
		style.normal.textColor = Color.white;
		style.normal.background = MakeTex(600, 1, new Color(3.0f, 3.0f, 4.0f, 0.4f));

		int sizeList = StateGame.stateGame.getListRecords ().Count;
		if (point1Online) {
			if(GUI.Button (new Rect (Screen.width * 87/100 , Screen.height  * 29/100 , Screen.height * 15/100,Screen.height* 7/100), "Share", this.guiSkin.button)){

				login(txtName1.text, StateGame.stateGame.getListRecords ()[sizeList-1].point.ToString());
			}
		}
		if (point2Online) {
			if(GUI.Button (new Rect (Screen.width * 87/100 , Screen.height  * 37/100 , Screen.height * 15/100,Screen.height* 7/100), "Share", this.guiSkin.button)){
				login(txtName2.text, StateGame.stateGame.getListRecords ()[sizeList-2].point.ToString());
			}
		}
		if (point3Online) {
			if(GUI.Button (new Rect (Screen.width * 87/100 , Screen.height  * 45/100 , Screen.height * 15/100,Screen.height* 7/100), "Share", this.guiSkin.button)){
				login(txtName3.text, StateGame.stateGame.getListRecords ()[sizeList-3].point.ToString());
			}
		}
		if (point4Online) {
			if(GUI.Button (new Rect (Screen.width * 87/100 , Screen.height  * 53/100 , Screen.height * 15/100,Screen.height* 7/100), "Share", this.guiSkin.button)){
				login(txtName4.text, StateGame.stateGame.getListRecords ()[sizeList-4].point.ToString());
			}
		}
		if (point5Online) {
			if(GUI.Button (new Rect (Screen.width * 87/100 , Screen.height  * 61/100 , Screen.height * 15/100,Screen.height* 7/100), "Share", this.guiSkin.button)){
				login(txtName5.text, StateGame.stateGame.getListRecords ()[sizeList-5].point.ToString());
			}
		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void login(string username, string point){

		Debug.LogWarning (point);
		User user = StateGame.stateGame.getUserByLogin (username);
		string pass = user.getPass();
		Hashtable data = new Hashtable();
		data.Add( "username",  username);
		data.Add( "password", pass );

		// Collect headers for upload score
		string token, client, expiry, type, uuid;

		HTTP.Request someRequest = new HTTP.Request("post", "http://boiling-sea-3589.herokuapp.com/api/auth/sign_in", data);

		//HTTP.Request someRequest = new HTTP.Request( "get", "https://www.google.com");
		someRequest.Send( ( request ) => {
			if(request.response != null) {
				token = request.response.GetHeader("Access-Token");
				client = request.response.GetHeader("Client");
				expiry = request.response.GetHeader("Expiry");
				type = request.response.GetHeader("Token-Type");
				uuid = request.response.GetHeader("Uid");

				Debug.LogWarning(request.response.Text);
				Debug.LogWarning(someRequest.isDone);
				if (someRequest.isDone) {
					uploadScore(point, token, client, expiry, type, uuid);
				} else {
					NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "User not found!!!!");
				}
			}
			
		});
	}

	void uploadScore(string point, string token, string client, string expiry, string type, string uuid){
		Hashtable data = new Hashtable();
		data.Add( "score",  point);
		HTTP.Request someRequest = new HTTP.Request( "post", "http://boiling-sea-3589.herokuapp.com/api/scores", data );

		someRequest.SetHeader("Access-Token", token);
		someRequest.SetHeader("Client", client);
		someRequest.SetHeader("Expiry", expiry);
		someRequest.SetHeader("Token-Type", type);
		someRequest.SetHeader("Uid", uuid);

		someRequest.Send( ( request ) => {
			if(request.response != null) {
				Debug.LogWarning(someRequest.isDone);
				if(someRequest.isDone){
					if(!someRequest.response.status.Equals(401)){
						NotificationCenter.DefaultCenter().PostNotification(this, "sendMessage", "Shared!!");
					}else {
						NotificationCenter.DefaultCenter().PostNotification(this, "This score was shared!");
					}
				}

			}
			
		});
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
