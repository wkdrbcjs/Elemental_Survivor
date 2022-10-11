using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{
    public static SkillManagement instance;
    public InherenceSkill _inherenceSkill;  
   
    
    [Header("속성 클래스")]
    public string ElementClass;

    [Header("[Enhanced Arrow]")]
    public GameObject EnhancedArrow;
    [SerializeField]
    private bool EnhancedArrowActive = false;

    [Header("[Elemental Missile]")]
    public bool PetAttack = false;
    public Wisp Pet;

    //[Header("스킬 리스트")]
    [Header("[Fire Ball]")]
    public GameObject FireBall;
    public bool FireBallActive = false;
    [SerializeField]
    private int FireBallAttackCount;

    [Header("[Fire Bite]")]
    public GameObject FireBite;
    [SerializeField]
    private int FireBiteAttackCount;
    public bool FireBiteActive = false;

    [Header("[Fire Beam]")]
    public GameObject FireBeam;
    [SerializeField]
    private int FireBeamAttackCount;
    public bool FireBeamActive = false;

    [Header("[Lava]")]
    public GameObject Lavas;
    public bool LavasActive = false;
    [SerializeField]
    public float Lava_Cooltime;

    [Header("[Earth Impale]")]
    public GameObject EarthImpale;
    [SerializeField]
    private float EarthImpaleCooltime;
    public bool EarthImpaleActive = false;

    [Header("[Earth Crash]")]
    public GameObject EarthCrash;
    [SerializeField]
    private int EarthCrashAttackCount;
    public bool EarthCrashActive = false;

    [Header("[Earth Saw]")]
    public GameObject EarthSaw;
    public bool EarthSawActive = false;

    [Header("[Earth Spike]")]
    public GameObject EarthSpike;
    [SerializeField]
    private float EarthSpikeCooltime;
    public bool EarthSpikeActive = false;

    [Header("[Wind Cutter]")]
    public GameObject WindCutter;
    public GameObject WindCutterBuff;
    public bool WindCutterActive = false;

    [Header("[Shadow Bolt]")]
    public GameObject ShadowBolt;
    public bool ShadowBoltActive = false;

    [Header("[Wind Slash]")]
    public GameObject WindSlash;
    [SerializeField]
    private float WindSlashCooltime;
    public bool WindSlashActive = false;

    [Header("[Tornado]")]
    public GameObject Tornado;
    public bool TornadoActive = false;


    
    //펫 상시 공격

    [Header("[Water Whip]")]
    public GameObject WaterWhip;
    [SerializeField]
    private float WaterWhipCooltime;
    public bool WaterWhipActive = false;

    [Header("[Water Splash]")]
    public GameObject WaterSplash;
    [SerializeField]
    public float WaterSplashCooltime;
    public bool WaterSplashActive = false;

    [Header("[Water Mine]")]
    public GameObject WaterMine;
    public bool WaterMineActive = false;

    [Header("[Ice Shot]")]
    public GameObject IceShot;
    public bool IceShotActive = false;
    [SerializeField]
    private float IceShot_Cooltime;

    [Header("[Thunder Storm]")]
    public GameObject ThunderStorm;
    public bool ThunderStormActive = false;
    [SerializeField]
    public float ThunderStorm_Chance;

    [Header("[Thunder Explosion]")]
    public GameObject ThunderExplosion;
    public bool ThunderExplosionActive = false;
    public int ThunderExplosion_Chance;

    [Header("[Thunder Ball]")]
    public GameObject ThunderBall;
    public bool ThunderBallActive = false;
    [SerializeField]
    private float ThunderBall_Chance;

    [Header("[Thunder Veil]")]
    public GameObject ThunderVeil;
    public bool ThunderVeilActive = false;
    [SerializeField]
    private float ThunderVeilCooltime;

    [Header("[기타]")]
    private GameObject playerObject;

    [SerializeField]
    private GameObject FirePos;

    [SerializeField]
    private GameObject FirePosPivot;

    [SerializeField]
    private GameObject PetPos;

    [SerializeField]
    private GameObject SkillCutScene;

    //private ItemInfoSet ItemInfoSet.instance;  

    public GameObject ClassSheild;
    public GameObject ClassSheildExplore;
    // FirePos = 투사체의 생성위치, FirePosPivot = 투사체가 날라가는 방향 


    private int coolTime;
    private int attackCount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //ItemInfoSet.instance = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();               

        playerObject = PlayerCtrl.playerInstance;
        
        StartCoroutine(CooltimeProcess());
    }
    public void SetElementalClass()
    {
        switch (_inherenceSkill.playerElement)
        {
            case PlayerElement.Fire:
                ElementClass = "Fire";
                break;

            case PlayerElement.Earth:
                ElementClass = "Earth";
                break;

            case PlayerElement.Wind:
                ElementClass = "Wind";
                break;

            case PlayerElement.Water:
                ElementClass = "Water";
                break;

            case PlayerElement.Thunder:
                ElementClass = "Thunder";
                break;

        }
    }
    private void SetValues()
    {
        EarthImpaleCooltime = ItemInfoSet.instance.Items[0].baseCooltime;

        WaterSplashCooltime = ItemInfoSet.instance.Items[1].baseCooltime;

        FireBallAttackCount = ItemInfoSet.instance.Items[3].AttackCount;

        ThunderBall_Chance = ItemInfoSet.instance.Items[4].Percentage;

        WaterWhipCooltime = ItemInfoSet.instance.Items[7].baseCooltime;

        FireBeamAttackCount = ItemInfoSet.instance.Items[9].AttackCount;

        ThunderExplosion_Chance = ItemInfoSet.instance.Items[10].Percentage;

        EnhancedArrowActive = playerObject.GetComponent<PlayerCtrl>().Skill_B;

        EarthCrashAttackCount = ItemInfoSet.instance.Items[12].AttackCount;

        WindSlashCooltime = ItemInfoSet.instance.Items[14].baseCooltime;

        FireBiteAttackCount = ItemInfoSet.instance.Items[15].AttackCount;

        ThunderStorm_Chance = ItemInfoSet.instance.Items[16].Percentage;

        EarthSpikeCooltime = ItemInfoSet.instance.Items[17].baseCooltime;

        IceShot_Cooltime = ItemInfoSet.instance.Items[18].baseCooltime;

        Lava_Cooltime = ItemInfoSet.instance.Items[20].baseCooltime;
        
        ThunderVeilCooltime = ItemInfoSet.instance.Items[21].baseCooltime;
    }

    public void GetInput(string input, GameObject m_gameObject)
    {
        SetValues();
        if (input == "NormalAttack")
        {
            attackCount += 1;
            if (PetAttack)
            {
                Pet.Attack();
            }
            if ((attackCount % FireBallAttackCount == 0) && (FireBallActive == true))
            {
                StartCoroutine(FireBallAttack());
            }
            if ((attackCount % FireBeamAttackCount == 0) && (FireBeamActive == true))
            {
                StartCoroutine(FireBeamAttack());
            }
            if ((attackCount % EarthCrashAttackCount == 0) && (EarthCrashActive == true))
            {
                StartCoroutine(EarthCrashAttack());
            }
            if ((attackCount % FireBiteAttackCount == 0) && (FireBiteActive == true))
            {
                StartCoroutine(FireBiteAttack());
            }
            if ((Random.Range(0,1000) <= ThunderBall_Chance) && (ThunderBallActive == true))
            {
                StartCoroutine(ThunderBallAttack());
            }           
            if (attackCount >= 100)
            {
                attackCount = 0;
            }

        }
        if (input == "Roll")
        {
            if (ShadowBoltActive == true)
            {
                StartCoroutine(ShadowBoltAttack());
            }
            if (TornadoActive == true)
            {
                StartCoroutine(TornadoAttack());
            }
            if (WaterMineActive == true)
            {
                StartCoroutine(WaterMineAttack());
            }
            if(EarthSawActive == true)
            {
                StartCoroutine(EarthSawAttack());
            }         
        }
        if (input == "RollOver")
        {
            if (WindCutterActive == true)
            {
                WindCutterBuffOn();
            }
        }
        if (input == "Enemy_Hit")
        {
            if (Random.Range(0, 1000) <= ThunderStorm_Chance && ThunderStormActive == true)
            {
                StartCoroutine(ThunderStromAtttack(m_gameObject));
            }
            if ((Random.Range(0, 1000) <= ThunderExplosion_Chance) && (ThunderExplosionActive == true))
            {
                StartCoroutine(ThunderExplosionAttack(m_gameObject));
            }
        }
        if (input == "NormalAttack_AfterDash")
        {
            if (WindCutterActive == true)
                StartCoroutine(WindCutterAttack());
        }


    }
    public IEnumerator CooltimeProcess()
    {
        SetValues();
        coolTime += 1;    

        if (ThunderVeilActive)
        {
            if (coolTime % ThunderVeilCooltime == 0)
            {
                StartCoroutine(ThunderVeilAttack());
            }
        }   

        if (IceShotActive)
        {
            if (coolTime % IceShot_Cooltime == 0)
            {
                StartCoroutine(IceShotAttack());               
            }
        }

        if (LavasActive)
        {
            if (coolTime % Lava_Cooltime == 0)
            {
                StartCoroutine(LavaAttack());
            }
        }    
        if (EarthImpaleActive)
        {
            if (coolTime % EarthImpaleCooltime == 0)
            {
                StartCoroutine(EarthImpaleAttack());
            }
        }

        if (WaterSplashActive)
        {
            if (coolTime % WaterSplashCooltime == 0)
            {
                StartCoroutine(WaterSplashAttack());
            }
        }

        if (WindSlashActive)
        {
            if (coolTime % WindSlashCooltime == 0)
            {
                StartCoroutine(WindSlashAttack());
            }
        }

        if (EarthSpikeActive)
        {
            if (coolTime % EarthSpikeCooltime == 0)
            {
                StartCoroutine(EarthSpikeAttack());
            }
        }

        if (WaterWhipActive)
        {
            if (coolTime % WaterWhipCooltime == 0)
            {
                StartCoroutine(WaterWhipAttack());
            }
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(CooltimeProcess());
    }
    IEnumerator ClassSkillCutScene()
    {
        SkillCutScene.SetActive(true);
        for (int i = 0; i < 25; i++)
        {
            SkillCutScene.transform.position -= new Vector3(40, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.2f);
        SkillCutScene.SetActive(false);
        SkillCutScene.transform.position += new Vector3(1000, 0, 0);
    }

    IEnumerator FireBallAttack()
    {        
        for (int index = 0;
          index < ItemInfoSet.instance.Items[3].ProjectileNum;
          index++)
        {
            Instantiate(FireBall, PetPos.transform.position, Quaternion.identity);          
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0f);

    }


    IEnumerator ShadowBoltAttack()
    {
        yield return new WaitForSeconds(0.1f);
        Vector2 m_RollVec = playerObject.GetComponent<PlayerCtrl>().RollVec;
        float M_RollRot = Mathf.Atan2(m_RollVec.y, m_RollVec.x) * Mathf.Rad2Deg;
        int m_PNum = ItemInfoSet.instance.Items[2].ProjectileNum;
        if (m_PNum==1)
        {
            Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot));
            if (ElementClass == ItemInfoSet.instance.Items[2].element.ToString())
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot));
            }
        }
        else if(m_PNum == 2)
        {
            Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot - 15));
            Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot + 15));

            if (ElementClass == ItemInfoSet.instance.Items[2].element.ToString())
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot - 15));
                Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot + 15));
            }
        }
        else
        {
            Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot));
            Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot - 25));
            Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot + 25));

            if (ElementClass == ItemInfoSet.instance.Items[2].element.ToString())
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot));
                Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot - 25));
                Instantiate(ShadowBolt, playerObject.transform.position, Quaternion.Euler(0f, 0f, M_RollRot + 25));
            }
        }       

        yield return new WaitForSeconds(0f);
    }
    IEnumerator WindCutterAttack()
    {
        //SoundManager.instance.SoundPlay(SoundManager.instance.Wind_Cutter);
        for (int index = 0;
            index < ItemInfoSet.instance.Items[8].ProjectileNum;
            index++)
        {
            Instantiate(WindCutter, playerObject.transform.position, WindCutter.transform.rotation);           
            yield return new WaitForSeconds(0.05f);
        }
    }
    void WindCutterBuffOn()
    {
        Instantiate(WindCutterBuff, playerObject.transform);       
    }
    IEnumerator ThunderStromAtttack(GameObject m_gameObject)
    {
        
        if (m_gameObject.gameObject != null)
        {
            
            yield return new WaitForSeconds(0.3f);
            Vector3 Pos = m_gameObject.gameObject.transform.position;
            Pos += new Vector3(0, -5, 0);
            GameObject Storm = Instantiate(ThunderStorm, Pos, Quaternion.identity);
            Projectile m_P = Storm.GetComponent<Projectile>();
            
            Storm.GetComponent<ThunderStorm>().Target = m_gameObject;
            yield return new WaitForSeconds(0.01f);
            int damage = (int)m_P.attackDamage;
            if (ElementClass == "Thunder")
            {
                damage = 2*(int)m_P.attackDamage;
                //Storm.GetComponent<BoxCollider2D>().enabled = true;
            }
            
            
            //m_gameObject.GetComponent<EnemyCtrl>().Hit(Storm, (int)m_P.attackDamage, 0.1f);
            if (Projectile.CalculateCritical(Storm.GetComponent<Projectile>().criticalChance))
            {
                //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                m_gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)(damage * m_P.criticalDamage), m_P.knockBack, m_P.CriticalColor, true);
            }
            else
            {
                //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                m_gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, damage, m_P.knockBack, m_P.DamageColor);
            }
            //if (ElementClass == "Thunder")
            //{

            //    Pos += new Vector3(0, -2, 0);
            //    Instantiate(ThunderStorm, Pos, Quaternion.identity);
            //    yield return new WaitForSeconds(0.2f);
            //    Pos += new Vector3(3, -2, 0);
            //    Instantiate(ThunderStorm, Pos, Quaternion.identity);
            //    yield return new WaitForSeconds(0.2f);
            //    Pos += new Vector3(-5.5f, -1f, 0);
            //    Instantiate(ThunderStorm, Pos, Quaternion.identity);
            //}
            //else
            //{
            //    Pos += new Vector3(0, -2, 0);
            //    Instantiate(ThunderStorm, Pos, Quaternion.identity);

            //}
            ////m_gameObject.GetComponent<MonsterAI>().Hit(this.gameObject, attackDamage / 2, knockBack / 5);
        }
    }
   
    IEnumerator ThunderVeilAttack()
    {
        GameObject Veil = Instantiate(ThunderVeil, playerObject.transform.position, playerObject.transform.rotation);     
        if (ElementClass == "Thunder")
        {
            Veil.GetComponent<ThunderVeil>().isEnhanced = true;
        }
        yield return new WaitForSeconds(0.0f);
    }
    IEnumerator ThunderBallAttack()
    {
        if(ElementClass == "Thunder")
        {
            for(int index =0; index <2; index++)
            {
                Instantiate(ThunderBall, playerObject.transform.position, ThunderBall.transform.rotation);
                yield return new WaitForSeconds(0.8f);
            }
            yield return new WaitForSeconds(0.0f);
        }
        else
        {
            Instantiate(ThunderBall, playerObject.transform.position, ThunderBall.transform.rotation);
            yield return new WaitForSeconds(0.0f);
        }
       
    }  
    IEnumerator TornadoAttack()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(Tornado, playerObject.transform.position, Tornado.transform.rotation);       
    }

    IEnumerator IceShotAttack()
    {
        for(int index=0; 
            index < ItemInfoSet.instance.Items[18].ProjectileNum;
            index++)
        {
            Instantiate(IceShot, playerObject.transform.position, IceShot.transform.rotation);           
            yield return new WaitForSeconds(0.5f);
        }               
    }

    IEnumerator LavaAttack()
    {
        Instantiate(Lavas, playerObject.transform.position, Lavas.transform.rotation);        
        yield return new WaitForSeconds(0);
    }

    IEnumerator EarthCrashAttack()
    {
        Instantiate(EarthCrash, EarthCrash.transform.position, EarthCrash.transform.rotation);        
        yield return new WaitForSeconds(0);
    }
    IEnumerator EarthImpaleAttack()
    {
        for (int index = 0;
            index < ItemInfoSet.instance.Items[0].ProjectileNum;
            index++)
        {
            Instantiate(EarthImpale, EarthImpale.transform.position, EarthImpale.transform.rotation);           
            yield return new WaitForSeconds(1.0f);
        }            
        yield return new WaitForSeconds(0);
    }
    IEnumerator EarthSawAttack()
    {
        yield return new WaitForSeconds(0.4f);
        for (int index = 0;
            index < ItemInfoSet.instance.Items[6].ProjectileNum;
            index++)
        {
            Instantiate(EarthSaw, playerObject.transform.position, Quaternion.identity);           
            yield return new WaitForSeconds(0.4f);
        }            
        yield return new WaitForSeconds(0);
    }
    IEnumerator FireBeamAttack()
    {
        for (int index = 0;
           index < ItemInfoSet.instance.Items[9].ProjectileNum;
           index++)
        {
            Instantiate(FireBeam, FireBeam.transform.position, FireBeam.transform.rotation);            
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0);
    }

    IEnumerator FireBiteAttack()
    {
        for (int index = 0;
          index < ItemInfoSet.instance.Items[15].ProjectileNum;
          index++)
        { 
            Instantiate(FireBite, playerObject.transform.position, FireBite.transform.rotation);            
            yield return new WaitForSeconds(0.4f);
        }           
    }

    IEnumerator ThunderExplosionAttack(GameObject m_gameObject)
    {
        if (m_gameObject.gameObject != null)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 Pos = m_gameObject.gameObject.transform.position;
            //Pos += new Vector3(0, -2, 0);
            for (int index = 0;
           index < ItemInfoSet.instance.Items[10].ProjectileNum;
           index++)
            {
                Instantiate(ThunderExplosion, Pos, Quaternion.identity);               
                yield return new WaitForSeconds(0.2f);
            }               
        }
    }

    IEnumerator WaterMineAttack()
    {
        for (int index = 0;
           index < ItemInfoSet.instance.Items[13].ProjectileNum;
           index++)
        {
            Instantiate(WaterMine, playerObject.transform.position+ new Vector3(Random.Range(-30,30), Random.Range(-30, 30), 0), Quaternion.identity);           
            yield return new WaitForSeconds(0.3f);
        }       
    }
    IEnumerator WaterSplashAttack()
    {
        for (int index = 0;
            index < ItemInfoSet.instance.Items[1].ProjectileNum;
            index++)
        {
            Instantiate(WaterSplash, WaterSplash.transform.position, Quaternion.identity);           
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0);       
    }

    IEnumerator WindSlashAttack()
    {
        for (int index = 0;
           index < ItemInfoSet.instance.Items[14].ProjectileNum;
           index++)
        {
            Instantiate(WindSlash, playerObject.transform.position, WindSlash.transform.rotation);           
            yield return new WaitForSeconds(0.3f);
        }           
    }
    IEnumerator EarthSpikeAttack()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(EarthSpike, playerObject.transform.position, Quaternion.identity);       
    }

    IEnumerator WaterWhipAttack()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(WaterWhip, playerObject.transform.position, WaterWhip.transform.rotation);       
    }
}

