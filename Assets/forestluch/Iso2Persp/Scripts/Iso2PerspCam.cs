using UnityEngine;
using System.Collections;

public class Iso2PerspCam : MonoBehaviour {
	Camera cam;
	GameObject anchor, center, selfPoint;
	float chA, chB, chC;
	float chAngle;
	float currentA, currentB, currentC;
	float currentAngle;
	float targetC;
	public float focusDistance = 100;
	bool isSetFinish = true;
	// a/|b
	// /--
	//  c
	void Awake () {
		cam = Camera.main;
		anchor = new GameObject ();
		center = new GameObject ();
		ResetFocus ();
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		setFocus ();
	}

	void setFocus(){
		if (!isSetFinish) {
			isSetFinish = true;
			chC = focusDistance;
			anchor.transform.position = cam.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, chC));
			center.transform.position = cam.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, chC));
			chA = Vector3.Distance (anchor.transform.position, cam.transform.position);
			chB = Vector3.Distance (anchor.transform.position, center.transform.position);
			chC = Vector3.Distance (cam.transform.position, center.transform.position);
			chAngle = Mathf.Atan (chB / chC) * 180 / Mathf.PI;
			targetC = chC;
			currentC = chC;
		}
	}

	void LateUpdate(){
		anchor.transform.position = cam.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, targetC));
		center.transform.position = cam.ScreenToWorldPoint (new Vector3(Screen.width/2, Screen.height/2, targetC));
		currentB = Vector3.Distance (anchor.transform.position,center.transform.position);
		currentA = Vector3.Distance (anchor.transform.position,cam.transform.position);
		currentC = Vector3.Distance (cam.transform.position,center.transform.position);
		currentAngle = Mathf.Atan(currentB/currentC)*180/Mathf.PI;
		targetC = chB / Mathf.Tan (Mathf.PI / (180 / currentAngle));
		targetC = Mathf.RoundToInt (targetC);
		cam.transform.Translate (Vector3.forward*-1* (targetC - currentC),Space.Self);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawRay (this.gameObject.transform.position, this.gameObject.transform.forward * (focusDistance));
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(this.gameObject.transform.position+this.gameObject.transform.forward * (focusDistance), 10);
		Gizmos.color = Color.red;
		Gizmos.DrawRay (this.gameObject.transform.position, this.gameObject.transform.forward * (targetC));
	}

	/// <summary>
	/// Use the focus distance (blue line) to new focus distance (red line).
	/// </summary>
	public void ResetFocus(){
		isSetFinish = false;
	}
}
