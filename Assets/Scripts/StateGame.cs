using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ListRecords = System.Collections.Generic.KeyValuePair<int, string>;


public class StateGame : MonoBehaviour {


	public static StateGame stateGame;
	private String pathFile;
	public  User userLoged;
	private User user;





	void Awake(){
		pathFile = Application.persistentDataPath + "/date.xml";
		if (stateGame == null) {
			stateGame = this;
			DontDestroyOnLoad (gameObject);
		} else if (stateGame != this) {
			Destroy(gameObject);
		}
	}



	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter ().AddObserver (this, "selectUser");
		NotificationCenter.DefaultCenter ().AddObserver (this, "saveUser");
		Load ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void selectUser(Notification noti) {
		this.userLoged = this.getUserByLogin (noti.data.ToString ());
	}
	public void Load() {
	}

	public void Save(User u) {
		List<User> list = getXmlFile();
		for (int i = 0; i < list.Count; i++) {
			if(list[i].getLogin().Equals(u.getLogin())){
				list[i] = u;
			}
		}
		saveXmlFile (list);

	}

	void saveUser(Notification noti) {
		user = (User)noti.data;
		Invoke ("saveUserFile", 1f);

	}

	public void saveUserFile(){

		if (File.Exists (pathFile)) {
			List <User> list = getXmlFile();
			list.Add(user);
			saveXmlFile(list);
		} else {
			List<User> list = new List<User>();
			list.Add(user);
			saveXmlFile(list);
		}
	}

	public void saveXmlFile(List<User> list) {
		TextWriter write = new StreamWriter(pathFile);
		XmlSerializer serial= new XmlSerializer(typeof(List<User>));
		serial.Serialize(write, list);
		write.Close();
	}

	public List<User> getXmlFile(){
		if (File.Exists (pathFile)) {
			TextReader reader = new StreamReader (pathFile);
			XmlSerializer serial = new XmlSerializer (typeof(List<User>));
			List<User> list = (List<User>)serial.Deserialize (reader);
			reader.Close ();
			return list;

		} else {
			List<User> list = new List<User>();
			return list;

		}

	}

	public bool isExistsUserByLogin(String login){
		if (File.Exists (pathFile)) {
			List<User> list = getXmlFile();
			for (int i = 0; i < list.Count; i++) {
				if(list[i].getLogin().Equals(login)){
					return true;
				}
			}
		}

		return false;
	}

	public User getUserByLogin(string login){
		List<User> list = getXmlFile();
		for (int i = 0; i < list.Count; i++) {
			if(list[i].getLogin().Equals(login)){
				this.userLoged = list[i];
				return list[i];
			}
		}

		return null;
	}

	public List<ListRecords> getListRecords(){
		List<ListRecords> listRecords = new List<ListRecords>();
		List<User> list = getXmlFile();
		foreach(User u in list){
			for(int i = 0 ; i < u.getPoints().Count ; i++) {
				ListRecords l = new ListRecords(u.getPoints()[i], u.getLogin());
				listRecords.Add(l);
			} 
		}
		listRecords. Sort (
			delegate(ListRecords p1, ListRecords p2) {
			return p1.point.CompareTo(p2.point);
			}
			);
		return listRecords;

	}

	public struct ListRecords 
	{
		public ListRecords(int intValue, string strValue)
		{
			point = intValue;
			nameUser = strValue;
		}
		
		public int point { get; private set; }
		public string nameUser { get; private set; }

	}

}

[Serializable]
public class User  {

	public string login;
	public string country;
	public string gender;
	public bool online;
	public string pass;
	public List<int> points;
	
	
	public User(){
		this.login = "";
		this.country = "";
		this.gender = "";
		this.online = false;
		this.pass = "";
		points = new List<int> ();
		
	}
	
	public void setLogin(string login){
		this.login = login;
	}
	public string getLogin() {
		return this.login;
	}
	public void setCountry(string country) {
		this.country = country;
	}
	public string getCountry(){
		return this.country;
	}
	public void setGender(string gender){
		this.gender = gender;
	}
	public string getGender(){
		return this.gender;
	}
	public void setOnline(bool online){
		this.online = online;
	}
	public bool getOnline(){
		return this.online;
	}
	public void setPass(string pass){
		this.pass = pass;
	}
	public string getPass(){
		return this.pass;
	}
	public void addPoints(int point) {
		if (this.points != null) {
			this.points.Add (point);
		} else {
			points = new List<int> ();
			this.points.Add (point);
		}
	}
	public List<int> getPoints(){
		if (this.points != null) {
			return this.points;
		}
		return new List<int> ();
	}
	
	
}














