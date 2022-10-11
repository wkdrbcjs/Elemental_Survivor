using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpin : Projectile
{
   
    public Animator anim;
    public float lifetime = 1.4f;

    public bool activateTornado = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);
        
        //ItemSkill3(this.gameObject);
        StartCoroutine(AnimLifetime(lifetime));
    }

    IEnumerator AnimLifetime(float lifetime)
    {

        yield return new WaitForSeconds(lifetime);

        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
            if (activateTornado)
            {
                collision.gameObject.transform.position += (transform.position - collision.gameObject.transform.position) / 50;
            }
        }
    }


    IEnumerator ReactTrigger()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
