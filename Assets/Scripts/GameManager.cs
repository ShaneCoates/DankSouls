using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int souls;
    public Text soulsText;
    float soulsPerSecond = 2;
    float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer >= (1 / soulsPerSecond)) {
            timer = 0;
            ++souls;
        }

        soulsText.text = "Souls: " + souls;
	}
}
