using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AlarmClock : MonoBehaviour {
	//public RawImage Pan;
	//public RawImage Egg;
	public RawImage dot;
	public GameObject KeyHitText;
	public GameObject SequenceText;
	public GameObject CompletedText;
	public GameObject SnoozeText;
	public GameObject AlarmOffText;
	public GameObject InsultText;
	public AnimationClip blink;
	public AudioSource AlarmSound;
	public GameObject Forever;
	//Animator EggA;
	string sequence = "s1";
	float currenttime;
	string slot1;
	string slot2;
	string prettyname;
	bool AlarmOff = false;
	bool Done=false;
	int TimesErrored=0;
	int PreviousTimesErrored=0;
	string LastLastHitKey;
	string LastHitKey;
	string CapitalLastHitKey;
	string CurrentKey;
	string CapitalCurrentKey;
	int RepeatCount=0;
	KeyCode ConvertedLastKeyHit;
	KeyCode ConvertedCurrentKey;
	string FormedString;
	bool SequenceEdited;
	string CurrentKeyLocked;
	int TasksDone=0;
	//	string ktest="K";
	// Use this for initialization
	void Start () {
		Forever = GameObject.Find ("Forever");
		LastHitKey = "";
		CurrentKey = "";
		CurrentKeyLocked = "";
		//CompletedText.GetComponent<Text>().text = "";
		InsultText.GetComponent<Text>().text="";
		//EggA = this.GetComponent<Animator>();
		AlarmOffText.SetActive (false);
	}
	
	// Update is called once per frame
	
	void Update () 
	{	if ((Time.time > currenttime) && currenttime != 0) {
			SnoozeText.SetActive(false);
			AlarmSound.enabled = true;
			CompletedText.SetActive(true);
	}
		if (Time.time > (currenttime + 2) && !AlarmOff) {
			CompletedText.SetActive (false);
	}
		bool skippy = false;
		if(Input.GetButtonDown("LeftArrowAlias")){
			CurrentKeyLocked="(";
		}
		if(Input.GetButtonDown("RightArrowAlias")){
			CurrentKeyLocked=")";
			skippy=true;
		}
		if(Input.GetButtonDown("UpArrowAlias")){
			CurrentKeyLocked="+";
			skippy=true;
		}
		if(Input.GetButtonDown("DownArrowAlias")){
			CurrentKeyLocked="-";
			skippy=true;
		}
		if (Input.inputString != ""&&(!skippy)) {
			CurrentKeyLocked = Input.inputString; //Set Current Key Locked. Will not set "nothing being hit"
			//Debug.Log(Input.inputString);
		}
		
		
		// 
		//(Input.GetKeyDown(ConvertedKey))
		
		CurrentKey = Input.inputString;
		
		//Work arounds for limitations of Input.inputstring to only express ascii characters
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			CurrentKey="(";}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			CurrentKey=")";}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			CurrentKey = "+";
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			CurrentKey = "-";
		}
		if(Input.GetKeyDown(KeyCode.S)){
			currenttime=Time.time+6;
		}
		//End work arounds
		
		if ((CurrentKey != LastHitKey) && (CurrentKey != "")) { //If a new key has been pressed. And a button is in fact being pressed
			LastHitKey = CurrentKey;
		}
		if (LastLastHitKey != LastHitKey) {
			LastLastHitKey = LastHitKey;
			RepeatCount = 0;
		}
		
		if ((CurrentKey == LastHitKey) && (true)) {
			RepeatCount++;
		}
		if (LastHitKey == "") { //If nothing has been pressed, no repeats. This is only valid at the start.
			RepeatCount = 0;		
		}
		KeyHitText.GetComponent<Text> ().text = "Last hit key was: " + LastHitKey + " " + RepeatCount + " Times.";
		StringMaker ();
		ErrorCheck ();
		
		TestSequence (ref sequence);
		
		//Debug.Log ("CurrentKey: " + CurrentKey+"Last Key Hit: " +LastHitKey);
		
	} //End Update
	
	void StringMaker(){ //Unused right now
		FormedString = FormedString + Input.inputString;
	}
	void ToEnglish(string slot1){ //Let's us make a pretty name for the hacky ( ) we use for left and right arrow
		if (slot1 == "(") {
			prettyname = "Left Arrow";
		} else if (slot1 == ")") {
			prettyname = "Right Arrow";
		} else if (slot1 == "+") {
			prettyname = "Up Arrow";
		} else if (slot1 == "-") {
			prettyname = "Down Arrow";
		} else if (slot1 == " ") {
			prettyname = "Spacebar";
		} else if (slot1 == "?") {
			prettyname = "Left Shift";
		} else {
			prettyname = slot1;
		}
		
	}
	void TestSequence(ref string InputSequence){
		
		if (InputSequence != "") {//Don't do this to an empty string.
			slot1 = InputSequence [0].ToString ();
			int PressTimes=InputSequence[1]-'0'; //clever trick to make a string an int.
			slot2 = InputSequence [1].ToString ();
			ToEnglish(slot1);//Make prettyname actually pretty
			SequenceText.GetComponent<Text>().text="Hit: " + prettyname + " To Turn Off The Alarm!";
			string RepeatCountString = RepeatCount.ToString ();
			
			if ((LastHitKey == slot1) && (RepeatCountString == slot2)) {
				
				//
				//Debug.Log ("Typed successfully");
				if(slot1=="*"){ //Move pan up when appropriate
					//Pan.transform.position=Pan.transform.position + new Vector3(0,30,0);
					//Egg.transform.position=Egg.transform.position + new Vector3(0,23,0);
				}
				if(slot1=="-"){
					//Pan.transform.position=Pan.transform.position + new Vector3(0,-30,0);
					//Egg.transform.position=Egg.transform.position + new Vector3(0,-15,0);
				}
				InputSequence = InputSequence.Remove (0, 2);
				SequenceEdited=true;
				CurrentKeyLocked="";
			}
			
			//Debug.Log (InputSequence);
		}
		if ((CurrentKeyLocked != slot1)&&(CurrentKeyLocked!="")&&!SequenceEdited) { //If you mismatched, and you are hitting something, and the sequence isn't about to change
			//	Debug.Log ("CurrentKeyLocked = " + CurrentKeyLocked + "slot1=" +slot1);			
			Debug.Log ("Wrong key hit.");
			TimesErrored++;
		}
		if ((InputSequence == "")&&!Done) { //Reached the end of a sequence
			Debug.Log ("Done with a sequence..");
			TasksDone++;
			TaskSwitcher (TasksDone);
			
		}
		SequenceEdited = false;
		if (Done) {
			Debug.Log ("Safe to exit Scene.");
			ForeverScript other=	Forever.GetComponent<ForeverScript>();
			other.LoadScene("Alarm");
		}
	}
	
	void TaskSwitcher(int TasksDone){

		if (TasksDone == 1) {
			SnoozeText.SetActive(true);
			AlarmSound.enabled = false;
			currenttime=Time.time+3;
			sequence = "s1";
			
		}
		if (TasksDone == 2) {
			sequence = " 1";
		}
		if (TasksDone == 3) {
			AlarmOffText.SetActive(true);
			CompletedText.SetActive(true);
			AlarmSound.enabled = false;
			AlarmOff = true;
			currenttime=0;
			sequence="x9";
			Done=true;
		}
		if (TasksDone == 4) {
			Done = true;

		}
		
	}
	void ErrorCheck(){
		if ((TimesErrored != PreviousTimesErrored)) {
			PreviousTimesErrored = TimesErrored;
			InsultText.GetComponent<Text> ().text = "Try Again!";
		} else {
			InsultText.GetComponent<Text> ().text = "";
		}
	}
}
