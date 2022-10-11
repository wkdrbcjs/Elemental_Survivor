using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ObjectPoolManager ObjectPoolManager;
    //public GameObject[] TargetSpawners;
    public GameObject MainCamera;
    PlayerCtrl player;
    public AudioSource sound;

    public float shortDis;

    /*[SerializeField]    private GameObject Spawner;
    private bool isSpawnAble;*/

    [Header("GameManagerKinds")]
    public UI_Manager uiManager;


    [Header("Player")]
    public GameObject Player;
    public GameObject skillManager;
    Vector3 Playerpos;
    public TraceCtrl cameraTracer;

    [Header("UI")]//UI
    //public GameObject GameOverUI;
    //public GameObject GameClearUI;
    //public GameObject StatUI;
    private bool isStatUIOpen;
    //public Image FadePanel;
    //public GameObject Result_UI;
    //public GameObject ClassExp_UI;
    //public GameObject Canvas_UI;
    //public GameObject Warning_UI;


    [Header("MonsPoolTimeLine")]
    public GameObject SpawnerController;   //  ������ ��Ʈ�ѷ�    
    public GameObject SelectingSpawner;    //  ���õ� ������
    private bool isSpawnAble;

    [Header("Booleans")]//��������
    private bool isGameStart;
    public bool isGameOver;
    private bool isWarningUIOpen;
    public bool isBossDie;
    public bool resultOn;
    public bool resultOver;

    [Header("Timer")]
    [SerializeField]private float currentTimeSecond;  // ��
    [SerializeField] private int currentTimeMinute;   // ��
    public Text TimeText;
    public Text PoolText;
    public float TimeRush = 1.0f;//�ð����Ӽӵ�(���)

    [Header("Time")]
    public int setTimeMinute = 15;
    public int setTimeSecond = 60;
    public int SetSpawnPoolTime = 30;
    public int timeCount = 0;

    [Header("Count")]
    public int killedEnemySum = 0;
    public int monsterExpSum = 0;


    [Header("Enemy")]
    public List<GameObject> FoundEnemys;
    public GameObject Enemy;
    public int enemyCount;
    public int enemyCount_Max;

    [Header("Boss")]
    public GameObject BossObject;
    public AudioClip BossBGM;
    public GameObject Boss_UI;
    public GameObject Boss_HpBar_UISEt;
    public Image Boss_HpBar;
    public GameObject CutScenePanelUp;
    public GameObject CutScenePanelDown;
    public GameObject BossSpeechBox;
    public GameObject BossWall;
    [SerializeField]
    private GameObject SpawnedBoss;

    [Header("MiniBoss")]
    public List<GameObject> MiniBossObjectList;
    [SerializeField]
    private GameObject SelectedMiniBossObj;
    private int MiniBossIDNum;
    private int eventNum = 0;
    private Vector3 spawnpos;

    [Header("Result(&other)")]
    public Image ClassExpBar;
    public Text remainExp;
    public Text classLevel;
    public Text StageResult;
    public Text ExpResult;
    public Text survivalTime;
    public Text killedEnemy;
    public Text bossExterminator;
    public Text timeExp;
    public Text monsterExp;
    public Text bossExp;
    public Text totalExp;
    public Text survivalTime_Value;
    public Text killedEnemy_Value;
    public Text bossExterminator_Value;
    public Text timeExp_Value;
    public Text monsterExp_Value;
    public Text bossExp_Value;
    public Text totalExp_Value;

    [Header("Boss(&other)")]
    public Text BossSpeech;
    private string BossSpeechText_1 = "";

    [Header("Mini_BossSpawnText")]
    public GameObject MiniBossSpawnUI;
    public Text MiniBossSpawnText;
    public Text MiniBossNameText;
    private string MiniBossSpawnString = "";

    [Header("��")]
    Wisp Pet;

    public GameObject MiniAlramUI;
    public Slider MiniBossBar;
    public Image MiniBossProtraitUI;
    public Sprite[] MiniBossProtrait = new Sprite[4];
    public Text MiniBossNameInUI;

    public GameObject[] Background = new GameObject[4];

    public int bossNum;
    public int bossCount;

    public GameObject NaviUI;
    private GameObject m_NaviUI;
    private void Awake()
    {
        instance = this;
        uiManager = GetComponentInChildren<UI_Manager>();
    }
    void Start()
    {
        player = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>(); 
        Pet = GameObject.FindGameObjectWithTag("Pet").GetComponent<Wisp>();
        ObjectPoolManager = ObjectPoolManager.instance;
        isGameOver = false;
        isGameStart = true;

        PoolText.enabled = false;

        // 15�� 1�ʿ��� ���� -> 30�� ������ ���� Ǯ�� �����Ǳ� ����
        // 15�� 0�ʿ��� �����ϸ� �ٷ� 59�ʰ� �ǹ��� -> ù��° ���� Ǯ �ȳ����� 30�� �ڿ��� ����.
        currentTimeMinute = setTimeMinute;
        currentTimeSecond = 1;

        //SpawnerController = GameObject.FindGameObjectWithTag("SpawnerController");
        
        // ������ ��Ʈ�ѷ����� ������ ������ ���� ����
        // �׷��� ������ ��Ʈ�ѷ��� SelectTimeLine�Լ����� �����ʸ� �����ϰ�
        // ������ �����ʸ� RecieveSpanwer�� ȣ���Ͽ� ����.
        // ���� Ÿ�Ӷ��� ������ SelectingSpawner�� ����
        SpawnerController.GetComponent<SpawnerController>().SelectSpawner();
        //  Ÿ�̸� ����
        StartCoroutine((Timer()));

        uiManager.StartCoroutine(uiManager.FadeIn());

        isSpawnAble = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isWarningUIOpen)
            {
                if (!isStatUIOpen)
                {
                    isStatUIOpen = true;
                    uiManager.StatUI.SetActive(true);
                    uiManager.StatUI.GetComponent<PCstatusUISet>().CheckSP();
                    Time.timeScale = 0.0f;
                }
                else
                {
                    isStatUIOpen = false;
                    uiManager.StatUI.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }
        }
    }
    public void OpenStatUI()
    {
        isStatUIOpen = true;
        uiManager.StatUI.SetActive(true);
        uiManager.StatUI.GetComponent<PCstatusUISet>().CheckSP();
        Time.timeScale = 0.0f;
    }

    public void BackButton()
    {
        isStatUIOpen = false;
        uiManager.StatUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenWarningUI()
    {
        isWarningUIOpen = true;
        uiManager.Warning_UI.SetActive(true);
    }
    public void CloseWarningUI()
    {
        isWarningUIOpen = false;
        uiManager.Warning_UI.SetActive(false);
    }

    //  ��������Ʈ�ѷ��� ȣ���Ͽ� ������ �����ʰ� �������� �޾ƿ�. 
    public void RecieveSpawner(GameObject spawner)
    {
        SelectingSpawner = spawner;
    }

    #region TimerUI
    IEnumerator Timer()
    {
        if (isGameOver)
        {
            yield break;
        }

        //  ���� ���� ����
        //  ��, �� ��� 0�� �Ǹ�
        if (currentTimeSecond == 0f && currentTimeMinute == 0f)
        {
            Playerpos = Player.transform.position;
            StartCoroutine(BossStart(Playerpos));
            yield break;
        }

        if (MiniBossBar.value <= MiniBossBar.maxValue)
        {
            MiniBossBar.value += 1;
        }

        //  ���ӵ� ���� ���� ���� ����
        //  14�� ������ 11,8,5,2 �� 55�ʸ��� ��
        if (currentTimeMinute % 3 == 2 && currentTimeSecond == 55f)
        {
            if (currentTimeMinute != 14)
            {
                eventNum += 1;
                Playerpos = Player.transform.position;
                StartCoroutine(Mini_BossStart(Playerpos));
                MiniBossBar.value = 0;
                MiniBossBar.maxValue = 180;

                //
                GameObject miniBoss = Instantiate(SelectedMiniBossObj, spawnpos, Quaternion.identity);
                float hp = miniBoss.GetComponent<EnemyCtrl>().HP;
                hp = hp * eventNum * 0.8f;
                miniBoss.GetComponent<EnemyCtrl>().HP = (int)hp;
                MiniBossIDNum = miniBoss.GetComponent<EnemyCtrl>().EnemyID;
            }
            if(eventNum >= 4)
            {
                MiniAlramUI.SetActive(false);
            }
            else
            {
                Mini_Boss_Set();
                MiniAlramUI.SetActive(true);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if(Background[i].transform.position.x - player.transform.position.x <= -900 )
            {
                Background[i].transform.Translate(Vector2.right * 1800);
            }
            if (Background[i].transform.position.x - player.transform.position.x >= 900)
            {
                Background[i].transform.Translate(Vector2.right * -1800);
            }
            if (Background[i].transform.position.y - player.transform.position.y <= -590)
            {
                Background[i].transform.Translate(Vector2.up * +1180);
            }
            if (Background[i].transform.position.y - player.transform.position.y >= 590)
            {
                Background[i].transform.Translate(Vector2.up * -1180);
            }
        }

            //  �� �ð��� 0�� �Ǹ�
        if (currentTimeSecond == 0f)
        {
            currentTimeMinute -= 1;             // �� -1
            currentTimeSecond = setTimeSecond;  // �ʴ� �ٽ� 60�ʷ�
        }

        currentTimeSecond -= 1f;

        // Ÿ�̸� ǥ�� ����
        ShowTimeText();
        
        timeCount += 1;
        if (DataSet.instance!=null)
        {
            DataSet.instance.timeCount += 1;
        }
        //yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1.0f/TimeRush);

        if ((int)currentTimeSecond % SetSpawnPoolTime == 0 && isSpawnAble == true) //   SetSpawnPoolTime ���� default = 20��
        {
            StartCoroutine(SpawnCool());
        }
        StartCoroutine(Timer());
    }
    
    void ShowTimeText()
    {
        TimeText.color = new Color(255, 255, 255);

        //  ���� �ð����� �п� �ش��ϴ� ������ 10�̸��̸� 
        
        if (currentTimeMinute < 10)
        {
            //  0 ���̰� ���� �� ���� �ٿ��� ǥ��
            TimeText.text = "0" + currentTimeMinute + ":" + currentTimeSecond;

            //  ���� �ʰ� 10�̸��� ���
            if (currentTimeSecond < 10)
            {
                //  0 ���̰� ���� �� ���� �ٿ��� ǥ��
                TimeText.text = "0" + currentTimeMinute + ":" + "0" + currentTimeSecond;
            }
        }
        else
        {
            TimeText.text = currentTimeMinute + ":" + currentTimeSecond;

            if (currentTimeSecond < 10)
            {
                TimeText.text = currentTimeMinute + ":" + "0" + currentTimeSecond;
            }
        }
    }
    #endregion

    //�˾Ƴ� SelectingSpawner���� �����ð����� ���� ���� Ǯ�� ������ ���� ����
    IEnumerator SpawnCool()
    {
        isSpawnAble = false;
        //EnemyPoolCtrl.cs�� NextPool �Լ�ȣ��
        if (SelectingSpawner.activeInHierarchy)
        { 
            SelectingSpawner.GetComponent<EnemyPoolCtrl>().NextSpawn(); 
        }
        

        PoolText.enabled = true;
        yield return new WaitForSeconds(2f);
        PoolText.enabled = false;

        //SetSpawnPoolTime(30��) ���� ���
        yield return new WaitForSeconds(2 - 2f);
        //isSpawnAble = true;
    }

    public void playerDie()
    {
        isGameOver = true;
        uiManager.GameOverUI.SetActive(true);
    }
    IEnumerator IntroMission()  //�̰� ����
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            //IntroText.text = BossSpeechText_1.Substring(0, i);

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        //IntroUI.SetActive(false);
    }

    //  ���� ����
    #region Boss
    //  pos�� player.transform.position
    IEnumerator BossStart(Vector3 pos)
    {
        //yield return new WaitForSeconds(1f);
        ObjectPoolManager.StopAllCoroutines(); //   ������Ʈ Ǯ�� �ڷ�ƾ ����
        player.moveable = false;
        player.stop = true;
        player.isInvincible = true;
        Pet.attackAble = false;

        skillManager.SetActive(false);
        uiManager.Canvas_UI.SetActive(false);
        Boss_UI.SetActive(true);
        
        cameraTracer.enabled =false;
        yield return new WaitForSeconds(0.3f);


        //�ٸ� ���Ϳ� ����ü ����
        StartCoroutine(FoundObjects());
        yield return new WaitForSeconds(0.3f);

        // ī�޶� �ƽ�
        for (int i = 0; i < 75; i++)
        {
            sound.volume -= 0.0005f;
            CutScenePanelDown.transform.Translate(Vector2.up * 3f);
            CutScenePanelUp.transform.Translate(Vector2.down * 3f);
            MainCamera.transform.Translate(Vector2.up);
            yield return new WaitForSeconds(0.02f);
        }

        //  �������� ����
        Vector3 spawnpos = new Vector3(pos.x, pos.y + 150f, 0);
        //GameObject b_Boss = Instantiate(BossObject, spawnpos, Quaternion.identity); //���� ����
        SpawnedBoss = Instantiate(BossObject, spawnpos, Quaternion.identity);

        //  ���� �ɾ���� ����
        yield return new WaitForSeconds(0f);

        SpawnedBoss.GetComponent<EnemyCtrl>().movementSpeed = 20f;
        SpawnedBoss.GetComponent<EnemyCtrl>().anim.SetTrigger("Move");


        yield return new WaitForSeconds(3f);
        SpawnedBoss.GetComponent<EnemyCtrl>().movementSpeed = 0f;
        SpawnedBoss.GetComponent<Animator>().SetTrigger("Idle");
        SpawnedBoss.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.5f);

        //  ���� ��ǳ��
        BossSpeechBox.SetActive(true);

        BossSpeechText_1 = "Another foolish mortal has returned.";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);

        BossSpeechText_1 = "I wonder what kind of scream you will die with.";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);

        BossSpeechText_1 = "You'll find out soon...";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }

        //  ���� �ֺ� �� ����  -> ���ѵ� ���������� �÷��̾� �̵� ����
        //  ���� ������ �� ���ο����� �ڷ���Ʈ ����
        Instantiate(BossWall, SpawnedBoss.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        BossSpeechBox.SetActive(false);
        sound.clip = BossBGM;
        sound.Play();

        //  ī�޶� ���ڸ��� �����ֱ�
        for (int i = 0; i < 75; i++)
        {
            sound.volume += 0.0005f;
            CutScenePanelDown.transform.Translate(Vector2.down * 3f);
            CutScenePanelUp.transform.Translate(Vector2.up * 3f);
            MainCamera.transform.Translate(Vector2.down);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        uiManager.Canvas_UI.SetActive(true);
        Boss_HpBar_UISEt.SetActive(true);
        skillManager.SetActive(true);
        skillManager.GetComponent<SkillManagement>().StartCoroutine(skillManager.GetComponent<SkillManagement>().CooltimeProcess());
        cameraTracer.enabled = true;
        SpawnedBoss.GetComponent<NamedMonster_Boss>().BossHpBar = Boss_HpBar;
        SpawnedBoss.GetComponent<NamedMonster_Boss>().BossPattern(SpawnedBoss.transform.position);
        SpawnedBoss.GetComponent<BoxCollider2D>().enabled = true;
        SpawnedBoss.GetComponent<EnemyCtrl>().invincible = false;

        player.stop = false;
        player.moveable = true;
        player.isInvincible = false;
        Pet.attackAble = true;

        m_NaviUI = Instantiate(NaviUI, transform.position, Quaternion.identity);
        m_NaviUI.GetComponent<NaviUi>().NamedMonster = SpawnedBoss;


    }
    public void BossEndCutScene()
    {
        StartCoroutine(BossEnd());
    }
    public IEnumerator BossEnd()
    {
        player.moveable = false;
        player.stop = true;
        player.isInvincible = true;
        Pet.attackAble = false;

        skillManager.SetActive(false);
        uiManager.Canvas_UI.SetActive(false);
        Boss_UI.SetActive(true);
        Boss_HpBar_UISEt.SetActive(false);
        cameraTracer.enabled = false;
        Destroy(m_NaviUI);
        for (int i = 0; i < 75; i++)
        {
            
            Vector2 cameraVec = SpawnedBoss.transform.position - MainCamera.transform.position;

            sound.volume -= 0.0005f;
            CutScenePanelDown.transform.Translate(Vector2.up * 3f);
            CutScenePanelUp.transform.Translate(Vector2.down * 3f);
            MainCamera.transform.Translate(cameraVec * 0.02f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1.0f);
        BossSpeechBox.SetActive(true);
        BossSpeechBox.transform.position -= new Vector3(50, 70,0);
        BossSpeechText_1 = "it's not over...";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.0f);

        BossSpeechText_1 = "I will be back...";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.0f);

        BossSpeechText_1 = "Anyway you will die...";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.0f);
        BossSpeechBox.SetActive(false);
        
        for (int i = 0; i < 75; i++)
        {
            Vector2 cameraVec = PlayerCtrl.playerInstance.transform.position - MainCamera.transform.position;
            CutScenePanelDown.transform.Translate(Vector2.down * 3f);
            CutScenePanelUp.transform.Translate(Vector2.up * 3f);
            MainCamera.transform.Translate(cameraVec*0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        cameraTracer.enabled = false;
        yield return new WaitForSeconds(1f);
        isGameOver = true;
        isBossDie = true;
        uiManager.Canvas_UI.SetActive(true);
        Boss_UI.SetActive(false);
        Boss_HpBar_UISEt.SetActive(true);
        uiManager.GameClearUI.SetActive(true);
    }

    public IEnumerator FoundObjects()
    {
        yield return new WaitForSeconds(0.01f);

        SpawnerController.SetActive(false);

        FoundEnemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyProjectile"));
        foreach (GameObject found in FoundEnemys)
        {
            Destroy(found.gameObject);
        }

        FoundEnemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerAttack"));
        foreach (GameObject found in FoundEnemys)
        {
            Destroy(found.gameObject);
        }

        //  ���� �����, ����ġ ���� ���� ����.
        //  ���� ����Ʈ�� ���� ��ü ���� ã�Ƽ� 9999������ �༭ ����ϰ� ��
        FoundEnemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach (GameObject found in FoundEnemys)
        {
            EnemyCtrl foundEnemy = found.GetComponent<EnemyCtrl>();
            foundEnemy.dropRate = 0;
            foundEnemy.exp = 0;
            if (!foundEnemy.isDead)
            {
                if(foundEnemy.EnemyShield!=null)
                {
                    foundEnemy.Critical = true;
                    foundEnemy.Disarm();
                }
                foundEnemy.Hit(this.gameObject,9999,0.1f);
            }
        }
    }

    #endregion

    //  ���ӵ� ���� ����
    #region Mini Boss

    void Mini_Boss_Set()
    {
        //int Num = Random.Range(0, 4);
        bossNum = Random.Range(0, 4);
        if (MiniBossObjectList[bossNum] == null)
        {
            Mini_Boss_Set();
            return;
        }
        
        
        SelectedMiniBossObj = MiniBossObjectList[bossNum];
        MiniBossProtraitUI.sprite = MiniBossProtrait[bossNum];

        switch (bossNum)
        {
            case 0:
                MiniBossNameInUI.text = "Grimgor, The Giant Orge";
                break;

            case 1:
                MiniBossNameInUI.text = "Minotour, Labyrinth Destroyer";
                break;

            case 2:
                MiniBossNameInUI.text = "Shien, The Red Orge";
                break;

            case 3:
                MiniBossNameInUI.text = "Phantom Knight";
                break;
        }
        MiniBossObjectList[bossNum] = null;
    }
    IEnumerator Mini_BossStart(Vector3 pos)
    {
        SpawnPositionSelect(pos);
        yield return new WaitForSeconds(0.1f);
        

        yield return null;
        StartCoroutine(Mini_Boss_SetUI());
    }

    IEnumerator Mini_Boss_SetUI()
    {
        MiniBossSpawnUI.SetActive(true);

        MiniBossSpawnText.text = "";
        MiniBossNameText.text = "";

        //  ���ӵ� ���� ���� ���� 
        MiniBossSpawnString = "A powerful monster appeared";
        for (int i = 0; i <= MiniBossSpawnString.Length; i++)
        {
            MiniBossSpawnText.text = MiniBossSpawnString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.05f);

        // ������ ���ӵ� ���� �̸��� ���� ����
        switch (MiniBossIDNum)
        {
            case 7: //  Minotaur 
                MiniBossSpawnString = "-Minotour, Labyrinth Destroyer-";
                break;
            case 8: //  Red Ogre
                MiniBossSpawnString = "-Shien, The Red Orge-";
                break;
            case 9: //  Phantom Knight 
                MiniBossSpawnString = "-Phantom Knight-";
                break;
            case 10: //  Ogre
                MiniBossSpawnString = "-Grimgor, The Giant Orge-";
                break;
        }
        // ������ ���ӵ� ���� �̸� ���
        for (int i = 0; i <= MiniBossSpawnString.Length; i++)
        {
            MiniBossNameText.text = MiniBossSpawnString.Substring(0, i);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(3f);

        MiniBossSpawnUI.SetActive(false);
    }


    void SpawnPositionSelect(Vector3 pos)
    {
        //  ������ ��ġ ����
        Vector3 getpos = GetRandomPosition(Player, 300f, 1.27f);
        spawnpos = new Vector3(getpos.x, getpos.y, 1);
    }

    public Vector3 GetRandomPosition(GameObject Target, float range, float curve)
    {
        
        float radius = range;
        Vector3 SpawnerPosition = Target.transform.position;

        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);

        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - anchorPosX, 2));

        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        float y = y_b + anchorPosY;

        Vector3 randomPosition = new Vector3(x, y / curve, 0);

        return randomPosition;
    }
    #endregion

    IEnumerator cor;

    #region Result
    public void BackToMenu()
    {
        Time.timeScale = 1;
        Destroy(DataSet.instance.gameObject);
        SceneManager.LoadScene("TitleScene");
    }

    public void StartResultUI()
    {
        if (DataSet.instance!=null)
        {
            DataSet.instance.isResultStart = true;
        }
        SceneManager.LoadScene("TitleScene");

        //cor = ResultUI();
        //StartCoroutine(cor);
    }

    //public void SkipResult ()
    //{
    //    StopCoroutine(cor);
    //    survivalTime.gameObject.SetActive(true);
    //    survivalTime_Value.gameObject.SetActive(true);
    //    survivalTime_Value.text = ((int)(timeCount / 60)).ToString() + ":" + ((int)(timeCount % 60)).ToString();

    //    killedEnemy.gameObject.SetActive(true);
    //    killedEnemy_Value.gameObject.SetActive(true);
    //    killedEnemy_Value.text = killedEnemySum.ToString();

    //    bossExterminator.gameObject.SetActive(true);
    //    bossExterminator_Value.gameObject.SetActive(true);
    //    if (isBossDie) { bossExterminator_Value.text = "yes"; }
    //    else { bossExterminator_Value.text = "no"; }

    //    ExpResult.gameObject.SetActive(true);

    //    timeExp.gameObject.SetActive(true);
    //    timeExp_Value.gameObject.SetActive(true);
    //    timeExp_Value.text = (timeCount * 2).ToString();

    //    monsterExp.gameObject.SetActive(true);
    //    monsterExp_Value.gameObject.SetActive(true);
  
    //    monsterExp_Value.text = ((int)(monsterExpSum / 20)).ToString();

    //    bossExp.gameObject.SetActive(true);
    //    bossExp_Value.gameObject.SetActive(true);
    //    if (isBossDie)
    //    {
    //        bossExp_Value.text = 500.ToString();
    //    }

    //    totalExp.gameObject.SetActive(true);
    //    totalExp_Value.gameObject.SetActive(true);
    //    int total;
    //    if (isBossDie)
    //    {
    //        total= (int)(timeCount * 2 + (int)(monsterExpSum / 20) + 500);
    //    }
    //    else
    //    {
    //        total = (int)(timeCount * 2 + (int)(monsterExpSum / 20));
    //    }
    //    totalExp_Value.text = total.ToString();
    //    ClassExp_UI.SetActive(true);

    //    classLevel.text = skillManager.GetComponent<SkillManagement>().ElementClass + " LV." + (1+(int)((total)/1000)).ToString();
    //    if (total < 1000)
    //    {
    //        remainExp.text = "Remain EXP: " + (1000-total).ToString();
    //        ClassExpBar.fillAmount = total / 1000f;
    //    }
    //    else
    //    {
    //        remainExp.text = "Remain EXP: " + (1000- (int)(total % 1000)).ToString();
    //        ClassExpBar.fillAmount = (total % 1000)/1000f;
    //    }
    //    resultOver = true;
    //}
    //IEnumerator ResultUI()
    //{
    //    int m_min = 0;
    //    int m_sec = 0;
    //    int m_value = 0;
    //    int m_totalExp = 0;
    //    //Spawner.SetActive(false);
    //    player.moveable = false;
    //    player.stop = true;
    //    skillManager.SetActive(false);
    //    GameOverUI.SetActive(false);
    //    GameClearUI.SetActive(false);
    //    Result_UI.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    //StageResult.GetComponent<GameObject>().SetActive(true);
    //    StageResult.gameObject.SetActive(true);
        
    //    yield return new WaitForSeconds(1f);
    //    resultOn = true;
    //    survivalTime.gameObject.SetActive(true);
    //    survivalTime_Value.gameObject.SetActive(true);
    //    for (int i = 0; i < timeCount; i++)
    //    {
    //        m_sec +=1;
    //        if(m_sec >= 60)
    //        {
    //            m_sec = 0;
    //            m_min += 1;
    //        }
    //        survivalTime_Value.text = m_min.ToString() + ":" + m_sec.ToString();

    //        yield return new WaitForSeconds(2f/timeCount);
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    killedEnemy.gameObject.SetActive(true);
    //    killedEnemy_Value.gameObject.SetActive(true);
    //    for (int i = 0; i < killedEnemySum; i++)
    //    {
    //        m_value += 1;
    //        killedEnemy_Value.text = m_value.ToString();
    //        yield return new WaitForSeconds(2f/killedEnemySum);
    //    }
    //    m_value = 0;
    //    yield return new WaitForSeconds(0.5f);

    //    bossExterminator.gameObject.SetActive(true);
    //    bossExterminator_Value.gameObject.SetActive(true);
    //    if (isBossDie) { bossExterminator_Value.text = "yes"; }
    //    else { bossExterminator_Value.text = "no"; }

    //    yield return new WaitForSeconds(1f);

    //    ExpResult.gameObject.SetActive(true);

    //    yield return new WaitForSeconds(0.5f);

    //    timeExp.gameObject.SetActive(true);
    //    timeExp_Value.gameObject.SetActive(true);
    //    for (int i = 0; i < timeCount; i++)
    //    {
    //        m_value += 2;
    //        timeExp_Value.text = m_value.ToString();
    //        yield return new WaitForSeconds(2f/timeCount);
    //    }
    //    m_totalExp += m_value;
    //    m_value = 0;
    //    yield return new WaitForSeconds(0.5f);

    //    monsterExp.gameObject.SetActive(true);
    //    monsterExp_Value.gameObject.SetActive(true);
    //    int m_monsterExp_Value = monsterExpSum / 20;
    //    for (int i = 0; i < m_monsterExp_Value; i++)
    //    {
    //        m_value += 1;
    //        monsterExp_Value.text = m_value.ToString();
    //        yield return new WaitForSeconds(2f / m_monsterExp_Value);
    //    }
    //    m_totalExp += m_value;
    //    m_value = 0;
    //    yield return new WaitForSeconds(0.5f);

    //    bossExp.gameObject.SetActive(true);
    //    bossExp_Value.gameObject.SetActive(true);
    //    if (isBossDie)
    //    {
    //        for (int i = 0; i < 500; i++)
    //        {
    //            m_value += 1;
    //            bossExp_Value.text = m_value.ToString();
    //            yield return new WaitForSeconds(0.006f);
    //        }
    //    }
        
    //    m_totalExp += m_value;
    //    m_value = 0;
    //    yield return new WaitForSeconds(0.5f);

    //    totalExp.gameObject.SetActive(true);
    //    totalExp_Value.gameObject.SetActive(true);
    //    for (int i = 0; i < m_totalExp; i++)
    //    {
    //        m_value += 1;
    //        totalExp_Value.text = m_value.ToString();
    //        yield return new WaitForSeconds(2f / m_totalExp);
    //    }
    //    m_value = 1;
    //    yield return new WaitForSeconds(0.5f);

    //    ClassExp_UI.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    int m_remainExp = 1000;
    //    classLevel.text = skillManager.GetComponent<SkillManagement>().ElementClass + " LV.1";
    //    for (int i = 0; i < m_totalExp; i++)
    //    {
    //        ClassExpBar.fillAmount += 1/1000f;
    //        m_remainExp -= 1;
    //        if(m_remainExp <= 0)
    //        {
    //            m_value +=1;
    //            m_remainExp = 1000;
    //            ClassExpBar.fillAmount = 0;
    //            classLevel.text = skillManager.GetComponent<SkillManagement>().ElementClass+" LV." + m_value.ToString();
    //        }
    //        remainExp.text = "Remain EXP: " + m_remainExp.ToString();
            
    //        yield return new WaitForSeconds(2f / m_totalExp);
    //    }
    //    yield return new WaitForSeconds(1f);
    //    resultOver = true;
    //}

    #endregion
}
