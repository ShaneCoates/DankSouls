using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minion : MonoBehaviour {
	GameObject target;
    public Camera cam;
    public int health = 5;
    public int speed;
    public int armour;
    public int damage;
    public float cooldown = 1;
    public int cost;
    public enum minionType
    {
        eSkeleton = 5,
        eBurningSkull = 10,
        eImp = 15, 
        eDemon = 30,
        eArchDemon = 40,
    }
    public minionType type;
	// Use this for initialization
	void Awake () {
		FindClosest ();
	}

	void FindClosest() {
		GameObject[] gos;
        if (damage > 0) {
            gos = GameObject.FindGameObjectsWithTag("Tower"); 
            if(gos.Length <= 0) {
                gos = GameObject.FindGameObjectsWithTag("Finish"); 
            }
        } else {
            gos = GameObject.FindGameObjectsWithTag("Finish"); 
        }
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
        GetComponent<NavMeshAgent>().acceleration = speed;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Finish") {
			other.gameObject.SetActive(false);
			GameObject.Destroy(other.gameObject);
			FindClosest();
		}
        if(other.gameObject.tag == "Bullet") {
            health -= other.gameObject.GetComponent<Bullet>().damage;
            GameObject.Destroy(other.gameObject);
            if(health <= 0) {
                GameObject.Destroy(gameObject);
            }
        }
	}

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Tower") {
            Debug.Log("tower");
        }
    }

	// Update is called once per frame
	void Update () {
        if(!target.activeInHierarchy) {
            FindClosest();
        }               
        
        var distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0) {
                target.GetComponent<Tower>().health -= damage;
                if (target.GetComponent<Tower>().health <= 0) {
                    target.SetActive(false);
                    FindClosest();
                }
                cooldown = 0.7f;
            }
        }
	}

    public void SetType(minionType _type) {
        type = _type;
        if (type == minionType.eSkeleton)
        {
            health = 10;
            speed = 5;
            armour = 5;
            damage = 0;
            cost = 5;
        } else if (type == minionType.eBurningSkull)  {
            health = 20;
            speed = 4;
            armour = 4;
            damage = 5;
            cost = 10;
        } else if(type == minionType.eImp) {
            health = 6;
            speed = 10;
            armour = 2;
            damage = 0;
            cost = 15;
        } else if(type == minionType.eDemon) {
            health = 30;
            speed = 3;
            armour = 7;
            damage = 10;
            cost = 30;
        } else if(type == minionType.eArchDemon) {
            health = 60;
            speed = 2;
            armour = 10;
            damage = 0;
            cost = 40;
        }

        FindClosest();

    }
}
