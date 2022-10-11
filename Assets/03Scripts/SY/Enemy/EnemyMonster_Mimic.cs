using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster_Mimic : EnemyMonster
{

    //  �̹� ����
    IEnumerator MimicTrans()
    {
        anim.SetBool("Trans", true);
        yield return new WaitForSeconds(0.2f);
        transform.tag = "Enemy";
        anim.SetBool("Trans", false);

        yield return new WaitForSeconds(0.5f);
        //  ���� �����ϱ� ���� �ִ� �ڽ� �ݶ��̴� ���ֱ�
        BoxColliderOnOff(true);

        //  ���������ϱ� �̵��ϱ� ���� �̵��ӵ� ���� �ʱ�ȭ
        movementSpeed = baseMovementSpeed;
    }

    protected override void distanceCheck()
    {
        if (isTrans == false)
        {
            //  �̹��� �񷽰� �ٸ��� �÷��̾��� �ǽð� ��ġ�� �����ؼ� �����Ÿ� ���� ������ ������.
            toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y,
            0);

            if (Vector3.Distance(transform.position, TraceTarget.transform.position) <= attackRange)
            {
                StartCoroutine(MimicTrans());
                isTrans = true;
            }
        }
    }


    protected override void CheckDropRate()
    {
        Instantiate(MonsterManager.instance.ItemChestOBJ, transform.position, transform.rotation);
    }
}
