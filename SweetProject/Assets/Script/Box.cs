using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GameObject jogador;
    public Transform player;
    public bool canHide;
    public bool hide;
    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canHide == true)
        {
            hide = true;
            player.gameObject.SetActive(false);
            jogador.GetComponent<Player>().canHip = false; ;
        }
        else if (Input.GetKeyDown(KeyCode.E) && hide == true)
        {
            hide = false;
            jogador.GetComponent<Player>().cooldown = false;
            player.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Invisible"))
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Invisible"))
        {
            canHide = false;
        }
    }
}
