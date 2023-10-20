using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Vitima : MonoBehaviour
{
    public Leaderboard leaderboard;

    public Animator transition;
    public float transitionTime = 1f;

    private GameObject jogador;
    public Transform player;

    //public int scoreTime;

    public bool canKill;
    public Light2D luz;

    public bool victory;
    public int nextLevel;
    public bool win;

    public AudioSource audioKill;
    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canKill == true)
        {
            audioKill.Play();
            if (jogador.GetComponent<Player>().sangue < 5) jogador.GetComponent<Player>().sangue++;
            jogador.GetComponent<Player>().score += 200;
            player.position = transform.position;
            victory = true;
        }
       /* if(victory == true)
        {
            StartCoroutine(Win());
        }*/
    }
    /*IEnumerator Win()
    {
        int scores = jogador.GetComponent<Player>().score;
        if (win == false) scores += scoreTime;
        yield return new WaitForSeconds(0.00001f);
        win = true;
        PlayerPrefs.SetInt("Score", scores);
        if(nextLevel == 0) yield return leaderboard.SubmitScoreRoutine(scores);
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(nextLevel);
        Destroy(gameObject);
    }*/

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Invisible"))
        {
            luz.intensity = 1;
            canKill = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Invisible"))
        {
            luz.intensity = 0;
            canKill = false;
        }
    }
}
