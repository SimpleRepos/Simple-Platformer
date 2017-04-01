using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public GameObject king;
    public GameObject snek;
    public float SPAWN_DELAY_SECONDS;
    public float snekPopulation;

    private GameObject current;
    private CameraFix camScript;
    private float spawnDelay = 0;

    private const float LEFT_LIMIT = -11;
    private const float RIGHT_LIMIT = 16;
    private const float SNEK_SPAWN_HEIGHT = 8;

    void Start()
    {
        camScript = GameObject.Find("Camera").GetComponent<CameraFix>();
        current = GameObject.Find("King");
        camScript.King = current;
        //Time.timeScale = 0.25f;
    }

    void Update () {
        if (!current)
        {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0) { doSpawn(); }
        }

        if (FindObjectsOfType<SnekController>().Length < snekPopulation) { spawnSnek(); }
    }

    private void spawnSnek()
    {
        float x = Random.Range(LEFT_LIMIT, RIGHT_LIMIT);
        Instantiate(snek, new Vector3(x, SNEK_SPAWN_HEIGHT, 0), Quaternion.identity);
    }

    private void doSpawn()
    {
        current = Instantiate(king);
        camScript.King = current;
        spawnDelay = SPAWN_DELAY_SECONDS;
    }
}
