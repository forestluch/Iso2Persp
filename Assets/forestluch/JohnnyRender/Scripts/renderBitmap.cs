using UnityEngine;
using System.Collections;
using System.IO;

public class renderBitmap : MonoBehaviour {

	Texture2D renderTex2T2d;
	public	int denWidth = 1024;
	public Camera cam;
	CameraClearFlags chSet;
	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		chSet = cam.clearFlags;
		createAlbumDirectory ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			capture();
		}
	}

	void capture () {
		StartCoroutine (save());
	}

	IEnumerator save(){
		RenderTexture rndTex;
		rndTex = new RenderTexture (denWidth,Mathf.RoundToInt(((float)denWidth/(float)Screen.width)*Screen.height),24,RenderTextureFormat.ARGB32);
		cam.clearFlags = CameraClearFlags.Depth;
		cam.targetTexture = rndTex;
		yield return new WaitForEndOfFrame ();
		renderTex2T2d = new Texture2D (rndTex.width,rndTex.height);
		RenderTexture.active = rndTex;
		renderTex2T2d.ReadPixels (new Rect(0, 0, rndTex.width, rndTex.height),0,0);
		renderTex2T2d.Apply ();
		yield return new WaitForEndOfFrame ();
		byte[] bytes = renderTex2T2d.EncodeToPNG();
		string TimeStamp = System.DateTime.Now.Year.ToString () + System.DateTime.Now.Month.ToString () + System.DateTime.Now.Day.ToString () + System.DateTime.Now.Hour.ToString () + System.DateTime.Now.Minute.ToString () + System.DateTime.Now.Second.ToString ();
		File.WriteAllBytes (Application.streamingAssetsPath+"/JohnnyRender/"+TimeStamp+".png", bytes);
		Debug.Log ("path: "+Application.streamingAssetsPath+"/JohnnyRender/"+TimeStamp+".png");
		yield return new WaitForEndOfFrame ();
		cam.targetTexture = null;
		cam.clearFlags = chSet;
		Destroy (rndTex);
	}

	static string createAlbumDirectory(){																						
		string dirPath = System.IO.Path.Combine (Application.streamingAssetsPath, "JohnnyRender");
		
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
		windowRect = GUI.TextField (new Rect (Screen.width/2 - 100, 20, 200, 20),"按下鍵盤A拍照",20);
	}
}
