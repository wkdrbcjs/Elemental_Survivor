using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPButtonCtrl : MonoBehaviour
{
    public PlayerCtrl Player;

    void Update()
    {        
        if(PlayerStatus.instance.getPlayerSP() <= 0)
        {
            for(int i=1; i<7; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }           
        }
        else if(PlayerStatus.instance.getPlayerSP() >= 1)
        {
            for (int i = 1; i < 7; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }       
    }
}
