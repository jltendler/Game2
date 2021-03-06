﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ResetAlarmClock : MonoBehaviour {
	public GameObject KeyHitText;
	public GameObject SequenceText;
	public GameObject CompletedText;
	public GameObject InsultText;
	public GameObject SnoozeText;
	public GameObject SnoozePanel;
	public GameObject AlarmClock;
	public GameObject AlarmClock7;
	public GameObject AlarmClock71;
	public GameObject AlarmClock715;
	public GameObject Forever;
	string sequence = "a1";
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
	
	// Use this for initialization
	void Start () {
		//JACK COPY AND PASTE THE LINE BELOW.
		Forever = GameObject.Find ("Forever");
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
		
		if (TasksDone == 1) {
			SequenceText.GetComponent<Text> ().text = "Set the alarm for 7AM! Hit: " + sequence[0] + " To Set the Hour!";
		}
		if (TasksDone == 2) {
			SequenceText.GetComponent<Text>().text="7AM is a little early Let's make it 7:15! Hit: " + sequence[0] + " To change the minutes!";
		}
		if (TasksDone == 3) {
			SequenceText.GetComponent<Text>().text="7AM is a little early Let's make it 7:15! Hit: " + sequence[0] + " To change the minutes!";
		}
		if (TasksDone == 4) {
			SequenceText.GetComponent<Text>().text="Time for bed! Hit: " + sequence[0] + " To set the alarm!";
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
			
			if(TasksDone==0){SequenceText.GetComponent<Text>().text="Set alarm for tomorrow morning. Hit: " + sequence[0] + " To start changing the clock.";
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
			ForeverScript other=	Forever.GetComponent<ForeverScript>();
			other.LoadScene("PMA");
			Debug.Log ("Safe to exit Scene.");
			//Call to next scene.
			
		}
	}
	
	void TaskSwitcher(int TasksDone){
		
		if (TasksDone == 1) {
			LastHitKey = "";
			CurrentKeyLocked = "";
			sequence = "71";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Alarm On!";
			currenttime=Time.time+2;
			
		}
		
		if (TasksDone == 2) {
			LastHitKey = "";
			CurrentKeyLocked = "";
			sequence = "11";
			AlarmClock7.SetActive(true);
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="7AM!";
			currenttime=Time.time+1;
			
		}
		if (TasksDone == 3) {
			sequence = "51";
			AlarmClock71.SetActive(true);
			//SnoozePanel.SetActive(true);
			//SnoozeText.GetComponent<Text>().text="The Tonight Show!";
			currenttime=Time.time+2;
		}

		if (TasksDone == 4)
		{
			sequence = "a1";
			AlarmClock715.SetActive(true);
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="7:15!";
			currenttime=Time.time+1;
		}

		if (TasksDone == 5) {
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="See You Tomorrow!";
			currenttime=Time.time+3;
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

