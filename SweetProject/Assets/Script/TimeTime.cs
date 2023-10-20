using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using Unity.VisualScripting;

public class TimeTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float currentTime = 0f;

    public int pontos = 200;
    private GameObject jogador;
    private GameObject vitima;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        vitima = GameObject.FindWithTag("Vitima");
    }

    // Update is called once per frame
    void Update()
    {
        TimeUp();
    }

    void TimeUp()
    {
        currentTime += 1 * Time.deltaTime;
        timeText.text = currentTime.ToString("0");

        if (currentTime >= 180f)
        {
            pontos = 1;
        }

        if (vitima.GetComponent<Vitima>().victory == true && currentTime < 180)
        {
            pontos = 540 - (int)currentTime * 3;
            //currentTime = 0;
            jogador.GetComponent<Player>().scoreTime = pontos;
            print(pontos);
        }
    }
}