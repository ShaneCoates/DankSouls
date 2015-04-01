using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	GameObject closest;
	Transform target;
	float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		target = FindClosest ();
		Vector3 targetDir = target.position - transform.position;

		float step = speed * Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0f);
		Debug.DrawRay (transform.position, newDir, Color.red);

		transform.rotation = Quaternion.LookRotation (newDir);

	}
	Transform FindClosest() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Minion"); 
		float distance = Mathf.Infinity; 
		Vector3 position = transform.position; 
		
		foreach (GameObject go in gos)  { 
			Vector3 diff = (go.transform.position - position);
			float currDistance = diff.sqrMagnitude; 
			if (currDistance < distance) { 
				closest = go; 
				distance = currDistance; 
			} 
		} 
		Transform trans = closest.transform;
		return trans;
	}
}
