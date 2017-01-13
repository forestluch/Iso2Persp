using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(Iso2PerspCam))]
public class Iso2PrepspCamEditor : Editor {
	Iso2PerspCam i2pc;
	void OnEnable () {
		i2pc = (Iso2PerspCam)target;
	}

	public override void OnInspectorGUI(){
		if (i2pc.gameObject.GetComponent<Camera> () == null) {
			EditorGUILayout.HelpBox ("ERROR!! this component must append on camera", MessageType.Error);
		} else {
			DrawDefaultInspector ();
			if (EditorApplication.isPlaying) {
				if (GUILayout.Button ("Reset Focus")) {
					i2pc.ResetFocus ();
				}
			} else {
				usage ();
			}
		}
	}

	void usage(){
		EditorGUILayout.HelpBox ("How to use\n1. Set the parameter Focus Distance\n" +
		"2. Watch the scene view the gizmos blue line and the end's blue sphere is show the camera want focus\n" +
		"3. On runtime mode camera current focus show with red line, if you want to modify the focus distance press Reset Focus button or call function ResetFocus();", MessageType.Info);
	}
}
