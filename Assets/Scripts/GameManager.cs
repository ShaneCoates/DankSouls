using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public Minion minion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Minion obj = GameObject.Instantiate(minion);
            obj.health = 5;
        }
	}
}
