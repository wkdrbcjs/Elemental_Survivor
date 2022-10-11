using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack_A : MonoBehaviour
{
    private SkillManagement _skillManagement;
    private GameObject[] monster;
    private PlayerCtrl Player;
    private GameObject TraceTarget;
    public List<GameObject> FoundObjects;
    int a = 0;
    Vector3 toPcVec;
    //투사체 기본 스탯//
    public int baseAttackDamage;
    public float baseKnockBack;
    public int basePenetration;
    public float baseProjectileScale;
    public float baseProjectileSpeed;

    //투사체 최종 스탯//
    public int attackDamage;
    public float knockBack;
    public int penetration;
    public float projectileSpeed;
    public float projectileScale;

    bool hit;

    void Start()
    {
        Player = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();
        _skillManagement = SkillManagement.instance;
        //값 세팅//
        attackDamage = baseAttackDamage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
        knockBack = baseKnockBack * PlayerStatus.instance.getKnockBack();
        penetration = basePenetration + PlayerStatus.instance.getPenetration();
        projectileSpeed = baseProjectileSpeed * PlayerStatus.instance.getProjectileSpeed();
        projectileScale = baseProjectileScale * PlayerStatus.instance.getProjectileScale();

        // 적용 //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        TraceTarget = GameObject.FindGameObjectWithTag("Player");
        FindTarget();

        //toPcVec = new Vector2(TraceTarget.transform.position.x - transform.position.x, TraceTarget.transform.position.y - transform.position.y);
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x));
        Quaternion angleAxis = Quaternion.AngleAxis(angle + Random.Range(-15,15), Vector3.forward);
        Quaternion vecAngleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = angleAxis;

        System.Array.Resize(ref monster, penetration);
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(Destroythis());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }
    private void FindTarget()
    {
        float shortDis;
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (FoundObjects.ToArray().Length == 0)
        {
            return;
        }
        else
        {
            shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);

            TraceTarget = FoundObjects[0];

            foreach (GameObject found in FoundObjects)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis)
                {
                    TraceTarget = found;
                    shortDis = Distance;
                }
            }
            toPcVec = new Vector2(TraceTarget.transform.position.x - transform.position.x, TraceTarget.transform.position.y - transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (hit == false)
        {
            hit = true;
            if (collision.gameObject.tag == "Enemy")
            {
                if (penetration > 0)
                {
                    int index = System.Array.IndexOf(monster, collision.gameObject);
                    if (index == -1)
                    {
                        monster[a] = collision.gameObject;
                        a += 1;
                        _skillManagement.GetInput("Enemy_Hit", collision.gameObject);

                        penetration -= 1;      //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, attackDamage, knockBack);
                        if (penetration <= 0)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
            hit = false;
        }
    }
    IEnumerator InflictAttack(Collider2D m_coll)
    {
        yield return new WaitForFixedUpdate();
        m_coll.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, attackDamage, knockBack);
    }
    IEnumerator Destroythis()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
    IEnumerator ThunderStormActive(Collider2D collision)
    {
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(0.3f);
        if (collision.gameObject != null)
        {
            //Instantiate(_skillManagement.ThunderStorm, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, attackDamage / 2, knockBack / 2);
        }
        else
        {
            //Debug.Log("asd");
        }

    }
}
