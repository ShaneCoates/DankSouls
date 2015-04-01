using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	GameObject target;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (Random.Range (-20, 20), 0.5f, Random.Range (-9, 9));
		FindClosest ();
	}

	void FindClosest() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Tower"); 
		float distance = Mathf.Infinity; 
		Vector3 position = transform.position; 
		
		foreach (GameObject go in gos)  { 
			if(go.activeInHierarchy == true) {
				Vector3 diff = (go.transform.position - position);
				float currDistance = diff.sqrMagnitude; 
				if (currDistance < distance) { 
					target = go; 
					distance = currDistance; 
				} 
			}
		} 
		GetComponent<NavMeshAgent> ().destination = target.transform.position;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Tower") {
			other.gameObject.SetActive(false);
			GameObject.Destroy(other.gameObject);
			FindClosest();
		}
	}

	// Update is called once per frame
	void Update () {
	}
}
