using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 diff = (transform.position - target.transform.position);
        float step = 10.0f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}
}
