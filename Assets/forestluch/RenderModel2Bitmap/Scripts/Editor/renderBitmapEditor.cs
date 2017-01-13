using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(renderBitmap))]
public class renderBitmapEditor : Editor {
	renderBitmap rdb;
	void OnEnable(){
		rdb = (renderBitmap)target;

	}

	public override void OnInspectorGUI(){
		#if UNITY_STANDALONE
		EditorGUILayout.HelpBox ("This script help you render the 3D model without usina any pro-3D modeling application such as 3DsMax or Maya.\n" +
			"All you have to do is pick the camera which you want and set the render bitmap width resolution (the height will be calucate automatical) than PRESS the Render Bitmap button.\n" +
			"Enjoy, have fun :D", MessageType.Info);
		DrawDefaultInspector ();
		GUIStyle mainButton = new GUIStyle(EditorStyles.miniButtonMid);
		mainButton.fontStyle = FontStyle.Normal;
		mainButton.fontSize = 40;
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Pick Main Camera")) {
			rdb.camera = Camera.main;
		}
		if (GUILayout.Button ("Open Streaming Assets")) {
			Application.OpenURL (Application.streamingAssetsPath);
		}
		GUILayout.EndHorizontal ();
		if (EditorApplication.isPlaying) {
			if (GUILayout.Button ("Render Bitmap!!", mainButton, GUILayout.Height (70))) {
				rdb.RenderBitmap ();
				SceneView.lastActiveSceneView.ShowNotification (new GUIContent ("Render Success"));
			}
		} else {
			EditorGUILayout.HelpBox ("Need runtime mode to active, you can PRESS the run button below to active.",MessageType.Warning);
			if (GUILayout.Button ("RUN")) {
				EditorApplication.isPlaying = true;
			}
		}
		#else
		EditorGUILayout.HelpBox ("Please set platform to standalone.",MessageType.Error);
		#endif

	}
}
