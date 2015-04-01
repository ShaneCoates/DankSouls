using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	GameObject closest;
	GameObject target;
    public GameObject bullet;
	float speed = 3f;
    public float range;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		target = FindClosest ();
		Vector3 targetDir = target.transform.position - transform.position;
        targetDir.y = 0;
        if (range >= targetDir.sqrMagnitude) {
            Shoot();
        }
		float step = speed * Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0f);
		Debug.DrawRay (transform.position, newDir, Color.red);

		transform.rotation = Quaternion.LookRotation (newDir);


        if(Input.GetKeyDown(KeyCode.A)) {
            Shoot();
        }

	}

    void Shoot() {
        GameObject g = GameObject.Instantiate(bullet);
        g.GetComponent<Bullet>().target = target;
        g.transform.position = transform.position;
    }

	GameObject FindClosest() {
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
        return closest;
	}
}
