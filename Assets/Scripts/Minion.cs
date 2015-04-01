using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	public GameObject dest;
	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent>().destination = dest.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
