using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	GameObject closest;
	GameObject target;
    public GameObject bullet;
    public float range;
    public float cooldown;
    int maxHealth;
    public int health;
    public int damage;
    public Sprite lightningSprite;
    public Sprite laserSprite;
    public Sprite fireBallSprite;
    public Sprite iceSprite;
    public GameObject sphere;
    public enum TowerType
    {
        eLaser = 0,
        eLightning = 1,
        eFireBall = 2,
        eIce = 3
    }
    public TowerType type;
    float timer;
	// Use this for initialization
	void Awake () {
        int t = Random.Range(0, 4);
        SetType((TowerType)t);
        maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		target = FindClosest ();
        if(target != null) {
		    Vector3 targetDir = target.transform.position - transform.position;
            targetDir.y = 0;
            if (timer > cooldown) {
                print(range);
                print(targetDir.sqrMagnitude);
                if ((range * 10) >= targetDir.sqrMagnitude) {
                    Shoot();
                    timer = 0;
                }
            }
        }
        GetComponent<Renderer>().material.color = new Vector4(1, ((float)health / maxHealth), ((float)health / maxHealth), 1); 
	}

    void OnMouseEnter() {
        sphere.SetActive(true);
    }

    void OnMouseExit()
    {
        sphere.SetActive(false);
    }

    void Shoot() {
        GameObject g = GameObject.Instantiate(bullet);
        g.GetComponent<Bullet>().target = target;
        g.transform.position = transform.position;
        g.GetComponent<Bullet>().damage = damage;
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

    public void SetType(TowerType _type) {
        type = _type;
        if(type == TowerType.eLaser) {
            GetComponent<SpriteRenderer>().sprite = laserSprite;
            cooldown = 0.25f;
            health = 50;
            range = 150;
            damage = 1;
        } else if(type == TowerType.eLightning) {
            GetComponent<SpriteRenderer>().sprite = lightningSprite;
            cooldown = 1f;
            health = 90;
            range = 180;
            damage = 4;
        } else if (type == TowerType.eFireBall) {
            GetComponent<SpriteRenderer>().sprite = fireBallSprite;
            cooldown = 1.5f;
            health = 80;
            range = 300;
            damage = 1;
        } else if(type == TowerType.eIce) {
            GetComponent<SpriteRenderer>().sprite = iceSprite;
            cooldown = 2f;
            health = 50;
            range = 75;
            damage = 3;
        }
        Vector3 sphereScale = new Vector3(range * 0.07f, range * 0.07f, 0);
        sphere.transform.localScale = sphereScale;
        sphere.SetActive(false);
        maxHealth = health;
    }
}
