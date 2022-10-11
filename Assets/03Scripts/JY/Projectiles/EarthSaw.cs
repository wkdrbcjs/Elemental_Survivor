using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSaw : Projectile
{

    private SpriteRenderer SprRender;
    private float AttackTerm;
    void Start()
    {
        ItemInfoInit(6);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        SprRender = GetComponent<SpriteRenderer>();

        ItemSkill1(this.gameObject);
        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //StartCoroutine(ColisionOn());
        AttackTerm = 0.3f;

        if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[6].element.ToString())
        {
            attackDamage /= 2; 
            GetComponent<Animator>().speed = 3;
            AttackTerm = 0.1f;
        }

        _SM.SoundPlay(_SM.Earth_Saw);

        StartCoroutine(Destroy());
    }
    public float GetBaseCooltime()
    {
        return baseCooltime;
    }
    private void Update()
    {
        if (_player.transform.position.x > transform.position.x)//ÁÂ¿ì¹ÝÀü
        {
            SprRender.flipX = false;
            SprRender.flipY = true;
        }
        else
        {
            SprRender.flipX = true;
        }

        transform.Translate(Vector2.right * 30f * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, _target, 30.0f * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyHit(collision);
        StartCoroutine(TargetActive(monsterSize));
        //if (EnemyHit(collision))
        //{
        //    if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[6].element.ToString())
        //    {
        //        //EnemyHit(collision);
        //        //StartCoroutine(TargetActive());
        //    }
        //}
    }
    IEnumerator TargetActive(int a)
    {
        yield return new WaitForSeconds(AttackTerm);
        monster[a] = null;
    }

    /*IEnumerator ColisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);


        GetComponent<BoxCollider2D>().enabled = false;      
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(ColisionOn());
    }*/

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
        StopAllCoroutines();
    }
}
