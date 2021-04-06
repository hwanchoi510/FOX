using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    private Text TimeText;
    private Text ScoreText;
    private Text LifeText;
    private int numCollectibles;
    [HideInInspector]public static int Score;
    public static int Life;
    [SerializeField] private float RemainingTime;

    // Start is called before the first frame update
    void Start()
    {
        TimeText = GameObject.Find("TimeNumber").GetComponent<Text>();
        ScoreText = GameObject.Find("ScoreNumber").GetComponent<Text>();
        LifeText = GameObject.Find("LifeNumber").GetComponent<Text>();
        numCollectibles = GameObject.Find("Collectibles").transform.childCount;
        Score = PlayerPrefs.GetInt("Score");
        Life = PlayerPrefs.GetInt("Life");
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();

        RemainingTime -= Time.deltaTime;
        TimeText.text = ((int)RemainingTime).ToString();
        numCollectibles = GameObject.Find("Collectibles").transform.childCount;

        if(numCollectibles == 0)
        {
            Score = Score + (int)RemainingTime * 10000;
            PlayerPrefs.SetInt("Score", Score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(RemainingTime <= 0)
        {
            PlayerPrefs.SetInt("Score", Score);
            SceneManager.LoadScene("GameOver");
        }

        LifeText.text = Life.ToString();
    }
}
