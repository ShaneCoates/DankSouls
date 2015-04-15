using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minion : MonoBehaviour {
	GameObject target;
    public Camera cam;
    public int health = 5;
	// Use this for initialization
	void Start () {
		FindClosest ();
	}

	void FindClosest() {
		GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Finish"); 
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
		if (other.gameObject.tag == "Finish") {
			other.gameObject.SetActive(false);
			GameObject.Destroy(other.gameObject);
			FindClosest();
		}
        if(other.gameObject.tag == "Bullet") {
            health -= 1;
            Debug.Log(health);
            GameObject.Destroy(other.gameObject);
            if(health <= 0) {
                GameObject.Destroy(gameObject);
            }
        }
	}

	// Update is called once per frame
	void Update () {
    

	}
}
