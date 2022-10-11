using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CustomStatus : MonoBehaviour//�ν����� â���� ���̰� �; monobehaviour ���
{
    //maxHP�� MP�� ������� ����
    public int maxHP = 0;
    public int maxMP = 0;
    public int MP_Regcovery = 0;
    public int armorPoint = 0;
    public int attackDamage = 0;
    public float attackSpeed = 0;
    public float movementSpeed = 0;
    public float criticalChance = 0.0f;
    public float criticalDamage = 0.0f;

    public CustomStatus(int maxHP = 0, int maxMP = 0, int MP_Regen = 0, int armorPoint = 0, int attackDamage = 0, float attackSpeed = 0, float movementSpeed = 0, float criticalChance=0, float criticalDamage=0)
    {
        this.maxHP = maxHP;
        this.maxMP = maxMP;
        this.MP_Regcovery = MP_Regen;
        this.armorPoint = armorPoint;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.movementSpeed = movementSpeed;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
    }



    //public static CustomStatus +(CustomStatus A, CustomStatus B)
    //{
    //    CustomStatus SumStatus = new CustomStatus(
    //             //A.maxHP + B.maxHP,
    //             //A.maxMP + B.maxMP,
    //             //A.MP_Regen + B.MP_Regen,
    //             //A.armorPoint + B.armorPoint,
    //             //A.attackDamage + B.attackDamage,
    //             //A.attackSpeed + B.attackSpeed,
    //             //A.movementSpeed + B.movementSpeed
    //             );
    //    SumStatus.maxHP = A.maxHP + B.maxHP;
    //    SumStatus.maxMP =  A.maxMP + B.maxMP;
    //    SumStatus.MP_Regen = A.MP_Regen + B.MP_Regen;
    //    SumStatus.armorPoint = A.armorPoint + B.armorPoint;
    //    SumStatus.attackDamage  = A.attackDamage + B.attackDamage;
    //    SumStatus.attackSpeed = A.attackSpeed + B.attackSpeed;
    //    SumStatus.movementSpeed = A.movementSpeed + B.movementSpeed;
    //    Debug.Log(A.attackDamage + "  " + B.attackDamage + "  " + SumStatus.attackDamage);


    //    return SumStatus;
    //}


    //Target�� Other�� ����
    //���� ���ϱ�
    public static void SumStatus(CustomStatus Target, CustomStatus Other)
    {
        Target.maxHP += Other.maxHP;
        Target.maxMP += Other.maxMP;
        Target.MP_Regcovery += Other.MP_Regcovery;
        Target.armorPoint += Other.armorPoint;
        Target.attackDamage += Other.attackDamage;
        Target.attackSpeed += Other.attackSpeed;
        Target.movementSpeed += Other.movementSpeed;
        Target.criticalChance += Other.criticalChance;
        Target.criticalDamage += Other.criticalDamage;
    }

    //���� ����
    public static void SubStatus(CustomStatus Target, CustomStatus Other)
    {
        Target.maxHP -= Other.maxHP;
        Target.maxMP -= Other.maxMP;
        Target.MP_Regcovery -= Other.MP_Regcovery;
        Target.armorPoint -= Other.armorPoint;
        Target.attackDamage -= Other.attackDamage;
        Target.attackSpeed -= Other.attackSpeed;
        Target.movementSpeed -= Other.movementSpeed;
        Target.criticalChance -= Other.criticalChance;
        Target.criticalDamage -= Other.criticalDamage;
    }
}
