using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAdditional : MonoBehaviour
{
    public List<GameObject> FoundEnemys;
    public GameObject Enemy;
    public float shortDis;
    private PlayerCtrl Player;

    private GameObject[] monster;
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

    private bool hit;
    // Start is called before the first frame update
    void Start()
    {
        Player = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();
        //GetComponent<Animator>().SetInteger("Random", Random.Range(0, 3));

        
        //값 세팅//
        attackDamage = baseAttackDamage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
        knockBack = baseKnockBack * PlayerStatus.instance.getKnockBack();
        penetration = basePenetration + PlayerStatus.instance.getPenetration();
        projectileSpeed = baseProjectileSpeed * PlayerStatus.instance.getProjectileSpeed();
        projectileScale = baseProjectileScale * PlayerStatus.instance.getProjectileScale();

        // 적용 //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, 1000);
        transform.position += new Vector3(Random.Range(-50, 50), Random.Range(-40, 40),0);
        if (Player.gameObject.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
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

                        penetration -= 1;      //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(Player.gameObject, attackDamage, knockBack);
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

    IEnumerator Bash()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
