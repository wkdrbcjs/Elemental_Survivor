using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster_PhantomKnight : NamedMonster
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        PhantomKnight_SpecialPartern();// ���� ����Ʈ�� �������� ����� ������ ��� �ߵ�
    }

    //  ���ҳ���Ʈ �Ϲ� ����
    protected override IEnumerator NamedMonster_NormalAttack()
    {
        movementSpeed = 0f;
        yield return new WaitForSeconds(0.0f);
        //attackCoolTime = 12;
        anim.SetTrigger("Attack");
        anim.SetBool("isDuringAnim", true);
        //yield return new WaitForSeconds(0.5f);
    }
    IEnumerator PhantomKnight_NormalAttack_Ready()
    {
        anim.speed = 0;
        GameObject proj = Instantiate(attackProjectile, TraceTarget.transform.position, Quaternion.identity);
        proj.transform.SetParent(transform, true);
        Rigid.isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        anim.speed = 1;
    }

    // ���ҳ���Ʈ ��ü ����
    void PhantomKnight_SpecialPartern()
    {
        //  ���� ���� ����� �ѱ�
        TraceTarget.GetComponent<PlayerCtrl>().isManaReduceOn = true;
        TraceTarget.GetComponent<PlayerCtrl>().GetDebuff(1);
    }

}
