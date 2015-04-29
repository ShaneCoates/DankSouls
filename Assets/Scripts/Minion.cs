using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minion : MonoBehaviour {
    public GameObject manager;
	GameObject target;
    public int health = 5;
    public int speed;
    public int armour;
    public int damage;
    public float cooldown = 1;
    public int cost;
    public Sprite side;
    public Sprite up;
    public Sprite down;
    Vector3 lastPos;
    Vector3 theScale;
    float normalSpeed;
    float slowCooldown;
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
        GetComponent<NavMeshAgent>().updateRotation = false;
        theScale = transform.localScale;
        normalSpeed = GetComponent<NavMeshAgent>().speed;
        slowCooldown = 0f;
        //transform.rotation = new Quaternion(90, transform.rotation.y, transform.rotation.z, 1);
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
    	GetComponent<NavMeshAgent>().destination = target.transform.position;
        GetComponent<NavMeshAgent>().acceleration = speed;
        GetComponent<NavMeshAgent>().speed = speed;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Finish") {
			GameObject.Destroy(this.gameObject);
            other.gameObject.GetComponent<Finish>().Hit();
		}
        if(other.gameObject.tag == "Bullet") {
            health -= other.gameObject.GetComponent<Bullet>().damage;
            GameObject.Destroy(other.gameObject);
            if(health <= 0) {
                GameObject.Destroy(gameObject);
            }
            if (other.gameObject.GetComponent<Bullet>().type == Tower.TowerType.eIce) {
                GetComponent<NavMeshAgent>().speed -= 1;
                slowCooldown = 0.5f;
            }
        }
	}

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Tower" && damage > 0) {
            if (cooldown <= 0) {
                target.GetComponent<Tower>().health -= damage;
                cooldown = 0.7f;
            }   
        }
    }

	// Update is called once per frame
	void Update () {
        if (damage > 0)
        {
            if (!target.activeInHierarchy)
            {
                FindClosest();
                Debug.Log("it died");
            }
            else
            {
                if (target.GetComponent<Tower>().health <= 0)
                {
                    target.GetComponent<Tower>().Kill();
                    FindClosest();
                }
            }
        }
        slowCooldown -= Time.deltaTime;
        if (slowCooldown <= 0) {
            GetComponent<NavMeshAgent>().speed = normalSpeed;
        }
        cooldown -= Time.deltaTime;

        
        Vector3 heading = lastPos - transform.position;
        float dist = heading.magnitude;
        Vector3 direction = heading / dist;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z)) {
           //Side
            if(direction.x >= 0) {
                GetComponent<SpriteRenderer>().sprite = side;
                transform.localScale = theScale;
            } else {
                GetComponent<SpriteRenderer>().sprite = side;
                Vector3 newScale = theScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
        } else {
            if (direction.z >= 0) {
                GetComponent<SpriteRenderer>().sprite = up;
            } else {
                GetComponent<SpriteRenderer>().sprite = down;
            }
        }
        lastPos = transform.position;
	}

    public void SetType(minionType _type) {
        type = _type;
        if (type == minionType.eSkeleton)
        {
            health = 10;
            speed = 50;
            armour = 5;
            damage = 0;
            cost = 5;
        } else if (type == minionType.eBurningSkull)  {
            health = 20;
            speed = 40;
            armour = 4;
            damage = 5;
            cost = 10;
        } else if(type == minionType.eImp) {
            health = 6;
            speed = 100;
            armour = 2;
            damage = 0;
            cost = 15;
        } else if(type == minionType.eDemon) {
            health = 30;
            speed = 30;
            armour = 7;
            damage = 10;
            cost = 30;
        } else if(type == minionType.eArchDemon) {
            health = 60;
            speed = 20;
            armour = 10;
            damage = 0;
            cost = 40;
        }

        FindClosest();

    }
}
