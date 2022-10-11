using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : Projectile
{   
    public float duration;



    private void Start()
    {
        ItemInfoInit(18);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        ItemSkill2(this.gameObject);
        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _SM.SoundPlay(_SM.Water_IceShot);
        StartCoroutine(DestroyProjectile());
    }

   
    private void Update()
    {
        transform.Translate(projectileSpeed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (penetration > 0)
                {
                    
                        //_skillManagement.GetInput("Enemy_Hit", collision.gameObject);

                        penetration -= 1;      //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                        collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterFreeze(duration));

                        if (penetration <= 0)
                        {
                            Destroy(this.gameObject);
                        }
                   
                }
            }
            hit = false;
        }*/
        if(EnemyHit(collision))
        {
            collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterFreeze(duration));
            if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[18].element.ToString())
            {
                collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterFreeze(duration));
                _SM.SoundPlay(_SM.Freeze_SFX);
            }
        }
    }    
    
    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(7.0f);
        Destroy(this.gameObject);
    }
}
