using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
//using LootLocker.Requests;
//using static System.TimeZoneInfo;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public BoxCollider2D box;
    private SpriteRenderer sr;

    public Leaderboard leaderboard;

    public Animator transition;
    public float transitionTime = 1f;

    public Color invisible;

    public TextMeshProUGUI barraSangue;
    public int sangue;
    public TextMeshProUGUI pontos;
    public int scoreTime;
    public int score;

    public float velocidade;
    public float inputHori;
    public float inputVert;

    public bool canKill;
    public bool canHide;
    public bool openDoor;
    public bool canIten;

    public bool habilidade;
    public bool canInvisible;
    public bool canBat;
    public Transform form;
    public bool canHip;
    public Transform luzHip;
    public bool cooldown;

    public Transform over;
    public bool morte;
    public bool die;
    public bool win;

    private GameObject inimigo;
    private GameObject cruz;
    private GameObject vitima;

    public Animator anim;
    public Animator barra;

    public AudioSource audioBat;

    public int nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        invisible = sr.color;
        sangue = 3;
        score = PlayerPrefs.GetInt("Score", 0);
        inimigo = GameObject.FindWithTag("Inimigo");
        cruz = GameObject.FindWithTag("Cruz");
        vitima = GameObject.FindWithTag("Vitima");
    }

    // Update is called once per frame
    void Update()
    {
        pontos.text = score.ToString();
        InputLogic();
        Habilidades();

        AnimationLogic();

        if (morte == true) StartCoroutine(Loss());
        if (vitima.GetComponent<Vitima>().victory == true) StartCoroutine(Win());
    }

    void FixedUpdate()
    {
        MoveLogic();
    }

    void InputLogic()
    {
        inputHori = Input.GetAxisRaw("Horizontal");
        inputVert = Input.GetAxisRaw("Vertical");

        sr.color = invisible;

        if (habilidade == true && sangue > 0 && canInvisible == false && canBat == false && canHip == false)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                sangue--;
                canInvisible = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                sangue--;
                canBat = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                sangue--;
                canHip = true;
            }
        }
        else if(habilidade == false)
        {
            cooldown = false;
            canInvisible = false;
            canBat = false;
            canHip = false;
        }
    }

    void MoveLogic()
    {
        Vector2 direcao = new Vector2(inputHori, inputVert);
        direcao = direcao.normalized;
        rb.velocity = direcao * velocidade;

    }

    void Habilidades()
    {
        if (cooldown) return;

        if (canInvisible == true)
        {
            gameObject.tag = "Invisible";
            invisible.a = 0.3f;
            StartCoroutine(CooldownHabi());
        } else if (canInvisible == false)
        {
            invisible.a = 1f;
            gameObject.tag = "Player";
        }
        if (canBat == true)
        {
            audioBat.Play();
            box.size = new Vector2(1.1f, 0.8f);
            velocidade = 6;
            form.gameObject.SetActive(true);
            StartCoroutine(CooldownHabi());
        }
        else if (canBat == false)
        {
            audioBat.Stop();
            box.size = new Vector2(0.5f, 1.5f);
            velocidade = 3;
        }
        if (canHip == true)
        {
            luzHip.gameObject.SetActive(true);
            StartCoroutine(CooldownHabi());
        }else luzHip.gameObject.SetActive(false);
    }
    IEnumerator CooldownHabi()
    {
        if(canBat == true)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            form.gameObject.SetActive(false);
        }
        cooldown = true;
        yield return new WaitForSecondsRealtime(4f);
        if (canBat == true)
        {
            form.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.3f);
            form.gameObject.SetActive(false);
        }
        cooldown = false;
        canInvisible = false;
        canBat = false;
        canHip = false;
        print(cooldown);  
    }

    void AnimationLogic()
    {
        anim.SetFloat("Horizontal", inputHori);
        anim.SetFloat("Vertical", inputVert);
        anim.SetBool("Transform", canBat);
        barra.SetInteger("Sangue", sangue);
    }

    IEnumerator Loss()
    {
        over.gameObject.SetActive(true);
        velocidade = 0;
        if (die == false) score = score + scoreTime;
        yield return new WaitForSeconds(0.00001f);
        die = true;
        //yield return leaderboard.SubmitScoreRoutine(score);
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator Win()
    {
        if (win == false) score += scoreTime;
        yield return new WaitForSeconds(0.00001f);
        win = true;
        PlayerPrefs.SetInt("Score", score);
        //if (nextLevel == 1) yield return leaderboard.SubmitScoreRoutine(score);
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(nextLevel);
        Destroy(vitima.gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Cruz"))
        {
            habilidade = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Cruz"))
        {
            habilidade = true;
        }
    }*/
}
