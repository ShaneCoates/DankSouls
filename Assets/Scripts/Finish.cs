using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {
    public GameManager manager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Hit() {
        manager.health--;
    }
}
