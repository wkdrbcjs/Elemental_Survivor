using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSkill_Wind : ElementalSkill
{

    [Header("Wind")]
    [SerializeField]
    private GameObject PetPos;

    public AudioSource sound;
    public AudioClip Snd_WindMagic;

    public float FadeTime = 3.0f;
    public float SpeedBuff = 0.5f;    
    
    public GameObject WindCutterBuff;
    [Header("0:Haste")]
    public float addValueSpeedBuff=1.0f;

    [Header("1:SpinAttack")]
    public GameObject SpinAttack;

    [Header("2:Windness")]
    public float attackSpeedBuff = 0.5f;

    [Header("4:WindShot")]
    public float FadeTimeAdd = 2.0f;

    [Header("5:WindShot")]
    public GameObject WindCutter;
    public GameObject Tornado;


    protected override void SetUpLev_5()
    {
        //windCutterAmount = 30;
    }




    protected override void Awake()
    {
        base.Awake();

    }

    public override void SkillCast()
    {
        StartCoroutine(WindSkillCast());
        SoundManager.instance.SoundPlay(SoundManager.instance.Wind_WindClassBuff);
        
        if (ClassLevel>=requiredClassLevel[1])
        {
            var instance = GameObject.Instantiate(SpinAttack, transform.position, Quaternion.identity);
            Projectile instanceProjectile = instance.GetComponent<Projectile>();
            addColorToProjectile(instanceProjectile);
            if (ClassLevel>=requiredClassLevel[3])
            {
                instance.GetComponent<WindSpin>().activateTornado = true;
            }
            if(ClassLevel>=requiredClassLevel[3])
            {
                instanceProjectile.baseProjectileScale *= 2;
                instanceProjectile.baseAttackDamage *= 2;
            }

        }
        if(ClassLevel >= requiredClassLevel[2])
        {
            buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, playerStatus.getAttackSpeed()*attackSpeedBuff, 0), 5.0f);
        }
        
    }

    IEnumerator WindSkillCast()
    {
        castable = false;
        float haste = SpeedBuff;

        yield return new WaitForSeconds(0.3f);
        Instantiate(WindCutterBuff, playerObject.transform);
        playerObject.GetComponents<BoxCollider2D>()[0].enabled = false;
        playerObject.GetComponents<BoxCollider2D>()[1].enabled = false;

        if(ClassLevel>=requiredClassLevel[0])
        {
            haste += addValueSpeedBuff;
        }
        
        if (ClassLevel >= requiredClassLevel[4])
        {
            FadeTime += FadeTimeAdd;
        }
        
        if (ClassLevel >= requiredClassLevel[5])
        {
            StartCoroutine(TornadoAttack());
        }

        FadeTime *= playerStatus.getDuration();

        PlayerStatus.instance.buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, 0, PlayerStatus.instance.getMovementSpeed() * haste), FadeTime);
        playerObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(FadeTime);

        Instantiate(WindCutterBuff, playerObject.transform);
        playerObject.GetComponents<BoxCollider2D>()[0].enabled = true;
        playerObject.GetComponents<BoxCollider2D>()[1].enabled = true;
        playerObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);

        yield return new WaitForSeconds(castCoolTime);
        castable = true;

    }

    

    IEnumerator TornadoAttack()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Tornado, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
