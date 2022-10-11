using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{  

    

    private void Start()
    {
        baseAttackDamage += ItemInfoSet.instance.Items[0].baseAttackDamage;
        baseKnockBack = 1f;
        basePenetration = 1;
        baseProjectileSpeed = 10.0f;
        baseProjectileScale += ItemInfoSet.instance.Items[5].baseProjectileScale;
        SetPenetration();

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration);

        ItemSkill1(this.gameObject);       
        StartCoroutine(Destroy());
    }

    void SetPenetration()
    {
        if(ItemInfoSet.instance.Items[0].ItemLevel < 6)
        {
            basePenetration = 1;
        }
        else
        {
            basePenetration = 2;
        }
    }

   

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int a = 0;
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
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                        if (penetration <= 0)
                        {
                            StartCoroutine(Destroythis());
                        }
                    }
                }
            }
            hit = false;
        }
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
