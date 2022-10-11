using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamedMonster_Boss : NamedMonster
{
    public Image BossHpBar;

    [Header("Teleport")]
    [SerializeField]
    private GameObject TelePortEffect;  //  �ڷ���Ʈ ����Ʈ
    private Vector3 TelePortPoint;      //  �ڷ���Ʈ�� ���� �������� ���� �ʿ䰡 ����
    public int SelectAttackNum;

    void Start()
    {
        base.Start();
        BossHpBar = GameManager.instance.Boss_HpBar_UISEt.GetComponent<Image>();
    }

    /*void Update()
    {}*/

    new protected void afterTakeDamage()
    {
        BossHpBar.fillAmount = (float) HP / (float) maxHP;
    }

    #region Boss
    //  ���� �ൿ ���� 
    //  GM���� Ÿ�̸Ӱ� 0�Ǹ� ȣ���� ��
    public void BossPattern(Vector3 BossPos)
    {
        // ������ ���� ��ġ�� �޾ƿ�.
        // ���� �������� �÷��̾� �������� 4������ �ɾ��.
        // �� ���� ������ ���� ��ġ�� ���ϴ� ��.
        // ���� : �ƽ� ���� ���� ��ġ �������� ���� �����Ǵµ� �� ���ο����� 
        // �ڷ���Ʈ�� �Ŀ� �����ϱ� ����
        TelePortPoint = BossPos;

        //  ���� ���� �ڷ�ƾ ����
        StartCoroutine(BaseAttack_Boss());          // ���� �⺻ ���� - �÷��̾� ��ġ�� ���� ���� �Ѱ� ����
        StartCoroutine(SpecialAttack_Boss());       // ���� Ư�� ���� - �ڷ���Ʈ ���� ���� ����
    }

    IEnumerator SpecialAttack_Boss()
    {
        //  ���� ����
        //  �ڷ���Ʈ ����
        StartCoroutine(TelePort());
        yield return new WaitForSeconds(1.5f);

        //  ���� ���� ����
        SelectAttack();
        yield return new WaitForSeconds(1.5f);
    }

    // �ڷ���Ʈ
    #region teleport
    IEnumerator TelePort()
    {
        TelePortEffect.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        TelePortEffect.SetActive(false);

        float randx = Random.Range(-1, 2);  //  -1  or  0 or 1
        float randy = Random.Range(-1, 2);
        float posx = randx * 100;           //  -100 or 0 or 100
        float posy = randy * 60;            //  -60  or 0 or 60

        //  �ڷ���Ʈ ��ġ ���ϱ�
        Vector3 teleportpos = TelePortPoint + new Vector3(posx, posy, 0);
        Vector3 isnewpos = teleportpos;

        //  ���� ���� �ִ� ��ġ�� ���ο� �ڷ���Ʈ ��ġ�� ������ 
        if (transform.position == isnewpos)
        { StartCoroutine(TelePort()); } // ���ο� ��ġ ���ϱ�
        // ���� ������ ����� ��ġ�� �̵�
        else
        { transform.position = teleportpos; }
        yield return new WaitForSeconds(1.0f);
    }
    #endregion

    void SelectAttack()
    {
        SelectAttackNum = Random.Range(0, 6);
        StartCoroutine(Boss_AttackKinds(SelectAttackNum));
    }

    IEnumerator BaseAttack_Boss()
    {
        // �÷��̾� ���� ��ġ�� ���� ���� �Ѱ� �����ϴ� ���� �⺻ ����
        Instantiate(attackProjectile_2, TraceTarget.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(2.5f, 3.5f));
        StartCoroutine(BaseAttack_Boss());
    }

    IEnumerator Boss_AttackKinds(int attacknum)
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);

        toPcVec = new Vector3
                     (TraceTarget.transform.position.x - transform.position.x,
                      TraceTarget.transform.position.y - transform.position.y,
                      0);

        switch (attacknum)
        {
            // ����ü�� ���������� �߻��ϵ� �ణ �뼺�뼺�ϰ�
            case 0:
                for (int j = 0; j < 15; j++)
                {
                    float plusRotate = j * 3f;

                    for (int i = 0; i < 20; i++)
                    {
                        float plusAngle = i * 18f;
                        float sum = plusAngle + plusRotate + Random.Range(-15, 16);

                        Quaternion angleAxis = Quaternion.Euler(
                            0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                            * Mathf.Rad2Deg + sum);

                        if (!isDead)
                        {
                            Instantiate(attackProjectile, transform.position, angleAxis);
                        }
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                break;

            //  �����¿� 4�������� 3������ ������ ����ü�� ������ �����ָ鼭 �߻�
            case 1:
                for (int k = 0; k < 20; k++) //�� 20���� �߻��Ұǵ�
                {
                    float plusAngle2 = k * 3; // 1�� ��� ���� ���� 3�� ��ŭ ������ �������� ��Ŵ�.

                    for (int j = 0; j < 4; j++) // �� 20���� 4��(4��������) ����̴�.
                    {
                        float other = j * 90f; // j�� 90���� �����ָ� �����¿� ������ ������ ��.
                        for (int i = -1; i < 2; i++) // i�� -1,0,1�� ���Ͽ�
                        {
                            float plusAngle = i * 15f + other; //15���� ���������μ� -15��, 0��, +15�� -> 3������ ������ �Ұ��̴�.

                            Quaternion angleAxis = Quaternion.Euler(
                                0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                                * Mathf.Rad2Deg + plusAngle + plusAngle2);

                            if (!isDead)
                            {
                                Instantiate(attackProjectile, transform.position, angleAxis);
                            }
                        }
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.2f);
                }
                break;

            // �����¿� 4�������� ���ٱ�� ������ ����ü�� ������ �����ָ鼭 �߻�
            case 2:
                for (int i = 0; i < 60; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        float plusAngle = i * 10f + (j * 90); // i*10 ->���� ���� ���� ������ �߰� ����, j*90 -> ����

                        Quaternion angleAxis = Quaternion.Euler(
                        0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                        * Mathf.Rad2Deg + plusAngle);

                        if (!isDead)
                        {
                            Instantiate(attackProjectile, transform.position, angleAxis);
                        }
                    }
                    yield return new WaitForSeconds(0.15f);
                }
                break;

            // ��Ը� ���� ����
            case 3:
                for (int j = 0; j < 5; j++) // 5��
                {
                    for (int i = 0; i < 20; i++) //25��
                    {
                        // �÷��̾� ���� ��ġ ����
                        // x ���� ��, y���� ���� ���� �������� ���ؼ�
                        // �ش� ��ġ�� �Ѹ�
                        Vector3 Pos = TraceTarget.transform.position + new Vector3(Random.Range(-60, 60), Random.Range(-60, 60), 0);

                        if (!isDead)
                        {
                            Instantiate(attackProjectile_2, Pos, Quaternion.identity);
                        }

                        yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                break;

            //  ���� 1 + ���� 2
            case 4:
                for (int i = 1; i < 60; i++)
                {
                    if (i % 15 == 0)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            float plusRotate = j * 5f;
                            for (int k = 0; k < 36; k++)
                            {
                                float plusAngle = k * 10f;
                                float sum = plusAngle + plusRotate;

                                Quaternion angleAxis = Quaternion.Euler(
                                    0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                                    * Mathf.Rad2Deg + sum);

                                if (!isDead)
                                {
                                    Instantiate(attackProjectile, transform.position, angleAxis);
                                }
                            }
                            yield return new WaitForSeconds(0.15f);
                        }
                        yield return new WaitForSeconds(0.15f);
                    }

                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            float plusAngle = i * 10f + (j * 90); // i*10 ->���� ���� ���� ������ �߰� ����, j*90 -> ����

                            Quaternion angleAxis = Quaternion.Euler(
                            0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                            * Mathf.Rad2Deg + plusAngle);

                            if (!isDead)
                            {
                                Instantiate(attackProjectile, transform.position, angleAxis);
                            }
                        }
                        yield return new WaitForSeconds(0.15f);
                    }
                }
                break;

            //  �߻�� ����ü ���� �� �÷��̾� ���� ���ư�.
            case 5:
                for (int i = 0; i < 5; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        float plusAngle = j * 20f;

                        Quaternion angleAxis = Quaternion.Euler(
                        0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                        * Mathf.Rad2Deg + plusAngle);

                        if (!isDead)
                        {
                            Instantiate(attackProjectile_1, transform.position, angleAxis);
                        }
                    }
                    yield return new WaitForSeconds(0.4f);
                }
                yield return new WaitForSeconds(1.0f);

                break;

            default:
                break;
        }
        anim.SetTrigger("Idle");
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpecialAttack_Boss());
    }
    #endregion

}
