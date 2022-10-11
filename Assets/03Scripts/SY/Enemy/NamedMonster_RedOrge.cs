using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster_RedOrge : NamedMonster
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    //  레드오우거 일반 공격
    protected override IEnumerator NamedMonster_NormalAttack()
    {
        paternCount -= 1;
        movementSpeed = 0f;
        yield return new WaitForSeconds(0.0f);
        //attackCoolTime = 12;
        anim.SetBool("isDuringAnim", true);
        anim.SetTrigger("Attack");
    }
    IEnumerator RedOrge_NormalAttack_Ready()
    {
        anim.speed = 0;
        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y, 0);

        Quaternion angleAxis1 = Quaternion.Euler(0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) * Mathf.Rad2Deg);

        Instantiate(attackProjectile, transform.position, angleAxis1);
        Rigid.isKinematic = true;
        yield return new WaitForSeconds(0.5f);
        anim.speed = 1;
        isAttack = true;
        yield return new WaitForSeconds(0.1f);
        Rigid.isKinematic = false;
        isAttack = false;
        if (paternCount > 0)
        {
            StartCoroutine(NamedMonster_NormalAttack());
            yield break;
        }
        paternCount = basePaternCount;
    }

    //  레드오우거 전체 공격
    protected override IEnumerator NamedMonster_SpecialPattern()
    {
        if (!isDead)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(attackProjectile_1, GetRandomPosition(65f), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            }
        }
    }
}
