using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveLookTarget : LookTarget {

	public Transform myTarget;
	private Transform player;
	private UnityStandardAssets.ImageEffects.ScreenOverlay screenOverlay;

	private BoxCollider myCollider;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		screenOverlay = player.GetComponentInChildren<UnityStandardAssets.ImageEffects.ScreenOverlay> ();
		myCollider = GetComponent<BoxCollider> ();
	}

	public override void Action(){
		myCollider.enabled = false;
		StartCoroutine(FadeIn(false));
	}

	private IEnumerator FadeIn(bool isFadingIn){
		yield return new WaitForSeconds (0.05f);
		screenOverlay.intensity += isFadingIn?-0.02f:0.02f;

		if (screenOverlay.intensity >= 1) {
			player.position = myTarget.position;
			myCollider.enabled = true;
			StartCoroutine(FadeIn (true));
		} else {
			if (screenOverlay.intensity > 0) {
				StartCoroutine (FadeIn (isFadingIn));
			}
		}
	}

	void OnDrawGizmos(){
		if (myTarget != null) {
			Gizmos.DrawLine (transform.position, myTarget.position);
		}
	}
}