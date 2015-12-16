using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour {

	public TextMesh point;
	public TextMesh record;
	public TextMesh nameUser;

	public Points score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		point.text = score.points.ToString();
		record.text = StateGame.stateGame.getListRecords () [StateGame.stateGame.getListRecords ().Count - 1].point.ToString();
		nameUser.text = StateGame.stateGame.getListRecords () [StateGame.stateGame.getListRecords ().Count - 1].nameUser;
	}

}
