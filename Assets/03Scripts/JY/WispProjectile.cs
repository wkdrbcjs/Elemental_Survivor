using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispProjectile : Projectile
{   
    [SerializeField]
    private float Speed = 20.0f;


    private void Start()
    {
        ItemInfoInit(5);

        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        ItemSkill2(this.gameObject);
         target = transform.position;

        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void Update()
    {
        transform.Translate(projectileSpeed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyHit(collision))
        {
            penetration -= 1;
            if (penetration <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnBecameInvisible()//화면밖으로 나갈때
    {
        Destroy(this.gameObject);//총알 파괴
    }  
}
