using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Transform pause;
    private GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindWithTag("Game");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause.gameObject.activeSelf)
            {
                game.SetActive(true);
                pause.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                game.SetActive(false);
                pause.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeGame()
    {
        game.SetActive(true);
        pause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
