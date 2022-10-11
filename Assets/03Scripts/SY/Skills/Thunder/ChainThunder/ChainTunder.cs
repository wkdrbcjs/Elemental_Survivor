using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTunder : MonoBehaviour
{
    private List<GameObject> FoundMonsters = new List<GameObject>();
    public GameObject Monster;
    public float shortDis;

    private SkillManagement _skillManagement;
    private GameObject[] monster;
    private PlayerCtrl PC;
    int a = 1;
    private bool isFinding;

    //투사체 기본 스탯//
    public int baseAttackDamage;
    public float baseKnockBack;
    public int basePenetration;
    public float baseProjectileScale;
    public float baseProjectileSpeed;
    public int baseChance;
    //투사체 최종 스탯//
    public int attackDamage;
    public float knockBack;
    public int penetration;
    public float projectileSpeed;
    public float projectileScale;
    public int Chance;
    // Start is called before the first frame update
    public Vector2 DF = new Vector2(0, 0);
    private Vector2 CV = new Vector2(0, 0);
    private Vector2 SF = new Vector2(0, 0);
    public Vector2 Vec = new Vector2(0, 0);

    public bool GetTarget = false;
    float currentDist = 0f;
    float closetDist = 100f;
    float targetDist = 100f;
    int closeDistIndex = 0;
    int TargetIndex = -1;


    public float criticalChance = 0.0f;//~1.0f;
    public float criticalDamage = 0.0f;
    //private List<GameObject> MonsterList = new List<GameObject>();
    [SerializeField]
    private GameObject Test;
    void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        _skillManagement = SkillManagement.instance;


        //값 세팅//
        attackDamage = baseAttackDamage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
        knockBack = baseKnockBack * PlayerStatus.instance.getKnockBack();
        penetration = basePenetration + PlayerStatus.instance.getPenetration();
        projectileSpeed = baseProjectileSpeed * PlayerStatus.instance.getProjectileSpeed();
        projectileScale = baseProjectileScale * PlayerStatus.instance.getProjectileScale();
        Chance = baseChance;

        criticalChance += PlayerStatus.instance.getCriticalChance();
        criticalDamage += PlayerStatus.instance.getCriticalDamage();
        // 적용 //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, 1000);

        RandomGameObject(70f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //StartCoroutine(NearestGameObject());
        if (!isFinding)
        {
            if (Monster == null || Monster.GetComponent<EnemyCtrl>().isDead == true)
            {
                RandomGameObject(70f);
            }
            int u = System.Array.IndexOf(monster, Monster);
            if (u != -1)
            {
                RandomGameObject(70f);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Monster.transform.position, projectileSpeed * Time.deltaTime);

                Vec = new Vector2(Monster.transform.position.x - this.transform.position.x, Monster.transform.position.y - this.transform.position.y).normalized * projectileSpeed;
                LookAt();

                CV = Vec;
                if (Vector2.Distance(gameObject.transform.position, Monster.transform.position) <= 0.5f)
                {
                    int index = System.Array.IndexOf(monster, Monster);
                    if (index == -1)
                    {
                        monster[a] = Monster;
                        a += 1;
                        penetration -= 1;
                        if (CalculateCritical(criticalChance))
                        {

                            Monster.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)(attackDamage * criticalDamage), knockBack, Color.white, true);
                        }
                        else
                        {

                            Monster.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, attackDamage, knockBack);
                        }
                        _skillManagement.GetInput("Enemy_Hit", Monster);
                        if (Monster.name == "Boss(Clone)")
                        {
                            Destroy(this.gameObject);
                        }

                        RandomGameObject(70f);
                        if (a % 2 == 0)
                        {
                            monster[a - 2] = null;
                            a = 1;
                        }
                        //StartCoroutine(TargetActive(a));
                        if (penetration <= 0)
                        {
                            Destroy(this.gameObject);
                            return;
                        }

                        if (Random.Range(0, 1000) < Chance)
                        {
                            penetration += 1;
                        }
                    }
                }
            }
        }
    }

    void LookAt()
    {
        Quaternion angleAxis = Quaternion.Euler(0, 0, Mathf.Atan2(Vec.normalized.y, Vec.normalized.x) * Mathf.Rad2Deg);

        transform.rotation = angleAxis;
    }
    //void 
    void RandomGameObject(float Range)
    {
        isFinding = true;
        Collider2D[] Hit = Physics2D.OverlapCircleAll(transform.position, Range);

        GameObject m_Monster = null;
        bool first = true;
        for (int i = 0; i < Hit.Length; i++)
        {
            if (Hit[i].tag == "Monster")
            {
                int index = System.Array.IndexOf(monster, Hit[i].gameObject);
                if (index == -1)
                {
                    if (first)
                    {
                        shortDis = (Hit[i].transform.position - transform.position).sqrMagnitude;
                        first = false;
                    }
                    else
                    {
                        Vector3 offset = Hit[i].transform.position - transform.position;

                        float Distance = offset.sqrMagnitude;
                        //shortDis = Distance;
                        if (Distance <= shortDis)
                        {
                            shortDis = Distance;
                            m_Monster = Hit[i].gameObject;
                        }
                    }
                }
            }
        }
        if (m_Monster == null)
        {
            RandomGameObject(200f);
            return;
        }
        Monster = m_Monster;
        isFinding = false;
    }
    public bool CalculateCritical(float CriticalChance)
    {
        float randomValue = Random.Range(0.01f, 1.0f);
        //Debug.Log("randomValue=" + randomValue + "CC=" + CriticalChance);

        if (randomValue <= CriticalChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
