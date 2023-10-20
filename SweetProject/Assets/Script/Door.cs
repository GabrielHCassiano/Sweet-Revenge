using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public bool openDoor;
    public bool closeDoor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor == true)
        {
            door.gameObject.SetActive(false);
        }
        else if (openDoor == false)
        {
            door.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Invisible"))
        {
            openDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Invisible"))
        {
            openDoor = false;
        }
    }
}
