using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBall : Projectile
{  
   
    public float duration;

    private Animator anim;


    void Start()
    {
        ItemInfoInit(4);

        defaultInit();

        anim = GetComponent<Animator>();     
           
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);
       
        System.Array.Resize(ref monster, penetration + 3000);
        duration = baseDuration;
        ItemSkill2(this.gameObject);
        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _SM.SoundPlay(_SM.Thunder_Ball);
        StartCoroutine(DurationCool());
    }
 
    void Update()
    {        
        transform.Translate(projectileSpeed * Time.deltaTime * Vector2.right);
    }
    private void OnTriggerStay2D(Collider2D collision)
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
                    //System.Array.Resize(ref monster, a+1);
                    a += 1;
                    //penetration -= 1;      //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//

                    //_skillManagement.GetInput("Enemy_Hit", collision.gameObject);

                    StartCoroutine(TargetActive(a)); // 연속 타격을 위해 일정 시간 지나면 배열에서 비우기

                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                    _skillManagement.GetInput("Enemy_Hit", collision.gameObject);
                    if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[4].element.ToString())
                    {
                        collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterSlow(0.3f,duration));
                    }
                }
            }
            hit = false;
        }*/
        EnemyHit(collision);
        StartCoroutine(TargetActive(monsterSize));
        //if (EnemyHit(collision))
        //{
        //    TargetActive(monsterSize);
        //    if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[4].element.ToString())
        //    {
        //        //collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterSlow(0.5f, duration));
        //    }
        //}
    }
  
    IEnumerator DurationCool()
    {
        yield return new WaitForSeconds(5.4f);       
        anim.SetBool("End", true);
        StartCoroutine(Over());
    }
    IEnumerator TargetActive(int a)
    {
        yield return new WaitForSeconds(0.2f);
        monster[a] = null;
    }
    IEnumerator Over()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}
