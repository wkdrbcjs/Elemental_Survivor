using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCrash : Projectile
{


    public float duration;
    bool on;
    [SerializeField]
    private Collider2D[] objects;
 
    void Start()
    { 
        ItemInfoInit(12);


        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        ItemSkill5(this.gameObject);
        duration = baseDuration;

        _SM.SoundPlay(_SM.Earth_Crash);

        StartCoroutine(DestroyThis());
    }

   

    private void Update()
    {  
    }
    IEnumerator CollisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        objects = Physics2D.OverlapCircleAll(transform.position, 20.0f);

        if (objects.Length > 0 && (this.gameObject != null))
        {
            for (int index = 0; index < objects.Length; index++)
            {
                if (objects[index].CompareTag("Enemy") && objects[index] == true)
                {
                    objects[index].transform.position = Vector3.MoveTowards(objects[index].transform.position,
                    transform.position, 50f);
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().enabled = false;
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
                if(_skillManagement.ElementClass == ItemInfoSet.instance.Items[12].element.ToString())
                {
                    collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterStun(duration));
                }
            }
        }
        hit = false;*/
        if (EnemyHit(collision))
        {
            if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[12].element.ToString())
            {
                collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterStun(duration));
            }
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.position += (transform.position - collision.gameObject.transform.position) / 80;
        }
    }*/
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1.4f);
        for (int index = 0; index < objects.Length; index++)
        {
            System.Array.Clear(objects, 0, objects.Length);
        }
        Destroy(this.gameObject);
    }
}
