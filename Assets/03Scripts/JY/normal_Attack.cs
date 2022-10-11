using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_Attack : Projectile
{
    // Start is called before the first frame update
    //private int penetration = 3;
    private PlayerCtrl Player;
    private Animator anim;


    public string SkillName = "Skill_";


    void Start()
    {
        anim = GetComponent<Animator>();
        Player = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();
        //값 세팅//


        defaultInit();

        // 적용 //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration+1);
        GetComponent<BoxCollider2D>().enabled = true;

        StartCoroutine(Destroythis());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(projectileSpeed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (penetration > 0)
                {
                    if(collision.gameObject != null)
                    {
                        int index = System.Array.IndexOf(monster, collision.gameObject);
                        if (index == -1)
                        {
                            monster[monsterSize] = collision.gameObject;
                            monsterSize += 1;
                            penetration -= 1;
                            _skillManagement.GetInput("Enemy_Hit", collision.gameObject);

                            if (CalculateCritical(criticalChance))
                            {


                                //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                                collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)(attackDamage * criticalDamage), knockBack, CriticalColor, true);
                            }
                            else
                            {

                                //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                                collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack, DamageColor);
                            }
                            if (penetration <= 0)
                            {
                                GetComponent<BoxCollider2D>().enabled = false;
                                anim.SetTrigger("Disable");
                                projectileSpeed = 0f;
                                //Destroy(this.gameObject);
                            }
                        }
                    }                   
                }
            }
            hit = false;
        }
    }
    IEnumerator Destroy()
    {
        Destroy(this.gameObject);
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Destroythis()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}


