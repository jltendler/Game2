using UnityEngine; using UnityEditor;

using System.Collections;


	public class TexturePostProcessor : AssetPostprocessor { void OnPostprocessTexture(Texture2D texture) { TextureImporter importer = assetImporter as TextureImporter; importer.anisoLevel = 0; importer.filterMode = FilterMode.Point;
			
			Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
			if (asset)
			{
				EditorUtility.SetDirty(asset);
			Debug.Log("Fixed the texture (1)");
			}
			else
			{
				texture.anisoLevel = 0;
				texture.filterMode = FilterMode.Point;       
			Debug.Log("Fixed the texture (2)");
			} 
		}
	
	// Update is called once per frame
	void Update () {
	
	}
}
