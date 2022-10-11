using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : Projectile
{

    private Animator WaterMineAnimator;
    private BoxCollider2D _watersplashCollider;

    void Start()
    {
        ItemInfoInit(1);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        WaterMineAnimator = gameObject.GetComponent<Animator>();
        _watersplashCollider = gameObject.GetComponent<BoxCollider2D>();

        ItemSkill5(this.gameObject);
        //SoundPlay(Snd_WaterMagic);
        StartCoroutine(Splash());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("PlayerAttack"))
            {
                return;
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (WaterMineAnimator.GetBool("Splash") == true)
                {
                    _skillManagement.GetInput("Enemy_Hit", collision.gameObject);
                    //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                    Debug.Log("Water Splash Hit");
                }
                else
                {
                    return;
                }
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                return;
            }
        }
        hit = false;*/
        if (WaterMineAnimator.GetBool("Splash") == true)
        {
            EnemyHit(collision);
        }
    }

    

    IEnumerator Splash()
    {
        yield return new WaitForSeconds(1.0f);
        WaterMineAnimator.SetBool("Splash", true);
        _SM.SoundPlay(_SM.Water_Splash);
        //_watersplashCollider.enabled = true;
        StartCoroutine(Destroy());
    }
    void CollisionOn()
    {
        
        _watersplashCollider.enabled = true;
    }
    void CollisionOff()
    {
        _watersplashCollider.enabled = false;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.6f);
        Destroy(this.gameObject);
        StopAllCoroutines();
    }
}
