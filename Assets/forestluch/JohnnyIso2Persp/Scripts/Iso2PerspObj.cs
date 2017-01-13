using UnityEngine;
using System.Collections;

public class Iso2PerspObj : MonoBehaviour {
	Camera cam;
	float chDis;
	float currentDis;
	Vector3 chScale;
	Vector3 chScreenPos;
	Vector3 boundCenter;
	Renderer[] rndList;
	GameObject gj;
	// Use this for initialization
	void Start () {
		rndList = this.gameObject.GetComponentsInChildren<Renderer> ();
		for (int i = 0; i < rndList.Length; i++) {
			boundCenter += rndList [i].bounds.center;
		}
		boundCenter /= rndList.Length;
		Debug.Log (boundCenter);
		gj = new GameObject ();
		gj.transform.localScale = this.gameObject.transform.localScale;
		gj.transform.position = boundCenter;
		this.gameObject.transform.SetParent (gj.transform);
		chScale = gj.transform.localScale;
		cam = Camera.main;
		chDis = Vector3.Distance (cam.ScreenToWorldPoint(new Vector3(0,0,5)),cam.ScreenToWorldPoint(new Vector3(Screen.width,0,5)));
		chScreenPos = cam.WorldToScreenPoint (gj.transform.position);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		currentDis = Vector3.Distance (cam.ScreenToWorldPoint(new Vector3(0,0,5)),cam.ScreenToWorldPoint(new Vector3(Screen.width,0,5)));
		gj.transform.localScale = chScale * (currentDis / chDis);
		gj.transform.localPosition = cam.ScreenToWorldPoint (chScreenPos);
	}
}
