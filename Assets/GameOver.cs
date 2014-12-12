using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameOver : MonoBehaviour {
	public GameObject SecondsText;
	public GameObject GameOverText;
	public GameObject Forever;
	public float totaltime=0;
	int totaltimeint=0;
	// Use this for initialization
	void Start () {
		//JACK COPY AND PASTE THE LINE BELOW.
		Forever = GameObject.Find ("Forever");
		ForeverScript other = Forever.GetComponent<ForeverScript> ();
		totaltime = Time.time - other.starttime;
		totaltimeint = Mathf.RoundToInt (totaltime);
		SecondsText.GetComponent<Text>().text="You completed your day in " +totaltimeint+ " seconds.";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			ForeverScript other = Forever.GetComponent<ForeverScript> ();
			other.firstpress=true;
			Application.LoadLevel(0);

				}

	//if Done
	}

}
