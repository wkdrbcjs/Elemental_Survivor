using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ElementalSkill_Fire : ElementalSkill
{

    [Header("Fire")]
    public GameObject fireStrike;
    public int fireBallAmount = 4;//4 is default
    public int manaReturn = 1;

    [SerializeField]
    private GameObject PetPos;

    public AudioSource sound;
    public AudioClip Snd_FireMagic;

    protected override void Awake()
    {
        base.Awake();

    }



    #region SetUp(intro)

    protected override void SetUpLev_5()
    {
        fireBallAmount = 8;
    }

    #endregion

    public override void SkillCast()
    {
        StartCoroutine(FireSkillCast());
        

        if (ClassLevel >= requiredClassLevel[4])
        {
            buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, (int)(playerStatus.getAttackDamage()*0.3f), 0), 10.0f);
        }
    }

    IEnumerator FireSkillCast()
    {
        castable = false;
        yield return new WaitForSeconds(0.1f);
        
        for (int ballcount = 0; ballcount < fireBallAmount; ballcount++)
        {
            var instance=Instantiate(fireStrike, PetPos.transform.position, Quaternion.identity);
            FireStrike firestrike = instance.GetComponent<FireStrike>();
            SoundManager.instance.SoundPlay(SoundManager.instance.Fire_MeteorStart, 0.5f);
            //addColorToProjectile(firestrike);
            firestrike.manaReturn = manaReturn;
            
            if (ClassLevel>=requiredClassLevel[0])
            {
                firestrike.activateBurn = true;
            }
            if (ClassLevel >= requiredClassLevel[1])
            {
                firestrike.activateFlower = true;
            }
            if(ClassLevel>=requiredClassLevel[2])
            {
                firestrike.activateManaAddiction = true;
                firestrike.manaReturn = manaReturn;
            }
            if(ClassLevel>=requiredClassLevel[3])
            {
                firestrike.activateMassiveEnergy = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(castCoolTime);
        castable = true;
    }
}
