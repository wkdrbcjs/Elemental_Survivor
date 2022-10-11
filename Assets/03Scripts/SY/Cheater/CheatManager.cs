using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public PlayerCtrl _PlayerCtrl;
    public SpawnerController _SpawnerController;
    [Header("TimeRush")]
    public GameManager gameManager;
    private bool DownQ;
    private bool DownE;

    [Header("Q-E")]
    public int RushValue=2;


    private bool Down1;
    private bool Down2;
    private bool Down3;
    private bool Down4;

    [Header("BuffTime")]
    public float BuffTime;


    [Header("HP:1, MP:2, AD-AS-MS-3")]
    public int AD_Value = 100;
    public float AS_Value = 0.5f;
    public float MS_Value = 5;

    [Header("CC-CD-4")]
    public float CC_Value = 0.3f;
    public float CD_Value = 0.5f;

    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {
        GetInput();
        TimeRush();
        Buff();
    }
    void GetInput()
    {
        DownQ = Input.GetKeyDown(KeyCode.Q);
        DownE = Input.GetKeyDown(KeyCode.E);

        Down1 = Input.GetKeyDown(KeyCode.Alpha1);
        Down2 = Input.GetKeyDown(KeyCode.Alpha2);
        Down3 = Input.GetKeyDown(KeyCode.Alpha3);
        Down4 = Input.GetKeyDown(KeyCode.Alpha4);
        if (Input.GetKeyDown(KeyCode.Z))// 클레스 레벨 30증가
        {
            DataSet.instance.CheatLevelUp();
        }
        if (Input.GetKeyDown(KeyCode.X))// 경험치 대량 획득
        {
            DataSet.instance.CheatExpBoost();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))// 모든아이템 획득 
        {
            
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            _PlayerCtrl.EnemyCollised(9999);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            gameManager.StartCoroutine(gameManager.FoundObjects());
            gameManager.isBossDie = true;
            DataSet.instance.killedBossSum += 1;
            int m_count = PlayerPrefs.GetInt("s_isBossDie");
            m_count += 1;
            PlayerPrefs.SetInt("s_isBossDie", m_count);
            gameManager.uiManager.GameClearUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ItemEnhance();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ItemEnhanceFire();
            ItemEnhanceAll();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ItemEnhanceEarth();
            ItemEnhanceAll();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ItemEnhanceWind();
            ItemEnhanceAll();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ItemEnhanceWater();
            ItemEnhanceAll();
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ItemEnhanceThunder();
            ItemEnhanceAll();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            //_SpawnerController.temp.GetComponent<EnemyPoolCtrl>().EnemyPool_change_Cheat();
        }
    }

    void TimeRush()
    {
        if(DownQ)
        {
            gameManager.TimeRush /=RushValue;
        }
        else if(DownE)
        {
            gameManager.TimeRush *= RushValue;
        }
    }
    
    void Buff()
    {
        if(Down1)
        {
            //PlayerStatus.instance.buffManager.BuffToPlayer(new CustomStatus(10, 0, 0, 0, 0, 0, 0), BuffTime);

            PlayerStatus.instance.addMaxHP(10);
            PlayerStatus.instance.addCurrentHP(PlayerStatus.instance.getMaxHP()/3);
        }
        if(Down2)
        {
            PlayerStatus.instance.addMaxMP(10);
            PlayerStatus.instance.addCurrentMP(PlayerStatus.instance.getMaxMP()/3);
        }
        if(Down3)//데미지 공속 이속 상승 
        {
            PlayerStatus.instance.buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, AD_Value, AS_Value, MS_Value), BuffTime);
        }
        if(Down4)//치명타율 치명타데미지 상승
        {
            PlayerStatus.instance.buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, 0, 0,CC_Value,CD_Value), BuffTime);
        }
    }
    void ItemEnhanceFire()
    {
        SkillManagement.instance.FireBeamActive = true;
        //SkillManagement.instance.FireBallActive = true;
        SkillManagement.instance.FireBiteActive = true;
        SkillManagement.instance.LavasActive = true;
    }

    void ItemEnhanceEarth()
    {
        SkillManagement.instance.EarthImpaleActive = true;
        SkillManagement.instance.EarthSawActive = true;
        SkillManagement.instance.EarthCrashActive = true;
        SkillManagement.instance.EarthSpikeActive = true;
    }

    void ItemEnhanceWind()
    {
        SkillManagement.instance.ShadowBoltActive = true;
        SkillManagement.instance.WindCutterActive = true;
        SkillManagement.instance.WindSlashActive = true;
        SkillManagement.instance.TornadoActive = true;
    }

    void ItemEnhanceWater()
    {
        SkillManagement.instance.WaterSplashActive = true;
        SkillManagement.instance.WaterMineActive = true;
        SkillManagement.instance.WaterWhipActive = true;
        SkillManagement.instance.IceShotActive = true;
    }

    void ItemEnhanceThunder()
    {
        SkillManagement.instance.ThunderBallActive = true;
        SkillManagement.instance.ThunderExplosionActive = true;
        SkillManagement.instance.ThunderStormActive = true;
        SkillManagement.instance.ThunderVeilActive = true;
    }
    void ItemEnhanceAll()
    {
        for (int index = 0; index < ItemInfoSet.instance.Items.Count - 1; index++)
        {
            ItemInfoSet.instance.Items[index].ItemLevel = 9;
            switch (index)
            {
                case 0:
                    ItemInfoSet.instance.Items[0].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[0].ProjectileNum += 3;
                    ItemInfoSet.instance.Items[0].baseCooltime = 12.0f;
                    break;
                case 1:
                    ItemInfoSet.instance.Items[1].baseAttackDamage += 30;
                    ItemInfoSet.instance.Items[1].baseCooltime -= 4;
                    ItemInfoSet.instance.Items[1].ProjectileNum += 2;
                    break;
                case 2:
                    ItemInfoSet.instance.Items[2].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[2].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[2].baseProjectileScale = 16.0f;
                    ItemInfoSet.instance.Items[2].basePenetration += 1;
                    break;
                case 3:
                    ItemInfoSet.instance.Items[3].baseAttackDamage += 30;
                    ItemInfoSet.instance.Items[3].baseProjectileScale *= 1.25f * 1.25f;
                    ItemInfoSet.instance.Items[3].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[3].AttackCount -= 1;
                    break;
                case 4:
                    ItemInfoSet.instance.Items[4].baseAttackDamage += 15;
                    ItemInfoSet.instance.Items[4].Percentage += 200;
                    break;
                case 5:
                    break;
                case 6:
                    ItemInfoSet.instance.Items[6].baseAttackDamage += 12;
                    ItemInfoSet.instance.Items[6].ProjectileNum += 3;
                    ItemInfoSet.instance.Items[6].baseCooltime = 4.0f;
                    break;
                case 7:
                    ItemInfoSet.instance.Items[7].baseAttackDamage += 40;
                    ItemInfoSet.instance.Items[7].ProjectileNum += 3;
                    break;
                case 8:
                    ItemInfoSet.instance.Items[8].ProjectileNum += 7;
                    ItemInfoSet.instance.Items[8].baseAttackDamage += 7;
                    break;
                case 9:
                    ItemInfoSet.instance.Items[9].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[9].AttackCount -= 2;
                    ItemInfoSet.instance.Items[9].ProjectileNum += 2;
                    break;
                case 10:
                    ItemInfoSet.instance.Items[10].Percentage += 200;
                    ItemInfoSet.instance.Items[10].ProjectileNum += 4;
                    ItemInfoSet.instance.Items[10].baseAttackDamage += 15;
                    ItemInfoSet.instance.Items[10].baseProjectileScale *= 1.5f * 1.5f;
                    break;
                case 11:
                    GameObject.Find("Player").GetComponent<PlayerCtrl>().Skill_B = true;
                    break;
                case 12:
                    ItemInfoSet.instance.Items[12].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[12].baseProjectileScale *= 1.5f * 1.5f * 1.5f;
                    ItemInfoSet.instance.Items[12].AttackCount -= 2;
                    break;
                case 13:
                    ItemInfoSet.instance.Items[13].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[13].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[13].baseDuration -= 2;
                    ItemInfoSet.instance.Items[13].baseProjectileScale *= 1.5f;
                    break;
                case 14:
                    ItemInfoSet.instance.Items[14].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[14].ProjectileNum += 5;
                    break;
                case 15:
                    ItemInfoSet.instance.Items[15].baseAttackDamage += 10;
                    ItemInfoSet.instance.Items[15].AttackCount -= 2;
                    ItemInfoSet.instance.Items[15].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[15].baseDuration += 2.0f;
                    break;
                case 16:
                    ItemInfoSet.instance.Items[16].baseAttackDamage += 30;
                    ItemInfoSet.instance.Items[16].Percentage += 200;
                    break;
                case 17:
                    ItemInfoSet.instance.Items[17].baseAttackDamage += 3;
                    ItemInfoSet.instance.Items[17].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[17].baseProjectileScale *= 1.25f;
                    break;
                case 18:
                    ItemInfoSet.instance.Items[18].baseAttackDamage += 15;
                    ItemInfoSet.instance.Items[18].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[18].baseCooltime -= 2.0f;
                    break;
                case 19:
                    ItemInfoSet.instance.Items[19].baseAttackDamage += 3;
                    ItemInfoSet.instance.Items[19].baseProjectileScale += 6.0f;
                    ItemInfoSet.instance.Items[19].baseCooltime -= 0.1f;
                    break;
                case 20:
                    ItemInfoSet.instance.Items[20].baseAttackDamage += 4;
                    ItemInfoSet.instance.Items[20].baseProjectileScale *= 1.25f * 1.25f;
                    ItemInfoSet.instance.Items[20].baseDuration += 4.0f;
                    break;
                case 21:
                    ItemInfoSet.instance.Items[21].baseAttackDamage += 10;
                    ItemInfoSet.instance.Items[21].baseDuration += 4.0f;
                    ItemInfoSet.instance.Items[21].baseProjectileScale *= 1.5f;
                    break;
            }
        }
    }
    void ItemEnhance()
    {
        SkillManagement.instance.FireBallActive = true;
        SkillManagement.instance.FireBeamActive = true;
        SkillManagement.instance.FireBiteActive = true;
        SkillManagement.instance.LavasActive = true;

        SkillManagement.instance.EarthImpaleActive = true;
        SkillManagement.instance.EarthSawActive = true;
        SkillManagement.instance.EarthCrashActive = true;
        SkillManagement.instance.EarthSpikeActive = true;

        SkillManagement.instance.ShadowBoltActive = true;
        SkillManagement.instance.WindCutterActive = true;
        SkillManagement.instance.WindSlashActive = true;
        SkillManagement.instance.TornadoActive = true;

        SkillManagement.instance.WaterSplashActive = true;
        SkillManagement.instance.WaterMineActive = true;
        SkillManagement.instance.WaterWhipActive = true;
        SkillManagement.instance.IceShotActive = true;

        SkillManagement.instance.ThunderBallActive = true;
        SkillManagement.instance.ThunderExplosionActive = true;
        SkillManagement.instance.ThunderStormActive = true;
        SkillManagement.instance.ThunderVeilActive = true;

        for (int index = 0; index < ItemInfoSet.instance.Items.Count - 1; index++)
        {
            ItemInfoSet.instance.Items[index].ItemLevel = 9;
            switch (index)
            {
                case 0:
                    ItemInfoSet.instance.Items[0].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[0].ProjectileNum += 3;
                    ItemInfoSet.instance.Items[0].baseCooltime = 12.0f;
                    break;
                case 1:
                    ItemInfoSet.instance.Items[1].baseAttackDamage += 30;
                    ItemInfoSet.instance.Items[1].baseCooltime -= 4;
                    ItemInfoSet.instance.Items[1].ProjectileNum += 2;
                    break;
                case 2:
                    ItemInfoSet.instance.Items[2].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[2].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[2].baseProjectileScale = 16.0f;
                    ItemInfoSet.instance.Items[2].basePenetration += 1;
                    break;
                case 3:
                    ItemInfoSet.instance.Items[3].baseAttackDamage += 30;
                    ItemInfoSet.instance.Items[3].baseProjectileScale *= 1.25f * 1.25f;
                    ItemInfoSet.instance.Items[3].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[3].AttackCount -= 1;
                    break;
                case 4:
                    ItemInfoSet.instance.Items[4].baseAttackDamage += 15;
                    ItemInfoSet.instance.Items[4].Percentage += 200;
                    break;
                case 5:
                    break;
                case 6:
                    ItemInfoSet.instance.Items[6].baseAttackDamage += 12;
                    ItemInfoSet.instance.Items[6].ProjectileNum += 3;
                    ItemInfoSet.instance.Items[6].baseCooltime = 4.0f;
                    break;
                case 7:
                    ItemInfoSet.instance.Items[7].baseAttackDamage += 40;
                    ItemInfoSet.instance.Items[7].ProjectileNum += 3;
                    break;
                case 8:
                    ItemInfoSet.instance.Items[8].ProjectileNum += 7;
                    ItemInfoSet.instance.Items[8].baseAttackDamage += 7;
                    break;
                case 9:
                    ItemInfoSet.instance.Items[9].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[9].AttackCount -= 2;
                    ItemInfoSet.instance.Items[9].ProjectileNum += 2;
                    break;
                case 10:
                    ItemInfoSet.instance.Items[10].Percentage += 200;
                    ItemInfoSet.instance.Items[10].ProjectileNum += 4;
                    ItemInfoSet.instance.Items[10].baseAttackDamage += 15;
                    ItemInfoSet.instance.Items[10].baseProjectileScale *= 1.5f * 1.5f;
                    break;
                case 11:
                    GameObject.Find("Player").GetComponent<PlayerCtrl>().Skill_B = true;
                    break;
                case 12:
                    ItemInfoSet.instance.Items[12].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[12].baseProjectileScale *= 1.5f * 1.5f * 1.5f;
                    ItemInfoSet.instance.Items[12].AttackCount -= 2;
                    break;
                case 13:
                    ItemInfoSet.instance.Items[13].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[13].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[13].baseDuration -= 2;
                    ItemInfoSet.instance.Items[13].baseProjectileScale *= 1.5f;
                    break;
                case 14:
                    ItemInfoSet.instance.Items[14].baseAttackDamage += 20;
                    ItemInfoSet.instance.Items[14].ProjectileNum += 5;
                    break;
                case 15:
                    ItemInfoSet.instance.Items[15].baseAttackDamage += 10;
                    ItemInfoSet.instance.Items[15].AttackCount -= 2;
                    ItemInfoSet.instance.Items[15].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[15].baseDuration += 2.0f;
                    break;
                case 16:
                    ItemInfoSet.instance.Items[16].baseAttackDamage += 30;
                    ItemInfoSet.instance.Items[16].Percentage += 200;
                    break;
                case 17:
                    ItemInfoSet.instance.Items[17].baseAttackDamage += 3;
                    ItemInfoSet.instance.Items[17].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[17].baseProjectileScale *= 1.25f;
                    break;
                case 18:
                    ItemInfoSet.instance.Items[18].baseAttackDamage += 15;
                    ItemInfoSet.instance.Items[18].ProjectileNum += 2;
                    ItemInfoSet.instance.Items[18].baseCooltime -= 2.0f;
                    break;
                case 19:
                    ItemInfoSet.instance.Items[19].baseAttackDamage += 3;
                    ItemInfoSet.instance.Items[19].baseProjectileScale += 6.0f;
                    ItemInfoSet.instance.Items[19].baseCooltime -= 0.1f;
                    break;
                case 20:
                    ItemInfoSet.instance.Items[20].baseAttackDamage += 4;
                    ItemInfoSet.instance.Items[20].baseProjectileScale *= 1.25f * 1.25f;
                    ItemInfoSet.instance.Items[20].baseDuration += 4.0f;
                    break;
                case 21:
                    ItemInfoSet.instance.Items[21].baseAttackDamage += 10;
                    ItemInfoSet.instance.Items[21].baseDuration += 4.0f;
                    ItemInfoSet.instance.Items[21].baseProjectileScale *= 1.5f;
                    break;
            }
        }
    }




}
