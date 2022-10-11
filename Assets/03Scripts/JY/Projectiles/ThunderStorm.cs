using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStorm : Projectile
{
    public GameObject Target;
    void Start()
    {
        ItemInfoInit(16);

        defaultInit();

        if (_SM.PlayingThunder_StormNum <= 1)
        {
            _SM.SoundPlay(_SM.Thunder_Storm);
            StartCoroutine(SoundCheck());
        }

        // Àû¿ë //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        //StartCoroutine(Destroythis());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject != Target)
            {
                _skillManagement.GetInput("Enemy_Hit", collision.gameObject);
                EnemyHit(collision);
            }
        }
    }
    IEnumerator SoundCheck()
    {
        _SM.PlayingThunder_StormNum += 1;
        yield return new WaitForSeconds(0.1f);
        _SM.PlayingThunder_StormNum -= 1;
    }

    IEnumerator Destroythis()
    {
        Destroy(this.gameObject);
        yield return new WaitForSeconds(1.1f);
        StopAllCoroutines();
        Destroy(this.gameObject);
    }     
}
