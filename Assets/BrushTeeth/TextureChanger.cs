using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureChanger : MonoBehaviour {
	public Texture2D TeethMesh;
	public Texture2D TeethClean;
	public Texture2D TeethGross;
	public Texture2D TeethModerate;
	public RawImage Test;
	int counter=0;
	int transparentcounter=0;
	int Valuedcounter=0;
	public Color Yellow;
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
