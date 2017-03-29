using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public GameObject king;
    public float SPAWN_DELAY_SECONDS;

    private GameObject current;
    private CameraFix camScript;
    private float spawnDelay = 0;

    void Start()
    {
        camScript = GameObject.Find("Camera").GetComponent<CameraFix>();
    }

    void Update () {
        if (!current)
        {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0) { doSpawn(); }
        }
    }

    private void doSpawn()
    {
        camScript.King = current = Instantiate(king);
        spawnDelay = SPAWN_DELAY_SECONDS;
    }
}
