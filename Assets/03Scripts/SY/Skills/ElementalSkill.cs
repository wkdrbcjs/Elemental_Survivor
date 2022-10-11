using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IndividualSkill->ElementalSkill


public abstract class ElementalSkill : MonoBehaviour
{
    public GameObject playerObject; //지정필요
    public PlayerStatus playerStatus; //지정필요
    public InherenceSkill inherenceSkill;

    public int ClassLevel = 1;
    public int[] requiredClassLevel = new int[]{1,2,3,4,5,6};//스킬요구레벨
    public int MP_Cost = 100;
    public bool castable = true;
    public float castCoolTime=1.0f;

    public bool Option_Damaged = false; //플레이어의 기본 데미지 기믹을 대체하는가

    public BuffManager buffManager;

    public Color DamageColor;
    public Color CriticalColor;
    


    protected virtual void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        inherenceSkill = GetComponentInParent<InherenceSkill>();
        ClassLevel = playerStatus.classLevel;
        SetUp();
    }

    public virtual bool Damaged()
    {
        return false;
    }//플레이어가 방어권이 있는지를 리턴함(피해를 받아야 하면 false, 방어하면 true;)



    #region Setup(intro)

    public void SetUp()
    {
        if (ClassLevel >= requiredClassLevel[0])
        {
            SetUpLev_0();
        }

        if (ClassLevel >= requiredClassLevel[1])
        {
            SetUpLev_1();
        }

        if (ClassLevel >= requiredClassLevel[2])
        {
            SetUpLev_2();
        }

        if (ClassLevel >= requiredClassLevel[3])
        {
            SetUpLev_3();
        }

        if (ClassLevel >= requiredClassLevel[4])
        {
            SetUpLev_4();
        }

        if (ClassLevel >= requiredClassLevel[5])
        {
            SetUpLev_5();
        }


    }

    //abstract?
    protected virtual void SetUpLev_0()
    {
        //Debug.Log("Not exist intro in Level0");
    }
    protected virtual void SetUpLev_1()
    {
        //Debug.Log("Not exist intro in Level1");
    }
    protected virtual void SetUpLev_2()
    {
        //Debug.Log("Not exist intro in Level2");
    }
    protected virtual void SetUpLev_3()
    {
        //Debug.Log("Not exist intro in Level3");
    }
    protected virtual void SetUpLev_4()
    {
        //Debug.Log("Not exist intro in Level4");
    }
    protected virtual void SetUpLev_5()
    {
        //Debug.Log("Not exist intro in Level5");
    }

    #endregion


    public abstract void SkillCast();

    protected void addColorToProjectile(Projectile projectile)
    {
        projectile.DamageColor = DamageColor;
        projectile.CriticalColor=CriticalColor;
    }
}
