using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    public TitleStatManager _TitleStatManager;
    public InherenceSkill m_InherenceSkill;
    public DataSet _Data;

    public GameObject Player;
    public GameObject Camera_Main;
    public GameObject UI;
    public GameObject TitleUI;
    public GameObject ClassSellectUI;
    public GameObject CreditUI;

    public Text[] classLevel_Text = new Text[5];
    public Image[] classExp_Bar = new Image[5];

    public GameObject WarnigText;

    public AudioSource Sound;
    public AudioSource Sound2;
    public AudioClip[] footStep;
    public Image FadePanel;

    public GameObject Result_UI;
    public GameObject ClassExp_UI;

    public Image ClassExpBar;
    public Text remainExp;
    public Text classLevel;

    public Text StageResult;
    public Text ExpResult;
    public Text survivalTime;
    public Text killedMonster;
    public Text bossExterminator;
    public Text timeExp;
    public Text monsterExp;
    public Text bossExp;
    public Text totalExp;

    public Text survivalTime_Value;
    public Text killedMonster_Value;
    public Text bossExterminator_Value;
    public Text timeExp_Value;
    public Text monsterExp_Value;
    public Text bossExp_Value;
    public Text totalExp_Value;
    public Text bonusExp_Value;

    public bool resultOn;
    public bool resultOver;

    public bool isBossDie;

    public float textSpeed = 1.0f;
    private float m_textSpeed;
    public int timeCount = 0;
    public int killedMonsterSum = 0;
    public int killedBossSum = 0;
    public int monsterExpSum = 0;

    public GameObject[] ClassInfoUI;

    public int SellectedClass = 0; //1:ºÒ 2:Èë 3:¹Ù¶÷ 4:¹° 5:¹ø°³

    public GameObject Lock_Earth;
    public GameObject Lock_Wind;
    public GameObject Lock_Water;
    public GameObject Lock_Thunder;

    public GameObject[] LevelPointDot = new GameObject[30];
    public GameObject[] LevelPointLine = new GameObject[5];

    public GameObject[] LevelBonus = new GameObject[5];

    public GameObject[] SkillUnlockPanel = new GameObject[6];
    // Start is called before the first frame update
    void Start()
    {
        _Data.SetData();
        _Data.LoadData();
        _Data = GameObject.Find("Data Loader").GetComponent<DataSet>();
        StartCoroutine(FadeOut());
        
        _Data.SceneNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (resultOver)
            {
                resultOver = false;
                _Data.SaveData();
                _Data.ResetData();
                Result_UI.SetActive(false);
                TitleUI.SetActive(true);
            }
            if (resultOn)
            {
                SkipResult();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("s_isBossDie", 3);
            ClassSellectUISet();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerPrefs.HasKey("classLevel_Fire")) { PlayerPrefs.SetInt("classLevel_Fire", 30); }
            if (PlayerPrefs.HasKey("classLevel_Earth")) { PlayerPrefs.SetInt("classLevel_Earth", 30); }
            if (PlayerPrefs.HasKey("classLevel_Wind")) { PlayerPrefs.SetInt("classLevel_Wind", 30); }
            if (PlayerPrefs.HasKey("classLevel_Water")) { PlayerPrefs.SetInt("classLevel_Water", 30); }
            if (PlayerPrefs.HasKey("classLevel_Thunder")) { PlayerPrefs.SetInt("classLevel_Thunder", 30); }

            PlayerPrefs.SetInt("s_isBossDie", 3);

            if (PlayerPrefs.HasKey("classExp_Fire")) { PlayerPrefs.SetInt("classExp_Fire", 0); }
            if (PlayerPrefs.HasKey("classExp_Earth")) { PlayerPrefs.SetInt("classExp_Earth", 0); }
            if (PlayerPrefs.HasKey("classExp_Wind")) { PlayerPrefs.SetInt("classExp_Wind", 0); }
            if (PlayerPrefs.HasKey("classExp_Water")) { PlayerPrefs.SetInt("classExp_Water", 0); }
            if (PlayerPrefs.HasKey("classExp_Thunder")) { PlayerPrefs.SetInt("classExp_Thunder", 0); }

            _Data.LoadData();
            ClassSellectUISet();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerPrefs.HasKey("classLevel_Fire")) { PlayerPrefs.SetInt("classLevel_Fire", 1); }
            if (PlayerPrefs.HasKey("classLevel_Earth")) { PlayerPrefs.SetInt("classLevel_Earth", 0); }
            if (PlayerPrefs.HasKey("classLevel_Wind")) { PlayerPrefs.SetInt("classLevel_Wind", 0); }
            if (PlayerPrefs.HasKey("classLevel_Water")) { PlayerPrefs.SetInt("classLevel_Water", 0); }
            if (PlayerPrefs.HasKey("classLevel_Thunder")) { PlayerPrefs.SetInt("classLevel_Thunder", 0); }

            PlayerPrefs.SetInt("s_isBossDie", 0);

            if (PlayerPrefs.HasKey("classExp_Fire")) { PlayerPrefs.SetInt("classExp_Fire", 0); }
            if (PlayerPrefs.HasKey("classExp_Earth")) { PlayerPrefs.SetInt("classExp_Earth", 0); }
            if (PlayerPrefs.HasKey("classExp_Wind")) { PlayerPrefs.SetInt("classExp_Wind", 0); }
            if (PlayerPrefs.HasKey("classExp_Water")) { PlayerPrefs.SetInt("classExp_Water", 0); }
            if (PlayerPrefs.HasKey("classExp_Thunder")) { PlayerPrefs.SetInt("classExp_Thunder", 0); }

            _Data.LoadData();
            ClassSellectUISet();
        }
    }
    public void LoadData()
    {
        timeCount = _Data.timeCount;
        killedMonsterSum = _Data.killedMonsterSum;
        killedBossSum = _Data.killedBossSum;
        monsterExpSum = _Data.monsterExpSum;
    }
    public void ClassSellectUISet()
    {


        int LevelSum = _Data.classLevel[0] + _Data.classLevel[1] + _Data.classLevel[2] + _Data.classLevel[3] + _Data.classLevel[4];
        
        if (PlayerPrefs.GetInt("s_isBossDie") >= 1)
        {
            if (_Data.classLevel[1] == 0)
            { 
                PlayerPrefs.SetInt("classLevel_Earth", 1);
                _Data.classLevel[1] = 1;
            }
            Lock_Earth.SetActive(false);
        }

        if (LevelSum >= 10)
        {
            if(_Data.classLevel[2] == 0)
            { 
                PlayerPrefs.SetInt("classLevel_Wind", 1);
                _Data.classLevel[2] = 1;
            }
            Lock_Wind.SetActive(false);
        }

        if (LevelSum >= 30)
        {
            if (_Data.classLevel[3] == 0)
            { 
                PlayerPrefs.SetInt("classLevel_Water", 1);
                _Data.classLevel[3] = 1;
            }
            Lock_Water.SetActive(false);
        }

        if (_Data.classLevel[0] >= 15 && _Data.classLevel[1]>=15 && _Data.classLevel[2] >= 15 && _Data.classLevel[3] >= 15 && PlayerPrefs.GetInt("s_isBossDie") >= 3)
        {
            if (_Data.classLevel[4] == 0)
            { 
                PlayerPrefs.SetInt("classLevel_Thunder", 1);
                _Data.classLevel[4] = 1;
            }
            Lock_Thunder.SetActive(false);
        }
        classLevel_Text[0].text = "LV." + _Data.classLevel[0].ToString();
        classLevel_Text[1].text = "LV." + _Data.classLevel[1].ToString();
        classLevel_Text[2].text = "LV." + _Data.classLevel[2].ToString();
        classLevel_Text[3].text = "LV." + _Data.classLevel[3].ToString();
        classLevel_Text[4].text = "LV." + _Data.classLevel[4].ToString();

        classExp_Bar[0].fillAmount = (float)_Data.classExp[0] / 1000f;
        classExp_Bar[1].fillAmount = (float)_Data.classExp[1] / 1000f;
        classExp_Bar[2].fillAmount = (float)_Data.classExp[2] / 1000f;
        classExp_Bar[3].fillAmount = (float)_Data.classExp[3] / 1000f;
        classExp_Bar[4].fillAmount = (float)_Data.classExp[4] / 1000f;
    }

    public void ClassInfoUISet()
    {
        _TitleStatManager.ResetStat();
        for (int i = 0; i < 23; i++)
        {
            
            LevelBonus[_Data.selectedClass].transform.GetChild(i).gameObject.GetComponent<Text>().color = new Color(255, 255, 255, 1f);
        }
        for (int i = 0; i < 30; i++)
        {
            LevelPointDot[i].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            
            if ((i + 1) % 5 == 0)
            {
                SkillUnlockPanel[((i + 1) / 5) - 1].SetActive(true);
                LevelPointLine[((i+1) / 5) - 1].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
                LevelPointLine[((i+1) / 5) - 1].GetComponentInChildren<Text>().color = new Color(255, 255, 255, 1f);
            }
        }
        int k = 0;
        //
        for (int i = 0; i < _Data.classLevel[_Data.selectedClass]; i++)
        {
            LevelPointDot[i].GetComponent<Image>().color = new Color(255, 230, 0, 1f);
            
            if ((i + 1) % 5 == 0)
            {
                SkillUnlockPanel[((i + 1) / 5) - 1].SetActive(false);
                LevelPointLine[((i+1) / 5) - 1].GetComponent<Image>().color = new Color(255, 200, 0, 1f);
                LevelPointLine[((i+1) / 5) - 1].GetComponentInChildren<Text>().color = new Color(255, 200, 0, 1f);
            }
            if ((i + 1) % 5 != 0 && i != 0)
            {
                string bonus = LevelBonus[_Data.selectedClass].transform.GetChild(k).gameObject.GetComponent<Text>().text;
                LevelBonus[_Data.selectedClass].transform.GetChild(k).gameObject.GetComponent<Text>().color = new Color(255, 200, 0, 1f);
                k += 1;
                switch (bonus)
                {
                    case "+1 STR": // È­¿°
                        {
                            _TitleStatManager.GetBaseSTR();
                            break;
                        }
                    case "+1 AGL": // È­¿°
                        {
                            _TitleStatManager.GetBaseAGL();
                            break;
                        }
                    case "+1 INT": // È­¿°
                        {
                            _TitleStatManager.GetBaseINT();
                            break;
                        }
                    case "+1 LUK": // È­¿°
                        {
                            _TitleStatManager.GetBaseLUK();
                            break;
                        }
                    case "+1 TGH": // È­¿°
                        {
                            _TitleStatManager.GetBaseTGH();
                            break;
                        }
                    case "+1 WIZ": // È­¿°
                        {
                            _TitleStatManager.GetBaseWIZ();
                            break;
                        }
                }
            }
        }
    }

    public void ReturnTitle_ClassSellectUI()
    {
        ClassSellectUI.SetActive(false);
        TitleUI.SetActive(true);
    }
    public void ReturnTitle_CreditUI()
    {
        CreditUI.SetActive(false);
        TitleUI.SetActive(true);
    }
    public void CreditButton()
    {
        TitleUI.SetActive(false);
        CreditUI.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SkillTest()
    {

    }
    public void StartButton()
    {
        if (_TitleStatManager.PlayerSP > 0)
        {
            StartCoroutine(WarningTextOn());
            return;
        }
        //_TitleStatManager.stat
        StartCoroutine(GameStart());
    }
    IEnumerator FadeOut()
    {
        if (_Data.isResultStart)
        {
            _Data.isResultStart = false;
            Sound.volume = 0f;
            Player.transform.position = new Vector3(-48, 27, 0);
            Player.transform.localScale = new Vector3(20f, 20f, 0.1f);
            Player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0f);
            Camera_Main.transform.position = new Vector3(-47f, 12f, -10f);
            StartCoroutine(ComeBack());
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 50; i++)
        {
            FadePanel.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.03f);
        }
        FadePanel.color = new Color(0, 0, 0, 1f);
        FadePanel.gameObject.SetActive(false);

        
    }
    IEnumerator ComeBack()
    {
        LoadData();
        PlayerPrefs.SetInt("s_isResultOn", 0);
        TitleUI.SetActive(false);
        ClassSellectUI.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Player.GetComponent<Animator>().SetTrigger("Back");
        
        StartCoroutine(FootStep());

        for (int i = 0; i < 50; i++)
        {
            Player.transform.Translate(Vector2.up * -0.1f);
            Player.transform.localScale += new Vector3(0.2f, 0.2f, 0.1f);
            Player.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.02f);

            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < 100; i++)
        {
            Sound.volume += 0.003f;
            Camera_Main.transform.Translate(Vector2.up * -0.15f);
            Player.transform.Translate(Vector2.up * -0.5f);
            yield return new WaitForSeconds(0.02f);
        }
        Player.GetComponent<Animator>().SetTrigger("Over");
        FadePanel.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        cor = ResultUI();
        StartCoroutine(cor);

    }
    IEnumerator GameStart()
    {
        int a = 0;
        //UI.SetActive(false);
        ClassSellectUI.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Player.GetComponent<Animator>().SetTrigger("Start");
        for (int i = 0; i < 100; i++)
        {
            Sound.volume -= 0.01f;
            Camera_Main.transform.Translate(Vector2.up * 0.15f);
            Player.transform.Translate(Vector2.up * 0.5f);
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(FootStep());

        for (int i = 0; i < 50; i++)
        {
            Player.transform.Translate(Vector2.up * 0.1f);
            Player.transform.localScale -= new Vector3(0.2f, 0.2f, 0.1f);
            Player.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.02f);
            
            yield return new WaitForSeconds(0.02f);
        }
        FadePanel.gameObject.SetActive(true);
        
        for (int i = 0; i < 50; i++)
        {
            FadePanel.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        _Data.SceneNum = 1;
        SceneManager.LoadScene("InGame");
    }
    
    public void ClassSellectUI_On()
    {
        TitleUI.SetActive(false);
        ClassSellectUI.SetActive(true);
        ClassSellectUISet();
        ClassInfoUISet();
    }
    public void ClassSellectButton(int i)
    {
        
        switch (i)
        {
            case 0: // È­¿°
                {
                    for (int a = 0; a < 5; a++)
                    {
                        ClassInfoUI[a].SetActive(false);
                    }
                    ClassInfoUI[i].SetActive(true);
                    _Data.selectedClass = 0;
                    break;
                }
            case 1: // ´ëÁö
                {
                    for (int a = 0; a < 5; a++)
                    {
                        ClassInfoUI[a].SetActive(false);
                    }
                    ClassInfoUI[i].SetActive(true);
                    _Data.selectedClass = 1;
                    break;
                }
            case 2: // ¹Ù¶÷
                {
                    for (int a = 0; a < 5; a++)
                    {
                        ClassInfoUI[a].SetActive(false);
                    }
                    ClassInfoUI[i].SetActive(true);
                    _Data.selectedClass = 2;
                    break;
                }
            case 3: // ¹ø°³
                {
                    for (int a = 0; a < 5; a++)
                    {
                        ClassInfoUI[a].SetActive(false);
                    }
                    ClassInfoUI[i].SetActive(true);
                    _Data.selectedClass = 4;
                    break;
                }
            case 4: // ¹°
                {
                    for (int a = 0; a < 5; a++)
                    {
                        ClassInfoUI[a].SetActive(false);
                    }
                    ClassInfoUI[i].SetActive(true);
                    _Data.selectedClass = 3;
                    break;
                }
        }
        ClassInfoUISet();
    }
    IEnumerator FootStep()
    {
        for (int i = 0; i < 2; i++)
        {
            //Sound2.PlayOneShot(footStep[1]);
            yield return new WaitForSeconds(0.4f);
            //Sound2.PlayOneShot(footStep[1]);
            yield return new WaitForSeconds(0.4f);
        }
    }
    IEnumerator WarningTextOn()
    {
        WarnigText.SetActive(true);
        yield return new WaitForSeconds(2f);
        WarnigText.SetActive(false);
    }
    IEnumerator cor;
    public void SkipResult()
    {
        StopCoroutine(cor);
        resultOn = false;
        survivalTime.gameObject.SetActive(true);
        survivalTime_Value.gameObject.SetActive(true);
        survivalTime_Value.text = ((int)(timeCount / 60)).ToString() + ":" + ((int)(timeCount % 60)).ToString();

        killedMonster.gameObject.SetActive(true);
        killedMonster_Value.gameObject.SetActive(true);
        killedMonster_Value.text = killedMonsterSum.ToString();

        bossExterminator.gameObject.SetActive(true);
        bossExterminator_Value.gameObject.SetActive(true);
        bossExterminator_Value.text = killedBossSum.ToString();

        ExpResult.gameObject.SetActive(true);

        timeExp.gameObject.SetActive(true);
        timeExp_Value.gameObject.SetActive(true);
        timeExp_Value.text = (timeCount * 2).ToString();

        monsterExp.gameObject.SetActive(true);
        monsterExp_Value.gameObject.SetActive(true);



        bossExp.gameObject.SetActive(true);
        bossExp_Value.gameObject.SetActive(true);
        bossExp_Value.text = (killedBossSum * 300).ToString();

        totalExp.gameObject.SetActive(true);
        totalExp_Value.gameObject.SetActive(true);
        bonusExp_Value.gameObject.SetActive(true);

        int total;
        total = (int)(timeCount * 2 + (int)(monsterExpSum / 20) + (killedBossSum * 300));

        ClassExp_UI.SetActive(true);

        bonusExp_Value.text = "(+ " + ((total * (100 + (5 * _Data.statExpBonus)) / 100) - total).ToString() + ")";
        total += ((total * (100 + (5 * _Data.statExpBonus)) / 100) - total);
        totalExp_Value.text = total.ToString();

        classLevel.text = "Earth" + " LV." + (1 + (int)((total) / 1000)).ToString();
        int m_remainExp = 0;
        int m_LevelValue = 0;
        if (_Data.classLevel[_Data.selectedClass] < 30)
        {


            if (total < 1000 - _Data.classExp[_Data.selectedClass])
            {
                m_remainExp = (1000 - _Data.classExp[_Data.selectedClass] - total);
                _Data.classExp[_Data.selectedClass] += total;
                remainExp.text = "Remain EXP: " + m_remainExp.ToString();

                ClassExpBar.fillAmount = _Data.classExp[_Data.selectedClass] / 1000f;
            }
            else
            {
                if (_Data.classExp[_Data.selectedClass] + (int)(total % 1000) >= 1000)
                {
                    m_remainExp = _Data.classExp[_Data.selectedClass] + (int)(total % 1000) - 1000;
                }
                else
                {
                    m_remainExp = (1000 - _Data.classExp[_Data.selectedClass] - (int)(total % 1000));
                }

                _Data.classExp[_Data.selectedClass] = 1000 - m_remainExp;
                m_LevelValue = (total - (int)(total % 1000)) / 1000;

                

                if (_Data.classLevel[_Data.selectedClass]+ m_LevelValue <= 30)
                {
                    _Data.classLevel[_Data.selectedClass] += m_LevelValue;
                }
                else if(_Data.classLevel[_Data.selectedClass] + m_LevelValue > 30)
                {
                    _Data.classLevel[_Data.selectedClass] = 30;
                    _Data.classExp[_Data.selectedClass] = 1000;
                }
                remainExp.text = "Remain EXP: " + (1000 - _Data.classExp[_Data.selectedClass]).ToString();
                ClassExpBar.fillAmount = _Data.classExp[_Data.selectedClass] / 1000f;
            }
        }
        else
        {
            remainExp.text = "Remain EXP: 0";
            ClassExpBar.fillAmount = 1.0f;
        }
        //m_remainExp = 1000;
        //ClassExpBar.fillAmount = 0;
        switch (_Data.selectedClass)
        {
            case 0:
                classLevel.text = "Fire" + " LV." + _Data.classLevel[0].ToString();
                break;
            case 1:
                classLevel.text = "Earth" + " LV." + _Data.classLevel[1].ToString();
                break;
            case 2:
                classLevel.text = "Wind" + " LV." + _Data.classLevel[2].ToString();
                break;
            case 3:
                classLevel.text = "Water" + " LV." + _Data.classLevel[3].ToString();
                break;
            case 4:
                classLevel.text = "Thunder" + " LV." + _Data.classLevel[4].ToString();
                break;
        }

        resultOver = true;
    }
    IEnumerator ResultUI()
    {
        int m_min = 0;
        int m_sec = 0;
        int m_value = 0;
        int m_totalExp = 0;
        Result_UI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //StageResult.GetComponent<GameObject>().SetActive(true);
        StageResult.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        resultOn = true;
        survivalTime.gameObject.SetActive(true);
        survivalTime_Value.gameObject.SetActive(true);
        for (int i = 0; i < timeCount; i++)
        {
            m_sec += 1;
            if (m_sec >= 60)
            {
                m_sec = 0;
                m_min += 1;
            }
            survivalTime_Value.text = m_min.ToString() + ":" + m_sec.ToString();

            yield return new WaitForSeconds(textSpeed / (float)timeCount);
        }

        yield return new WaitForSeconds(0.5f);

        killedMonster.gameObject.SetActive(true);
        killedMonster_Value.gameObject.SetActive(true);
        for (int i = 0; i < killedMonsterSum; i++)
        {
            m_value += 1;
            killedMonster_Value.text = m_value.ToString();
            yield return new WaitForSeconds((textSpeed / (float)killedMonsterSum));
        }
        m_value = 0;
        yield return new WaitForSeconds(0.5f);

        bossExterminator.gameObject.SetActive(true);
        bossExterminator_Value.gameObject.SetActive(true);
        bossExterminator_Value.text = _Data.killedBossSum.ToString();

        yield return new WaitForSeconds(0.5f);

        ExpResult.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        timeExp.gameObject.SetActive(true);
        timeExp_Value.gameObject.SetActive(true);
        for (int i = 0; i < timeCount; i++)
        {
            m_value += 2;
            timeExp_Value.text = m_value.ToString();
            yield return new WaitForSeconds(textSpeed / (float)timeCount);
        }
        m_totalExp += m_value;
        m_value = 0;
        yield return new WaitForSeconds(0.5f);

        monsterExp.gameObject.SetActive(true);
        monsterExp_Value.gameObject.SetActive(true);
        int m_monsterExp_Value = monsterExpSum / 20;
        if (m_monsterExp_Value > 5000)
        {
            m_textSpeed = 0.0001f;
        }
        else
        {
            m_textSpeed = textSpeed / (float)m_monsterExp_Value;
        }
        for (int i = 0; i < m_monsterExp_Value; i++)
        {
            m_value += 1;
            monsterExp_Value.text = m_value.ToString();
            yield return new WaitForSeconds(m_textSpeed);
        }
        m_totalExp += m_value;
        m_value = 0;
        yield return new WaitForSeconds(0.5f);

        bossExp.gameObject.SetActive(true);
        bossExp_Value.gameObject.SetActive(true);
        //bossExp_Value.text = (killedBossSum * 300).ToString();
        for (int i = 0; i < (killedBossSum * 300); i++)
        {
            m_value += 1;
            bossExp_Value.text = m_value.ToString();
            yield return new WaitForSeconds(0.006f);
        }

        m_totalExp += m_value;
        m_value = 0;
        yield return new WaitForSeconds(0.5f);

        totalExp.gameObject.SetActive(true);
        totalExp_Value.gameObject.SetActive(true);
        Debug.Log(textSpeed / (float)m_totalExp);
        if (m_totalExp > 5000)
        {
            m_textSpeed = 0.0001f;
        }
        else
        {
            m_textSpeed = textSpeed / (float)m_totalExp;
        }
        for (int i = 0; i < m_totalExp; i++)
        {
            m_value += 1;
            totalExp_Value.text = m_value.ToString();
            yield return new WaitForSeconds(m_textSpeed);
        }
        m_value = 0;
        yield return new WaitForSeconds(0.5f);
        bonusExp_Value.gameObject.SetActive(true);
        int m_BonusExp = (m_totalExp * (100 + (5 * _Data.statExpBonus)) / 100) - m_totalExp;
        for (int i = 0; i < m_BonusExp; i++)
        {
            bonusExp_Value.text = "(+ " + m_value.ToString() + ")";
            m_value += 1;
            yield return new WaitForSeconds(textSpeed / (float)(m_totalExp / _Data.statExpBonus));
        }
        m_totalExp += m_value;
        yield return new WaitForSeconds(0.5f);
        totalExp_Value.text = (m_totalExp).ToString();
        yield return new WaitForSeconds(1f);
        
        ClassExp_UI.SetActive(true);
        ClassExpBar.fillAmount = _Data.classExp[_Data.selectedClass] / 1000f;
        switch (_Data.selectedClass)
        {
            case 0:
                classLevel.text = "Fire" + " LV." + _Data.classLevel[0].ToString();
                break;
            case 1:
                classLevel.text = "Earth" + " LV." + _Data.classLevel[1].ToString();
                break;
            case 2:
                classLevel.text = "Wind" + " LV." + _Data.classLevel[2].ToString();
                break;
            case 3:
                classLevel.text = "Water" + " LV." + _Data.classLevel[3].ToString();
                break;
            case 4:
                classLevel.text = "Thunder" + " LV." + _Data.classLevel[4].ToString();
                break;
        }
        
        //yield return new WaitForSeconds(0.5f);
        int m_remainExp = 1000 - _Data.classExp[_Data.selectedClass];

        int m_level = _Data.classLevel[_Data.selectedClass];
        //ClassExpBar.fillAmount = 1 / 1000f;
        if (_Data.classLevel[_Data.selectedClass] < 30)
        {
            for (int i = 0; i < m_totalExp; i++)
            {
                if (_Data.classLevel[_Data.selectedClass] < 30)
                {
                    m_remainExp -= 1;
                    ClassExpBar.fillAmount += 1f / 1000f;
                    if (m_remainExp <= 0)
                    {

                        _Data.classLevel[_Data.selectedClass] += 1;
                        m_remainExp = 1000;
                        ClassExpBar.fillAmount = 0;
                        switch (_Data.selectedClass)
                        {
                            case 0:
                                classLevel.text = "Fire" + " LV." + _Data.classLevel[0].ToString();
                                break;
                            case 1:
                                classLevel.text = "Earth" + " LV." + _Data.classLevel[1].ToString();
                                break;
                            case 2:
                                classLevel.text = "Wind" + " LV." + _Data.classLevel[2].ToString();
                                break;
                            case 3:
                                classLevel.text = "Water" + " LV." + _Data.classLevel[3].ToString();
                                break;
                            case 4:
                                classLevel.text = "Thunder" + " LV." + _Data.classLevel[4].ToString();
                                break;
                        }
                    }
                }
                else
                {
                    m_remainExp = 0;
                    remainExp.text = "Remain EXP: 0";
                    ClassExpBar.fillAmount = 1.0f;
                }
                remainExp.text = "Remain EXP: " + m_remainExp.ToString();
                yield return new WaitForSeconds(textSpeed / (float)m_totalExp);
            }
        }
        else
        {
            remainExp.text = "Remain EXP: 0";
            ClassExpBar.fillAmount = 1.0f;
        }

        _Data.classExp[_Data.selectedClass] = 1000 - m_remainExp;
        _Data.SaveData();
        _Data.ResetData();
        yield return new WaitForSeconds(2f);
        resultOver = true;
        Result_UI.SetActive(false);
        TitleUI.SetActive(true);
    }
}
