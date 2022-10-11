using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthImpale : Projectile
{


    private void Start()
    {
        ItemInfoInit(0);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);
       
        ItemSkill5(this.gameObject);

        _SM.SoundPlay(_SM.Earth_Impale);

        StartCoroutine(Destroy());

        float objPosXCal = transform.position.x - _player.transform.position.x;
        if (objPosXCal < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            //transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (objPosXCal > 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
        //EnemyHit(collision);
    }
    public float GetBaseCooltime()
    {
        return baseCooltime;
    }
    void CollisionOn()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    void CollisionOff()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(this.gameObject);
    }
}
