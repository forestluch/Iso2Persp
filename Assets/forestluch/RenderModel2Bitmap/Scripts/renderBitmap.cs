using UnityEngine;
using System.Collections;
using System.IO;

public class renderBitmap : MonoBehaviour {
	#if UNITY_STANDALONE
	Texture2D renderTex2T2d;
	public Camera camera;
	public	int widthResolution = 1024;
	CameraClearFlags chSet;
	static string folderPath;
	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName == "") {
			folderPath = "NoNameScene"+"_renderBitmap";
		} else {
			folderPath = Application.loadedLevelName+"_renderBitmap";
		}
		if (camera == null) {
			camera = Camera.main;
		}
		chSet = camera.clearFlags;
		createAlbumDirectory ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			RenderBitmap();
		}
	}

	public void RenderBitmap () {
		StartCoroutine (renderMapSave());
	}

	IEnumerator renderMapSave(){
		RenderTexture rndTex;
		rndTex = new RenderTexture (widthResolution,Mathf.RoundToInt(((float)widthResolution/(float)camera.pixelWidth)*camera.pixelHeight),24,RenderTextureFormat.ARGB32);

		camera.clearFlags = CameraClearFlags.Depth;
		camera.targetTexture = rndTex;
		yield return new WaitForEndOfFrame ();
		renderTex2T2d = new Texture2D (rndTex.width,rndTex.height);
		RenderTexture.active = rndTex;
		renderTex2T2d.ReadPixels (new Rect(0, 0, rndTex.width, rndTex.height),0,0);
		renderTex2T2d.Apply ();
		yield return new WaitForEndOfFrame ();
		byte[] bytes = renderTex2T2d.EncodeToPNG();
		string TimeStamp = System.DateTime.Now.Year.ToString () + System.DateTime.Now.Month.ToString () + System.DateTime.Now.Day.ToString () + System.DateTime.Now.Hour.ToString () + System.DateTime.Now.Minute.ToString () + System.DateTime.Now.Second.ToString ();
		File.WriteAllBytes (Application.streamingAssetsPath+"/"+folderPath+"/"+TimeStamp+".png", bytes);
		Debug.Log ("path: "+Application.streamingAssetsPath+"/"+folderPath+"/"+TimeStamp+".png");
		yield return new WaitForEndOfFrame ();
		camera.targetTexture = null;
		camera.clearFlags = chSet;
		Destroy (rndTex);
	}

	static string createAlbumDirectory(){																						
		string dirPath = System.IO.Path.Combine (Application.streamingAssetsPath, folderPath);
		
		Debug.Log ("dirPath: " + dirPath);
		
		bool isDirExist = System.IO.Directory.Exists(dirPath);
		
		if(!isDirExist){
			Debug.Log ("isDirExist = false, now create one.");
			DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(dirPath);
		}else{
			Debug.Log ("isDirExist = true, dir already exist.");
			return dirPath;
		}
		
		Debug.Log ("" + System.IO.Directory.Exists(dirPath));
		
		return dirPath;
	}

	void OnGUI(){
		string windowRect;
		windowRect = GUI.TextField (new Rect (Screen.width/2 - 100, 20, 200, 20),"Press button on inspector or keyboard A button to render bitmap",20);
	}
	#endif
}
