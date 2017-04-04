using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public GameObject king;
    public GameObject snek;
    public float SPAWN_DELAY_SECONDS;
    public int INITIAL_POPULATION;
    public float GROWTH_RATE;

    public Scorekeeper scoreboard;

    private GameObject current;
    private CameraFix camScript;
    private float spawnDelay = 0;
    private float snekDelay = 0;

    private float startTime;

    private const float LEFT_LIMIT = -11;
    private const float RIGHT_LIMIT = 16;
    private const float SNEK_SPAWN_HEIGHT = 8;

    void Start()
    {
        camScript = GameObject.Find("Camera").GetComponent<CameraFix>();
        current = GameObject.Find("King");
        camScript.King = current;
    }

    public void restart()
    {
        foreach (var sscript in FindObjectsOfType<SnekController>()) { Destroy(sscript.gameObject); }
        startTime = Time.time;
        scoreboard.reset();
    }

    void Update () {
        if (!current)
        {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0) { spawnKing(); }
        }

        if (snekDelay <= 0)
        {
            int targetPopulation = INITIAL_POPULATION + (int)((Time.time - startTime) / GROWTH_RATE);
            int pop = FindObjectsOfType<SnekController>().Length;
            if (pop < targetPopulation) { spawnSnek(); }
            scoreboard.Population = pop + 1;
        }
        else
        {
            snekDelay -= Time.deltaTime;
        }
    }

    private void spawnSnek()
    {
        float x = Random.Range(LEFT_LIMIT, RIGHT_LIMIT);
        GameObject noob = Instantiate(snek, new Vector3(x, SNEK_SPAWN_HEIGHT, 0), Quaternion.identity);
        noob.GetComponent<SnekController>().scoreboard = scoreboard;
        snekDelay = SPAWN_DELAY_SECONDS;
    }

    private void spawnKing()
    {
        current = Instantiate(king);
        current.GetComponent<KingController>().scoreboard = scoreboard;
        camScript.King = current;
        spawnDelay = SPAWN_DELAY_SECONDS;
        restart();
    }
}
