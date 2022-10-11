using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster_Orge : NamedMonster
{
    void Start()
    {
        base.Start();
    }

    // ����� Ư�� ���� - ��� ���� ������
    protected override IEnumerator NamedMonster_SpecialPattern()
    {
        if (SelectingObj == null)
        {
            SelectingObj = Instantiate(attackProjectile_1, TraceTarget.transform.position, Quaternion.identity);
        }
        //  ��� ������ ��ü�� ��ġ ���� �� ����� Ȱ��ȭ
        if (!isDead)
        {
            SelectingObj.transform.position = GetRandomPosition(150f);
            yield return new WaitForSeconds(0.1f);
            SelectingObj.GetComponent<TileSpwner>().GoblinActiveTrue(true);

            //  ��� Ȱ��ȭ �Ǿ� �ִ� �ð� 7��
            yield return new WaitForSeconds(7f);
            SelectingObj.GetComponent<TileSpwner>().GoblinActiveTrue(false);

            for (int i = 0; i < SelectingObj.GetComponent<TileSpwner>().TileCount; i++)
            {
                //  ��� ��ü���� ��Ȱ��ȭ �����̱� ������ GetComponentsInChildren�� ������ ������ �� ����.
                //  �� ������ ()���� bool ���ڰ��� false�� �⺻������ �����Ǿ� �ֱ� ����.
                //  �׷��� true�� ���ڰ����� �ָ� ��Ȱ��ȭ�� �ڽİ�ü�� ������ ������ �� ����.
                SelectingObj.GetComponentsInChildren<EnemyMonster_Goblin>(true)[i].GoblinNonActive();
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    // ����� �⺻ ���� - ���� ����
    protected override IEnumerator NamedMonster_NormalAttack()
    {
        if (!isDead)
        {
            Instantiate(attackProjectile, TraceTarget.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
    }

}
