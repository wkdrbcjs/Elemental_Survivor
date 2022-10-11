using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSkill_Water : ElementalSkill
{

    [Header("Water")]
    [SerializeField]
    private GameObject PetPos;

    public AudioSource sound;
    public AudioClip Snd_WaterMagic;


    public GameObject HailStorm;

    Camera Camera;






    [Header("0 : ")]
    public float FreezeTime;
    public float FreezeTimeBonus;

    [Header("1 :")]
    public bool activateMassiveEnergy = false;

    [Header("2 : ")]
    public float lifetime;
    public float lifetimeBonus;

    [Header("3 : ")]
    public float DamageCycle;
    public float DamageCycleBonus;

    [Header("4 : ")]
    public float FreezeChance;
    public float FreezeChanceBonus;

    [Header("5 : ")]
    public bool activateExtraAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    public override void SkillCast()
    {
        Vector3 cameravec = Camera.ScreenToWorldPoint(Input.mousePosition);
        cameravec.z = 0;
        StartCoroutine(SkillCast_Water());
        
        //sound.PlayOneShot(Snd_WaterMagic, 0.5f);

        var instance = Instantiate(HailStorm, cameravec , Quaternion.identity);
        HailStorm hailStorm = instance.GetComponent<HailStorm>();
        addColorToProjectile(hailStorm);
        hailStorm.freezeTime = FreezeTime;
        hailStorm.activateMassiveEnergy = activateMassiveEnergy;
        hailStorm.lifetime = lifetime;
        hailStorm.damageCycle = DamageCycle;
        hailStorm.freezeChance = FreezeChance;
        hailStorm.activateExtraAttack = activateExtraAttack;
    }
    // Update is called once per frame

    protected override void SetUpLev_0()
    {
        FreezeTime += FreezeTimeBonus;
    }
    protected override void SetUpLev_1()
    {
        activateMassiveEnergy = true;
    }
    protected override void SetUpLev_2()
    {
        lifetime += lifetimeBonus;
    }
    protected override void SetUpLev_3()
    {
        DamageCycle = DamageCycleBonus;
    }
    protected override void SetUpLev_4()
    {
        FreezeChance += FreezeChanceBonus;
    }
    protected override void SetUpLev_5()
    {
        activateExtraAttack = true;
    }

    IEnumerator SkillCast_Water()
    {
        castable = false;
        yield return new WaitForSeconds(castCoolTime);
        castable = true;
    }
}
