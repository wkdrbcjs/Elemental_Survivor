using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderVeil : Projectile
{
    private GameObject ThunderSheild;

    public float duration;

    //몬스터 타격 관리//
    public bool isEnhanced;

    private void Start()
    {
        ThunderSheild = transform.Find("Thunder Shield").gameObject;

        ItemInfoInit(21);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        if (isEnhanced)
        {
            ThunderSheild.SetActive(true);
            //TunderSheild.transform.localScale = new Vector3(projectileScale, projectileScale, 1);
        }

        //StartCoroutine(ColisionOn());
        
        StartCoroutine(Cool());
        //System.Array.Resize(ref monster, 1000);
    }
    private void Update()
    {
        ItemSkill3(this.gameObject);
    }

    IEnumerator ColisionOn()
    {
        System.Array.Clear(monster, 0, penetration + 3000);
        GetComponent<BoxCollider2D>().enabled = true;
        _SM.SoundPlay(_SM.Thunder_Veil);
        yield return new WaitForSeconds(0.1f);

        //StartCoroutine(ColisionOn());
    }
    IEnumerator ColisionOff()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
    }
    void Explosion()
    {
        System.Array.Clear(monster, 0, penetration + 3000);
        knockBack += 2;
        attackDamage *= 3;
        transform.localScale *= 1.5f;
        _SM.SoundPlay(_SM.Thunder_Storm);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator Cool()
    {
        yield return new WaitForSeconds(6f);
        GetComponent<Animator>().SetTrigger("Off");
        ThunderSheild.GetComponent<Animator>().SetTrigger("Off");
    }

    void over()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyHit(collision))
        {
            if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[21].element.ToString())
            {
                collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterStun(duration));
            }
        }
    }   
}