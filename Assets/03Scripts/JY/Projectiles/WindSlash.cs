using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlash : Projectile
{
    bool go;

    void Start()
    {
        ItemInfoInit(14);

        defaultInit();

        // System.Array.Resize(ref monster, 1000);
        System.Array.Resize(ref monster, penetration + 3000);

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);
        go = true;
        _SM.SoundPlay(_SM.Wind_Slash);
        StartCoroutine(Boom());

        //StartCoroutine(DestroyProjectile());
    }
    private void Update()
    {
        //transform.position = transform.GetChild(0).position;
        transform.position = Vector3.MoveTowards(transform.position, transform.GetChild(0).position, 55.0f * Time.deltaTime);
        if(!go && Vector3.Distance(_player.transform.position, transform.position) < 1.5)
        {
            StartCoroutine(DestroyProjectile());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);                                   
            }
            hit = false;
        }*/
        EnemyHit(collision); //EnemyHit(collision);
    }    

    IEnumerator DestroyProjectile()
    {
        //yield return new WaitForSeconds(3.3f);        
        gameObject.GetComponent<Animator>().SetBool("End", true);
        yield return new WaitForSeconds(0.7f);
        StopAllCoroutines();       
        Destroy(this.gameObject);        
    }
    IEnumerator Boom()
    {
        
        yield return new WaitForSeconds(1.5f);
        System.Array.Clear(monster, 0, penetration + 3000);
        go = false;
    }
}
