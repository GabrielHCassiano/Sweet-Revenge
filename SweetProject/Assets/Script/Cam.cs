using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Cam : MonoBehaviour
{

    private GameObject jogador;
    public Transform player;

    public float currentTime = 0f;
    public float timeBegin;
    public float timeEnd;
    public bool lado;

    public Transform fovPoint;
    public float fovAngle;
    public float range;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RayHit();

        MoveLogic();
    }

    void MoveLogic()
    {

        if (lado == false) currentTime += 1 * Time.deltaTime;

        if (currentTime >= timeEnd) 
        {
            lado = true;
            currentTime = timeEnd;
        }
        if (lado == true) currentTime -= 1 * Time.deltaTime;

        if (currentTime <= timeBegin)
        {
            lado = false;
            currentTime = timeBegin;
        }

        anim.SetFloat("move", currentTime);
        fovPoint.rotation = Quaternion.AngleAxis(45f * currentTime, Vector3.forward);
    }

    void RayHit()
    {
        Vector2 direcao = player.position - transform.position;
        float angle = Vector3.Angle(direcao, fovPoint.up);
        RaycastHit2D hit = Physics2D.Raycast(fovPoint.position, direcao, range);
        if (angle < fovAngle / 2 && hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(fovPoint.position, direcao, Color.red);
                jogador.GetComponent<Player>().morte = true;
            }
        }
    }
}
