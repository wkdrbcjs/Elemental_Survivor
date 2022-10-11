using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class BuffManager : MonoBehaviour
{
    //maxHP, maxMP, ArmorPoint는 버프에서 사용하지 않을것
    public PlayerStatus playerStatus;
    public GameObject Buff;

    [Header("BuffPrint")]
    public GameObject BuffFont;//폰트출력 부분은 BuffCtrl로 옮기는 것도 고려해야 할듯하지만 BuffCtrl보단 여기있는게 더 좋아보임
    public float delayTime=0.3f;

    public int Fontsize = 15;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = PlayerStatus.instance;
        //BuffToPlayer(new CustomStatus(0, 0, 10, 0, 50,0.2f,1.0f,0.25f,0.2f),100.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject BuffToPlayer(CustomStatus buffStatus, float lifetime)
    {
        var instance = Instantiate(Buff,transform);
        CustomStatus.SumStatus(instance.GetComponent<BuffCtrl>().BuffValue, buffStatus);
        instance.GetComponent<BuffCtrl>().lifetime = lifetime ;

        StartCoroutine(PrintBuff(buffStatus));

        return instance;
    }
    // 
    void printText(string TextValue, Color? fontColor)
    {
        Color BackColor = fontColor.HasValue ? fontColor.Value : Color.white;
        //Debug.Log(TextValue + "color : "+BackColor);
        GameObject buffText = Instantiate(BuffFont, transform);
        buffText.transform.Find("Text").GetComponent<Text>().text = TextValue;
        buffText.transform.Find("Text").GetComponent<Text>().color = BackColor;
        buffText.transform.Find("Text").GetComponent<Text>().fontSize = Fontsize;
    }

    void printText(string TextValue, Color? fontColor, int fontsize )
    {
        Color BackColor = fontColor.HasValue ? fontColor.Value : Color.white;
        //Debug.Log(TextValue + "color : "+BackColor);
        GameObject buffText = Instantiate(BuffFont, transform);
        buffText.transform.Find("Text").GetComponent<Text>().text = TextValue;
        buffText.transform.Find("Text").GetComponent<Text>().color = BackColor;
        buffText.transform.Find("Text").GetComponent<Text>().fontSize = fontsize;
    }

    IEnumerator PrintBuff(CustomStatus buffStatus)
    {

        if (buffStatus.maxHP != 0)
        {
            string sign = buffStatus.maxHP > 0 ? "+" : "-";
            printText("HP" + sign + buffStatus.maxHP.ToString(), Color.red);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.maxMP != 0)
        {
            string sign = buffStatus.maxMP > 0 ? "+" : "-";
            printText("MP" + sign + buffStatus.maxMP.ToString(), Color.blue);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.MP_Regcovery != 0)
        {
            string sign = buffStatus.MP_Regcovery > 0 ? "+" : "-";
            printText("MP Bonus" + sign + buffStatus.MP_Regcovery.ToString(), Color.blue);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.armorPoint != 0)
        {
            string sign = buffStatus.armorPoint > 0 ? "+" : "-";
            printText("Armor" + sign + buffStatus.attackDamage.ToString(), Color.gray);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.attackDamage != 0)
        {
            string sign = buffStatus.attackDamage > 0 ? "+" : "-";
            printText("AttackDamage"  + sign + buffStatus.attackDamage.ToString(), Color.yellow);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.attackSpeed != 0)
        {
            string sign = buffStatus.attackSpeed > 0 ? "+" : "-";
            printText("AttackSpeed" + sign + buffStatus.attackSpeed.ToString(), Color.yellow);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.movementSpeed != 0)
        {
            string sign = buffStatus.movementSpeed > 0 ? "+" : "-";
            printText("MovementSpeed" + sign + buffStatus.movementSpeed.ToString(), Color.white);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.criticalChance != 0)
        {
            string sign = buffStatus.criticalChance > 0 ? "+" : "-";
            printText("CriticalChance" + sign + buffStatus.criticalChance.ToString(), Color.red);
            yield return new WaitForSeconds(0.5f);
        }
        if (buffStatus.criticalDamage != 0)
        {
            string sign = buffStatus.criticalDamage > 0 ? "+" : "-";
            printText("CriticalDamage" + sign + buffStatus.criticalDamage.ToString(), Color.cyan);
            yield return new WaitForSeconds(0.5f);
        }
    }

}