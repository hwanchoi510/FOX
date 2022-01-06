using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TextPage : MonoBehaviour
{
    private Button ReturnButton;
    // Start is called before the first frame update
    void Start()
    {
        ReturnButton = GameObject.Find("Return").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            StartCoroutine(WaitSound(GameObject.Find("ButtonSound").GetComponent<AudioSource>()));
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private IEnumerator WaitSound(AudioSource sound)
    {
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        ReturnToMenu();
    }
}
