using UnityEngine;
using System.Collections;

public class ControladorPersonaje : MonoBehaviour {

	float constantSpeed = 0.1f;
	public bool moveUp = false;
	public bool moveDown = false;
	public bool start = false;
	public bool build = false;
	bool activeStart = true;
	public Transform Personaje;
	Animator anim ;

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "Rebote");
		NotificationCenter.DefaultCenter().AddObserver(this, "GameOver");
		anim = GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			GetComponent<AudioSource>().Play();
			if(activeStart) {
				start = true;
				if(Application.loadedLevelName.Equals("GeometricSurvivor"))
				anim.SetBool("Active", true);

			}

			if(!build) {
				NotificationCenter.DefaultCenter().PostNotification(this, "Move");
				NotificationCenter.DefaultCenter().PostNotification(this, "ChangeSpeed" );
				build = true;
			}

		}
			

		if (start) {
			NotificationCenter.DefaultCenter().PostNotification(this, "ChangeSpeed", constantSpeed );		
			WaitClick ();	
		}
		if (Personaje.gameObject.transform.position.y > 6f || Personaje.gameObject.transform.position.y < -6f) {
			if(start) {
				NotificationCenter.DefaultCenter().PostNotification(this, "GameOver");
				start= false;
			}

			
		}
	}

	void OnCollisionEnter2D( Collision2D coll) {
		if (coll.gameObject.name.Equals ("Cono(Clone)") || coll.gameObject.name.Equals ("ConoCuad(Clone)") || coll.gameObject.name.Equals ("triangle(Clone)")) {
			NotificationCenter.DefaultCenter().PostNotification(this, "GameOver");
			start= false;

		}

	}



	void WaitClick() {

	if (Input.GetMouseButtonDown (0)) {

		if (!moveUp && !moveDown) {
			MoveforwardAndUp();
			moveUp = true;
			moveDown = false;

			}else {
				if (moveUp) {
					MoveforwardAndDown ();
					moveUp = false;
					moveDown = true;
				} else {
					MoveforwardAndUp();		
					moveUp = true;
					moveDown = false;
				}
			}
		

		}else {
			if(moveUp) {
				MoveforwardAndUp();
			}else {
				MoveforwardAndDown ();
			}
		}



	}

	void Rebote(Notification notification) {
		if (moveUp) {
			MoveforwardAndDown ();
			moveUp = false;
			moveDown = true;
		} else {
			MoveforwardAndUp();		
			moveUp = true;
			moveDown = false;
		}
	}

	void OnTriggerEnter2D(Collider2D objectss) {

		if (objectss.tag == "Cono") {
			NotificationCenter.DefaultCenter().PostNotification(this, "GameOver");

		}
			}

	void MoveforwardAndUp() {
		if(start)
		transform.position = new Vector3 (transform.position.x + constantSpeed, transform.position.y +constantSpeed, transform.position.z);
	}
	void MoveforwardAndDown() {
		if(start)
		transform.position = new Vector3 (transform.position.x + constantSpeed, transform.position.y -constantSpeed, transform.position.z);
	}

	void GameOver() {
		activeStart = false;
	}




}
