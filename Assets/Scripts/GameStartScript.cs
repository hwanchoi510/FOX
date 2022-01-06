using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            BGMscript.Instance.gameObject.GetComponent<AudioSource>().Stop();
            PlayerPrefs.SetInt("Life", 3);
            StartCoroutine(WaitSound(GameObject.Find("ButtonSound").GetComponent<AudioSource>()));
            
        }
    }

    private IEnumerator WaitSound(AudioSource sound)
    {
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        SceneManager.LoadScene("BetweenStage1");
    }
}
