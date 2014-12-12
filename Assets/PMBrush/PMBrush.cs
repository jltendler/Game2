using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PMBrush : MonoBehaviour {
	public RawImage Brush;
	public GameObject KeyHitText;
	public GameObject SequenceText;
	public GameObject CompletedText;
	public GameObject InsultText;
	public GameObject BrushAlias;
	public GameObject SnoozeText;
	public GameObject SnoozePanel;
	public GameObject Forever;
	string sequence = ")1(1)1(1";
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
	float duude, dude;
	
	string CurrentKeyLocked;
	int TasksDone=0;
	public GameObject selfAlias;
	TextureChanger scriptyscript;
	//	string ktest="K";
	// Use this for initialization
	void Start () {	
		//JACK COPY AND PASTE THE LINE BELOW.
		Forever = GameObject.Find ("Forever");
		duude=150f; dude=-150;
		LastHitKey = "";
		CurrentKey = "";
		CurrentKeyLocked = "";
		CompletedText.GetComponent<Text>().text = "";
		InsultText.GetComponent<Text>().text="";
		scriptyscript=	selfAlias.GetComponent<TextureChanger>();
	}
	
	// Update is called once per frame
	
	void Update () 
	{	if ((Time.time > currenttime)&&!Done) {
			CompletedText.SetActive(false);
			SnoozePanel.SetActive(false);
			currenttime = 0;
		}
		
		if (TasksDone == 1) {
			SequenceText.GetComponent<Text> ().text = "Keep Brushing! Still gross! Hit: " + prettyname + " To Brush!";
		}
		if (TasksDone == 2) {
			SequenceText.GetComponent<Text>().text="Looking Better! Keep it up! Hit: " + prettyname + " To Brush!";
		}
		if (TasksDone == 3) { 
			SequenceText.GetComponent<Text>().text="Almost clean! Hit: " + prettyname + " To Brush!";
		}
		if (TasksDone == 4) {
			SequenceText.GetComponent<Text>().text="All Done! Hit: " + prettyname + " To Be Clean!";
		}
		
		bool skippy = false;
		if(Input.GetButtonDown("LeftArrowAlias")){
			CurrentKeyLocked="(";
			skippy=true;
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
			SequenceText.GetComponent<Text>().text="Almost Bed Time! Hit: " + prettyname + " To brush your teeth!";
			string RepeatCountString = RepeatCount.ToString ();
			
			if ((LastHitKey == slot1) && (RepeatCountString == slot2)) {
				
				//
				//Debug.Log ("Typed successfully");
				if(slot1=="("){ //Move brush left when appropriate
					
					BrushAlias.transform.position=BrushAlias.transform.position + new Vector3(dude,0,0);
				}
				if(slot1==")"){ //Move brush right when appropriate
					BrushAlias.transform.position=BrushAlias.transform.position + new Vector3(duude,0,0);
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
		if (Done&&(currenttime<Time.time)) {
			ForeverScript other=	Forever.GetComponent<ForeverScript>();
			other.LoadScene("PMB");
			Debug.Log ("Safe to exit Scene.");
		}
	}
	
	void TaskSwitcher(int TasksDone){
		
		if (TasksDone == 1) {
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text = "BRUSH!";
			SequenceText.GetComponent<Text> ().text = "Keep Brushing! Still gross! Hit: " + prettyname + " To Brush!";
			scriptyscript.Clean();
			sequence = ")1(1)1(1";
			currenttime=Time.time+2;
		}
		if (TasksDone == 2) {
			sequence = ")1(1)1(1)1";
			scriptyscript.Clean ();
			
			
		}
		if (TasksDone == 3) {
			sequence="(1)1(1)1(1)1";
			scriptyscript.Clean();
		}
		if (TasksDone == 4) {
			//Hit Space To Spit.
			sequence=" 1";
			
		}
		if (TasksDone == 5) {
			SnoozePanel.SetActive(true);
			SnoozeText.GetComponent<Text>().text = "Bed Time!";
			sequence="";
			currenttime = Time.time+3;
			Done=true;
		}
		
	}
	void ErrorCheck(){
		if ((TimesErrored != PreviousTimesErrored)) {
			PreviousTimesErrored = TimesErrored;
			InsultText.GetComponent<Text> ().text = "You Dun Goofed.";
		} else {
			InsultText.GetComponent<Text> ().text = "";
		}
	}
	/*	void KeycodeConverter(){
		CapitalLastHitKey = LastHitKey.ToUpper();
		CapitalCurrentKey = CurrentKey.ToUpper ();
		if (LastHitKey != "") {
						ConvertedLastKeyHit = (KeyCode)System.Enum.Parse (typeof(KeyCode), CapitalLastHitKey);
				}
		if (LastHitKey == "") {
			ConvertedLastKeyHit=KeyCode.F15;
				}
		if(CurrentKey!=""){
			ConvertedCurrentKey=(KeyCode)System.Enum.Parse(typeof(KeyCode),CapitalCurrentKey);
			                                               }
		if (CurrentKey == "") {
			ConvertedCurrentKey=KeyCode.F15;		
		}

	}
	*/
	/*
	 * KeyCode ConvertToKeycode(char foley){
		string sasha= foley.ToString;
			sasha = sasha.ToUpper ();
		KeyCode tasha = (KeyCode)System.Enum.Parse (typeof(KeyCode), foley);
			return tasha;
	}
	*/
	
}
