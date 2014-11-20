using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Typing : MonoBehaviour {
	public GameObject KeyHitText;
	public GameObject SequenceText;
	public GameObject CompletedText;
	public GameObject InsultText;
	string sequence = "a1b2c3d4";


	bool Done=false;
	string LastLastHitKey;
	string LastHitKey;
	string CapitalLastHitKey;
	string CurrentKey;
	string CapitalCurrentKey;
	int RepeatCount=0;
	KeyCode ConvertedLastKeyHit;
	KeyCode ConvertedCurrentKey;
	string FormedString;
	int TasksDone=0;
//	string ktest="K";
	// Use this for initialization
	void Start () {
		LastHitKey = "";
		CurrentKey = "";
		CompletedText.GetComponent<Text>().text = "";
		InsultText.GetComponent<Text>().text="";
	}
	
	// Update is called once per frame
	void Update () 
		{



		// 
		//(Input.GetKeyDown(ConvertedKey))

						CurrentKey = Input.inputString;



						if ((CurrentKey != LastHitKey) && (CurrentKey != "")) {
								LastHitKey = CurrentKey;
						}
						if (LastLastHitKey != LastHitKey) {
								LastLastHitKey = LastHitKey;
								RepeatCount = 0;
						}
						
						if ((CurrentKey == LastHitKey) && (true)) {
								RepeatCount++;
						}
						if (LastHitKey == "") {
								RepeatCount = 0;		
						}
						KeyHitText.GetComponent<Text> ().text = "Last hit key was: " + LastHitKey + " " + RepeatCount + " Times.";
		StringMaker ();

		TestSequence (ref sequence);

		//Debug.Log ("CurrentKey: " + CurrentKey+"Last Key Hit: " +LastHitKey);

		} //End Update

	void StringMaker(){
				FormedString = FormedString + Input.inputString;
		}
	void TestSequence(ref string InputSequence){
				if (InputSequence != "") {//Don't do this to an empty string.
						string slot1 = InputSequence [0].ToString ();
			int PressTimes=InputSequence[1]-'0';
						string slot2 = InputSequence [1].ToString ();
			SequenceText.GetComponent<Text>().text="You need to hit: " + slot1 + " " +(PressTimes) + " Times!";
						string RepeatCountString = RepeatCount.ToString ();

						if ((LastHitKey == slot1) && (RepeatCountString == slot2)) {
								Debug.Log ("Typed successfully");
								InputSequence = InputSequence.Remove (0, 2);
						}
					
						Debug.Log (InputSequence);
				}
				if ((InputSequence == "")&&!Done) { //Do this to an empty string
						Debug.Log ("Done with typing current task. Moving to next.");
						TasksDone++;
						TaskSwitcher (TasksDone);
		
				}
		}

		void TaskSwitcher(int TasksDone){
		CompletedText.GetComponent<Text>().text = "You Have Completed " + TasksDone + " Sequences!";
		if (TasksDone == 1) {
			sequence="f9d2e3r4";
					
		}
		if (TasksDone == 2) {
						sequence = "p2r3t4";

				}
		if (TasksDone == 3) {
						Done = true;
				}

		}

	void KeycodeConverter(){
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
	/*KeyCode ConvertToKeycode(char foley){
		string sasha= foley.ToString;
			sasha = sasha.ToUpper ();
		KeyCode tasha = (KeyCode)System.Enum.Parse (typeof(KeyCode), foley);
			return tasha;
	}
	*/

}
