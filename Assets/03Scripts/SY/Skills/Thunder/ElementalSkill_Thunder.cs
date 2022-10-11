using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSkill_Thunder : ElementalSkill
{
    
    [Header("Thunder")]
    [SerializeField]
    private GameObject PetPos;

    [Header("0 : Sight of Jupiter")]
    public GameObject ExecutionOfThunder;

    [Header("1 : Celeritas")]
    public float criticalBuffTime = 5.0f;
    public GameObject ChainThunder;

    [Header("2 : Bless of Dodona")]
    public float projectileScaleValue = 1.5f;
    public GameObject ThunderBall;

    [Header("3 : Cleaver of Keraunos")]
    public int projectileAmount = 12;

    [Header("4 : SharpEyes")]
    public float criticalChanceBonus = 0.3f;
    public float criticalDamageBonus = 0.5f;

    public AudioSource sound;
    public AudioClip Snd_ThunderMagic;

    //[Header("5 : Spere of Astrape")]


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log((int)ProjectileElement.Thunder);
    }
    public override void SkillCast()
    {
        StartCoroutine("SkillCast_Thunder");
        

        int count = 1;
        
        if(ClassLevel>=requiredClassLevel[3])
        {
            if(Projectile.CalculateCritical(PlayerStatus.instance.getCriticalChance()+0.3f)|| ClassLevel >= requiredClassLevel[5])
            {
                count = projectileAmount;
            }
        }
        if (ClassLevel >= requiredClassLevel[1])
        {

            if (Projectile.CalculateCritical(PlayerStatus.instance.getCriticalChance() + 0.3f) || ClassLevel >= requiredClassLevel[5])
            {
                count = 3;
            }
        }
        if (ClassLevel >= requiredClassLevel[2])
        {
            if (Projectile.CalculateCritical(PlayerStatus.instance.getCriticalChance() + 0.3f) || ClassLevel >= requiredClassLevel[5])
            {
                //count = 6;
            }
        }

        for (int i=0; i<count; i++)
        {
            Quaternion playerRotation = playerObject.GetComponent<PlayerCtrl>().getFirePosPivotTransform().rotation;
            playerRotation.eulerAngles += new Vector3(0, 0, i*(360/count));
            var instance=Instantiate(ExecutionOfThunder, transform.position, playerRotation);
            //instance.transform.parent = transform;
            ExecutionofThunder instanceProjectile= instance.GetComponent<ExecutionofThunder>();
            instanceProjectile.traceTarget = playerObject;
            addColorToProjectile(instanceProjectile);

            if (ClassLevel>=requiredClassLevel[0])
            {
                instanceProjectile.activateParalyzation = true;
            }
            
        }
        if (ClassLevel >= requiredClassLevel[0])
        {
            StartCoroutine(ThunderBallCreate(count, 1));
        }
        if (ClassLevel >= requiredClassLevel[1])
        {
            StartCoroutine(ThunderBallCreate(count, 2));
        }
        if (ClassLevel >= requiredClassLevel[2])
        {
            StartCoroutine(ThunderBallCreate(count, 3));
        }
        if (ClassLevel >= requiredClassLevel[3])
        {
            StartCoroutine(ThunderBallCreate(count, 4));
        }
        CustomStatus customStatus = new CustomStatus(0, 0, 0, 0, 0, 0, 0, 0, 0);
        if(ClassLevel>=requiredClassLevel[1])
        {
            //Debug.Log("cc buff");
            CustomStatus.SumStatus(customStatus, new CustomStatus(0, 0, 0, 0, 0, PlayerStatus.instance.getCriticalChance() / 3, PlayerStatus.instance.getCriticalChance() * 10));
             PlayerStatus.instance.buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, PlayerStatus.instance.getCriticalChance()*5, PlayerStatus.instance.getCriticalChance()*10), criticalBuffTime);
        }
        if(ClassLevel>=requiredClassLevel[4])
        {
            StartCoroutine(ThunderBallCreate(count, 5));
            CustomStatus.SumStatus(customStatus, new CustomStatus(0, 0, 0, 0, 0, 0, 0, criticalChanceBonus, criticalDamageBonus));
               //PlayerStatus.instance.buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, 0, 0, criticalChanceBonus, criticalDamageBonus), criticalBuffTime);
            //StartCoroutine(ThunderBallCreate(count));
        }
        PlayerStatus.instance.buffManager.BuffToPlayer(customStatus, criticalBuffTime);
    }

    IEnumerator SkillCast_Thunder()
    {
        castable = false;
        yield return new WaitForSeconds(castCoolTime);
        castable=true;
    }

    IEnumerator ThunderBallCreate(int m_count, int m_a)
    {
        //for(int i=0; i<2; i++)
        //{


        //    Instantiate(ThunderBall, playerObject.transform.position, ThunderBall.transform.rotation);
        //}
        
        yield return new WaitForSeconds(0.3f * m_a);
        SoundManager.instance.SoundPlay(SoundManager.instance.Thunder_ThunderClass,0.5f);
        for (int i = 0; i < m_count; i++)
        {
            Quaternion playerRotation = playerObject.GetComponent<PlayerCtrl>().getFirePosPivotTransform().rotation;
            playerRotation.eulerAngles += new Vector3(0, 0, i * (360 / m_count) +30* m_a);
            var instance = Instantiate(ExecutionOfThunder, transform.position, playerRotation);
            //instance.transform.parent = transform;
            ExecutionofThunder instanceProjectile = instance.GetComponent<ExecutionofThunder>();
            instanceProjectile.traceTarget = playerObject;
            addColorToProjectile(instanceProjectile);

            if (ClassLevel >= requiredClassLevel[0])
            {
                instanceProjectile.activateParalyzation = true;
            }
            if (ClassLevel >= requiredClassLevel[1])
            {
            }
            if (ClassLevel >= requiredClassLevel[2])
            {
                instanceProjectile.baseProjectileScale *= projectileScaleValue;
            }
            if (ClassLevel >= requiredClassLevel[4])
            {
                instanceProjectile.criticalChance += 1.0f;
            }
        }
    }


}
