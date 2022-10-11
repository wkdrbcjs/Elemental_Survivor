using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster_Minotaur : NamedMonster
{
    void Start()
    {
        base.Start();
    }

    // �̳�Ÿ��ν� �Ϲ� ����
    protected override IEnumerator NamedMonster_NormalAttack()
    {
        movementSpeed = 0f;
        yield return new WaitForSeconds(0.1f);
        attackCoolTime = CoolTime;
        anim.SetBool("Move", false);
        anim.SetBool("isDuringAnim", true);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.7f);
        
        if (!isDead)
        {
            for (int i = 0; i < 3; i++)
            {
                toPcVec = new Vector3
                    (TraceTarget.transform.position.x - transform.position.x,
                    TraceTarget.transform.position.y - transform.position.y, 0);

                Quaternion angleAxis1 = Quaternion.Euler(0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) * Mathf.Rad2Deg);
                //  TileSpawnPos�� �̳�Ÿ�츣�� ��ü �ڽ����� �ִ� ����ü�� ������ġ ��ü��.
                Instantiate(attackProjectile, TileSpawnPos.transform.position, angleAxis1);
                yield return new WaitForSeconds(0.2f);
            }
        }

        yield return new WaitForSeconds(0f);
        if (isMoveAble) //  �÷��̾� �߰� ������ ����
        {
            anim.SetBool("Move", true);
        }
        else  
        {}
    }

    // �̳�Ÿ��ν� Ư�� ����
    protected override IEnumerator NamedMonster_SpecialPattern()
    {
        if (SelectingObj == null)
        {
            SelectingObj = Instantiate(attackProjectile_1, TraceTarget.transform.position, Quaternion.identity);
        }
        //  ��ġ �Ű��ְ� Ȱ��ȭ ���ָ� 
        //  �� ��ü ������ ���� �ð��� ��Ȱ��ȭ �� ��
        if (!isDead)
        {
            SelectingObj.transform.position = TraceTarget.transform.position;
            SelectingObj.SetActive(true);
        }
        yield return new WaitForSeconds(0f);
    }

}
