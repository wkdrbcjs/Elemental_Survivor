using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using SYMath;

public class PlayerCtrl : MonoBehaviour
{
    public static GameObject playerInstance;

    public GameManager GameManager;
    public SkillManagement SkillManagement;
    public AudioSource sound;

    public AudioClip Snd_Dash;
    public AudioClip Snd_Shot;
    public AudioClip Snd_LevelUp;

    [Header("GetInput")]//GetInput
    private int h = 0;
    private int v = 0;

    private bool wDown;
    private bool aDown;
    private bool sDown;
    private bool dDown;

    private bool wUp;
    private bool aUp;
    private bool sUp;
    private bool dUp;

    [Header("Boolean")]
    public bool stop;
    public bool isRoll;
    public bool isNoHitTime;
    private bool DashAttack;
    public bool isInvincible=false;
    
    [Header("Animation")]//Animation
    public Sprite Portrait_Normal;
    public Sprite Portrait_Hit;
    public AnimationCurve curve;
    private Animator anim;
    private bool isAnimOn; //
    private bool AttackON = true; //���� ��Ÿ�� ������ ���� ���� ���� �������� �˸��� ����. NormalAttack()�� ����� ��, false�� �ǰ� attackSpeed�ʰ� ������ Ture
    private bool Rollable = true;  //������ ��Ÿ�� ������ �����⸦ ���� ���� �������� �˸��� ����. Roll()�� ����� �� false�� �ǰ� 1�ʰ� ������ True;
    private bool StopMove = false;
    private bool StopMoveForAttack = false;


    [Header("Battle")]
    private bool isDie = false;
    private Collider2D playerCollider;
    [SerializeField] BoxCollider2D colliderA;
    [SerializeField] BoxCollider2D colliderB;


    [Header("Move&Turn")]//Move&Turn
    //public int attakcDamageForText;
    private Transform tr;
    private Rigidbody2D rigid;
    private SpriteRenderer rander;
    private Vector3 currPos;
    public Vector2 moveVec;
    public bool moveable=true;



    [Header("PlayerStatus")]//PlayerStatus
    public PlayerStatus playerStatus;
    

    //�÷��̾� ����//
    [Header("Dash")]
    //public float moveSpeed;
    public float baseDashCoolTime;
    public float dashCoolTime;
    public float dashRange;
    




    [Header("Skill")]
    public InherenceSkill inherenceSkill;

    public bool Skill_B;

    public Vector2 RollVec; //������ ������ ���� ����

    public bool shieldOn;

    [Header("UI")]
    //public Image UI_HP_Bar;
    public Image UI_EXP_Bar;
    public Image UI_Portrait;




    //[SerializeField] public Slider HP_Bar;
    [SerializeField] public Text HPText;


    [SerializeField]public GameObject FirePos; // ����ü�� �����Ǵ� ��ġ�� ��� �ִ� ��ü, FirePosPivot�� �θ�� �ΰ� �־ FirePosPivot�� ĳ���� ��ġ���� ���콺 ���� ���� ȸ���ϸ� FirePos�� ĳ���͸� �߽����� ���� �׸��� ����
    [SerializeField]public  GameObject FirePosPivot; // ĳ���Ϳ� ���ӵǾ ������ ��ǥ���� ������ ���콺 ���⿡ ���� ȸ�� -> FirePosPivot = ��� ���� 

    
    [SerializeField]private Text LevelText;

    [SerializeField]private GameObject DamageFont;
    [SerializeField]private GameObject DashEffect;

    [SerializeField]private Slider DashCooltimeBar;
    [SerializeField]private GameObject Tornado;
    [SerializeField]private GameObject Test;
    [SerializeField]private GameObject LevelUPText;
    [SerializeField]private GameObject LevelUPEffect;
    [SerializeField]private GameObject LevelUPText_Effect;

    
    Vector3 mouseVec;


    public GameObject Skill_Bezier_Projectile;
    public GameObject nomalAttack_Projectile;
    public GameObject PowerAttack_Projectile;
    //public GameManager gameManager;
    private int attackCount = 0;

    private bool Attack_AfterRoll;
    //float tempspeed;

    [Header("�÷��̾� �����̻�")]
    public GameObject Stun_Eft;

    [Header("ManaReduceDebuff")]
    public GameObject DebuffEffect_ManaReduce;
    public bool isManaReduceOn;
    IEnumerator AttackCor;

    // Start is called before the first frame update
    void Awake()
    {
        playerInstance = this.gameObject;
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rander = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        playerStatus = GetComponentInChildren<PlayerStatus>();
        inherenceSkill = GetComponentInChildren<InherenceSkill>();


        colliderA = GetComponents<BoxCollider2D>()[0];
        colliderB = GetComponents<BoxCollider2D>()[1];
        

        dashCoolTime = baseDashCoolTime;

        //tempspeed = moveSpeed;

        LevelText.text = "Level." + playerStatus.getPlayerLevel().ToString(); 
        StartCoroutine(ActiveSkill_ByCool());
    }
    private void FixedUpdate()
    {
        if (!isDie)
        {
            GetInput();
            Turn();
            Move();
        }
    }
   

    void GetInput()
    {
        if (!StopMove)
        {
            h = ((int)Input.GetAxisRaw("Horizontal"));
            v = ((int)Input.GetAxisRaw("Vertical"));
        }

        wDown = Input.GetKey(KeyCode.W);
        wUp = Input.GetKeyUp(KeyCode.W);
        sDown = Input.GetKey(KeyCode.S);
        sUp = Input.GetKeyUp(KeyCode.S);
        aDown = Input.GetKey(KeyCode.A);
        aUp = Input.GetKeyUp(KeyCode.A);
        dDown = Input.GetKey(KeyCode.D);
        dUp = Input.GetKeyUp(KeyCode.D);

        //if (Input.GetMouseButton(0))
        //{
            if(AttackON && !StopMove && !isDie)
            {
                AttackCor = BaseAttack();
                StartCoroutine(AttackCor);
            }

        //}

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Roll());
        }

        if (Input.GetMouseButton(1))
        {
            if (playerStatus.getCurrentMP() >= inherenceSkill.elementalSkill.MP_Cost && inherenceSkill.elementalSkill.castable)
            {
                StartCoroutine(CastElementalSkill());
            }
        }
    }

    #region Attack

    IEnumerator BaseAttack()
    {
        if (!stop)
        {
            if (AttackON && !StopMove && !isDie)
            {
                AttackON = false;
                //StopMoveForAttack = true;
                attackCount += 1;
                sound.PlayOneShot(Snd_Shot, 0.1f);

                SkillManagement.GetInput("NormalAttack", null);
                anim.SetBool("isAttack", true);
                anim.SetTrigger("Attack");
                playerStatus.addCurrentMP(playerStatus.getMP_Recovery());

                yield return new WaitForSeconds(0.1f);

                if (attackCount % 4 == 0)
                {
                    StartCoroutine(ActivateSkill_ByNormalAttack());// FirePos = ����ü�� ������ġ, FirePosPivot = ����ü�� ���󰡴� ���� 
                }
                else
                {
                    Instantiate(nomalAttack_Projectile, FirePos.transform.position, FirePosPivot.transform.rotation);
                }

                if (DashAttack)
                {
                    SkillManagement.GetInput("NormalAttack_AfterDash", null);
                    DashAttack = false;
                }

                yield return new WaitForSeconds(1/playerStatus.getAttackSpeed());
                AttackON = true;
            }
        }
    }
    //IEnumerator BaseAttack_AfterRoll()
    //{
    //    if (!stop)
    //    {
    //        if (AttackON && Attack_AfterRoll && !StopMove && !isDie)
    //        {

    //            //Attack_AfterRoll = false;
    //            AttackON = false;
    //            attackCount += 1;
    //            sound.PlayOneShot(Snd_Shot, 0.1f);
    //            //tempspeed = moveSpeed;
    //            //moveSpeed = 0;
    //            SkillManagement.GetInput("NormalAttack", null);
    //            anim.SetBool("isAttack", true);
    //            anim.SetTrigger("Attack");
    //            playerStatus.addCurrentMP(playerStatus.getMP_Recovery());
    //            //StartCoroutine(MP_Increase());

    //            yield return new WaitForSeconds(0.1f);

    //            if (attackCount % 4 == 0)
    //            {
    //                StartCoroutine(ActivateSkill_ByNormalAttack());// FirePos = ����ü�� ������ġ, FirePosPivot = ����ü�� ���󰡴� ���� 
    //            }
    //            else
    //            {
    //                Instantiate(nomalAttack_Projectile, FirePos.transform.position, FirePosPivot.transform.rotation);

    //            }

    //            if (DashAttack)
    //            {
    //                SkillManagement.GetInput("NormalAttack_AfterDash", null);
    //                DashAttack = false;
    //            }

    //            yield return new WaitForSeconds(1/playerStatus.getAttackSpeed());
    //            Attack_AfterRoll = false;
    //            AttackON = true;
    //        }
    //    }
    //}

 

    IEnumerator ActiveSkill_ByCool()
    {
        //Instantiate(Skill_Bezier_Projectile, FirePos.transform.position, transform.rotation);
        yield return new WaitForSeconds(5f);
        //StartCoroutine(ActiveSkill_ByCool());
    }
    IEnumerator ActivateSkill_ByNormalAttack()
    {
        if (Skill_B)
        {
            Instantiate(PowerAttack_Projectile, FirePos.transform.position, FirePosPivot.transform.rotation);
        }
        else
        {
            Instantiate(nomalAttack_Projectile, FirePos.transform.position, FirePosPivot.transform.rotation);
        }
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator DashCoolBarOn()
    {
        DashCooltimeBar.gameObject.SetActive(true);
        for (int i =0; i < 100; i++)
        {
            DashCooltimeBar.value += 1;
            yield return new WaitForSeconds(dashCoolTime/100);
        }

        DashCooltimeBar.value = 0;
        DashCooltimeBar.gameObject.SetActive(false);
        Rollable = true;
    }

    #endregion


    #region Movement
    void Move()
    {
        moveVec = (Vector2.up * v) + (Vector2.right * h);
        if (!stop&&moveable)
        {
            if (!StopMove) // ��� ���� �̵�. ������ ���� �̵��� �Է¹��� �ʱ� ����. StopMove = ������ �ִϸ��̼��� ������ �� Ture�� �ǰ� ������ �ִϸ��̼��� ������ ��������Ʈ �� False�� ��
            {
                if (!StopMoveForAttack)
                {
                    tr.position += (Vector3)moveVec * playerStatus.getMovementSpeed() * Time.deltaTime;
                }
            }
            else // StopMove�� True�� ��Ȳ�� ������ �ִϸ��̼��� ���� ���� �� �ۿ� ���⿡ StopMove�� True�� ��, ĳ���Ͱ� ���콺 �������� ������ �̵��ϵ��� ���� �߾��. 
            {
                //Instantiate(DashEffect,transform.position, Quaternion.identity);
                tr.position += (Vector3)RollVec * dashRange * Time.deltaTime; // ������� �̵� ĳ���Ͱ� �̵��ϴ� ȿ��
            }
        }

        if (wDown || aDown || sDown || dDown)// Ű���� �Է� -> �ȱ� �ִϸ��̼� ��ȯ
        {
            if (!isAnimOn) //�̹� �ִϸ��̼��� ��� ���ε� �� �õ��� �ɾ ù ��° ��������Ʈ�� ����Ǵ� �� �����ϱ� ����
            {
                anim.SetBool("isMove", true);
                anim.SetTrigger("W");
                isAnimOn = true; 

            }
        }
        else //Ű���忡�� �ƹ��͵� �Է¹ް� ���� �ʴ� ���� -> Idle�� ��ȯ
        {
            anim.SetBool("isMove", false);
            isAnimOn = false;
        }
    }
    void AttackOver() //���� ����� �̺�Ʈ �Լ� -> ���� �ִϸ��̼� �� ������ ��������Ʈ �� ȣ�� ��
    {
        //moveSpeed = playerStatus.getMovementSpeed();//-basespeed����
        StopMoveForAttack = false;
        anim.SetBool("isAttack", false);
    }
    void RollOver() //������ ����� �̺�Ʈ �Լ� ->������ �ִϸ��̼� �� ������ ��������Ʈ �� ȣ�� ��
    {
        
        SkillManagement.GetInput("RollOver", null);
        DashAttack = true;
        StartCoroutine(RollAttack());
        AttackON = true;
        Attack_AfterRoll = true;
        rigid.velocity = Vector2.zero; //Ȥ�� �𸣴� �浹�� ���� ������ ��Ȳ�� ���� ���ؼ�
        StopMove = false;
        isRoll = false;
        //GetComponent<TrailRenderer>().enabled = false;
        anim.SetBool("isRoll", false);
    }

    void AnimOver()// �ȱ�� Idle ����� �̺�Ʈ �Լ� -> ���� ����
    {
        isAnimOn = false;
    }
    IEnumerator Roll() //������
    {
        if (Rollable&&!stop&&moveable)
        {
            if (AttackCor != null)
            {
                StopCoroutine(AttackCor);
            }
            
            sound.PlayOneShot(Snd_Dash,0.1f);
            StartCoroutine(DashCoolBarOn());
            
            isRoll = true;
            Rollable = false; // ������ ��Ÿ���� ���� ����
            StopMove = true; // ������ �ִϸ��̼� ���� �ٸ� �ִϸ��̼��� ������� �ʰ� �ϱ� ���� ���� -> �ִϸ��̼� �̺�Ʈ�� ������ �ִϸ��̼��� ������ ��������Ʈ ��� ��, ȣ���Ͽ� false ó��
            rigid.velocity = Vector2.zero; //Ȥ�� �𸣴� �浹�� ���� ��Ȳ�� ���� ���ؼ�
                                           // GetComponent<TrailRenderer>().enabled = true;
            SkillManagement.GetInput("Roll", null);
            anim.SetBool("isRoll", true);
            anim.SetTrigger("Roll");

            RollVec = FirePosPivot.transform.TransformDirection(Vector2.right); // FirePosPivot�� PC���� ���콺�� ���ϴ� �����̼� ���� ������ �ֱ⿡ �����⸦ �Է��� ��, 
            Instantiate(DashEffect, transform.position, FirePosPivot.transform.rotation, transform);
            yield return new WaitForSeconds(dashCoolTime);                              // �ش� ������ FirePosPivot�� �����̼ǰ��� ���Ͱ�����  ��ȯ�Ͽ� RollVec�� �����ϰ� Move()���� �ش� ���Ͱ��� �̿��Ͽ� ������ �̵�
            
        } // ������� ĳ���Ͱ� �̵��ϴ� ȿ���� Move()�� �־��
    }
    IEnumerator RollAttack()
    {
        yield return new WaitForSeconds(0.3f);
        DashAttack = false;
    }
    void Turn()
    {

        Vector3 mPosition = Input.mousePosition; 
        Vector3 oPosition = transform.position; 
        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        mouseVec = target;
        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        if( 45.0f<= rotateDegree && rotateDegree < 135.0f)//���콺�� PC���� ���� ���� ��
        {
            anim.SetInteger("Vertical", 1 );
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange"); // ������ ����Ǿ����� �˸��� ���ؼ�
        }
        else if ( -135.0f < rotateDegree && rotateDegree < -45.0f) //���콺�� PC���� �Ʒ��� ���� ��
        {
            anim.SetInteger("Vertical", -1);
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange");
        }
        else if (-45.0f < rotateDegree && rotateDegree <= 45.0f) //���콺�� PC���� �����ʿ� ���� ��
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", 1);
            anim.SetTrigger("DirChange");
        }
        else if ((rotateDegree <= 180.0f && rotateDegree > 135.0f) || (rotateDegree <= -135.0f && rotateDegree > -180.0f)) //���콺�� PC���� ���ʿ� ���� ��
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", -1);
            anim.SetTrigger("DirChange");
        }
        FirePosPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree); // ��� ���� ����, PC���� ���콺�� ���ϴ� ����� ����
    }

    public void setRollable(bool val)
    {
        Rollable = val;
    }

    #endregion

    
    private IEnumerator CastElementalSkill()
    {
       
        playerStatus.addCurrentMP(-inherenceSkill.elementalSkill.MP_Cost);
        //SkillManagement.GetInput("ManaOn",null);
        inherenceSkill.CastSkill();
        yield return new WaitForSeconds(0f);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectile") ) && !isInvincible)
        {
            
            if (collision.GetComponents<BoxCollider2D>()[0].enabled)
            {
                if (collision.GetComponents<BoxCollider2D>()[0] == collision)   //�̰� ����?
                {
                    if (collision.GetComponent<EnemyCtrl>() != null)
                    {
                        int getDamage = collision.GetComponent<EnemyCtrl>().damage;

                        EnemyCollised(getDamage);
                    }
                }
            }
        }
    }

    public void EnemyCollised(int damage)
    {
        if (!isNoHitTime && !isRoll)
        {
            damage -= PlayerStatus.instance.getArmorPoint();
            if (damage <= 0)
            {
                damage = 0;
            }
            //Text damageText = Instantiate(DamageFont, transform).GetComponent<Text>();
            if (inherenceSkill.Damaged())
            {
                //SkillManagement.GetInput("ClassSkill", null);//->����
                shieldOn = false;
                damage = 0;
                //attakcDamageForText = "Damage";


                GameObject damageText = Instantiate(DamageFont, transform);
                damageText.transform.Find("Text").GetComponent<Text>().text = "Protected!";
                damageText.transform.Find("Text").GetComponent<Text>().color = Color.cyan;
                damageText.transform.Find("Text").GetComponent<Text>().fontSize += 10;

                //Text damageText = Instantiate(DamageFont, transform).GetComponent<Text>();
                //damageText.text = "BLOCK";
                //damageText.color = Color.gray;
                //damageText.fontSize += 10;
            }
            else
            {
                //attakcDamageForText = Damage;
                GameObject damageText = Instantiate(DamageFont, transform);
                damageText.transform.Find("Text").GetComponent<Text>().text = damage.ToString();

                //Text damageText = Instantiate(DamageFont, transform).GetComponent<Text>();
                //damageText.text = damage.ToString();
            }
            isNoHitTime = true;
            Camera.main.GetComponent<CameraShake>().VibrateForTime(0.3f);

            //playerStatus.StartCoroutine(addCurrentHP(-damage));
            //playerStatus.StartCoroutine(HPBarReduce(damage));
            playerStatus.addCurrentHP(-damage);
            StartCoroutine(damagedEffect());
            if (playerStatus.getCurrentHP() <= 0)
            {
                //HPText.enabled = false;
                //HP_Bar.gameObject.SetActive(false);
                StartCoroutine(PlayerDie());
            }
        }
    }
    IEnumerator damagedEffect()
    {
        UI_Portrait.sprite = Portrait_Hit;
        Color tempColor = rander.color;
        //HPText.color -= new Color(0f, 0.9f, 0.9f, 0f);
        for (int i = 0; i < 1; i++)
        {
            rander.color -= new Color(0f, 0.7f, 0.7f, 0f);
            yield return new WaitForSeconds(0.1f);

            rander.color += new Color(0f, 0.7f, 0.7f, 0f);
            yield return new WaitForSeconds(0.1f);
        }

        //HPText.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        rander.color = Color.white;
        UI_Portrait.sprite = Portrait_Normal;

        isNoHitTime = false;
    }



    #region HP&MP&EXP UI

    //public IEnumerator HPBarReduce(int damage)
    //{

    //    for (int i = 0; i < damage; i++)
    //    {
    //        HP_Bar.value -= 1f;
    //        playerStatus.addCurrentHP(-1);
    //        UI_HP_Bar.fillAmount = (float)playerStatus.getCurrentHP() / (float)playerStatus.getMaxHP();
    //        yield return new WaitForSeconds(0.4f / damage);
    //        HPText.text = "HP " + playerStatus.getCurrentHP().ToString();
    //        //currentHp -= Damage;
    //        if (playerStatus.getCurrentHP() <= 0)
    //        {
    //            HPText.enabled = false;
    //            HP_Bar.gameObject.SetActive(false);
    //            StartCoroutine(PlayerDie());
    //            break;
    //        }
    //    }
    //    //HPText.text = "HP " + currentHp.ToString();
    //}

    //public void HPBar()
    //{
    //    HP_Bar.maxValue = playerStatus.getMaxHP(); ;
    //    HP_Bar.value = playerStatus.getCurrentHP();

    //    UI_HP_Bar.fillAmount = (float)playerStatus.getCurrentHP() / (float)playerStatus.getMaxHP();
    //    HPText.text = "HP " + playerStatus.getCurrentHP().ToString();
    //}
    
    public void GetExp(int m_exp)
    {
        StartCoroutine(EXP_Bar_Icrease(m_exp));
    }
    IEnumerator EXP_Bar_Icrease(int m_exp)
    {
        for (int i = 0; i < m_exp; i++)
        {
            playerStatus.addCurrentPlayerEXP(1);
            UI_EXP_Bar.fillAmount = (float)playerStatus.getCurrentPlayerEXP() / playerStatus.getMaxPlayerEXP();
            yield return new WaitForSeconds(0.01f);
            //HPText.text = "HP " + currentHp.ToString();

            if (playerStatus.getCurrentPlayerEXP() >= playerStatus.getMaxPlayerEXP()/*&&LevelUPText.activeSelf*/)
            {
                LevelUP();
            }
        }
    }
    //

    void LevelUP()
    {
        playerStatus.LevelUp();
        UI_EXP_Bar.fillAmount = 0;
        //LevelUPText.SetActive(true);
        LevelUPEffect.SetActive(true);
        Instantiate(LevelUPText_Effect, transform);
        //LevelUPText.GetComponent<TextAnimationSet>().StartCoroutine(LevelUPText.GetComponent<TextAnimationSet>().Flash());
        LevelText.text = "Level." + playerStatus.getPlayerLevel().ToString();
        sound.PlayOneShot(Snd_LevelUp, 0.1f);
        GameManager.OpenStatUI();
    }


    #endregion





    public void Die()
    {
        StartCoroutine(PlayerDie());
    }
    IEnumerator PlayerDie()
    {
        colliderA.enabled = false;
        colliderB.enabled = false;
        isDie = true;
        anim.SetBool("isDead",true);
        anim.SetTrigger("Die");
        //anim.SetInteger("Vertical", -1);
        //anim.SetInteger("Horizontal", 0);

        yield return new WaitForSeconds(1);
        while (rander.color.a > 0)
        {
            var color = rander.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a -= (1.0f * Time.deltaTime);

            rander.color = color;
            //wait for a frame
            yield return null;
        }
        //HPText.enabled = false;
        //HP_Bar.gameObject.SetActive(false);   //playerStatus�� �̵�
        GameManager.playerDie();
    }

    public Transform getFirePosPivotTransform()
    {
        return FirePosPivot.transform;
    }


    #region �����̻�
    public void PlayerOperation()
    {
        StartCoroutine(OperationCtrl());
    }
    public IEnumerator OperationCtrl()
    {
        stop = true;
        GameObject StunEft = Instantiate(Stun_Eft, transform);
        yield return new WaitForSeconds(2.0f);
        Destroy(StunEft);
        stop = false;
    }
    #endregion

    #region ���� ����Ʈ ��ü ���� ���� ���� ����� ȿ��
    // ���� ����Ʈ�� ���� ���� ������� ���õ� ȿ�� �Դϴ�. �ش� �κ��� �״�� �Ű� ���� �����ϸ� �Ǵµ�, �� �κ��� �÷��̾� ��ũ��Ʈ�� ��ġ�� ���� �� ������ �л����� ��������ּ���.
    // ������ ��� public bool isManaReduceOn, public GameObject DebuffEffect_ManaReduce �̷��� �� ������ �߰��� �ֽø� �˴ϴ�.
    public void GetDebuff(int m_type)
    {
        switch (m_type)
        {
            case 1: // ���� ����
                StartCoroutine(Debuff_ManaReduce());
                break;

            case 2:
                break;
        }
    }
    IEnumerator Debuff_ManaReduce()
    {
        GameObject m_debuffEffect = Instantiate(DebuffEffect_ManaReduce, transform);

        while (isManaReduceOn)
        {
            if (playerStatus.getCurrentMP() > 0)
            {
                playerStatus.addCurrentMP(-5);
            }

            GameObject damageText = Instantiate(DamageFont, transform);

            damageText.transform.Find("Text").GetComponent<Text>().text = "-5 MP";
            damageText.transform.Find("Text").GetComponent<Text>().color = Color.cyan;
            damageText.transform.Find("Text").GetComponent<Text>().fontSize -= 10;

            yield return new WaitForSeconds(2f); // 1�� ���� �ݺ�
        }
        Destroy(m_debuffEffect);
    }
    #endregion
}
