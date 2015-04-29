using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int souls = 100;
    public Text soulsText;
    float soulsPerSecond = 2;
    float timer;
    public float maxHealth;
    public float health;
    public Image healthBar;
    Vector3 originalScale;
	// Use this for initialization
	void Start () {
        maxHealth = 10;
        health = maxHealth;
        souls = 100;
        originalScale = healthBar.GetComponent<RectTransform>().transform.localScale;

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer >= (1 / soulsPerSecond)) {
            timer = 0;
            ++souls;
        }

        soulsText.text = "Souls: " + souls;

        Vector3 newScale = originalScale;
        newScale.x *= ((float)health / maxHealth);
        Debug.Log(newScale.x);
        healthBar.GetComponent<RectTransform>().transform.localScale = newScale;
	}
}
