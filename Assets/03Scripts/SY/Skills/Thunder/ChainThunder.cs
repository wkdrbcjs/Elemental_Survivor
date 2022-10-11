using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainThunder : MonoBehaviour
{
    private List<GameObject> FoundEnemys = new List<GameObject>();
    public GameObject Monster;
    public float shortDis;

    private SkillManagement _skillManagement;
    private GameObject[] monster;
    private PlayerCtrl Player;
    //int monsterSize = 1;
    private bool isFinding;
    int a = 1;

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

    //public int baseChance;
    public int Chance;
    public float range = 50.0f;

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

    //private List<GameObject> MonsterList = new List<GameObject>();
    [SerializeField]
    private GameObject Test;
    void Start()
    {
        Player =PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();
        _skillManagement = GameObject.Find("SkillManagement").GetComponent<SkillManagement>();

        //값 세팅//
        //defaultInit();
        attackDamage = baseAttackDamage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
        knockBack = baseKnockBack * PlayerStatus.instance.getKnockBack();
        penetration = basePenetration;// + PC.penetration;
        projectileSpeed = baseProjectileSpeed;// * PC.projectileSpeed;
        projectileScale = baseProjectileScale;// * PC.projectileScale;

        // 적용 //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, 1000);

        RandomGameObject(range);
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
                return;
            }
            int u = System.Array.IndexOf(monster, Monster);
            if (u != -1)
            {
                RandomGameObject(70f);
            }
            else
            {
                //transform.position = Vector2.MoveTowards(transform.position,
                //new Vector3(Monster.transform.position.x, Monster.transform.position.y, 0),
                //projectileSpeed * Time.deltaTime);
                //transform.position = Vector2.MoveTowards(transform.position, Monster.transform.position, projectileSpeed * Time.deltaTime);
                transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
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
                        Monster.GetComponent<EnemyCtrl>().Hit(this.gameObject, attackDamage, knockBack);
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
    void UpdateTarget()
    {
        shortDis = 0f;
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
            if (Hit[i].tag == "Enemy")
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
            isFinding = false;
            return;
        }
        Monster = m_Monster;
        
    }
}
