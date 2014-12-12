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
	public RawImage TVScreen;
	public Texture2D Channel1;
	public Texture2D Channel2;
	public Texture2D Channel3;
	public GameObject Forever;
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
	public Texture2D StaticTexture;
	public Color RandomBW;
	// Use this for initialization
	void Start () {
		//JACK COPY AND PASTE THE LINE BELOW.
		Forever = GameObject.Find ("Forever");
		int counterbw = 0;
		LastHitKey = "";
		CurrentKey = "";
		CurrentKeyLocked = "";
		InsultText.GetComponent<Text>().text="";

		StaticTexture = new Texture2D (526, 471);

		for (int i=0; i<526; i++) {
			for(int j=0; j<471; j++){
				counterbw++;
				float ColorVal=Random.Range(0f,1f);
				RandomBW=new Color(ColorVal,ColorVal,ColorVal,1f);
				StaticTexture.SetPixel(i,j,RandomBW);
			}
				}
		StaticTexture.Apply ();
		Debug.Log ("Counterbw: " + counterbw);
	}
	
	// Update is called once per frame





	void Update () 
	{	if ((Time.time > currenttime)&&!Done) {
			CompletedText.SetActive(false);
			SnoozePanel.SetActive(false);
			currenttime = 0;
		}
		
		if (TasksDone == 1) {
			SequenceText.GetComponent<Text> ().text = "Let's flip through the channels! Hit: " + prettyname + " To change to Channel 1!";
		}
		if (TasksDone == 2) {
			SequenceText.GetComponent<Text>().text="Not too exciting! Hit: " + prettyname + " To change the channel again!";
		}
		if (TasksDone == 3) {
			SequenceText.GetComponent<Text>().text="Hmmm let's see what else is on! Hit: " + prettyname + " To switch to Channel 3!";
		}
		if (TasksDone == 4) {
			SequenceText.GetComponent<Text>().text="Nothing interesting. Let's just get ready for bed. Hit: " + prettyname + " To turn off the TV!";
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
			ForeverScript other=	Forever.GetComponent<ForeverScript>();
			other.LoadScene("TV");
			Debug.Log ("Safe to exit Scene.");
			//Call to next scene.
			
		}
	}
	
	void TaskSwitcher(int TasksDone){
		
		if (TasksDone == 1) {
			TVScreen.texture=StaticTexture;
			//Start the TV, Pick first channel
			//START THE STATIC 
			//TVScreen.SetActive(true);
			TVScreen.texture=StaticTexture;
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
			TVScreen.texture=Channel1;
			LastHitKey = "";
			CurrentKeyLocked = "";
			sequence = "21";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="The Tonight Show!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
			
		}
		if (TasksDone == 3) {
			//Channel 2
			TVScreen.texture=Channel2;
			sequence = "31";
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text="Cartoons!";
			CompletedText.SetActive(true);
			currenttime=Time.time+2;
		}
		if (TasksDone == 4) {
			//Channel 3
			TVScreen.texture=Channel3;
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
			currenttime=(Time.time+3);
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

