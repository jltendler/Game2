using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Radio : MonoBehaviour {
	public GameObject KeyHitText;
	public GameObject SequenceText;
	public GameObject CompletedText;
	public GameObject InsultText;
	public GameObject SnoozeText;
	public GameObject SnoozePanel;
	public GameObject Speaker;
	public AudioClip GNR, Country, Dub, Polka, Scream, Static, CW;
	string sequence = "11";
	float currenttime;
	string slot1;
	string slot2;
	string prettyname;
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
	//Song List: (OFF), 1:Top Hits, 2:Classic Rock, 3:Polka/Folk/Spanish, 4:Screamo, 5:Dubstep Drop, 6: Careless Whisper
	//Jam Out = Turn dial = increase volume

	
	// Use this for initialization
	void Start () {
		LastHitKey = "";
		CurrentKey = "";
		CurrentKeyLocked = "";
		InsultText.GetComponent<Text>().text="";
	}
	
	// Update is called once per frame
	
	void Update () 
	{	if ((Time.time > currenttime)&&!Done) {
			CompletedText.SetActive(false);
			SnoozePanel.SetActive(false);
			currenttime = 0;
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
		}
		
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
		//	SequenceText.GetComponent<Text>().text="Hit: " + prettyname + " To Change Radio Station!";

			if(TasksDone==0){SequenceText.GetComponent<Text>().text="Let's turn on the radio. Hit: " + prettyname + " To Select a station.";
				Debug.Log("First time.");}
			string RepeatCountString = RepeatCount.ToString ();
			
			if ((LastHitKey == slot1) && (RepeatCountString == slot2)) {
				
				InputSequence = InputSequence.Remove (0, 2);
				SequenceEdited=true;
				CurrentKeyLocked="";
			}
		}
		if ((CurrentKeyLocked != slot1)&&(CurrentKeyLocked!="")&&!SequenceEdited) { //If you mismatched, and you are hitting something, and the sequence isn't about to change
			Debug.Log ("Wrong key hit.");
			TimesErrored++;
		}
		if ((InputSequence == "")&&!Done) { //Reached the end of a sequence
			Debug.Log ("Done with a sequence...");
			TasksDone++;
			TaskSwitcher (TasksDone);
			
		}
		SequenceEdited = false;
		if ((Done)&&(Time.time>currenttime)) {
			Debug.Log ("Safe to exit Scene.");
			//Call to next scene.

		}
	}
	
	void TaskSwitcher(int TasksDone){
		
		if (TasksDone == 1) {
			//Classic Rock
			sequence = "21";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Classic Rock!";
			SequenceText.GetComponent<Text>().text="Good Song! But I'm not feeling it. Hit: " + sequence[0] + " To Change Radio Station!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			
		}
		if (TasksDone == 2) {
			sequence = "31";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Exotic Tunes!";
			SequenceText.GetComponent<Text>().text="Such an ex. Hit: " + sequence[0] + " To Change Radio Station!";
			//Polka,Folk,Spanish Radio
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
		
		}
		if (TasksDone == 3) {
			//Screamo
			sequence="41";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="RAWRRRRRRRRR";
			SequenceText.GetComponent<Text>().text="Woah! I'm really not feeling it. Hit: " + sequence[0] + " To Change Radio Station!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
		
		}
		if (TasksDone == 4) {
			//Dubstep
			sequence="51";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="DDDDDDDROP THE BASS!";
			SequenceText.GetComponent<Text>().text="A little too much bass for me. Hit: " + sequence[0] + " To Change Radio Station!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
		
		}
		if (TasksDone == 5) {
			//Country
			sequence="61";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="BLUEGRASS!";
			SequenceText.GetComponent<Text>().text="Preset 6 is always good! Hit: " + sequence[0] + " to tune in!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;

		}
		if (TasksDone == 6) {
			//Careless Whisper
			sequence="+1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Smooth Jazzzzzzzz!";
			SequenceText.GetComponent<Text>().text="Awww Yeah! Hit: " + "Up Arrow" + " To Increase the Volume!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			
		}
		if (TasksDone == 7) {
			//Careless Whisper
			sequence="+1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Volume Up!";
			SequenceText.GetComponent<Text>().text="Still can't hear it! Hit: " + "Up Arrow" + " To turn up the volume again!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			LastHitKey="";
			CurrentKeyLocked="";
			
		}
		if (TasksDone == 8) {
			sequence="+1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Volume Max!";
			Debug.Log ("Found Your Station");

			currenttime=Time.time+5;

		}
		if (TasksDone == 9) {
			currenttime=(Time.time+5);
			Done=true;
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

