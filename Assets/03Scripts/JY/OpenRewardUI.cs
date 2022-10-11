using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRewardUI : MonoBehaviour
{
    private RewardManager _rewardManager;   

    private void Awake()
    {
        _rewardManager = GameObject.Find("RewardManager").GetComponent<RewardManager>();        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {       
        if(coll.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.0f;
            //coll.gameObject.GetComponent<PlayerCtrl>().moveSpeed = 0.0f;
            _rewardManager.SendMessage("ItemSet", SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
    }     
}
