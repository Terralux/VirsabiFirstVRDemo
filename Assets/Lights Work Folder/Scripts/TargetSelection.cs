using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetSelection : MonoBehaviour {

	public float waitTime = 0.1f;
	private LookTarget lt;
	private LookTarget prevLt;

	private bool isInFocus;
	private float passedTime;
	private Image uiTimer;

	// Use this for initialization
	void Start () {
		uiTimer = GameObject.FindGameObjectWithTag ("UI Timer").GetComponent<Image> ();
		StartCoroutine (WaitForNextRaycast ());
	}

	IEnumerator WaitForNextRaycast(){
		yield return new WaitForSeconds (waitTime);
		RaycastHit hit = new RaycastHit ();
		Ray ray = new Ray (transform.position, transform.forward);

		isInFocus = false;

		if (Physics.Raycast (ray, out hit, 100f)) {
			if (hit.collider != null) {
				prevLt = lt;
				lt = hit.collider.gameObject.GetComponent<LookTarget> ();
				if (lt == null) {
					lt = hit.collider.transform.parent.GetComponent<LookTarget> ();
				}
				if (lt != null) {
					isInFocus = true;

					if (lt != prevLt) {
						passedTime = 0f;
					}
				}
			}
		}

		StartCoroutine (WaitForNextRaycast ());
	}

	void Update () {
		if (isInFocus) {
			passedTime += Time.deltaTime;
			uiTimer.fillAmount = passedTime / 3f;

			if (passedTime >= 3) {
				isInFocus = false;
				lt.Action ();
			}
		} else {
			Reset ();
		}
	}

	public void Reset(){
		passedTime = 0f;
		isInFocus = false;
		uiTimer.fillAmount = passedTime / 3f;
	}

}
