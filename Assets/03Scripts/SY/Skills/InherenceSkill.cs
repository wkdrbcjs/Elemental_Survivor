using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum PlayerElement { Normal, Fire, Earth, Wind, Water, Thunder }
public class InherenceSkill : MonoBehaviour
{
    //public List<ElementalSkill> SkillList = new List<ElementalSkill>();
    //public ElementalSkill[] SkillArray = new ElementalSkill[7];

    public InherenceSkill instance;

    public PlayerStatus playerStatus;
    public ElementalSkill elementalSkill;

    public GameObject Skill_Fire;
    public GameObject Skill_Earth;
    public GameObject Skill_Wind;
    public GameObject Skill_Thunder;
    public GameObject Skill_Water;

    public PlayerElement playerElement = PlayerElement.Fire;

    public Text ClassText;

    public Wisp wisp;


    void Awake()
    {
        instance = this;
        //Debug.Log("skill set");
    }
    private void Start()
    {

        SetElemental(playerElement);
        //elementalSkill.SetUp();
    }

    public void SetElemental(PlayerElement element)
    {
        switch (element)
        {
            case PlayerElement.Fire:
                elementalSkill = Skill_Fire.GetComponent<ElementalSkill_Fire>();
                ClassText.text = "Fire LV." + playerStatus.classLevel;
                break;
            case PlayerElement.Earth:
                elementalSkill = Skill_Earth.GetComponent<ElementalSkill_Earth>();
                ClassText.text = "Earth LV." + playerStatus.classLevel;
                break;
            case PlayerElement.Wind:
                elementalSkill = Skill_Wind.GetComponent<ElementalSkill_Wind>();
                ClassText.text = "Wind LV." + playerStatus.classLevel;
                break;
            case PlayerElement.Thunder:
                elementalSkill = Skill_Thunder.GetComponent<ElementalSkill_Thunder>();
                ClassText.text = "Thunder LV." + playerStatus.classLevel;
                break;
            case PlayerElement.Water:
                elementalSkill = Skill_Water.GetComponent<ElementalSkill_Water>();
                ClassText.text = "Water LV." + playerStatus.classLevel;
                break;
        }

        ClassText.color = elementalSkill.CriticalColor;

        if (wisp != null)
        {
            wisp._wisps = this.playerElement;
            wisp.WispSelect(this.playerElement);
        }

    }

    public bool Damaged()
    {
        bool optionExist=false; 

        if (elementalSkill.Damaged())
        {
            optionExist = true;
        }
        return optionExist;
    }


    public void CastSkill()
    {
        //Debug.Log("inhe:castSkill");
        SoundManager.instance.SoundPlay(SoundManager.instance.ClassSkill_General);
        elementalSkill.SkillCast();
    }
}
