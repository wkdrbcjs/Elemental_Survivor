using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBolt : Projectile
{

   
    private void Start()
    {      

        ItemInfoInit(2);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);        

        System.Array.Resize(ref monster, penetration + 3000);

        //ItemSkill6(this.gameObject);
        _SM.SoundPlay(_SM.Wind_Shadow);
        StartCoroutine(Destroy());  
    }

    private void Update()
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
                if (collision.gameObject != null)
                {
                    int Index = System.Array.IndexOf(monster, collision.gameObject);
                    if (Index == -1)
                    {
                        monsterSize += 1;
                        if (monster.Length < monsterSize)
                        {
                            System.Array.Resize<GameObject>(ref monster, monsterSize * 2);
                        }
                        monster[monsterSize] = collision.gameObject;
                        //_skillManagement.GetInput("Enemy_Hit", collision.gameObject);

                        if (CalculateCritical(criticalChance))
                        {


                            //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                            collision.gameObject.GetComponent<EnemyCtrl>().Hit(_player, (int)(attackDamage * criticalDamage), knockBack, CriticalColor, true);
                        }
                        else
                        {

                            //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                            collision.gameObject.GetComponent<EnemyCtrl>().Hit(_player, (int)attackDamage, knockBack, DamageColor);
                        }
                        hit = false;
                    }
                }
            }
            hit = false;
        }
        EnemyHit(collision);
    }

    IEnumerator TargetActive(int a)
    {
        yield return new WaitForSeconds(0.2f);
        //monster[a-1] = null;
    }
    IEnumerator Destroythis()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
    IEnumerator Destroy()
    {     
        yield return new WaitForSeconds(6.0f);
        Destroy(this.gameObject);
    }
}
