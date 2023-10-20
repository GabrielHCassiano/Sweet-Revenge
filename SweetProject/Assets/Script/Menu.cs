using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class Menu : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f;

    public void jogar()
    {
        StartCoroutine(LoadLevel("Level1"));
        //SceneManager.LoadScene("Game");
    }

    public void VoltarMenu()
    {
        StartCoroutine(Voltar());
    }

    public void Sair()
    {
        Application.Quit();
    }

    IEnumerator Voltar()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator LoadLevel(string level)
    {
        PlayerPrefs.SetInt("Score", 0);
        anim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(level);
    }
}
