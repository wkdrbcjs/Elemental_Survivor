using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    public List<GameObject> FoundEnemys;
    public GameObject Enemy;
    public float shortDis;

    private GameObject[] monster;
    private PlayerCtrl Player;
    private SkillManagement _skillManagement;
    int a = 0;


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

    // Start is called before the first frame update
    void Start()
    {
        _skillManagement = SkillManagement.instance;
        Player = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();

        //값 세팅//
        baseAttackDamage += (int)(baseAttackDamage * PlayerStatus.instance.getMaxHP() * 0.001f);
        attackDamage = baseAttackDamage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
        knockBack = baseKnockBack * PlayerStatus.instance.getKnockBack();
        penetration = basePenetration + PlayerStatus.instance.getPenetration();
        projectileSpeed = baseProjectileSpeed * PlayerStatus.instance.getProjectileSpeed();
        projectileScale = baseProjectileScale * PlayerStatus.instance.getProjectileScale();

        // 적용 //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, 1000);


        
        //Bash();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void NearestGameObject()
    {
        Vector3 mPosition = Input.mousePosition;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        FoundEnemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        int i = 0;
        foreach (GameObject found in FoundEnemys)
        {
            
            float Distance = Vector3.Distance(transform.position, found.transform.position);

            if (Distance < 30)
            {
                //shortDis = Distance;
                monster[i] = found;
                i += 1;
            }
        }
        FoundEnemys.Clear();
    }
    void NearestProjectile()
    {
        Vector3 mPosition = Input.mousePosition;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        FoundEnemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyProjectile"));

        //int i = 0;
        foreach (GameObject found in FoundEnemys)
        {

            float Distance = Vector3.Distance(transform.position, found.transform.position);

            if (Distance < 50)
            {
                if(found.GetComponent<EnemyProjectile>().Type == 1)
                {
                    found.GetComponent<EnemyProjectile>().Speed = 0f;
                    found.GetComponent<Animator>().SetTrigger("End");

                }
                else
                {
                    Destroy(found.gameObject);
                }
                
            }
        }
    }
    void Bash()
    {
        NearestGameObject();
        NearestProjectile();
        //GetComponent<CircleCollider2D>().enabled = true;
        for (int i=0; i< monster.Length; i++)
        {
            if (monster[i] != null)
            {
                monster[i].GetComponent<EnemyCtrl>().Hit(this.gameObject, attackDamage, 0f);
                Vector2 knockBackVec = new Vector2(monster[i].transform.position.x - transform.position.x, monster[i].transform.position.y- transform.position.y);
                StartCoroutine(DragEnemy(knockBackVec, monster[i]));
            }
        }
    }
    IEnumerator DragEnemy(Vector2 dragVec, GameObject m_mon)
    {
        if(m_mon.GetComponent<EnemyCtrl>().isBoss)
        {
            yield break;
        }
        float a = 1.0f;
        //m_mon.transform.position += (Vector3)dragVec * a* Time.deltaTime;
        //yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < knockBack * 10; i++)
        {
            if(m_mon == null) { yield break; }
            m_mon.transform.position += (Vector3)dragVec.normalized * a;
            a -= 1 / (knockBack * 10);
            yield return new WaitForSeconds(0.01f);
        }
    }
    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
