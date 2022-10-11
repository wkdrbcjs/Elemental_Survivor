using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_WallCtrl : MonoBehaviour
{
    //private void Start(){}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<PlayerCtrl>().StopRoll(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<PlayerCtrl>().StopRoll(true);
        }
    }

    public void over()
    {
        Destroy(gameObject);
    }
}
