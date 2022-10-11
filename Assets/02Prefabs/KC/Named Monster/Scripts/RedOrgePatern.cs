using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrgePatern : MonoBehaviour
{
    public GameObject TraceTarget;

    public Animator anim;
    private SpriteRenderer SprRend;
    private Vector3 toPcVec;
    private bool On;


    [SerializeField]
    private GameObject attackProjectile;

    // Start is called before the first frame update
    void Start()
    {
        TraceTarget = PlayerCtrl.playerInstance;
        anim = GetComponent<Animator>();
        SprRend = GetComponent<SpriteRenderer>();

        if (TraceTarget.transform.position.x > transform.position.x)//ÁÂ¿ì¹ÝÀü
        {
            SprRend.flipX = true;
        }
        else
        {
            SprRend.flipX = false;
        }
        anim.SetBool("isDuringAnim", true);
        anim.SetTrigger("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        AttackMove();
    }
    private void AttackMove()
    {
        if (On)
        {
            transform.position += toPcVec.normalized * 900.0f * Time.deltaTime;
        }
    }
    void AttackOver()
    {
        //Destroy(this.gameObject);
    }
    IEnumerator RedOrge_NormalAttack_Ready()
    {
        anim.speed = 0;
        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y, 0);

        Quaternion angleAxis1 = Quaternion.Euler(0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) * Mathf.Rad2Deg);

        Instantiate(attackProjectile, transform.position, angleAxis1);
        yield return new WaitForSeconds(2f);
        On = true;
        anim.speed = 1;
        yield return new WaitForSeconds(0.13f);
        On = false;

        for (int i = 0; i < 50; i++)
        {
            SprRend.color -= new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
    }
}
