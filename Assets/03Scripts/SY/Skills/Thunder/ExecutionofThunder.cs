using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionofThunder : Projectile
{
    private PlayerCtrl playerObject;




    public float duration;
    public float cooltime;





    public bool activateParalyzation=false;
    //public GameObject enemyParalyzer;

    public GameObject traceTarget;

    public bool activateMassiveEnergy = false;

    bool moveOn;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = PlayerStatus.instance.Player;

        //값 세팅//
        //baseAttackDamage += playerObject.Skill_ABaseDamage;
        defaultInit();


        duration = baseDuration;
        cooltime = baseCooltime;
        // 적용 //
        if(activateMassiveEnergy)
        {
            projectileScale *= 2;
        }
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);


        System.Array.Resize(ref monster, 1000);

        if(traceTarget!=null)
        {
            //transform.parent = traceTarget.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveOn)
        {
            transform.Translate(Vector2.right * 200f* Time.deltaTime);
        }
        else
        {
            transform.position = playerObject.gameObject.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EnemyHit(collision))
        {
            if (activateParalyzation)
            {
                //var instance = Instantiate(enemyParalyzer, collision.transform);
                if (collision.GetComponent<EnemyCtrl>().EnemyID == 50)
                {
                    //Destroy(instance);
                }
            }
        }
      
    }


    public IEnumerator ColisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        moveOn = true;
        traceTarget = null;
        yield return new WaitForSeconds(0.0f);
        if (Random.Range(0, 1000) < (300 + 10 /* *PC.Luck)*/))
        {
            //StartCoroutine(_skillManagement.ThunderClassAttack());
        }
        //GetComponent<BoxCollider2D>().enabled = false;
    }
    void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}
