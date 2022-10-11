using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCombo : Projectile
{
    int i =0;
    private void Start()
    {
        ItemInfoInit(7);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        ItemSkill2(this.gameObject);
        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        SoundPlay(Snd_WaterMagic);
        Invoke(nameof(Delete), 2.4f);
        //StartCoroutine(CollosionCtrl());
    }
      

    private void Update()
    {
        ItemSkill3(this.gameObject);
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
                    _skillManagement.GetInput("Enemy_Hit", collision.gameObject);
                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                }
            }
        }
        hit = false;*/
        EnemyHit(collision);
    }
    IEnumerator CollisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void AngleCtlr()
    {
        ItemSkill2(this.gameObject);
        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        System.Array.Clear(monster, 0, penetration + 3000);
        _SM.SoundPlay(_SM.Water_WaterWhip[i]);
        i += 1;
    }

    /*IEnumerator CollosionCtrl()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1.1f);

        GetComponent<BoxCollider2D>().enabled = false;
        System.Array.Clear(monster, 0, 1000);
        a = 0;
        yield return new WaitForSeconds(0.2f);

        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1.0f);

        GetComponent<BoxCollider2D>().enabled = false;
        System.Array.Clear(monster, 0, 1000);
        a = 0;
        yield return new WaitForSeconds(0.3f);

        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1.2f);
    }*/


    void Delete()
    {
        Destroy(gameObject);
    }


}
