using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	[Range(1f,10f)]
	public float waitTime = 5f;
	public Transform[] transforms;
	private int index = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine (WaitForNextTeleport ());
	}

	IEnumerator WaitForNextTeleport(){
		yield return new WaitForSeconds (waitTime);
		transform.position = transforms [index].position;
		index++;
		if (index > transforms.Length - 1) {
			index = 0;
		}
		StartCoroutine (WaitForNextTeleport ());
	}
}
