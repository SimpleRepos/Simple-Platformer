using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour {
    public GameObject ScoreText;
    public GameObject HighScoreText;
    public GameObject PopulationText;
    private OutlinedTextScript scoreTS;
    private OutlinedTextScript hiscoreTS;
    private OutlinedTextScript popTS;

    private int score = 0;
    private int hiScore = 0;
    private int population = 0;

    public int Score
    {
        get { return score; }
        set { score = value; refresh(); }
    }

    public int Population
    {
        get { return population; }
        set { population = value; refresh(); }
    }

    public void reset()
    {
        score = 0;
        population = 0;
        refresh();
    }

    private void refresh()
    {
        scoreTS.text = "Score: " + score.ToString();
        if (score > hiScore) { hiScore = score; }
        hiscoreTS.text = "High Score: " + hiScore.ToString();
        popTS.text = "Snake Population: " + population.ToString();
    }

    private void Start()
    {
        scoreTS = ScoreText.GetComponent<OutlinedTextScript>();
        hiscoreTS = HighScoreText.GetComponent<OutlinedTextScript>();
        popTS = PopulationText.GetComponent<OutlinedTextScript>();
        refresh();
    }

}
