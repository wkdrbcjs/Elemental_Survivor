using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : Projectile
{



    private void Start()
    {
        ItemInfoInit(17);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, penetration + 3000);

        StartCoroutine(ColisionOn());       

        Invoke("Delete", 1.0f);
    }

   

    IEnumerator ColisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.2f);

        GetComponent<BoxCollider2D>().enabled = false;
        System.Array.Clear(monster, 0, 1000);
        monsterSize = 0;
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(ColisionOn());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //collision.gameObject.GetComponent<MonsterAI>().Hit(this.gameObject, attackDamage, knockBack);
                int index = System.Array.IndexOf(monster, collision.gameObject);
                if (index == -1)
                {
                    monster[a] = collision.gameObject;
                    a += 1;

                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                    if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[17].element.ToString())
                    {
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                    }
                }
            }
            hit = false;
        }*/
        if(EnemyHit(collision))
        {
            if (_skillManagement.ElementClass == ItemInfoSet.instance.Items[17].element.ToString())
            {
                EnemyHit(collision);
            }
        }
    }
    void Delete()
    {       
        Destroy(gameObject);       
    }  
}
