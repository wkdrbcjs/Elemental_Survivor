using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : Projectile
{


    // Start is called before the first frame update
    void Start()
    {
        ItemInfoInit(9);


        defaultInit();

        //System.Array.Resize(ref monster, penetration + 100);
        System.Array.Resize(ref monster, penetration + 3000);

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        ItemSkill5(this.gameObject);
        _SM.SoundPlay(_SM.Fire_Beam);
        StartCoroutine(Destroy());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                _skillManagement.GetInput("Enemy_Hit", collision.gameObject);
                //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                if(ItemInfoSet.instance.Items[9].ItemLevel > 6)
                {
                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                }
            }
        }
        hit = false;*/
        EnemyHit(collision);
    } 

   
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
