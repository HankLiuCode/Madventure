using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        SaveProgress();
    }

    public void UnPause()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void SaveProgress()
    {
        Character character = GameObject.Find("Player").GetComponent<Character>();
        NPC npc = GameObject.Find("NPC").GetComponent<NPC>();
        ItemManager.instance.Save();
        character.Save();
        npc.Save();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
