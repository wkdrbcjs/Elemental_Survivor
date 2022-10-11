using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyCtrl : MonoBehaviour
{
    #region Variables
    public GameManager GameManager;
    public ObjectPoolManager ObjectPoolManager;

    public SoundManager _SM;
    public AudioSource sound;
    public AudioClip snd_Hit;
    bool HitSound = true;
    public Animator anim;
    protected SpriteRenderer SprRend;
    
    public int EnemyID;
    public enum EnemyType 
    {
        /*
        EnemyID
        0 : 웜
        1 : 밴디트, 배트, 구울, 코볼트, 리퍼, 락골렘, 사티르, 스프라우트, 언데드, 와스프(꿀벌), 울프
        2 : 골렘
        3 : 임프, 셰이드, 스파이크슬라임(투사체 1개 발사)
        4 : mon_TentacleSlime(투사체 3개 발사)
        5 : 미믹
        6 : 보스
        7 : 미노
        8 : 레오
        9 : 팬나
        10: 오거
         */
        FixedUpdate, 
        LateUpdate, 
        ManualUpdate,
    }

    public GameObject TraceTarget;
    private float objPosXCal;
    protected GameObject font;

    [Header("int Type")]
    public float movementSpeed;
    public float baseMovementSpeed;
    public int HP;
    protected int maxHP;
    public int attackRange;
    protected int attackCoolTime;
    public int CoolTime;
    public int damage;
    public int dropRate;
    public int exp;
    public int pararizeDamage;
    protected Vector3 toPcVec;

    [Header("Bool Type")]
    public bool isHit;
    public bool isDead;
    public bool isTrans;
    public bool invincible;
    protected bool isAttack = false;
    protected bool isSuperArmor = false;
    public bool ccImume = false;

    public bool isFreezed=false;
    public bool isParalyzed=false;
    public bool isStunned = false;
    public bool isBurned = false;
    protected bool isMoveAble = false;

    //처음은 오브젝트 풀상태가 아니니깐
    protected bool isObjectpool = false;


    [SerializeField]protected bool isStateBattle;

    public bool isBoss;
    public bool isNamed;


    [Header("EnemyProjectile")]
    [SerializeField]
    protected GameObject attackProjectile;        //  보스 몬스터 일반 투사체
    [SerializeField]
    protected GameObject SelectingObj;            //  미노타우로스-벽 객체, 오우거-고블린 무리 - 활성화 비활성화 원활하게 하기 위해서


    public GameObject EnemyShield;

    //[Header("Reward OR Mimic")]
    //[SerializeField]
    //protected GameObject ItemChest;
    //[SerializeField]
    //private GameObject Mimic;

    [Header ("DamageFont")]
    //public GameObject DamageFont;
    public GameObject DamageFontPos;
    public int attackDamageForText;

    //[Header("etc")]
    //public GameObject Freeze_Eft;
    //public GameObject Slow_Eft;
    //public GameObject Stun_Eft;
    //[SerializeField]
    //private GameObject HitEffect;

    public bool Critical = false;
    #endregion

    //  비활성화 상태에서 다시 활성화 되었을 때 = 오브젝트 풀링 되었을 때
    private void OnEnable()
    {
        //  혹시 모를 타겟 재설정
        TraceTarget = PlayerCtrl.playerInstance;
        if (TraceTarget == null)
        {
            TraceTarget = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected virtual void Start()
    {
        GameManager = GameManager.instance;
        ObjectPoolManager = ObjectPoolManager.instance;

        _SM = SoundManager.instance;
        TraceTarget = PlayerCtrl.playerInstance;
        if(TraceTarget==null)
        {
            TraceTarget=GameObject.FindGameObjectWithTag("Player");
        }

        anim = GetComponent<Animator>();
        SprRend = GetComponent<SpriteRenderer>();
        
        attackCoolTime = 5;
        maxHP = HP;
    }

    protected void FixedUpdate()
    {
        AttackMove();
        distanceCheck();
    }

    protected void BoxColliderOnOff(bool val)
    {
        GetComponents<BoxCollider2D>()[0].enabled = val;
        GetComponents<BoxCollider2D>()[1].enabled = val;
    }

    public void Trace()
    {
        if (EnemyID == 5 && isTrans == false)
        {
            movementSpeed = 0f;
        }

        //이 객체 포지션 = moveToward써서 지정 방향으로 이동시킬 것
        //지정 방향 : TraceTarget 방향
        if (!isDead && !isFreezed && !isStunned)
        {
            transform.position = Vector3.MoveTowards(transform.position,
               new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0), 
                movementSpeed * Time.deltaTime);
        }

        //  몬스터가 바라볼 방향 개선
        objPosXCal = transform.position.x - TraceTarget.transform.position.x;

        //  미믹과 보스몬스터들의 경우 
        //  다른 몬스터 스프라이트들과 달리 반대방향을 보고 있어서
        //  -1을 곱해주어 방향을 반대로 보게함.
        if (EnemyID == 5)
        {
            if (isTrans)
            { objPosXCal *= -1; }
            else
            { objPosXCal *= 0; }
        }

        //  웜 또는 보스 및 네임드이거나 고블린 이 외의 몬스터
        if (EnemyID == 0 || EnemyID >= 6 || EnemyID != 50)
        {
            objPosXCal *= -1;
        }

        if (!isDead)
        {
            //  스프라이트 좌우반전
            if (objPosXCal < 0f)
            {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (objPosXCal > 0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            { }
        }
    }

    private void AttackMove()
    {
        //  골렘과 레드오우거의 경우에 한하여 돌진 이동 처리
        //  애니메이션 처리는 각 몬스터 코루틴에서 처리
        //  isAttack 변수가 false 였다가 공격 쿨타임에 맞춰 true로 전환되면
        //  업데이트에 의해 돌진하는 이동처리가 진행
        if (isAttack && !isDead && !isFreezed)
        {
            if (EnemyID == 2)//골렘
                transform.position += toPcVec.normalized * 120.0f * Time.deltaTime;

            if (EnemyID == 8)//레드오우거(보스)
                transform.position += toPcVec.normalized * 900.0f * Time.deltaTime;
        }
    }

    protected virtual void distanceCheck()
    {
        //if(isNamed)
        //Debug.Log("Base distanceCheck");
    }

    public virtual void Disarm()
    {
        //Debug.Log("Base disarm");
    }

    #region Hit, KnockBack, MakeDamageFont
    public void Hit(GameObject collision, int attackDamage, float knockBack, Color? DamageColor = null, bool FontBold = false)
    {       
        DamageColor = DamageColor.HasValue ? DamageColor.Value : Color.white;
        Vector2 knockBackVec = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
        if(HitSound && _SM.PlayingHitSoundNum <=1)
        {
            _SM.SoundPlay(_SM.HitSound,0.2f);
            StartCoroutine(Sound());
        }           
        if (!isHit && !isDead)
        {
            StartCoroutine(KnockBack(knockBackVec, attackDamage, knockBack, DamageColor, FontBold));
        }
        if (isParalyzed)
        {
            StartCoroutine(ParalyzeDamage());
        }
    }
    IEnumerator ParalyzeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        takeDamage(pararizeDamage, new Color(255, 255, 0));
    }
    IEnumerator Sound()
    {
        HitSound = false;
        _SM.PlayingHitSoundNum += 1;
        yield return new WaitForSeconds(0.2f);
        _SM.PlayingHitSoundNum -= 1;
        HitSound = true;
    }

    IEnumerator KnockBack(Vector2 m_knockBackVec, int attackDamage, float knockBack, Color ? DamageColor = null, bool FontBold=false)
    {//칼라는 컴파일상수가 아니라서 이렇게 써야됨 http://www.ngmaster.net/bbs/board.php?bo_table=study&wr_id=321
        Color BackColor = DamageColor.HasValue ? DamageColor.Value : Color.white;

        float knockBack_Base = 1.0f; //기존의 a, 별다른 변경 없음

        takeDamage(attackDamage, DamageColor, FontBold);
        if (!isAttack && !isSuperArmor && knockBack != 0 && !isDead)
        {
            anim.SetTrigger("Hit");
            anim.SetBool("isDuringAnim", true);
            float m_knockBack = knockBack;
            if (knockBack < 0)
            {
                m_knockBack *= -1f;
                knockBack_Base *= -1f;
            }

            if(EnemyID != 50)
                movementSpeed = 0;

            if (!(EnemyID >= 6))
            {
                for (int i = 0; i < m_knockBack * 10; i++)
                {
                    transform.position += (Vector3)m_knockBackVec.normalized * knockBack_Base;
                    knockBack_Base -= 1 / (knockBack * 10);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        //anim.SetBool("isDuringAnim", false);
        //isMonsterHPzero();
    }
    
    public bool takeDamage(int attackDamage, Color? DamageColor = null, bool FontBold = false)
    {
        Color BackColor = DamageColor.HasValue ? DamageColor.Value : Color.white;

        if (invincible)
        {
            attackDamage = 0;
        }
        attackDamageForText = attackDamage;

        HP -= attackDamage;

        MakeDamageFont(DamageColor, FontBold);
        Instantiate(MonsterManager.instance.MonsterHit_Eft, transform);
        afterTakeDamage();
        return isMonsterHPzero();
    }

    protected void afterTakeDamage()
    {
        
    }
    
    //  데미지 폰트 생성
    /*IEnumerator*/
    void MakeDamageFont(Color? DamageColor = null, bool FontBold = false)
    {
        Color FontColor = DamageColor.HasValue ? DamageColor.Value : Color.white;
        if (DamageFontPos!=null)
        {
            //var instance = Instantiate(DamageFont, DamageFontPos.transform.position, Quaternion.identity, transform);
            font = Instantiate(MonsterManager.instance.DamageFont, DamageFontPos.transform.position, Quaternion.identity, transform);
            DamageFontManage damageFontManage= font.GetComponent<DamageFontManage>();
            damageFontManage.text.text=attackDamageForText.ToString();
            damageFontManage.text.color = FontColor;
            if(FontBold)
            {
                damageFontManage.text.color = new Color(255, 255, 0);
                damageFontManage.text.fontSize = (int)(damageFontManage.text.fontSize*1.5f);
            }
        }
        //yield return new WaitForSeconds(0.0f);
    }
    #endregion

    #region EnemyHPCal
    bool isMonsterHPzero()
    {
        if (HP <= 0)
        {
            isAttack = false;
            isDead = true;

            movementSpeed = 0;

            //사망처리를 했는데도 간혹 플레이어 공격에 맞는 경우가 있음.
            //collier 꺼주기
            BoxColliderOnOff(false);

            //  고블린은 따로 진행하므로
            if (EnemyID != 50)
            {
                //  기존
                //Destroy(transform.GetChild(0).gameObject);
            }
            if(anim!=null)
            {
                anim.SetBool("isDuringAnim", true);
                anim.SetBool("isDead", true);
                anim.SetTrigger("Die");
            }

            //  보스 몬스터 이외에 나머지 몬스터들은 사망시 점멸해야 함.
            //  변경 : 사망시 점멸 -> 사망시 확률 체크
            if(EnemyID != 6)
            {
                StartCoroutine(ObjFlash());
            }
            else
            {
                GameManager.BossEndCutScene();
                StopAllCoroutines();
            }
            return true;
        }
        return false;
    }

    //  오브젝트 점멸
    //  스프라이트 랜더러 껐다켜는 방식
    protected IEnumerator ObjFlash()
    {
        yield return new WaitForSeconds(0.2f);
        CheckDropRate();

        //if (EnemyID != 50)
        //{
        //    CheckDropRate();
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    GoblinNonActive();
        //}
    }

    //  드롭 확률 체크
    protected virtual void CheckDropRate()
    {
        int rand = Random.Range(0, 1000);       // 0~999
        int objselect = Random.Range(0, 100);    // 0~9

        GameObject RewardORMimic;

        if (objselect > 6)
        { RewardORMimic = MonsterManager.instance.ItemChestOBJ; }
        else
        { RewardORMimic = MonsterManager.instance.MimicOBJ; }

        //  랜덤값이 몬스터 각각의 드롭율 값보다 작을 경우 미믹 or 보상 상자 드랍
        if (rand < dropRate)
        {
            Instantiate(RewardORMimic, transform.position, transform.rotation);
        }
    }
    #endregion


    #region etc
    public Vector3 GetRandomPosition(float range)
    {
        float radius = range;
        Vector3 SpawnerPosition = TraceTarget.transform.position;

        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);

        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - anchorPosX, 2));

        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        float y = y_b + anchorPosY;

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }

    public IEnumerator MonsterFreeze(float duration)   
    {
        if (!ccImume && !isFreezed)
        {
            isFreezed = true;
            movementSpeed = 0.0f;
            //SprRend.color = Color.cyan;//new Color(100,150,255,1);
            //GameObject frezzeEft = Instantiate(FreezeEffect, transform);
            GameObject freezeEft = Instantiate(MonsterManager.instance.Freeze_Eft, transform);
            freezeEft.GetComponent<Animator>().SetInteger("Num", Random.Range(0, 4));
            freezeEft.GetComponent<Animator>().SetTrigger("On");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            
            yield return new WaitForSeconds(0.1f);
            anim.speed = 0;
            yield return new WaitForSeconds(duration);
            freezeEft.GetComponent<Animator>().SetTrigger("End");
            
            yield return new WaitForSeconds(0.2f);
            Destroy(freezeEft);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            anim.speed = 1;
            //SprRend.color = Color.white;
            movementSpeed = baseMovementSpeed;
            isFreezed = false;
        }
    }

    public IEnumerator MonsterSlow(float Value, float duration)
    {
        if (!ccImume && !isParalyzed)
        {
            movementSpeed -= movementSpeed * Value;
            GameObject SlowEft = Instantiate(MonsterManager.instance.Slow_Eft, transform);
            isParalyzed = true;
            yield return new WaitForSeconds(duration);
            Destroy(SlowEft);
            movementSpeed = baseMovementSpeed;
            isParalyzed = false;
        }           
    }

    public IEnumerator MonsterStun(float duration)
    {
        if (!ccImume && !isStunned)
        {
            float m_BaseMovementSpeed = baseMovementSpeed;
            movementSpeed = 0.0f;
            baseMovementSpeed = 0.0f;
            GameObject StunEft = Instantiate(MonsterManager.instance.Stun_Eft, transform);
            isStunned = true;
            yield return new WaitForSeconds(duration);
            Destroy(StunEft);
            baseMovementSpeed = m_BaseMovementSpeed;
            movementSpeed = baseMovementSpeed;
            isStunned = false;
        }
    }

    void DieOver()
    {
        #region 경험치 처리
        //CheckDropRate();
        GameManager.enemyCount -= 1;
        TraceTarget.GetComponent<PlayerCtrl>().GetExp(exp);

        GameManager.killedEnemySum += 1;
        GameManager.monsterExpSum += exp;

        if (DataSet.instance!=null)
        {
            DataSet.instance.killedMonsterSum += 1;
            DataSet.instance.monsterExpSum += exp;
        }
        #endregion

        //  사망했을 때 전체 대상자 : 보스, 네임드, 일반, 미믹, 고블린
        //  보스   : 엔딩 위해 Destroy 되면 안 됨.
        //  네임드 : afterDieOver 후 삭제되어야 함.
        //  일반몹 : 오브젝트 풀링 대상이므로 Destroy되면 안 됨.
        //  미믹   : Destroy되어도 됨.
        //  고블린 : 자체적인 오브젝트 풀링을 가지고 있기 때문에 Destroy되면 안 됨.

        //  네임드들과 보스의 afterDieOver처리를 위해 사용
        //  일반 몬스터들은 afterDieOver에 작성된 것이 없기 때문에
        afterDieOver();
        //  그 후 네임드와 미믹은 삭제 되도록
        if (isNamed || EnemyID == 5)
        {
            Destroy(gameObject, 0.3f);
        }
        //  오브젝트 풀링 준비
        //  네임드와 미믹은 삭제되므로 처리 필요X
        //  보스, 미믹, 고블린은 오브젝트 풀링에서 제외 대상
        else if (!isBoss && EnemyID != 5 && EnemyID != 50)
        {
            forPoolling();
        }
        else { }
    }

    protected virtual void afterDieOver()
    { }

    private void forPoolling()
    {
        //  오브젝트 풀링을 위해 비활성화
        //  이후 OnEnable 정보 초기화하여 객체 활성화만 다시 해주면 객체 재사용 가능
        gameObject.SetActive(false);    //  객체 비활성화
        StopAllCoroutines();

        //  객체 재사용할 때 DamagefontPos가 있어야 데미지 표시가 되니깐
        //  혹시 모를 지워지지 않은 데미지 표시 객체들만 지워주기
        int childCount = transform.childCount;
        for (int i = 2; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //  상태 초기화 - BoxCollider, boo변수등 
        //  그리고 EnemyMonster.cs에서도 OnEnable 사용하여 몬스터 공격 처리 해주기
        SprRend.color = new Color(255, 255, 255, 255);
        BoxColliderOnOff(true);
        isAttack = false;
        isDead = false;
        isStateBattle = false;

        //  처음 몬스터가 생성될 때부터 OnEnable -> start 호출 순으로 진행되면 안되므로
        isObjectpool = true;

        //  HP, movementSpeed, animation 등
        HP = maxHP;
        movementSpeed = baseMovementSpeed;

        if (anim != null)
        {
            anim.SetBool("isDuringAnim", false);
            anim.SetBool("isDead", false);
        }

        //  정보 초기화 끝났다면 오브젝트 풀링을 해줄 객체로 정보 전달 -> ObjectPoolManager
        ObjectPoolManager.GetComponent<ObjectPoolManager>().enemyObjs.Add(this.gameObject);
    }

    void HitOver()
    {
        movementSpeed = baseMovementSpeed;
        anim.SetBool("isDuringAnim", false);
        
    }
    void AttackOver()
    {
        movementSpeed = baseMovementSpeed;
        anim.SetBool("isDuringAnim", false);
        
    }
    #endregion

}

