using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (Random.Range (-20, 20), 0.5f, Random.Range (-9, 9));
		FindClosest ();
	}

	void FindClosest() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Tower"); 
		GameObject closest = new GameObject(); 
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
		GetComponent<NavMeshAgent> ().destination = closest.transform.position;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
