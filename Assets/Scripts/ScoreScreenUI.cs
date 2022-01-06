using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScreenUI : MonoBehaviour
{
    private Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GameObject.Find("ScoreNumber").GetComponent<Text>();

        ScoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetInt("Life", 3);
            PlayerPrefs.SetInt("Score", 0); 
            StartCoroutine(WaitSound(GameObject.Find("ButtonSound").GetComponent<AudioSource>()));
            BGMscript.Instance.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator WaitSound(AudioSource sound)
    {
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        SceneManager.LoadScene("Main Menu");
    }
}
