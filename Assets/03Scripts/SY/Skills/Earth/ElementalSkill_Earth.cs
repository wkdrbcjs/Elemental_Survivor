using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSkill_Earth : ElementalSkill
{
    [Header("Earth")]
    public bool overlapAble = false;//방어수 중첩가능여부
    public int ShieldCount = 1000;
    public GameObject ironWall;
    public GameObject ironWallKnockBack;
    public bool IronWallDamage = false;

    [Header("Buffs")]//각각을 만드는게 아니라 그냥 각 스킬레벨별로 배열을 쭉 만들고 ==null을 붙이고 안붙이고로 정하면 좋을 것 같음
    public GameObject ironWallMS_Buff = null;
    public GameObject ironWallAS_Buff = null;//ironwall이 활성화된 상태에서 계속 부여되는 버프


    [SerializeField]
    private GameObject PetPos;

    public AudioSource sound;
    public AudioClip Snd_EarthMagic;


    public GameObject ClassShield;
    public GameObject ShieldExplore;
    public GameObject ShieldExploreAdd;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        if (ShieldCount >= 1)
        {
            ironWallAS_Buff = buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, (playerStatus.lastingStatus.attackSpeed * 0.3f), 0), float.MaxValue);
        }
    }

    public override bool Damaged()
    {
        bool changeOption = false;
        
        if (true)
        {
            if (ShieldCount <= 0)
            {
                ironWall.SetActive(false);
            }
            else if (ShieldCount > 0)
            {

                changeOption = true;
                if (IronWall.instance == null)
                {
                    //Debug.Log("shield EXP");
                    ShieldCount--;
                    Instantiate(ironWallKnockBack, playerObject.transform);
                    SoundManager.instance.SoundPlay(SoundManager.instance.Earth_EarthClass);
                    //PetPos.GetComponent<PetCtrl>().PetAttack();
                    //PetPos.GetComponent<Wisp>().
                    //Instantiate(ShieldExplore, playerObject.transform);
                }
                else return true;//실드발동중이면 아래의 버프들은 생기지 않음

                if(ShieldCount<=0)
                {
                    ironWall.SetActive(false);
                }

                //Instantiate(ironWall, transform, false);
                if (ClassLevel >= requiredClassLevel[0])
                {
                    if (ironWallAS_Buff != null && ShieldCount == 0)
                    {
                        Destroy(ironWallAS_Buff);
                        ironWallAS_Buff = null;
                    }
                }

                if (ClassLevel >= requiredClassLevel[1])
                {
                    if (ironWallMS_Buff == null)
                    {
                        ironWallMS_Buff = buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, 0, PlayerStatus.instance.lastingStatus.movementSpeed * 0.5f), 3.0f * PlayerStatus.instance.duration);
                    }

                    //Debug.Log("hp회복");
                }
                else return changeOption;

                if (ClassLevel >= requiredClassLevel[2])
                {
                    PlayerStatus.instance.addMaxHP(5,true);

                }
                else return changeOption;

                if (ClassLevel >= requiredClassLevel[3])
                {
                    //Debug.Log("공격력 상승");
                    //PlayerStatus.instance.attackDamage *= 2;
                    buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 10, 0, 0), 3.0f * PlayerStatus.instance.duration);
                }
                else return changeOption;

                if (ClassLevel >= requiredClassLevel[4])
                {
                    PlayerStatus.instance.addCurrentHP(10);
                    buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 10, 0, 0, 0), 2.0f * PlayerStatus.instance.duration);
                }
                else return changeOption;

                if (ClassLevel >= requiredClassLevel[5])
                {
                    StartCoroutine(AddtionalBash());
                        
                }
            }
        }

       


        return changeOption;
    }

    IEnumerator AddtionalBash()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 15; i++)
        {
            Instantiate(ShieldExploreAdd, transform.position,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0f,0.1f));
        }
    }
    #region SetUp(intro)
    protected override void SetUpLev_5()
    {
        overlapAble = true;
    }

    protected override void SetUpLev_2()
    {
        //Debug.Log("damageon");
        //밀쳐내기 데미지
        IronWallDamage = true;
    }


    #endregion

    public override void SkillCast()
    {
        StartCoroutine(EarthSkillCast());
        SoundManager.instance.SoundPlay(SoundManager.instance.Earth_EarthClass);

        if (ShieldCount < 1 || overlapAble)
        {
            ShieldCount += 1;
            if (ironWallAS_Buff == null)
            {
                ironWallAS_Buff = buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, PlayerStatus.instance.getAttackSpeed() * +0.3f, 0), float.MaxValue);
            }
            ironWall.SetActive(true);
        }
    }

    IEnumerator EarthSkillCast()
    {
        castable = false;
        //PetPos.GetComponent<PetCtrl>().PetAttack();
        var instance = Instantiate(ShieldExplore, playerObject.transform);
        playerObject.GetComponent<PlayerCtrl>().isNoHitTime = true;
        yield return new WaitForSeconds(0.5f);
        ClassShield.SetActive(true);
        //playerObject.GetComponent<PlayerCtrl>().shieldOn = true;
        playerObject.GetComponent<PlayerCtrl>().isNoHitTime = false;
        yield return new WaitForSeconds(castCoolTime);
        castable = true;
    }


}
