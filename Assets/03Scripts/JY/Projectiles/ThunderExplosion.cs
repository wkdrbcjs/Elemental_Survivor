using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderExplosion : Projectile
{
    bool isActivate;
    public float duration;
    private Animator ThunderExplosionAnimator;
    public AudioClip Snd_ThunderExplosion;
    // Start is called before the first frame update
    void Start()
    {
        ItemInfoInit(10);

        defaultInit();

        System.Array.Resize(ref monster, penetration + 3000);
        duration = baseDuration;
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);
        ThunderExplosionAnimator = gameObject.GetComponent<Animator>();
        _SM.SoundPlay(_SM.Thunder_MineStart);
        StartCoroutine(StopCollider());
        StartCoroutine(Destroy());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            if (ThunderExplosionAnimator.GetCurrentAnimatorStateInfo(0).IsName("Thunder Explosion Inactive"))
            {
                //Debug.Log("ThunderExplosion Active");
                ThunderExplosionAnimator.SetBool("Active", true);
                //StartCoroutine(OnCollider());
                isActivate = true;
                _SM.SoundPlay(_SM.Thunder_MineEnd);
                //StartCoroutine(Destroy());
            }           
        }
        /*if (ThunderExplosionAnimator.GetBool("Active") == true)
        {
            if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[10].element.ToString())
            {
                collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterStun(duration));
            }            
        }*/              
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (isActivate)
        {
            if(EnemyHit(collision))
            {
                if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[10].element.ToString())
                {
                    collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterStun(duration));
                }
            }
            StartCoroutine(TargetActive(monsterSize));
        }
    }  

    IEnumerator TargetActive(int a) // 피격 간격을 설정하는 함수
    {
        yield return new WaitForSeconds(0.2f);
        monster[a] = null; //몬스터 배열에서 해당 몬스터를 지워서 다시 피격될 수 있게 설정
    }


    IEnumerator StopCollider()
    {
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    IEnumerator OnCollider()
    {
        for(int i =0; i<10; i++)
        {
            //isActivate = false;

            yield return new WaitForSeconds(0.1f);

            
            //isActivate = true;
        }
    }
    IEnumerator Destroy()
    {
        //yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(5.0f);        
        Destroy(this.gameObject);
        //StopAllCoroutines();
    }
}
