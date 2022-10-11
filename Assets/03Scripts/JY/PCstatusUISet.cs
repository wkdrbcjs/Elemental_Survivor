using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCstatusUISet : MonoBehaviour
{
    public PlayerCtrl Player;
    //public PlayerStatus _playerctrl;
    
    public Text Level;
    public Text ElementLevel;
    public Text SP;

    public Text STR_Point;
    public Text AGL_Point;
    public Text INT_Point;
    public Text LUK_Point;
    public Text TGH_Point;
    public Text WIZ_Point;

    public Text AD_Text;
    public Text KB_Text;

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

    private int statSTR = 0;
    private int statAGL = 0;
    private int statINT = 0;
    private int statLUK = 0;
    private int statTGH = 0;
    private int statWIZ = 0;

    public GameObject LevelUPText;

    int i = 0;
    public GameObject[] StatButton = new GameObject[6];
    void Start()
    {
        

        Player = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();

        CheckSP();

    }

    public void CheckSP()
    {
        if (PlayerStatus.instance.getPlayerSP() > 0)
        {
            LevelUPText.SetActive(true);

            StatButton[0].SetActive(true);
            StatButton[1].SetActive(true);
            StatButton[2].SetActive(true);
            StatButton[3].SetActive(true);
            StatButton[4].SetActive(true);
            StatButton[5].SetActive(true);
        }

        if (PlayerStatus.instance.getPlayerSP() <= 0)
        {
            LevelUPText.SetActive(false);

            StatButton[0].SetActive(false);
            StatButton[1].SetActive(false);
            StatButton[2].SetActive(false);
            StatButton[3].SetActive(false);
            StatButton[4].SetActive(false);
            StatButton[5].SetActive(false);
        }

        STR_Point.text = statSTR.ToString();
        AGL_Point.text = statAGL.ToString();
        INT_Point.text = statINT.ToString();
        LUK_Point.text = statLUK.ToString();
        TGH_Point.text = statTGH.ToString();
        WIZ_Point.text = statWIZ.ToString();

        AD_Text.text = PlayerStatus.instance.getAttackDamage().ToString("0.00") + "%";
        KB_Text.text = PlayerStatus.instance.getKnockBack().ToString("0.00") + "%";
        AS_Text.text = PlayerStatus.instance.getAttackSpeed().ToString("0.00") + "s";
        MS_Text.text = PlayerStatus.instance.getMovementSpeed().ToString("0.00") + "s";
        //DC_Text.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().dashCoolTime.ToString("0.00") + "s";
        DC_Text.text = Player.dashCoolTime.ToString("0.00") + "s";
        PSP_Text.text = ((PlayerStatus.instance.getProjectileSpeed() - 1) * 100f).ToString("0.0") + "%";
        PSC_Text.text = ((PlayerStatus.instance.getProjectileScale() - 1) * 100f).ToString("0.0") + "%";
        PE_Text.text = PlayerStatus.instance.getPenetration().ToString();

        CC_Text.text = (PlayerStatus.instance.getCriticalChance()*100f).ToString("0.0") + "%";
        CD_Text.text = (PlayerStatus.instance.getCriticalDamage() * 100f).ToString("0.0") + "%";

        BR_Text.text = PlayerStatus.instance.getBonusRate().ToString("0.00") + "%";
        MH_Text.text = PlayerStatus.instance.getMaxHP().ToString();
        AP_Text.text = PlayerStatus.instance.getArmorPoint().ToString();
        MR_Text.text = PlayerStatus.instance.getMP_Recovery().ToString();
        SD_Text.text = ((PlayerStatus.instance.getDuration() - 1) * 100f).ToString("0.0") + "%";

        CR_Text.text = ((PlayerStatus.instance.getCooltimeReduction() * -100f)).ToString("0.0") + "%";

        Level.text = "Level." + PlayerStatus.instance.getPlayerLevel().ToString();
        SP.text = "Point: " + PlayerStatus.instance.getPlayerSP().ToString();
    }
    public void GetSTR()
    {
        //_playerctrl.attackDamage += (int)(_playerctrl.attackDamage * 0.05);
        statSTR += 1;
        if (statSTR % 2 == 0)
        {

        }
        PlayerStatus.instance.addAttackDamage(5);
        PlayerStatus.instance.addKnockBack(0.05f);
        PlayerStatus.instance.addPlayerSP(-1);
        CheckSP();

    }
    public void GetAGL()
    {        
        statAGL += 1;
        if (statAGL % 2 == 0)
        {
            PlayerStatus.instance.addMovementSpeed(1.5f);
            Player.dashCoolTime *= 0.95f;
        }
        
        PlayerStatus.instance.addAttackSpeed(PlayerStatus.instance.getAttackSpeed() * 0.05f);
        
        PlayerStatus.instance.addPlayerSP(-1);
        CheckSP();
    }
    public void GetINT()
    {
        statINT += 1;
        if (statINT % 2 == 0)
        {
            PlayerStatus.instance.addPenetration(1);
        }
        PlayerStatus.instance.addProjectileSpeed(0.05f);
        PlayerStatus.instance.addProjectileScale(0.05f);
        PlayerStatus.instance.addPlayerSP(-1);
        CheckSP();
    }
    public void GetLUK()
    {
        statLUK += 1;
        if (statLUK % 2 == 0)
        {
            PlayerStatus.instance.addBonusRate(0.10f);
        }

        PlayerStatus.instance.addCriticalChance(0.03f);
        PlayerStatus.instance.addCriticalDamage(0.05f);

        PlayerStatus.instance.addPlayerSP(-1);
        CheckSP();
    }
    public void GetTGH()
    {
        statTGH += 1;
        if (statTGH % 2 == 0)
        {
            PlayerStatus.instance.addArmorPoint(1);
        }
        PlayerStatus.instance.addMaxHP(5);
        PlayerStatus.instance.addPlayerSP(-1);
        CheckSP();
    }
    public void GetWIZ()
    {
        statWIZ += 1;
        if (statWIZ % 2 == 0)
        {
            PlayerStatus.instance.addMP_Recovery(1);
            PlayerStatus.instance.addCooltimeReduction(-0.05f);
            PlayerStatus.instance.addCooltimeReduction(-0.05f);
        }

        PlayerStatus.instance.addDuration(0.05f);
        PlayerStatus.instance.addPlayerSP(-1);
        CheckSP();
    }
}
