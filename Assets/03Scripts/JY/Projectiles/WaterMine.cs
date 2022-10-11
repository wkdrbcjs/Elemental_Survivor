using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMine : Projectile
{
    public float duration;
    public Animator WaterMineAnimator;
    public AudioClip Snd_WaterExplosion;
    // Start is called before the first frame update
    void Start()
    {
        ItemInfoInit(13);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);


        WaterMineAnimator = GetComponent<Animator>();
        _SM.SoundPlay(_SM.Water_MineStart);
        StartCoroutine(ExplosionSound());
        StartCoroutine(StartToIdle());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EnemyHit(collision))
        {
            if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[13].element.ToString())
            {
                collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterSlow(0.5f, duration));
            }
        }
    }

    IEnumerator ExplosionSound()
    {
        yield return new WaitForSeconds(1.4f);
        SoundPlay(Snd_WaterExplosion);
    }



    IEnumerator StartToIdle()
    {
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;     
        yield return new WaitForSeconds(1.0f);
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        WaterMineAnimator.SetBool("Idle", true);
        
        StartCoroutine(IdleToExplosion());
    }

    IEnumerator IdleToExplosion()
    {
        yield return new WaitForSeconds(ItemInfoSet.instance.Items[16].baseDuration);
        WaterMineAnimator.SetBool("Explosion", true);
        _SM.SoundPlay(_SM.Water_MineEnd);
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {       
        yield return new WaitForSeconds(1.2f);        
        Destroy(this.gameObject);
        StopAllCoroutines();
    }
    IEnumerator CollisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

}

