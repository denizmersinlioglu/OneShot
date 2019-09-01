using UnityEngine;
using System.Collections;

public class GameManager: MonoBehaviour {

    private BaseProjectile Ball {
        get { return FindObjectOfType<BaseProjectile>(); }
    }

    private BaseTarget[] Targets {
        get { return FindObjectsOfType<BaseTarget>(); }
    }

    // Use this for initialization
    void Start() {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update() {

    }
}
