using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	// Use this for initialization
	void Start () {

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
		Debug.Log (closest.transform.position);
		GetComponent<NavMeshAgent> ().destination = closest.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
