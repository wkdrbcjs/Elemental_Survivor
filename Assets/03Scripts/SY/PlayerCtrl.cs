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
    private bool AttackON = true; //공격 쿨타임 때문에 공격 쿨이 전부 돌았음을 알리는 변수. NormalAttack()가 실행될 때, false기 되고 attackSpeed초가 지나면 Ture
    private bool Rollable = true;  //구르기 쿨타임 때문에 구르기를 쿨이 전부 돌았음을 알리는 변수. Roll()이 실행될 때 false가 되고 1초가 지나면 True;
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
    

    //플레이어 스탯//
    [Header("Dash")]
    //public float moveSpeed;
    public float baseDashCoolTime;
    public float dashCoolTime;
    public float dashRange;
    




    [Header("Skill")]
    public InherenceSkill inherenceSkill;

    public bool Skill_B;

    public Vector2 RollVec; //구르는 방향을 담을 벡터

    public bool shieldOn;

    [Header("UI")]
    //public Image UI_HP_Bar;
    public Image UI_EXP_Bar;
    public Image UI_Portrait;




    //[SerializeField] public Slider HP_Bar;
    [SerializeField] public Text HPText;


    [SerializeField]public GameObject FirePos; // 투사체가 생성되는 위치를 담고 있는 객체, FirePosPivot을 부모로 두고 있어서 FirePosPivot이 캐릭터 위치에서 마우스 방향 따라 회전하면 FirePos는 캐릭터를 중심으로 원을 그리며 공전
    [SerializeField]public  GameObject FirePosPivot; // 캐릭터에 종속되어서 동일한 좌표값을 가지며 마우스 방향에 따라 회전 -> FirePosPivot = 사격 방향 

    
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

    [Header("플레이어 상태이상")]
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
                    StartCoroutine(ActivateSkill_ByNormalAttack());// FirePos = 투사체의 생성위치, FirePosPivot = 투사체가 날라가는 방향 
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
    //                StartCoroutine(ActivateSkill_ByNormalAttack());// FirePos = 투사체의 생성위치, FirePosPivot = 투사체가 날라가는 방향 
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
            if (!StopMove) // 평상 시의 이동. 구르는 동안 이동을 입력받지 않기 위함. StopMove = 구르기 애니메이션이 시작할 때 Ture가 되고 구르기 애니메이션의 마지막 스프라이트 때 False가 됨
            {
                if (!StopMoveForAttack)
                {
                    tr.position += (Vector3)moveVec * playerStatus.getMovementSpeed() * Time.deltaTime;
                }
            }
            else // StopMove가 True인 상황은 구르기 애니메이션이 진행 중일 때 밖에 없기에 StopMove가 True일 때, 캐릭터가 마우스 방향으로 빠르게 이동하도록 설정 했어요. 
            {
                //Instantiate(DashEffect,transform.position, Quaternion.identity);
                tr.position += (Vector3)RollVec * dashRange * Time.deltaTime; // 구르기로 이동 캐릭터가 이동하는 효과
            }
        }

        if (wDown || aDown || sDown || dDown)// 키보드 입력 -> 걷기 애니메이션 전환
        {
            if (!isAnimOn) //이미 애니메이션이 출력 중인데 또 시동을 걸어서 첫 번째 스프라이트만 재생되는 걸 방지하기 위함
            {
                anim.SetBool("isMove", true);
                anim.SetTrigger("W");
                isAnimOn = true; 

            }
        }
        else //키보드에서 아무것도 입력받고 있지 않는 상태 -> Idle로 전환
        {
            anim.SetBool("isMove", false);
            isAnimOn = false;
        }
    }
    void AttackOver() //공격 모션의 이벤트 함수 -> 공격 애니메이션 맨 마지막 스프라이트 때 호출 됨
    {
        //moveSpeed = playerStatus.getMovementSpeed();//-basespeed였음
        StopMoveForAttack = false;
        anim.SetBool("isAttack", false);
    }
    void RollOver() //구르기 모션의 이벤트 함수 ->구르기 애니메이션 맨 마지막 스프라이트 때 호출 됨
    {
        
        SkillManagement.GetInput("RollOver", null);
        DashAttack = true;
        StartCoroutine(RollAttack());
        AttackON = true;
        Attack_AfterRoll = true;
        rigid.velocity = Vector2.zero; //혹시 모르는 충돌로 인한 좆같은 상황을 막기 위해서
        StopMove = false;
        isRoll = false;
        //GetComponent<TrailRenderer>().enabled = false;
        anim.SetBool("isRoll", false);
    }

    void AnimOver()// 걷기와 Idle 모션의 이벤트 함수 -> 위와 동일
    {
        isAnimOn = false;
    }
    IEnumerator Roll() //구르기
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
            Rollable = false; // 구르기 쿨타임을 위한 변수
            StopMove = true; // 구르는 애니메이션 동안 다른 애니메이션이 재생되지 않게 하기 위한 변수 -> 애니메이션 이벤트로 구르기 애니메이션의 마지막 스프라이트 재생 시, 호출하여 false 처리
            rigid.velocity = Vector2.zero; //혹시 모르는 충돌로 인한 상황을 막기 위해서
                                           // GetComponent<TrailRenderer>().enabled = true;
            SkillManagement.GetInput("Roll", null);
            anim.SetBool("isRoll", true);
            anim.SetTrigger("Roll");

            RollVec = FirePosPivot.transform.TransformDirection(Vector2.right); // FirePosPivot이 PC에서 마우스로 향하는 로테이션 값을 가지고 있기에 구르기를 입력할 때, 
            Instantiate(DashEffect, transform.position, FirePosPivot.transform.rotation, transform);
            yield return new WaitForSeconds(dashCoolTime);                              // 해당 시점의 FirePosPivot의 로테이션값을 벡터값으로  변환하여 RollVec에 저장하고 Move()에서 해당 벡터값을 이용하여 구르기 이동
            
        } // 구르기로 캐릭터가 이동하는 효과는 Move()에 있어요
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

        if( 45.0f<= rotateDegree && rotateDegree < 135.0f)//마우스가 PC보다 위에 있을 때
        {
            anim.SetInteger("Vertical", 1 );
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange"); // 방향이 변경되었음을 알리기 위해서
        }
        else if ( -135.0f < rotateDegree && rotateDegree < -45.0f) //마우스가 PC보다 아래에 있을 때
        {
            anim.SetInteger("Vertical", -1);
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange");
        }
        else if (-45.0f < rotateDegree && rotateDegree <= 45.0f) //마우스가 PC보다 오른쪽에 있을 때
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", 1);
            anim.SetTrigger("DirChange");
        }
        else if ((rotateDegree <= 180.0f && rotateDegree > 135.0f) || (rotateDegree <= -135.0f && rotateDegree > -180.0f)) //마우스가 PC보다 왼쪽에 있을 때
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", -1);
            anim.SetTrigger("DirChange");
        }
        FirePosPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree); // 사격 방향 설정, PC에서 마우스로 향하는 방향과 동일
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
                if (collision.GetComponents<BoxCollider2D>()[0] == collision)   //이건 뭐임?
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
                //SkillManagement.GetInput("ClassSkill", null);//->폭발
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
        //HP_Bar.gameObject.SetActive(false);   //playerStatus로 이동
        GameManager.playerDie();
    }

    public Transform getFirePosPivotTransform()
    {
        return FirePosPivot.transform;
    }


    #region 상태이상
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

    #region 팬텀 나이트 전체 패턴 마나 감소 디버프 효과
    // 팬텀 나이트의 마나 감소 디버프에 관련된 효과 입니다. 해당 부분을 그대로 옮겨 가서 적용하면 되는데, 이 부분은 플레이어 스크립트와 겹치니 적용 후 전세윤 학생에게 말씀드려주세요.
    // 변수의 경우 public bool isManaReduceOn, public GameObject DebuffEffect_ManaReduce 이렇게 두 가지만 추가해 주시면 됩니다.
    public void GetDebuff(int m_type)
    {
        switch (m_type)
        {
            case 1: // 마나 감소
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

            yield return new WaitForSeconds(2f); // 1초 마다 반복
        }
        Destroy(m_debuffEffect);
    }
    #endregion
}
