using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleStatManager : MonoBehaviour
{
    public DataSet _Data;

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

    public int BasemaxHp;
    public int BasecurrentHp;
    public int BasecurrentMp;
    public int BaseattackDamage;
    public float BaseattackSpeed;
    public float BaseknockBack;
    public int Basepenetration;
    public float BaseprojectileSpeed;
    public float BaseprojectileScale;
    public float BasemoveSpeed;
    public float BasedashCoolTime;
    public float BasedashRange;
    public int BaseManaRecovery;
    public int BasearmorPoint;
    public float BasecriticalChance;
    public float BasecriticalDamage;
    public float BasebonusRate;
    public float Baseduration;
    public float BasecooltimeReduction;

    public int PlayerSP;


    public Text STR_Point;
    public Text AGL_Point;
    public Text INT_Point;
    public Text LUK_Point;
    public Text TGH_Point;
    public Text WIZ_Point;
    public Text ExpBonus_Point;

    public Text ID_Text;
    public Text KR_Text;

    public Text AS_Text;
    public Text MS_Text;
    public Text DC_Text;

    public Text PSP_Text;
    public Text PSC_Text;
    public Text PE_Text;

    public Text CC_Text;
    public Text CD_Text;
    public Text BR_Text;

    public Text MH_Text;
    public Text AP_Text;

    public Text MR_Text;
    public Text SD_Text;
    public Text CR_Text;

    public Text SP;
    public GameObject WarnigText;

    private int statSTR = 0;
    private int statAGL = 0;
    private int statINT = 0;
    private int statLUK = 0;
    private int statTGH = 0;
    private int statWIZ = 0;

    private int statBaseSTR = 0;
    private int statBaseAGL = 0;
    private int statBaseINT = 0;
    private int statBaseLUK = 0;
    private int statBaseTGH = 0;
    private int statBaseWIZ = 0;

    private int statExpBonus = 0;

    int i = 0;

    public GameObject[] StatButton = new GameObject[7];
    public GameObject[] StatMinusButton = new GameObject[7];
    void Start()
    {
        _Data = DataSet.instance;

        //_playerctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
        BasemaxHp = maxHp;
        BasecurrentHp = currentHp;
        BasecurrentMp = currentMp;
        BaseattackDamage =  attackDamage;
        BaseattackSpeed =  attackSpeed;
        BaseknockBack =  knockBack;
        Basepenetration =  penetration;
        BaseprojectileSpeed =  projectileSpeed;
        BaseprojectileScale =  projectileScale;
        BasemoveSpeed =  moveSpeed;
        BasedashCoolTime =  dashCoolTime;
        BasedashRange =  dashRange;
        BaseManaRecovery =  ManaRecovery;
        BasearmorPoint =  armorPoint;
        BasecriticalChance =  criticalChance;
        BasecriticalDamage =  criticalDamage;
        BasebonusRate = bonusRate;
        Baseduration = duration;
        BasecooltimeReduction =  cooltimeReduction;

        CheckSP();

    }

    void Update()
    {

    }
    public void CheckSP()
    {
        if (PlayerSP > 0)
        {
            //LevelUPText.SetActive(true);

            StatButton[0].SetActive(true);
            StatButton[1].SetActive(true);
            StatButton[2].SetActive(true);
            StatButton[3].SetActive(true);
            StatButton[4].SetActive(true);
            StatButton[5].SetActive(true);
            StatButton[6].SetActive(true);
        }

        if (PlayerSP <= 0)
        {
            //LevelUPText.SetActive(false);

            StatButton[0].SetActive(false);
            StatButton[1].SetActive(false);
            StatButton[2].SetActive(false);
            StatButton[3].SetActive(false);
            StatButton[4].SetActive(false);
            StatButton[5].SetActive(false);
            StatButton[6].SetActive(false);
        }

        if (statSTR == 0) StatMinusButton[0].SetActive(false);
        else StatMinusButton[0].SetActive(true);

        if (statAGL == 0) StatMinusButton[1].SetActive(false);
        else StatMinusButton[1].SetActive(true);

        if (statINT == 0) StatMinusButton[2].SetActive(false);
        else StatMinusButton[2].SetActive(true);

        if (statLUK == 0) StatMinusButton[3].SetActive(false);
        else StatMinusButton[3].SetActive(true);

        if (statTGH == 0) StatMinusButton[4].SetActive(false);
        else StatMinusButton[4].SetActive(true);

        if (statWIZ == 0) StatMinusButton[5].SetActive(false);
        else StatMinusButton[5].SetActive(true);

        if (statExpBonus == 0) StatMinusButton[6].SetActive(false);
        else StatMinusButton[6].SetActive(true);

        STR_Point.text = (statSTR+statBaseSTR).ToString();
        AGL_Point.text = (statAGL+ statBaseAGL).ToString();
        INT_Point.text = (statINT+ statBaseINT).ToString();
        LUK_Point.text = (statLUK+ statBaseLUK).ToString();
        TGH_Point.text = (statTGH+ statBaseTGH).ToString();
        WIZ_Point.text = (statWIZ+ statBaseWIZ).ToString();
        ExpBonus_Point.text = statExpBonus.ToString();
        ID_Text.text = "+"+attackDamage.ToString("0.00") + "%";
        KR_Text.text = "+" + (Mathf.Round(knockBack*100)*0.01f).ToString("0.00") + "%";
        AS_Text.text = attackSpeed.ToString("0.00") + "s";
        MS_Text.text = "+" + moveSpeed.ToString("0.00");
        DC_Text.text = dashCoolTime.ToString("0.00") + "s";
        PSP_Text.text = "+" + (projectileSpeed*100).ToString("0.00") + "%";
        PSC_Text.text = "+" + (projectileScale * 100).ToString("0.00") + "%";
        PE_Text.text = "+" + penetration.ToString();
        CC_Text.text = "+" + (criticalChance * 0.1f).ToString("0.00") + "%";
        CD_Text.text = "+" + (criticalDamage * 100).ToString("0.00") + "%";
        BR_Text.text = "+" + (bonusRate * 10f).ToString("0.00") + "%";
        MH_Text.text = "+" + maxHp.ToString();
        AP_Text.text = "+" + armorPoint.ToString();
        MR_Text.text = "+" + ManaRecovery.ToString();
        SD_Text.text = "+" + (duration * 100).ToString("0.00") + "%";
        CR_Text.text = (cooltimeReduction * -100).ToString("0.00") + "%";

        //Level.text = "Level." + _playerctrl.Level.ToString();

        SP.text = "Point: " + PlayerSP.ToString();

        if (_Data != null)
        {
            _Data.statSTR = (statSTR + statBaseSTR);
            _Data.statAGL = (statAGL + statBaseAGL);
            _Data.statINT = (statINT + statBaseINT);
            _Data.statLUK = (statLUK + statBaseLUK);
            _Data.statTGH = (statTGH + statBaseTGH);
            _Data.statWIZ = (statWIZ + statBaseWIZ);
        } 
        
    }



    public void GetSTR()
    {
        //_playerctrl.attackDamage += (int)(_playerctrl.attackDamage * 0.05);

        statSTR += 1;
        if ((statSTR + statBaseSTR) % 2 == 0)
        {

        }
        attackDamage += 5;
        knockBack += 0.05f;

        _Data.attackDamage += 1;
        _Data.knockBack += 1;

        PlayerSP -= 1;
        CheckSP();
    }
    public void GetAGL()
    {
        statAGL += 1;
        if ((statAGL+ statBaseAGL) % 2 == 0)
        {
            moveSpeed += 1.5f;
            dashCoolTime -= 0.05f;

            _Data.moveSpeed += 1;
            _Data.dashCoolTime += 1;
        }

        attackSpeed -= 0.05f;
        _Data.attackSpeed += 1;
        PlayerSP -= 1;
        CheckSP();
    }
    public void GetINT()
    {
        statINT += 1;
        if ((statINT + statBaseINT) % 2 == 0)
        {
            penetration += 1;
            _Data.penetration += 1;
        }
        projectileSpeed += 0.05f;
        projectileScale += 0.05f;

        _Data.projectileSpeed += 1;
        _Data.projectileScale += 1;

        PlayerSP -= 1;
        CheckSP();
    }
    public void GetLUK()
    {
        statLUK += 1;
        if ((statLUK+ statBaseLUK) % 2 == 0)
        {
            bonusRate += 0.10f;
            _Data.bonusRate += 1;
        }
        criticalChance += 30f;
        criticalDamage += 0.05f;

        _Data.criticalChance += 1;
        _Data.criticalDamage += 1;
        PlayerSP -= 1;
        CheckSP();
    }
    public void GetTGH()
    {
        statTGH += 1;
        if ((statTGH + statBaseTGH) % 2 == 0)
        {
            armorPoint += 1;
            _Data.armorPoint += 1;
        }
        maxHp += 5;
        _Data.maxHp += 1;
        PlayerSP -= 1;
        CheckSP();
    }
    public void GetWIZ()
    {
        statWIZ += 1;
        if ((statWIZ + statBaseWIZ) % 2 == 0)
        {
            ManaRecovery += 1;
            cooltimeReduction += 0.05f;

            _Data.ManaRecovery += 1;
            _Data.cooltimeReduction += 1;
        }

        duration += 0.05f;

        _Data.duration += 1;

        PlayerSP -= 1;
        CheckSP();
    }
    public void DecreaseSTR()
    {
        //_playerctrl.attackDamage += (int)(_playerctrl.attackDamage * 0.05);
        if (statSTR > 0)
        {
            if ((statSTR+statBaseSTR) % 2 == 0)
            {

            }
            attackDamage -= 5;
            knockBack -= 0.05f;

            _Data.attackDamage -= 1;
            _Data.knockBack -= 1;
            statSTR -= 1;
            PlayerSP += 1;
            CheckSP();
        }
            
    }
    public void DecreaseAGL()
    {
        if ((statAGL+statBaseAGL) > 0)
        {
            if (statAGL % 2 == 0)
            {
                moveSpeed -= 1.5f;
                dashCoolTime += 0.05f;

                _Data.moveSpeed -= 1;
                _Data.dashCoolTime -= 1;
            }

            attackSpeed += 0.05f;
            _Data.attackSpeed -= 1;
            statAGL -= 1;
            PlayerSP += 1;
            CheckSP();

        }
        
    }
    public void DecreaseINT()
    {
        if (statINT > 0)
        {
            if ((statINT+statBaseINT) % 2 == 0)
            {
                penetration -= 1;
                _Data.penetration -= 1;
            }
            projectileSpeed -= 0.05f;
            projectileScale -= 0.05f;

            _Data.projectileSpeed -= 1;
            _Data.projectileSpeed -= 1;
            statINT -= 1;
            PlayerSP += 1;
            CheckSP();
        }
            
    }
    public void DecreaseLUK()
    {
        if (statLUK > 0)
        {
            
            if ((statLUK+statBaseLUK) % 2 == 0)
            {
                bonusRate -= 0.10f;
                _Data.bonusRate -= 1;
            }
            criticalChance -= 30f;
            criticalDamage -= 0.05f;

            _Data.criticalChance -= 1;
            _Data.criticalDamage -= 1;
            statLUK -= 1;
            PlayerSP += 1;
            CheckSP();
        }
            
    }
    public void DecreaseTGH()
    {
        if (statTGH > 0)
        {
            
            if ((statTGH+statBaseTGH) % 2 == 0)
            {
                armorPoint -= 1;
                _Data.armorPoint -= 1;
            }
            maxHp -= 5;
            _Data.maxHp -= 1;
            statTGH -= 1;
            PlayerSP += 1;
            CheckSP();
        }
            
    }
    public void DecreaseWIZ()
    {
        if (statWIZ > 0)
        {
            if ((statWIZ+statBaseWIZ) % 2 == 0)
            {
                ManaRecovery -= 1;
                cooltimeReduction -= 0.05f;

                _Data.ManaRecovery -= 1;
                _Data.cooltimeReduction += 0.05f;
            }

            duration -= 0.05f;

            _Data.duration -= 1;
            statWIZ -= 1;
            PlayerSP += 1;
            CheckSP();
        }
            
    }
    public void GetBaseSTR()
    {
        //_playerctrl.attackDamage += (int)(_playerctrl.attackDamage * 0.05);
        statBaseSTR += 1;
        if ((statSTR + statBaseSTR)% 2 == 0)
        {

        }
        attackDamage += 5;
        knockBack += 0.05f;

        _Data.attackDamage += 1;
        _Data.knockBack += 1;

        CheckSP();
    }
    public void GetBaseAGL()
    {
        statBaseAGL += 1;
        if ((statAGL+statBaseAGL) % 2 == 0)
        {
            moveSpeed += 1.5f;
            dashCoolTime -= 0.05f;

            _Data.moveSpeed += 1;
            _Data.dashCoolTime += 1;
        }

        attackSpeed -= 0.05f;
        _Data.attackSpeed += 1;
        CheckSP();
    }
    public void GetBaseINT()
    {
        statBaseINT += 1;
        if ((statINT+statBaseINT) % 2 == 0)
        {
            penetration += 1;
            _Data.penetration += 1;
        }
        projectileSpeed += 0.05f;
        projectileScale += 0.05f;

        _Data.projectileSpeed += 1;
        _Data.projectileScale += 1;

        CheckSP();
    }
    public void GetBaseLUK()
    {
        statBaseLUK += 1;
        if ((statLUK+statBaseLUK) % 2 == 0)
        {
            bonusRate += 0.10f;
            _Data.bonusRate += 1;
        }
        criticalChance += 30f;
        criticalDamage += 0.05f;

        _Data.criticalChance += 1;
        _Data.criticalDamage += 1;
        CheckSP();
    }
    public void GetBaseTGH()
    {
        statBaseTGH += 1;
        if ((statTGH+statBaseTGH) % 2 == 0)
        {
            armorPoint += 1;
            _Data.armorPoint += 1;
        }
        maxHp += 5;
        _Data.maxHp += 1;
        CheckSP();
    }
    public void GetBaseWIZ()
    {
        statBaseWIZ += 1;
        if ((statWIZ+statBaseWIZ) % 2 == 0)
        {
            ManaRecovery += 1;
            cooltimeReduction += 0.05f;

            _Data.ManaRecovery += 1;
            _Data.cooltimeReduction += 1;
        }

        duration += 0.05f;

        _Data.duration += 1;

        CheckSP();
    }
    public void IncreaseEXPBonus()
    {
        statExpBonus += 1;
        _Data.statExpBonus += 1;

        PlayerSP -= 1;

        CheckSP();
    }
    public void DescreaseEXPBonus()
    {
        statExpBonus -= 1;
        _Data.statExpBonus -= 1;

        PlayerSP += 1;

        CheckSP();
    }

    public void ResetStat()
    {
        statSTR = 0;
        statAGL = 0;
        statINT = 0;
        statLUK = 0;
        statTGH = 0;
        statWIZ = 0;

        statBaseSTR = 0;
        statBaseAGL = 0;
        statBaseINT = 0;
        statBaseLUK = 0;
        statBaseTGH = 0;
        statBaseWIZ = 0;
        statExpBonus = 0;

        maxHp =  BasemaxHp;
        currentHp =  BasecurrentHp;
        currentMp =  BasecurrentMp;
        attackDamage =  BaseattackDamage;
        attackSpeed =  BaseattackSpeed;
        knockBack =  BaseknockBack;
        penetration =  Basepenetration;
        projectileSpeed =  BaseprojectileSpeed;
        projectileScale =  BaseprojectileScale;
        moveSpeed =  BasemoveSpeed;
        dashCoolTime =  BasedashCoolTime;
        dashRange =  BasedashRange;
        ManaRecovery =  BaseManaRecovery;
        armorPoint =  BasearmorPoint;
        criticalChance =  BasecriticalChance;
        criticalDamage =  BasecriticalDamage;
        bonusRate =  BasebonusRate;
        duration =  Baseduration;
        cooltimeReduction =  BasecooltimeReduction;

        PlayerSP = 6;
        if (_Data != null)
        {
            _Data.ResetStatData();
        }
        
        CheckSP();
    }
}
