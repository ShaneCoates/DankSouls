using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	GameObject target;
	// Use this for initialization
	void Start () {
		Debug.Log (GetComponent<BoxCollider> ().isTrigger);
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
			} else {
				Debug.Log ("dead");
			}
		} 
		Debug.Log(GetComponent<NavMeshAgent>().destination);
		GetComponent<NavMeshAgent> ().destination = target.transform.position;

	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("hit");

		if (other.gameObject.tag == "Tower") {
			other.gameObject.SetActive(false);
			GameObject.Destroy(other.gameObject);
			Debug.Log(GetComponent<NavMeshAgent>().destination);
		}
	}

	// Update is called once per frame
	void Update () {
		FindClosest();
	}
}
