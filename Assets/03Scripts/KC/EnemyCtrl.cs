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
        0 : ��
        1 : ���Ʈ, ��Ʈ, ����, �ں�Ʈ, ����, ����, ��Ƽ��, �������Ʈ, �𵥵�, �ͽ���(�ܹ�), ����
        2 : ��
        3 : ����, ���̵�, ������ũ������(����ü 1�� �߻�)
        4 : mon_TentacleSlime(����ü 3�� �߻�)
        5 : �̹�
        6 : ����
        7 : �̳�
        8 : ����
        9 : �ҳ�
        10: ����
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

    //ó���� ������Ʈ Ǯ���°� �ƴϴϱ�
    protected bool isObjectpool = false;


    [SerializeField]protected bool isStateBattle;

    public bool isBoss;
    public bool isNamed;


    [Header("EnemyProjectile")]
    [SerializeField]
    protected GameObject attackProjectile;        //  ���� ���� �Ϲ� ����ü
    [SerializeField]
    protected GameObject SelectingObj;            //  �̳�Ÿ��ν�-�� ��ü, �����-��� ���� - Ȱ��ȭ ��Ȱ��ȭ ��Ȱ�ϰ� �ϱ� ���ؼ�


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

    //  ��Ȱ��ȭ ���¿��� �ٽ� Ȱ��ȭ �Ǿ��� �� = ������Ʈ Ǯ�� �Ǿ��� ��
    private void OnEnable()
    {
        //  Ȥ�� �� Ÿ�� �缳��
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

        //�� ��ü ������ = moveToward�Ἥ ���� �������� �̵���ų ��
        //���� ���� : TraceTarget ����
        if (!isDead && !isFreezed && !isStunned)
        {
            transform.position = Vector3.MoveTowards(transform.position,
               new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0), 
                movementSpeed * Time.deltaTime);
        }

        //  ���Ͱ� �ٶ� ���� ����
        objPosXCal = transform.position.x - TraceTarget.transform.position.x;

        //  �̹Ͱ� �������͵��� ��� 
        //  �ٸ� ���� ��������Ʈ��� �޸� �ݴ������ ���� �־
        //  -1�� �����־� ������ �ݴ�� ������.
        if (EnemyID == 5)
        {
            if (isTrans)
            { objPosXCal *= -1; }
            else
            { objPosXCal *= 0; }
        }

        //  �� �Ǵ� ���� �� ���ӵ��̰ų� ��� �� ���� ����
        if (EnemyID == 0 || EnemyID >= 6 || EnemyID != 50)
        {
            objPosXCal *= -1;
        }

        if (!isDead)
        {
            //  ��������Ʈ �¿����
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
        //  �񷽰� ���������� ��쿡 ���Ͽ� ���� �̵� ó��
        //  �ִϸ��̼� ó���� �� ���� �ڷ�ƾ���� ó��
        //  isAttack ������ false ���ٰ� ���� ��Ÿ�ӿ� ���� true�� ��ȯ�Ǹ�
        //  ������Ʈ�� ���� �����ϴ� �̵�ó���� ����
        if (isAttack && !isDead && !isFreezed)
        {
            if (EnemyID == 2)//��
                transform.position += toPcVec.normalized * 120.0f * Time.deltaTime;

            if (EnemyID == 8)//��������(����)
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
    {//Į��� �����ϻ���� �ƴ϶� �̷��� ��ߵ� http://www.ngmaster.net/bbs/board.php?bo_table=study&wr_id=321
        Color BackColor = DamageColor.HasValue ? DamageColor.Value : Color.white;

        float knockBack_Base = 1.0f; //������ a, ���ٸ� ���� ����

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
    
    //  ������ ��Ʈ ����
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

            //���ó���� �ߴµ��� ��Ȥ �÷��̾� ���ݿ� �´� ��찡 ����.
            //collier ���ֱ�
            BoxColliderOnOff(false);

            //  ����� ���� �����ϹǷ�
            if (EnemyID != 50)
            {
                //  ����
                //Destroy(transform.GetChild(0).gameObject);
            }
            if(anim!=null)
            {
                anim.SetBool("isDuringAnim", true);
                anim.SetBool("isDead", true);
                anim.SetTrigger("Die");
            }

            //  ���� ���� �̿ܿ� ������ ���͵��� ����� �����ؾ� ��.
            //  ���� : ����� ���� -> ����� Ȯ�� üũ
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

    //  ������Ʈ ����
    //  ��������Ʈ ������ �����Ѵ� ���
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

    //  ��� Ȯ�� üũ
    protected virtual void CheckDropRate()
    {
        int rand = Random.Range(0, 1000);       // 0~999
        int objselect = Random.Range(0, 100);    // 0~9

        GameObject RewardORMimic;

        if (objselect > 6)
        { RewardORMimic = MonsterManager.instance.ItemChestOBJ; }
        else
        { RewardORMimic = MonsterManager.instance.MimicOBJ; }

        //  �������� ���� ������ ����� ������ ���� ��� �̹� or ���� ���� ���
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
        #region ����ġ ó��
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

        //  ������� �� ��ü ����� : ����, ���ӵ�, �Ϲ�, �̹�, ���
        //  ����   : ���� ���� Destroy �Ǹ� �� ��.
        //  ���ӵ� : afterDieOver �� �����Ǿ�� ��.
        //  �Ϲݸ� : ������Ʈ Ǯ�� ����̹Ƿ� Destroy�Ǹ� �� ��.
        //  �̹�   : Destroy�Ǿ ��.
        //  ��� : ��ü���� ������Ʈ Ǯ���� ������ �ֱ� ������ Destroy�Ǹ� �� ��.

        //  ���ӵ��� ������ afterDieOveró���� ���� ���
        //  �Ϲ� ���͵��� afterDieOver�� �ۼ��� ���� ���� ������
        afterDieOver();
        //  �� �� ���ӵ�� �̹��� ���� �ǵ���
        if (isNamed || EnemyID == 5)
        {
            Destroy(gameObject, 0.3f);
        }
        //  ������Ʈ Ǯ�� �غ�
        //  ���ӵ�� �̹��� �����ǹǷ� ó�� �ʿ�X
        //  ����, �̹�, ����� ������Ʈ Ǯ������ ���� ���
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
        //  ������Ʈ Ǯ���� ���� ��Ȱ��ȭ
        //  ���� OnEnable ���� �ʱ�ȭ�Ͽ� ��ü Ȱ��ȭ�� �ٽ� ���ָ� ��ü ���� ����
        gameObject.SetActive(false);    //  ��ü ��Ȱ��ȭ
        StopAllCoroutines();

        //  ��ü ������ �� DamagefontPos�� �־�� ������ ǥ�ð� �Ǵϱ�
        //  Ȥ�� �� �������� ���� ������ ǥ�� ��ü�鸸 �����ֱ�
        int childCount = transform.childCount;
        for (int i = 2; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //  ���� �ʱ�ȭ - BoxCollider, boo������ 
        //  �׸��� EnemyMonster.cs������ OnEnable ����Ͽ� ���� ���� ó�� ���ֱ�
        SprRend.color = new Color(255, 255, 255, 255);
        BoxColliderOnOff(true);
        isAttack = false;
        isDead = false;
        isStateBattle = false;

        //  ó�� ���Ͱ� ������ ������ OnEnable -> start ȣ�� ������ ����Ǹ� �ȵǹǷ�
        isObjectpool = true;

        //  HP, movementSpeed, animation ��
        HP = maxHP;
        movementSpeed = baseMovementSpeed;

        if (anim != null)
        {
            anim.SetBool("isDuringAnim", false);
            anim.SetBool("isDead", false);
        }

        //  ���� �ʱ�ȭ �����ٸ� ������Ʈ Ǯ���� ���� ��ü�� ���� ���� -> ObjectPoolManager
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

