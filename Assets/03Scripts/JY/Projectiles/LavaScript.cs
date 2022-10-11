using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : Projectile
{
    //���� Ÿ�� ����//


    public float duration;


    private void Start()
    {
        ItemInfoInit(20);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        //System.Array.Resize(ref monster, 1000);
        System.Array.Resize(ref monster, penetration + 3000);

        ItemSkill3(this.gameObject);
        _SM.SoundPlay(_SM.Fire_Lava);
        StartCoroutine(LavaDestroy());
        StartCoroutine(ColisionOn());       
    }

   

    IEnumerator LavaDestroy()
    {
        yield return new WaitForSeconds(baseDuration);
        Destroy(gameObject);
    }
    IEnumerator ColisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);


        GetComponent<BoxCollider2D>().enabled = false;
        //System.Array.Clear(monster, 0, 1000);
        //a = 0;
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(ColisionOn());
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                int index = System.Array.IndexOf(monster, collision.gameObject);
                if (index == -1)
                {
                    monster[a] = collision.gameObject;
                    a += 1;

                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                    if(ItemInfoSet.instance.Items[7].ItemLevel > 7)
                    {
                        //���� �̼Ӱ��� ���� �Լ��� ���Ϳ��� ������ ���� ��ó�� ó���ؾ��� �� �ϳ׿�.
                        //collision.gameObject.GetComponent<EnemyCtrl>().movementSpeed *= 0.7f;
                        collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterSlow(0.3f, duration));
                    }                   
                }
                collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                if (ItemInfoSet.instance.Items[7].ItemLevel > 7)
                {
                    //���� �̼Ӱ��� ���� �Լ��� ���Ϳ��� ������ ���� ��ó�� ó���ؾ��� �� �ϳ׿�.
                    //collision.gameObject.GetComponent<EnemyCtrl>().movementSpeed *= 0.7f;
                    collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterSlow(0.3f, duration));
                }
            }
            hit = false;
        }*/     
        //EnemyHit(collision);
        
        if(EnemyHit(collision))
        {
            if (ItemInfoSet.instance.Items[7].ItemLevel > 7)
            {
                //���� �̼Ӱ��� ���� �Լ��� ���Ϳ��� ������ ���� ��ó�� ó���ؾ��� �� �ϳ׿�.
                //collision.gameObject.GetComponent<EnemyCtrl>().movementSpeed *= 0.7f;
                collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterSlow(0.3f, duration));
            }
        }
        StartCoroutine(TargetActive(monsterSize, monster));
    }    
}
