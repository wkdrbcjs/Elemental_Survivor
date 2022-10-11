using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBite : Projectile
{
    bool isAttack;
    bool isCollisionOn;
    GameObject TargetMonster;

    [SerializeField]
    private Animator FireBiteAnimator;

    // Start is called before the first frame update
    void Start()
    {
        ItemInfoInit(15);

        defaultInit();

        //System.Array.Resize(ref monster, penetration + 100);
        System.Array.Resize(ref monster, penetration + 3000);

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);
        FireBiteAnimator = gameObject.GetComponent<Animator>();
        SoundPlay(Snd_FireMagic);
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        
        if (!isAttack)
        {
            if (TargetMonster == null)
            {
                FindNearestMonster(this.gameObject);
                TargetMonster = Monster;
            }
            
            //transform.position = Vector3.MoveTowards(transform.position, _target, baseProjectileSpeed * Time.deltaTime);
            angle = Mathf.Atan2(TargetMonster.transform.position.y - transform.position.y, TargetMonster.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Translate(Vector2.right * baseProjectileSpeed * Time.deltaTime);
            ChekDistance();
        }
        else
        {
            transform.Translate(Vector2.right * 50f * Time.deltaTime);
        }
        //transform.Translate(baseProjectileSpeed * Time.deltaTime * Vector2.right);
    }

    void ChekDistance()
    {
        if (Vector3.Distance(transform.position, TargetMonster.transform.position) <= 10 && isAttack == false)
        {
            isAttack = true;
            GetComponent<Animator>().SetTrigger("ActiveAttack");
            _SM.SoundPlay(_SM.Fire_FireBite);
            return;
        }
    }
    void ActiveAttack()
    {
        isCollisionOn = true;
    }

    void DisactiveAttack()
    {
        isCollisionOn = false;
        isAttack = false;
        FindNearestMonster(this.gameObject);
        TargetMonster = Monster;
    }
    void CollisionOn()
    {
        isCollisionOn = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                _skillManagement.GetInput("Enemy_Hit", collision.gameObject);
                //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                if(ItemInfoSet.instance.Items[15].ItemLevel > 4)
                {
                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                }
            }
        }
        hit = false;*/
        if (isCollisionOn)
        {
            EnemyHit(collision);
        }
        
        StartCoroutine(TargetActive(monsterSize));
    }
    IEnumerator TargetActive(int a) // 피격 간격을 설정하는 함수
    {
        yield return new WaitForSeconds(0.5f);
        monster[a] = null; //몬스터 배열에서 해당 몬스터를 지워서 다시 피격될 수 있게 설정
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(baseDuration);
        FireBiteAnimator.SetBool("FireBiteEnd", true);
        yield return new WaitForSeconds(0.6f);
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
    void FindNearestMonster(GameObject ItemObject)
    {
        FoundMonsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        //Debug.Log("foundmonsters"+FoundMonsters.Count);
        if (FoundMonsters.Count > 0)
        {
            shortDis = Vector2.Distance(transform.position, FoundMonsters[0].transform.position); // 첫번째를 기준으로 잡아주기 
            Monster = FoundMonsters[0]; // 첫번째를 먼저         
            foreach (GameObject found in FoundMonsters)
            {
                float Distance = Vector3.Distance(transform.position, found.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    shortDis = Distance;
                    Monster = found;
                }
            }

            //target = transform.position;
            //_target = Monster.transform.position;
        }
    }
}
