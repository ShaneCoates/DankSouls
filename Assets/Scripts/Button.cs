using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button : MonoBehaviour {
    public Minion minion;
    public Minion.minionType type;
    public GameManager manager;
	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Press() {
        if (manager.souls >= (int)type) {
            int count = 1;
            if (type == Minion.minionType.eSkeleton) count = 5;
            if (type == Minion.minionType.eBurningSkull) count = 2;
            if (type == Minion.minionType.eImp) count = 2;

            for (int i = 0; i < count; ++i) {
                Minion obj = GameObject.Instantiate(minion, minion.transform.position, minion.transform.rotation) as Minion;
                obj.SetType(type);
            }
            manager.souls -= (int)type;
        }
    }
}
