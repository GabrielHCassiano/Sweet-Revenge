using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class Inimigo : MonoBehaviour
{

    public float velocidade;

    public Transform inimigo;
    public Transform[] posInimigo;
    public int idPos;

    private GameObject jogador;
    public Transform player;

    public Light2D luz;
    public bool canKill;
    public bool kill;
    public bool canRay = true;
    public bool hip;

    public Transform roda;
    public Transform fovPoint;
    public float fovAngle;
    public float range;

    public Animator anim;
    public AudioSource audioKill;
    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Player");

        inimigo.position = posInimigo[0].position;
        idPos = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(canRay == true || kill == false)RayHit();

        MoveLogic();
        VisoDirecao();
        Iteration();
        AnimationLogic();
    }

    void MoveLogic()
    {
        inimigo.position = Vector3.MoveTowards(inimigo.position, posInimigo[idPos].position, velocidade * Time.deltaTime);

        if (inimigo.position == posInimigo[idPos].position) idPos += 1;

        if (idPos == posInimigo.Length) idPos = 0;
    }

    void VisoDirecao()
    {
        Transform visaoTransform = roda.transform;
        Vector3 inimigoPos = inimigo.position;
        Vector3 posInimPos = posInimigo[idPos].position;

        Vector3 diff = posInimPos - inimigoPos;
        float angle = Mathf.Atan2(-diff.x, diff.y) * Mathf.Rad2Deg;
        visaoTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Iteration()
    {
        if (Input.GetKeyDown(KeyCode.E) && canKill == true)
        {
            canKill = false;
            kill = true;
            audioKill.Play();
            if (jogador.GetComponent<Player>().sangue < 3)jogador.GetComponent<Player>().sangue++;
            jogador.GetComponent<Player>().score += 200;
            player.position = inimigo.position;
            velocidade = 0;
            fovPoint.gameObject.SetActive(false);
            canRay = false;
            StartCoroutine(Kill());
        }

        if (jogador.GetComponent<Player>().canHip == true && hip == true)
        {
            canRay = false;
            fovPoint.gameObject.SetActive(false);
            velocidade = 0f;
        }
        else if (jogador.GetComponent<Player>().canHip == false && hip == false && kill == false)
        {
            hip = false;
            canRay = true;
            fovPoint.gameObject.SetActive(true);
            velocidade = 2.5f;
        }
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    void RayHit()
    {
        Vector2 direcao = player.position - transform.position;
        float angle = Vector3.Angle(direcao, fovPoint.up);
        RaycastHit2D hit = Physics2D.Raycast(fovPoint.position, direcao, range);
        if(angle < fovAngle / 2 && hit.collider != null)
        {
            if(hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(fovPoint.position, direcao, Color.red);
                jogador.GetComponent<Player>().morte = true;
            }
        }
    }

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
        {
            hip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
        {
            hip = false;
        }
    }
    void AnimationLogic()
    {
        Vector2 direcao = posInimigo[idPos].position - transform.position;
        anim.SetFloat("Horizontal", direcao.x);
        anim.SetFloat("Vertical", direcao.y);
    }
}
