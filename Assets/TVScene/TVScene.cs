using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TVScene : MonoBehaviour {
	public GameObject KeyHitText;
	public GameObject SequenceText;
	public GameObject CompletedText;
	public GameObject InsultText;
	public GameObject SnoozeText;
	public GameObject SnoozePanel;
	public GameObject TVScreen;
	public GameObject Channel1;
	public GameObject Channel2;
	public GameObject Channel3;
	string sequence = " 1";
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
			SequenceText.GetComponent<Text> ().text = "Let's flip through the channels! Hit: " + sequence[0] + " To change to Channel 1!";
		}
		if (TasksDone == 2) {
			SequenceText.GetComponent<Text>().text="Not too exciting! Hit: " + sequence[0] + " To change the channel again!";
		}
		if (TasksDone == 3) {
			SequenceText.GetComponent<Text>().text="Hmmm let's see what else is on! Hit: " + sequence[0] + " To switch to Channel 3!";
		}
		if (TasksDone == 4) {
			SequenceText.GetComponent<Text>().text="Nothing interesting. Let's just get ready for bed. Hit: " + sequence[0] + " To turn off the TV!";
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
			
			if(TasksDone==0){SequenceText.GetComponent<Text>().text="Let's see whats on TV tonight. Hit: " + "SpaceBar" + " To turn on the TV.";
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
			//Start the TV, Pick first channel
			//START THE STATIC 
			//TVScreen.SetActive(true);
			LastHitKey = "";
			CurrentKeyLocked = "";
			sequence = "11";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="TV On!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			
		}
		
		if (TasksDone == 2) {
			//Channel 1
			Channel1.SetActive(true);
			LastHitKey = "";
			CurrentKeyLocked = "";
			sequence = "21";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Cartoon Network!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			
		}
		if (TasksDone == 3) {
			//Channel 2
			Channel1.SetActive(false);
			Channel2.SetActive(true);
			sequence = "31";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="The Tonight Show!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
		}
		if (TasksDone == 4) {
			//Channel 3
			Channel2.SetActive(false);
			Channel3.SetActive(true);
			sequence = " 1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Food Network!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
		}
		
		if (TasksDone == 5)
		{
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Time For Bed!";
			Debug.Log ("Enjoy Your Cookies");
			currenttime=Time.time+5;
			
		}
		if (TasksDone == 6) {
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

