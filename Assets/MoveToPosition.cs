using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour {

	[Range(0.1f,1f)]
	public float speed = 1f;
	public Transform target;

	private Vector3 origin;
	private float fraction = 0f;

	// Use this for initialization
	void Awake () {
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		fraction += speed * Time.deltaTime;

		if (Vector3.Distance (target.position, transform.position) > 0.5f) {
			transform.position = Vector3.Lerp (origin, target.position, fraction);
		} else {
			Destroy (this);
		}
	}
}
