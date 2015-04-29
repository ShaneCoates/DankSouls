using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public GameObject target;
    public int damage;
    public Tower.TowerType type;
    public GameObject fireParticles;
    public GameObject iceParticles;

	// Use this for initialization
	void Awake () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(target == null) {
            GameObject.Destroy(gameObject);
        }
        //Vector3 diff = (transform.position - target.transform.position);
        float step = 40.0f * Time.deltaTime;
        Vector3 newPos = Vector3.MoveTowards(transform.position, target.transform.position, step);
        newPos.y = 0.6f;
        transform.position = newPos;

        if (type == Tower.TowerType.eIce)
        {
            iceParticles.SetActive(true);
        }
        else if (type == Tower.TowerType.eFireBall)
        {
            fireParticles.SetActive(true);
        }
	}
}
