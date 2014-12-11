using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureChanger : MonoBehaviour {
	public Texture2D TeethMesh;
	public Texture2D TeethClean;
	public Texture2D TeethGross;
	public Texture2D TeethModerate;
	public Texture2D TeethAlmostClean;
	public RawImage Test;
	public RawImage Test2;
	public RawImage Test3;
	public RawImage Test4;
	public RawImage MouthAlias;
	int counter=0;
	int transparentcounter=0;
	int Valuedcounter=0;
	int CleanCounter=0;
	public Color Yellow;
	public Color LessYellow;
	public Color OffWhite;
	public Color Transparent;
	Color tempcolor;
	// Use this for initialization
	void Start () {

		Transparent =TeethClean.GetPixel (1, 1);
		TeethGross = new Texture2D (644, 383);
		TeethGross.wrapMode = TextureWrapMode.Clamp;
		int pixels = 644 * 383;
		//Debug.Log (TeethGross.GetPixel (0, 0) * 255);
		Color[] colors = new Color[pixels];
		Color[] cleancolors = new Color[pixels];
		cleancolors = TeethClean.GetPixels ();
		int rounded = 0;
		TeethGross.SetPixels (cleancolors);

		for (int i=0; i<644; i++) {
			for (int j=0; j<383; j++){
				tempcolor=TeethMesh.GetPixel(i,j);
				if((tempcolor.a!=0)){ //Check to see if the tempcolor (The pixel of the mask) is not transparent.
					TeethGross.SetPixel(i,j,Yellow); //Since it isn't transparent, we want to make a change.
					Valuedcounter++;
				}

			}



				}

		Debug.Log (cleancolors[1]);
		Debug.Log ("Counter = "+counter);
		Debug.Log("Valued Counter = " +Valuedcounter);


		TeethGross.Apply ();
		Test.texture = TeethGross;
		MouthAlias.texture = TeethGross;



		//Next

		Transparent =TeethClean.GetPixel (1, 1);
		TeethModerate = new Texture2D (644, 383);
		TeethModerate.wrapMode = TextureWrapMode.Clamp;
		//Debug.Log (TeethGross.GetPixel (0, 0) * 255);
		colors = new Color[pixels];
		rounded = 0;
		TeethModerate.SetPixels (cleancolors);
		
		for (int i=0; i<644; i++) {
			for (int j=0; j<383; j++){
				tempcolor=TeethMesh.GetPixel(i,j);
				if((tempcolor.a!=0)){ //Check to see if the tempcolor (The pixel of the mask) is not transparent.
					TeethModerate.SetPixel(i,j,LessYellow); //Since it isn't transparent, we want to make a change.
					Valuedcounter++;
				}
				
			}
			
			
			
		}

		
		
		TeethModerate.Apply ();
		Test2.texture = TeethModerate;

		//Next!

		Transparent =TeethClean.GetPixel (1, 1);
		TeethAlmostClean = new Texture2D (644, 383);
		TeethAlmostClean.wrapMode = TextureWrapMode.Clamp;
		//Debug.Log (TeethGross.GetPixel (0, 0) * 255);
		colors = new Color[pixels];
		rounded = 0;
		TeethAlmostClean.SetPixels (cleancolors);
		
		for (int i=0; i<644; i++) {
			for (int j=0; j<383; j++){
				tempcolor=TeethMesh.GetPixel(i,j);
				if((tempcolor.a!=0)){ //Check to see if the tempcolor (The pixel of the mask) is not transparent.
					TeethAlmostClean.SetPixel(i,j,OffWhite); //Since it isn't transparent, we want to make a change.
					Valuedcounter++;
				}
				
			}
			
			
			
		}
		
		
		
		TeethAlmostClean.Apply ();
		Test3.texture = TeethAlmostClean;
		Test4.texture = TeethClean;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Clean(){
		if (CleanCounter == 0) {
						CleanCounter++;
			MouthAlias.texture=TeethModerate;
				}
		else if (CleanCounter == 1) {
			CleanCounter++;
			MouthAlias.texture=TeethAlmostClean;
		}
		else if (CleanCounter == 2) {
			CleanCounter++;
			MouthAlias.texture=TeethClean;
		}

	}
}
