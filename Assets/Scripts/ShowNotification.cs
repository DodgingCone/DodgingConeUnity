using UnityEngine;
using System.Collections;

public class ShowNotification : MonoBehaviour {

	public TextMesh message;
	void Start () {
		NotificationCenter.DefaultCenter ().AddObserver (this, "sendMessage");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void sendMessage(Notification noti){
		message.text = noti.data.ToString ();
		Invoke ("deleteMessage", 5f);
	}

	public void deleteMessage(){
		message.text = "";
	}
}
