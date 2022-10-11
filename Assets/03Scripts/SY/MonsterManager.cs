using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance;

    [Header("Reward OR Mimic")]
    public GameObject ItemChestOBJ;
    public GameObject MimicOBJ;

    [Header("Effect")]
    public GameObject Freeze_Eft;
    public GameObject Slow_Eft;
    public GameObject Stun_Eft;
    public GameObject MonsterHit_Eft;

    [Header("DamageFont")]
    public GameObject DamageFont;
    
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;

        }
    }

}
