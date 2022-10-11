using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    public PlayerCtrl Player;

    //public string playerClass = "Crusader"; //Temp
    public int classLevel = 0;  //여기에 설정해야 함



    public int playerLevel = 1;
    public int maxPlayerEXP=100;
    public int increaseMaxPlayerEXP = 20;
    public int currentPlayerEXP = 0;
    public int playerSP = 0;


    //buffStatus와 lastingStatus의 구분 :
    //buff는 BuffCtrl의 생기고 없어짐에 의해서만 제어됨
    //lastingStatus : 변화를 주지 않으면 계속 유지됨
    public CustomStatus buffStatus;
    public CustomStatus lastingStatus;//여기서 초기값 지정
    public BuffManager buffManager;

    [Header("PlayerStatus")]
    public int currentHP = 100;
    public int currentMP = 0;


    [Header("PlayerSubStatus")]
    public float duration = 1.0f;//연결안됨       //플레이어의 투사체가 지속되는 시간계수
    public float knockBack = 0.0f;     //공격시 넉백거리
    public float bonusRate = 1.0f;      //플레이어 획득 경험치, 골드량 계수
    public int penetration = 1;        //투사체 관통 횟수
    public float projectileSpeed = 1.0f;//투사체 이동속도 계수
    public float projectileScale = 1.0f;//투사체 크기 계수
    public float movementSpeed = 1.5f;
    public float cooltimeReduction; //쿨타임?

    //[Header("Critical")]
    //public float criticalChance;
    //public float criticalDamage;


    [Header("UI")]
    public Image UI_HP_Bar;
    public Text UI_HP_Text;
    public Slider HP_Bar;
    public Text HPText;

    public Image UI_MP_Bar;
    public Text UI_MP_Text;
    public Slider MP_Bar;



    void Awake()
    {
        Player = GetComponentInParent<PlayerCtrl>();
        currentHP = lastingStatus.maxHP;
        setCurrentMP(lastingStatus.maxMP);
        //buffStatus = new CustomStatus(0, 0, 0, 0, 0, 0, 0);// + buffStatus;
        //lastingStatus = new CustomStatus(100, 10, 3, 1, 10, 3.0f, 0.5f);// + lastingStatus;
        buffManager = GetComponentInChildren<BuffManager>();
        //attackSpeed = lastingStatus.attackSpeed;
        //movementSpeed = lastingStatus.movementSpeed;
        instance = this;
        UI_HP_Text.gameObject.SetActive(true);
        UI_MP_Text.gameObject.SetActive(true);
        setHPMP_Text();
    }



    #region Adder

    public void LevelUp()
    {
        playerLevel += 1;
        currentPlayerEXP = 0;
        increaseMaxPlayerEXP += 50;
        maxPlayerEXP += increaseMaxPlayerEXP;
        addPlayerSP(1);
    }

    public void addCurrentPlayerEXP(int EXP_Value)
    {
        currentPlayerEXP += EXP_Value;
    }


    public bool addCurrentHP(int HP_Value)
    {
       return setCurrentHP(getCurrentHP() + HP_Value);

    }

    public void addMaxHP(int HP_Value, bool bothCurrentHP = true)//현재HP도 올릴지 여부
    {
        setMaxHP(lastingStatus.maxHP + HP_Value, bothCurrentHP);
    }

    public bool addCurrentMP(int MP_Value)
    {
        return setCurrentMP(getCurrentMP() + MP_Value);
    }

    public void addMaxMP(int MP_Value)
    {
        lastingStatus.maxMP += MP_Value;
        MP_Bar.maxValue = lastingStatus.maxMP;
    }
    public void addMP_Recovery(int MPR_Value)
    {
        lastingStatus.MP_Regcovery += MPR_Value;
    }


    public void addArmorPoint(int ArmorValue)
    {
        lastingStatus.armorPoint += ArmorValue;
    }

    public void addAttackDamage(int AD_Value)
    {
        lastingStatus.attackDamage += AD_Value;
    }

    public void addAttackSpeed(float AS_Value)
    {
        lastingStatus.attackSpeed += AS_Value;
    }

    public void addMovementSpeed(float SpeedValue)  //플레이어 스탯 변화에 추가작업이 필요한 경우 함수생성
    {
        movementSpeed = lastingStatus.movementSpeed += SpeedValue;
        //Player.moveSpeed = movementSpeed;
    }


    public void addCooltimeReduction(float CR_Value)
    {
        cooltimeReduction += CR_Value;
    }

    public void addDuration(float DurationValue)
    {
        duration += DurationValue;
    }
    public void addKnockBack(float KnockBackValue)
    {
        knockBack += KnockBackValue;
    }

    public void addBonusRate(float BonusValue)
    {
        bonusRate += BonusValue;
    }

    public void addPenetration(int PenetrationValue)
    {
        penetration += PenetrationValue;
    }

    public void addProjectileSpeed(float ProjectileSpeedValue)
    {
        projectileSpeed += ProjectileSpeedValue;
    }
    public void addProjectileScale(float ProjectileScaleValue)
    {
        projectileScale += ProjectileScaleValue;
    }

    public void addPCurrentlayerEXP(int EXP_Value)
    {
        currentPlayerEXP += Mathf.FloorToInt(EXP_Value * bonusRate);
    }

    public void addPlayerSP(int SP_Value)
    {
        playerSP += SP_Value;
    }

    public void addCriticalChance(float CC_Value)
    {
        lastingStatus.criticalChance += CC_Value;
    }

    public void addCriticalDamage(float CD_Value)
    {
        lastingStatus.criticalDamage += CD_Value;
    }

    #endregion



    #region Getter-Level

    public int getPlayerLevel()
    {
        return playerLevel;
    }    

    public int getCurrentPlayerEXP()
    {
        return currentPlayerEXP;
    }

    public int getMaxPlayerEXP()
    {
        return maxPlayerEXP;
    }

    #endregion

    #region Getter-HP,MP
    public int getMaxHP()
    {
        return lastingStatus.maxHP + buffStatus.maxHP;
    }

    public int getCurrentHP()
    {
        return currentHP;
    }

    public int getMaxMP()
    {
        return lastingStatus.maxMP + buffStatus.maxMP;
    }

    public int getCurrentMP()
    {
        return currentMP;
    }

    public int getMP_Recovery()
    {
        return lastingStatus.MP_Regcovery + buffStatus.MP_Regcovery;
    }

    #endregion


    #region Getter-others
    public int getArmorPoint()
    {
        return lastingStatus.armorPoint + buffStatus.armorPoint;
    }


    public int getAttackDamage()
    {
        return lastingStatus.attackDamage + buffStatus.attackDamage;
    }
    public float getMovementSpeed()
    {
        return lastingStatus.movementSpeed + buffStatus.movementSpeed;
    }



    public float getAttackSpeed()
    {
        return lastingStatus.attackSpeed + buffStatus.attackSpeed;
    }

    public float getCooltimeReduction()
    {
        return cooltimeReduction;
    }


    public float getBonusRate()
    {
        return bonusRate;
    }

    public float getKnockBack()
    {
        return knockBack;
    }

    public int getPenetration()
    {
        return penetration;
    }

    public float getProjectileSpeed()
    {
        return projectileSpeed;
    }
    public float getProjectileScale()
    {
        return projectileScale;
    }




    public float getDuration()
    {
        return duration;
    }

    public int getPlayerSP()
    {
        return playerSP;
    }

    public float getCriticalChance()
    {
        return lastingStatus.criticalChance + buffStatus.criticalChance;
    }

    public float getCriticalDamage()
    {
        return lastingStatus.criticalDamage+buffStatus.criticalChance;
    }



    #endregion

    #region Setter

    public void setMaxHP(int HP_Value, bool bothCurrentHP = true)
    {
        int addingHP = HP_Value - getMaxHP();
        Debug.Log("HP_Value:" + HP_Value + "maxHP:" + getMaxHP());

        lastingStatus.maxHP = HP_Value;
        HP_Bar.maxValue = getMaxHP();
        UI_HP_Bar.fillAmount = (float)(HP_Bar.value / getMaxHP());
        //Debug.Log("HP_Bar.value:" +HP_Bar.value + ", getMaxHP():"+ getMaxHP());
        if (bothCurrentHP)
        {
            addCurrentHP(addingHP);
        }
        //UI_HP_Bar.fillAmount = HP_Bar.value / getMaxHP();
        setHPMP_Text();
    }

    public bool setCurrentHP(int HP_Value)
    {
        //UI_HP_Bar.fillAmount = getCurrentHP() / getMaxHP();

        HP_Bar.maxValue = getMaxHP();
        StartCoroutine(HPBarAnimation(HP_Value - currentHP));
        currentHP = HP_Value;

        if (currentHP > getMaxHP())
        {
            setCurrentHP(getMaxHP());
            setHPMP_Text();
            return true;
        }
        else if (currentHP < 0)
        {
            setCurrentHP(0);
            setHPMP_Text();
            return true;
        }
        else
        {
            setHPMP_Text();
            return false;
        }
    }

    public bool setCurrentMP(int MP_Value)
    {
        
        //Debug.Log("UI_MP_Bar.fillAmount" + (float)getCurrentMP() / (float)getMaxMP());
        StartCoroutine(MPBarAnimation(MP_Value-currentMP));
        currentMP = MP_Value;
        UI_MP_Bar.fillAmount = getCurrentMP() / getMaxMP();
        //UI_MP_Bar.fillAmount = getCurrentMP() / getMaxMP();
        if (currentMP >= getMaxMP())
        {
            currentMP = getMaxMP();
            setHPMP_Text();
            return true;
        }
        else if (currentMP < 0)
        {
            currentMP = 0;
            setHPMP_Text();
            return true;
        }
        else {
            setHPMP_Text();
            return false;
            }
    }

    public void setAttackSpeed(float AS_Value)
    {
        lastingStatus.attackSpeed = AS_Value;
    }


    #endregion



    #region UI
    IEnumerator MPBarAnimation(int MP_Value)
    {
        MP_Bar.value = getCurrentMP();
        int positive = 1;
        if(MP_Value<0)
        {
            positive = -1;
            //while (MP_Value != 0)
            
        }
        //Debug.Log("MPIncrease, value : " + MP_Value+"positive"+positive);
        for (; MP_Value != 0; MP_Value -= positive)
        {
            MP_Bar.value += positive;
            UI_MP_Bar.fillAmount = MP_Bar.value / getMaxMP();
            //Debug.Log("value : " + MP_Value);
            yield return new WaitForSeconds(0.01f);
        }

    }


    public IEnumerator HPBarAnimation(int HP_Value)
    {
            HP_Bar.value = getCurrentHP();
            int positive = 1;
            if (HP_Value < 0)
            {
                positive = -1;
                //while (MP_Value != 0)

            }
            //Debug.Log("MPIncrease, value : " + MP_Value+"positive"+positive);
            for (; HP_Value != 0; HP_Value -= positive)
            {
                HP_Bar.value += positive;
                UI_HP_Bar.fillAmount = HP_Bar.value / getMaxHP();
                //Debug.Log("value : " + MP_Value);
                yield return new WaitForSeconds(0.01f);
            }
        
        //HPText.text = "HP " + currentHp.ToString();
    }
    
    public void setHPMP_Text()
    {
        UI_HP_Text.gameObject.SetActive(true);
        UI_MP_Text.gameObject.SetActive(true);
        
        
        UI_HP_Text.text = getCurrentHP() + "/" + getMaxHP();
        UI_MP_Text.text = getCurrentMP() + "/" + getMaxMP();

        HP_Bar.maxValue = getMaxHP();
        MP_Bar.maxValue = getMaxMP();

    }

    //public void HPBar()
    //{
    //    HP_Bar.maxValue = getMaxHP();;
    //    HP_Bar.value = getCurrentHP();

    //    UI_HP_Bar.fillAmount = (float)getCurrentHP() / (float)getMaxHP();
    //    HPText.text = "HP " + getCurrentHP().ToString();
    //}

    #endregion


}
