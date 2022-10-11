using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamedMonster : EnemyCtrl
{

    [Header("Patern")]
    private int PaternCoolTime;
    protected int paternCount;


    [Header("EnemyProjectile")]
    [SerializeField]
    protected GameObject attackProjectile_1;      //  ���� ���� Ư�� ����ü
    [SerializeField]
    protected GameObject attackProjectile_2;      //  ���� ���� ���� ����
    public GameObject TileSpawnPos;



    [Header("���� UI �� ���ӵ� ����")]
    [SerializeField]
    protected Sprite Portrait_spr;
    public GameObject NaviUI;
    protected GameObject m_NaviUI;
    public GameObject BossWall;
    [SerializeField]
    protected GameObject Spawned_BossWall;
    public GameObject Range;
    protected Rigidbody2D Rigid;


    [Header("etc")]
    public int basePaternCoolTime;
    public int basePaternCount;


    protected override void Start()
    {
        base.Start();
        Rigid = GetComponent<Rigidbody2D>();

        paternCount = basePaternCount;
        PaternCoolTime = basePaternCoolTime;

        invincible = true;
        m_NaviUI = Instantiate(NaviUI, transform.position, Quaternion.identity);
        m_NaviUI.GetComponent<NaviUi>().NamedMonster = this.gameObject;
        m_NaviUI.GetComponent<NaviUi>().Portrait.GetComponent<SpriteRenderer>().sprite = Portrait_spr;
        
        //  ���ӵ� ��ü ���� �ڷ�ƾ ȣ��
        StartCoroutine((MiniBoss_SpecialAttack_Cool()));
        //  �Ϲ� ���� �ڷ�ƾ = update �Լ��� distancecheck���� �̷���.

        isSuperArmor = true;
    }

    // Update is called once per frame
    protected new void FixedUpdate()
    {
        Trace();
        base.FixedUpdate();
    }

    protected override void distanceCheck()
    {
        if (isNamed && !isStateBattle)
        {
            anim.SetBool("isDuringAnim", true);
            movementSpeed = 0;
            toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y,
            0);

            if (Vector3.Distance(transform.position, TraceTarget.transform.position) <= 60)
            {
                Disarm();
            }
        }
    }
    public override void Disarm()
    {
        EnemyShield.SetActive(false);
        BoxColliderOnOff(true);
        invincible = false;
        Spawned_BossWall = Instantiate(BossWall, TraceTarget.transform.position, Quaternion.identity);
        anim.SetBool("isDuringAnim", false);
        if (!Critical)
        {
            StartCoroutine(FadeIn());
            isStateBattle = true;
            StartCoroutine(MiniBoss_NormalAttack_Cool());
            movementSpeed = baseMovementSpeed;
        }
    }    
    
    //  ���̵���
    IEnumerator FadeIn()
    {
        if (HP > 0)
        {
        SpriteRenderer spr = Range.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 50; i++)
        {
            spr.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.04f);
        }
        Destroy(Range);
        }
    }
    IEnumerator MiniBoss_SpecialAttack_Cool()
    {
        // ���ӵ� ��ü ���� �ڷ�ƾ �� �Լ� ȣ��
        PaternCoolTime -= 1;
        if (PaternCoolTime <= 0 && !isDead && !isFreezed)
        {
            StartCoroutine(NamedMonster_SpecialPattern());
           
            PaternCoolTime = basePaternCoolTime;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(MiniBoss_SpecialAttack_Cool());
    }
    protected virtual IEnumerator NamedMonster_SpecialPattern()
    {
        //Debug.Log("NamedMonster_SpecialPattern Base");
        yield return 0;
    }

    protected IEnumerator MiniBoss_NormalAttack_Cool() //  ���ӵ� �Ϲ� ����
    {
        attackCoolTime -= 1;
        if (attackCoolTime <= 0 && !isDead && !isFreezed)
        {
            StartCoroutine(NamedMonster_NormalAttack());

            attackCoolTime = CoolTime;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(MiniBoss_NormalAttack_Cool());
    }
    protected virtual IEnumerator NamedMonster_NormalAttack()
    {
        Debug.Log("NamedMonster_NormalAttack Base");
        yield return 0;
    }

    protected override void afterDieOver()
    {
        if (EnemyID == 6)
        {
            GameManager.isBossDie = true;
            DataSet.instance.killedBossSum += 1;
            int m_count = PlayerPrefs.GetInt("s_isBossDie");
            m_count += 1;
            PlayerPrefs.SetInt("s_isBossDie", m_count);

            return;

        }

        if (isNamed)
        {
            if (EnemyID == 9) { TraceTarget.GetComponent<PlayerCtrl>().isManaReduceOn = false; }

            if (DataSet.instance != null && exp != 0)
            {
                DataSet.instance.killedBossSum += 1;
            }

            Destroy(m_NaviUI);
            Destroy(SelectingObj);
            Destroy(Spawned_BossWall);

        }
    }


}
