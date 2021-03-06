using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private AudioSource Sound;
    public static bool isPaused;
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Sound.Play();
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        BGMscript.Instance.gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
