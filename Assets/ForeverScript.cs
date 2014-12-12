using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ForeverScript : MonoBehaviour {
	public bool shuffled;
	public List<int> scenes = new List<int>();
	public int level;
	public int randomIndex;
	public GameObject PauseCanvas;
	public GameObject PauseText;
	public bool paused=false;
	public float starttime = 0;
	public bool firstpress=true;
	// Use this for initialization
	void Start () {
	
		scenes = new List<int>(Enumerable.Range(1,11));
	}
	void Awake(){
		DontDestroyOnLoad (this);
	}
	public void PauseGame(){

		if (paused == false) {
						Screen.showCursor = true;
						Time.timeScale = 0;
						PauseCanvas.SetActive (true);
						paused = true;
			AudioListener.pause=true;
				} else if (paused == true) {
						Screen.showCursor = false;
						Time.timeScale = 1;
						PauseCanvas.SetActive (false);
						paused = false;
			AudioListener.pause=false;
				}


	}
	// Update is called once per frame
	void Update () {
	//	Debug.Log (Application.loadedLevel);
	if (Application.loadedLevel == 0) {
						Screen.showCursor = true;
				}
		if(Input.GetKeyDown(KeyCode.Escape)){
			PauseGame();
		}
	}
	public void SetShuffle(bool ShuffleSetting){
				if (ShuffleSetting) {
			shuffled=true;

				} else if (!ShuffleSetting) {
						shuffled = false;
				}
		}

	public void ExitGame(){
		Application.Quit ();
		}
	public void LoadScene(string SceneName){

		if (firstpress) {
						starttime = Time.time;
			firstpress=false;
				}
		if (shuffled) {
			if(scenes.Count==0){
				Application.LoadLevel(12);
				Debug.Log("GAME OVER DUDE.");
			}
			else{
			Debug.Log("Shuffled.");


			randomIndex=Random.Range(0,scenes.Count);
			int level=scenes[randomIndex];
			scenes.RemoveAt(randomIndex);
			shuffled = true;

			Debug.Log ("Shuffler has chosen: " + level);
			Application.LoadLevel(level);

			}
				
		} //End Shuffled 


		else if (!shuffled) {
		//normal stuff.

			//Menu alarm brush egg car raido card computer oven
		Debug.Log("Not shuffled.");
			if(SceneName=="MainMenu"){
				Application.LoadLevel(1);
			}
			if(SceneName=="Alarm"){
				Application.LoadLevel(2);
			}
			if(SceneName=="Brush"){
				Application.LoadLevel(3);
			}
			if(SceneName=="Egg"){
				Application.LoadLevel(4);
			}
			if(SceneName=="Car"){
				Application.LoadLevel(5);
			}
			if(SceneName=="Radio"){
				Application.LoadLevel(6);
			}
			if(SceneName=="Card"){
				Application.LoadLevel(7);
			}
			if(SceneName=="Computer"){
				Application.LoadLevel(8);
			}
			if(SceneName=="Oven"){
				Application.LoadLevel(9);
			}
			if(SceneName=="TV"){
				Application.LoadLevel(10);
			}
			if(SceneName=="PMB"){
				Application.LoadLevel(11);
			}
			if(SceneName=="PMA"){
				Application.LoadLevel (12);
			}
			if(SceneName=="GameOver"){
				Application.LoadLevel (0);
			}
			}
				}
	}

