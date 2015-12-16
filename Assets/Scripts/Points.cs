using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class Points : MonoBehaviour {

	public int points = 0;
	public string login;
	public TextMesh marcador;
	public  StateGame stateGame;
	public User userLoged;
	bool record = true;

	// Use this for initialization
	void Start () {
		stateGame =  StateGame.stateGame;
		userLoged = stateGame.userLoged;
		NotificationCenter.DefaultCenter().AddObserver(this, "Increment");
		NotificationCenter.DefaultCenter().AddObserver(this, "GameOver");

	}
	
	// Update is called once per frame
	void Update () {

		if (record) {
		}

/*		if (points > (int)StateGame.stateGame.highScore) {
			GetComponent<AudioSource>().Play();
			record = false;
		}*/
	}

	void Increment(Notification notification) {
		login = stateGame.userLoged.getLogin ();
		int pointsincrease = (int)notification.data;
		points += pointsincrease;
		UpdateScore ();
//		Debug.LogWarning( StateGame.stateGame.usuario);
	}


	void GameOver(Notification noti) {

		List<int> pointsList = stateGame.userLoged.getPoints ();

		if (pointsList.Count < 11 && pointsList.Count > 0) {

			if(pointsList.Count < 10 && !pointsList.Contains(points)) {
				stateGame.userLoged.addPoints (points);
			}else {
				if(!pointsList.Contains(points)) {
					if (pointsList [0] < points) {
						stateGame.userLoged.getPoints ()[0] = points;
					}
				}
			}
			stateGame.userLoged.getPoints ().Sort ();

		} else {
			if(pointsList.Count == 0) {
				stateGame.userLoged.addPoints (points);
				stateGame.userLoged.getPoints ().Sort ();
			}
		}
		stateGame.Save (stateGame.userLoged);
	
		/*if (points > StateGame.stateGame.highScore) {
			StateGame.stateGame.highScore = points;
			StateGame.stateGame.usuario = user;
			StateGame.stateGame.Save();
		}*/
	}
	void UpdateScore() {

		marcador.text = points.ToString();
	}

}
