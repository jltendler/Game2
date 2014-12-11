using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OvenCooking : MonoBehaviour {
	public GameObject KeyHitText;
	public GameObject SequenceText;
	public GameObject CompletedText;
	public GameObject InsultText;
	public GameObject SnoozeText;
	public GameObject SnoozePanel;
	public GameObject OvenText;
	public GameObject OvenOffLight;
	public GameObject OvenOpen;
	public GameObject OvenHalf;
	public GameObject OvenClosed;
	public GameObject Cookies;
	string sequence = "p1";
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
		
		if ((TasksDone == 1) && (Input.GetKeyUp("3"))) {
			OvenText.GetComponent<Text>().text="3";
		} else if ((TasksDone == 1) && (Input.GetKeyUp("7"))) {
			OvenText.GetComponent<Text>().text="37";
		} else if ((TasksDone == 1) && (Input.GetKeyUp("5"))) {
			OvenText.GetComponent<Text>().text="375";
		}

		if ((TasksDone == 2) && (Input.GetKeyUp("t"))) {
			OvenText.GetComponent<Text>().text="00:00";
		} else if ((TasksDone == 2) && (Input.GetKeyUp("1"))) {
			OvenText.GetComponent<Text>().text="10:00";
		} else if ((TasksDone == 2) && (Input.GetKeyUp("5"))) {
			OvenText.GetComponent<Text>().text="15:00";
		} 

		if ((TasksDone == 4) && (Input.GetKeyUp (KeyCode.E))) {
			OvenClosed.SetActive (false);
			OvenHalf.SetActive (true);
			OvenOffLight.SetActive(false);	
		} else if ((TasksDone == 4) && (Input.GetKeyUp (KeyCode.D))) {
			OvenHalf.SetActive (false);
			OvenOpen.SetActive (true);
			Cookies.SetActive(true);
		}
		
		if (TasksDone == 1) {
			SequenceText.GetComponent<Text> ().text = "Pre heat the oven! Hit: " + sequence[0] + " To turn up the heat!";
		}
		if (TasksDone == 2) {
			SequenceText.GetComponent<Text>().text="Set a timer! Hit: " + sequence[0] + " To start the timer!";
		}
		if (TasksDone == 3) {
			SequenceText.GetComponent<Text>().text="Cookies are done! Hit: " + sequence[0] + " To turn off the oven!";
		}
		if (TasksDone == 4) {
			SequenceText.GetComponent<Text>().text="The cookies need to cool! Hit: " + sequence[0] + " To take out the cookies!";
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
			
			if(TasksDone==0){SequenceText.GetComponent<Text>().text="Let's make some cookies. Hit: " + prettyname + " To turn on the oven.";
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
			//Oven Light On & PreHeat Oven
			OvenText.SetActive(true);
			OvenText.GetComponent<Text>().text="";
			OvenOffLight.SetActive(false);
			LastHitKey = "";
			CurrentKeyLocked = "";
			sequence = "317151s1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Oven On!";
			//SequenceText.GetComponent<Text>().text="Time for work! Hit: " + sequence[0] + " To Swipe Your Card!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			
		}
		
		if (TasksDone == 2) {
			//Set Timer
			OvenText.GetComponent<Text>().text="375";
			LastHitKey = "";
			CurrentKeyLocked = "";
			sequence = "t11151s1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Pre Heat!";
			//SequenceText.GetComponent<Text>().text="Flip your card Dummy! Hit: " + "Spacebar" + " To flip!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			
		}
		if (TasksDone == 3) {
			//
			OvenText.GetComponent<Text>().text="00:00";
			sequence = "q1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="15 Minutes Later!";
			//SequenceText.GetComponent<Text>().text="Swipe your card again! Hit: " + sequence[0] + " To Clock In!";
			CompletedText.SetActive(true);
			currenttime=Time.time+5;
		}
		if (TasksDone == 4) {
			//Oven Light Off
			OvenText.SetActive(false);
			OvenOffLight.SetActive(true);
			sequence = "e1d1c1";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Oven Off!";
			//SequenceText.GetComponent<Text>().text="Swipe your card again! Hit: " + sequence[0] + " To Clock In!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
		}

		if (TasksDone == 5)
		{
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="NOM NOM NOM!";
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

