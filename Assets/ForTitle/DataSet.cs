using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSet : MonoBehaviour
{
    public InherenceSkill _InherenceSkill;
    public PlayerStatus _PlayerStatus;
    public PCstatusUISet _PCstatusUISet;
    public Wisp _Pet;
    public static DataSet instance = null;

    public int selectedClass = 0;
    public int SceneNum = 0;

    public int[] classLevel = new int[5];
    public int[] classExp = new int[5];

    public int timeCount = 0;
    public int killedMonsterSum = 0;
    public int killedBossSum = 0;
    public int monsterExpSum = 0;

    public bool isResultStart;

    public int maxHp;
    public int currentHp;
    public int currentMp;
    public int attackDamage;
    public float attackSpeed;
    public float knockBack;
    public int penetration;
    public float projectileSpeed;
    public float projectileScale;
    public float moveSpeed;
    public float dashCoolTime;
    public float dashRange;
    public int ManaRecovery;
    public int armorPoint;
    public float criticalChance;
    public float criticalDamage;
    public float bonusRate;
    public float duration;
    public float cooltimeReduction;

    public int statSTR = 0;
    public int statAGL = 0;
    public int statINT = 0;
    public int statLUK = 0;
    public int statTGH = 0;
    public int statWIZ = 0;
    public int statExpBonus = 0;
    private void Awake()
    {
        if (instance == null)
        {
            PlayerPrefs.SetInt("s_isResultOn", 0);
            SetData();
            LoadData();
            SaveData();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }   
        }
        
    }
    public void SetData()
    {
        if (!PlayerPrefs.HasKey("classLevel_Fire"))     { PlayerPrefs.SetInt("classLevel_Fire", 1); }
        if (!PlayerPrefs.HasKey("classLevel_Earth"))    { PlayerPrefs.SetInt("classLevel_Earth",0); }
        if (!PlayerPrefs.HasKey("classLevel_Wind"))     { PlayerPrefs.SetInt("classLevel_Wind", 0); }
        if (!PlayerPrefs.HasKey("classLevel_Water"))    { PlayerPrefs.SetInt("classLevel_Water", 0); }
        if (!PlayerPrefs.HasKey("classLevel_Thunder"))  { PlayerPrefs.SetInt("classLevel_Thunder", 0); }

        if (!PlayerPrefs.HasKey("classExp_Fire"))       { PlayerPrefs.SetInt("classExp_Fire", 0); }
        if (!PlayerPrefs.HasKey("classExp_Earth"))      { PlayerPrefs.SetInt("classExp_Earth", 0); }
        if (!PlayerPrefs.HasKey("classExp_Wind"))       { PlayerPrefs.SetInt("classExp_Wind", 0); }
        if (!PlayerPrefs.HasKey("classExp_Water"))      { PlayerPrefs.SetInt("classExp_Water", 0); }
        if (!PlayerPrefs.HasKey("classExp_Thunder"))    { PlayerPrefs.SetInt("classExp_Thunder", 0); }

        if (!PlayerPrefs.HasKey("s_killedMonsterSum"))  { PlayerPrefs.SetInt("s_killedMonsterSum", 0); }
        if (!PlayerPrefs.HasKey("s_monsterExpSum"))     { PlayerPrefs.SetInt("s_monsterExpSum", 0); }
        if (!PlayerPrefs.HasKey("s_isBossDie"))         { PlayerPrefs.SetInt("s_isBossDie", 0); }
        if (!PlayerPrefs.HasKey("s_isResultOn"))        { PlayerPrefs.SetInt("s_isResultOn", 0); }
    }
    public void LoadData()
    {
        classLevel[0] = PlayerPrefs.GetInt("classLevel_Fire");
        classLevel[1] = PlayerPrefs.GetInt("classLevel_Earth");
        classLevel[2] = PlayerPrefs.GetInt("classLevel_Wind");
        classLevel[3] = PlayerPrefs.GetInt("classLevel_Water");
        classLevel[4] = PlayerPrefs.GetInt("classLevel_Thunder");

        classExp[0] = PlayerPrefs.GetInt("classExp_Fire");
        classExp[1] = PlayerPrefs.GetInt("classExp_Earth");
        classExp[2] = PlayerPrefs.GetInt("classExp_Wind");
        classExp[3] = PlayerPrefs.GetInt("classExp_Water");
        classExp[4] = PlayerPrefs.GetInt("classExp_Thunder");
    }
    public void SaveData()
    {
        if (PlayerPrefs.HasKey("classLevel_Fire"))     { PlayerPrefs.SetInt("classLevel_Fire", classLevel[0]); }
        if (PlayerPrefs.HasKey("classLevel_Earth"))    { PlayerPrefs.SetInt("classLevel_Earth", classLevel[1]); }
        if (PlayerPrefs.HasKey("classLevel_Wind"))     { PlayerPrefs.SetInt("classLevel_Wind", classLevel[2]); }
        if (PlayerPrefs.HasKey("classLevel_Water"))    { PlayerPrefs.SetInt("classLevel_Water", classLevel[3]); }
        if (PlayerPrefs.HasKey("classLevel_Thunder"))  { PlayerPrefs.SetInt("classLevel_Thunder", classLevel[4]); }

        //if (PlayerPrefs.HasKey("classLevel_Fire")) { PlayerPrefs.SetInt("classLevel_Fire", 1); }
        //if (PlayerPrefs.HasKey("classLevel_Earth")) { PlayerPrefs.SetInt("classLevel_Earth", 0); }
        //if (PlayerPrefs.HasKey("classLevel_Wind")) { PlayerPrefs.SetInt("classLevel_Wind", 0); }
        //if (PlayerPrefs.HasKey("classLevel_Water")) { PlayerPrefs.SetInt("classLevel_Water", 0); }
        //if (PlayerPrefs.HasKey("classLevel_Thunder")) { PlayerPrefs.SetInt("classLevel_Thunder", 0); }
        
        //PlayerPrefs.SetInt("s_isBossDie", 0);

        //if (PlayerPrefs.HasKey("classExp_Fire")) { PlayerPrefs.SetInt("classExp_Fire", 0); }
        //if (PlayerPrefs.HasKey("classExp_Earth")) { PlayerPrefs.SetInt("classExp_Earth", 0); }
        //if (PlayerPrefs.HasKey("classExp_Wind")) { PlayerPrefs.SetInt("classExp_Wind", 0); }
        //if (PlayerPrefs.HasKey("classExp_Water")) { PlayerPrefs.SetInt("classExp_Water", 0); }
        //if (PlayerPrefs.HasKey("classExp_Thunder")) { PlayerPrefs.SetInt("classExp_Thunder", 0); }

        if (PlayerPrefs.HasKey("classExp_Fire"))       { PlayerPrefs.SetInt("classExp_Fire", classExp[0]); }
        if (PlayerPrefs.HasKey("classExp_Earth"))      { PlayerPrefs.SetInt("classExp_Earth", classExp[1]); }
        if (PlayerPrefs.HasKey("classExp_Wind"))       { PlayerPrefs.SetInt("classExp_Wind", classExp[2]); }
        if (PlayerPrefs.HasKey("classExp_Water"))      { PlayerPrefs.SetInt("classExp_Water", classExp[3]); }
        if (PlayerPrefs.HasKey("classExp_Thunder"))    { PlayerPrefs.SetInt("classExp_Thunder", classExp[4]); }
    }
    public void ResetData()
    {
        timeCount = 0;
        killedMonsterSum = 0;
        killedBossSum = 0;
        monsterExpSum = 0;

    }
    public void ResetStatData()
    {
        maxHp = 0;
        currentHp = 0;
        currentMp = 0;
        attackDamage = 0;
        attackSpeed = 0;
        knockBack = 0;
        penetration = 0;
        projectileSpeed = 0;
        projectileScale = 0;
        moveSpeed = 0;
        dashCoolTime = 0;
        dashRange = 0;
        ManaRecovery = 0;
        armorPoint = 0;
        criticalChance = 0;
        criticalDamage = 0;
        bonusRate = 0;
        duration = 0;
        cooltimeReduction = 0;

        statSTR = 0;
        statAGL = 0;
        statINT = 0;
        statLUK = 0;
        statTGH = 0;
        statWIZ = 0;

        statExpBonus = 0;
    }
    void Update()
    {

        if (_InherenceSkill == null)
        {
            if(SceneNum == 1)
            {
                if (GameObject.Find("InherenceSkills").GetComponent<InherenceSkill>() != null)
                {
                    _InherenceSkill = GameObject.Find("InherenceSkills").GetComponent<InherenceSkill>();
                    _PlayerStatus = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>();
                    _PCstatusUISet = GameObject.Find("Main Canvas").transform.Find("Stat UI").gameObject.GetComponentInChildren<PCstatusUISet>();
                    _Pet = GameObject.Find("Pet").GetComponent<Wisp>();

                    switch (selectedClass)

                    {
                        case 0:
                            _PlayerStatus.classLevel = classLevel[0];
                            _InherenceSkill.playerElement = PlayerElement.Fire;
                            
                            _InherenceSkill.GetComponentInChildren<ElementalSkill_Fire>().ClassLevel = classLevel[0];
                            _InherenceSkill.SetElemental(PlayerElement.Fire);
                            //_Pet.WispSelect(PlayerElement.Fire);
                            break;

                        case 1:
                            _PlayerStatus.classLevel = classLevel[1];
                            _InherenceSkill.playerElement = PlayerElement.Earth;
                            _InherenceSkill.GetComponentInChildren<ElementalSkill_Earth>().ClassLevel = classLevel[1];
                            _InherenceSkill.SetElemental(PlayerElement.Earth);
                            //_Pet.WispSelect(PlayerElement.Earth);
                            break;

                        case 2:
                            _PlayerStatus.classLevel = classLevel[2];
                            _InherenceSkill.playerElement = PlayerElement.Wind;
                            _InherenceSkill.GetComponentInChildren<ElementalSkill_Wind>().ClassLevel = classLevel[2];
                            _InherenceSkill.SetElemental(PlayerElement.Wind);
                            //_Pet.WispSelect(PlayerElement.Wind);
                            break;

                        case 3:
                            _PlayerStatus.classLevel = classLevel[3];
                            _InherenceSkill.playerElement = PlayerElement.Water;
                            _InherenceSkill.GetComponentInChildren<ElementalSkill_Water>().ClassLevel = classLevel[3];
                            _InherenceSkill.SetElemental(PlayerElement.Water);
                            _InherenceSkill.GetComponentInChildren<ElementalSkill_Water>().SetUp();
                            //_Pet.WispSelect(PlayerElement.Water);
                            break;

                        case 4:
                            _PlayerStatus.classLevel = classLevel[4];
                            _InherenceSkill.playerElement = PlayerElement.Thunder;
                            _InherenceSkill.GetComponentInChildren<ElementalSkill_Thunder>().ClassLevel = classLevel[4];
                            _InherenceSkill.SetElemental(PlayerElement.Thunder);
                            //_Pet.WispSelect(PlayerElement.Thunder);
                            break;
                    }
                    
                    _PCstatusUISet.gameObject.SetActive(true);

                    for (int i = 0; i < statSTR; i++)
                    {
                        PlayerStatus.instance.addPlayerSP(+1);
                        //Debug.Log("sp");
                        _PCstatusUISet.GetSTR();
                        //Debug.Log("str");

                    }
                    for (int i = 0; i < statAGL; i++)
                    {
                        PlayerStatus.instance.addPlayerSP(+1);
                        _PCstatusUISet.GetAGL();
                        
                    }
                    for (int i = 0; i < statINT; i++)
                    {
                        PlayerStatus.instance.addPlayerSP(+1);
                        _PCstatusUISet.GetINT();
                        
                    }
                    for (int i = 0; i < statLUK; i++)
                    {
                        PlayerStatus.instance.addPlayerSP(+1);
                        _PCstatusUISet.GetLUK();
                        
                    }
                    for (int i = 0; i < statTGH; i++)
                    {
                        PlayerStatus.instance.addPlayerSP(+1);
                        _PCstatusUISet.GetTGH();
                        
                    }
                    for (int i = 0; i < statWIZ; i++)
                    {
                        PlayerStatus.instance.addPlayerSP(+1);
                        _PCstatusUISet.GetWIZ();
                        
                    }

                    _PCstatusUISet.gameObject.SetActive(false);

                    SkillManagement.instance.SetElementalClass();
                }                
            }
        }
    }

    public void CheatLevelUp()
    {
        switch (selectedClass)

        {
            case 0:
                _PlayerStatus.classLevel = 30;
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Fire>().ClassLevel = 30;
                _InherenceSkill.SetElemental(PlayerElement.Fire);
                _Pet.WispSelect(PlayerElement.Fire);
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Fire>().SetUp();
                break;

            case 1:
                _PlayerStatus.classLevel = 30;
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Earth>().ClassLevel = 30;
                _InherenceSkill.SetElemental(PlayerElement.Earth);
                _Pet.WispSelect(PlayerElement.Earth);
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Earth>().SetUp();
                break;

            case 2:
                _PlayerStatus.classLevel = 30;
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Wind>().ClassLevel = 30;
                _InherenceSkill.SetElemental(PlayerElement.Wind);
                _Pet.WispSelect(PlayerElement.Wind);
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Wind>().SetUp();
                break;

            case 3:
                _PlayerStatus.classLevel = 30;
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Water>().ClassLevel = 30;
                _InherenceSkill.SetElemental(PlayerElement.Water);
                _Pet.WispSelect(PlayerElement.Water);
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Water>().SetUp();
                break;

            case 4:
                _PlayerStatus.classLevel = 30;
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Thunder>().ClassLevel = 30;
                _InherenceSkill.SetElemental(PlayerElement.Thunder);
                _Pet.WispSelect(PlayerElement.Thunder);
                _InherenceSkill.GetComponentInChildren<ElementalSkill_Thunder>().SetUp();
                break;
        }
        //_InherenceSkill.set
    }
    public void CheatExpBoost() 
    {
        monsterExpSum = 15000 * 20;
    }

    
}
