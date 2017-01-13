using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(Iso2PerspObj))]
public class Iso2PrepspObjEditor : Editor {
	Iso2PerspObj i2po;
	void OnEnable(){
		i2po = (Iso2PerspObj)target;
	}

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();

		usage ();
	}

	void usage(){
		EditorGUILayout.HelpBox ("How to use\nIt is automatic!! Enjoy!!", MessageType.Info);
	}

}
