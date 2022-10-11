using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamedMonster_Boss : NamedMonster
{
    public Image BossHpBar;

    [Header("Teleport")]
    [SerializeField]
    private GameObject TelePortEffect;  //  텔레포트 이펙트
    private Vector3 TelePortPoint;      //  텔레포트를 위해 기준점을 잡을 필요가 있음
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
    //  보스 행동 시작 
    //  GM에서 타이머가 0되면 호출할 것
    public void BossPattern(Vector3 BossPos)
    {
        // 보스의 현재 위치를 받아옴.
        // 보스 생성이후 플레이어 방향으로 4초정도 걸어옴.
        // 그 시점 이후의 보스 위치를 말하는 것.
        // 이유 : 컷신 이후 보스 위치 기준으로 벽이 생성되는데 벽 내부에서만 
        // 텔레포트한 후에 공격하기 위해
        TelePortPoint = BossPos;

        //  보스 공격 코루틴 시작
        StartCoroutine(BaseAttack_Boss());          // 보스 기본 공격 - 플레이어 위치에 장판 공격 한개 생성
        StartCoroutine(SpecialAttack_Boss());       // 보스 특수 공격 - 텔레포트 이후 공격 선택
    }

    IEnumerator SpecialAttack_Boss()
    {
        //  공격 절차
        //  텔레포트 수행
        StartCoroutine(TelePort());
        yield return new WaitForSeconds(1.5f);

        //  공격 패턴 결정
        SelectAttack();
        yield return new WaitForSeconds(1.5f);
    }

    // 텔레포트
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

        //  텔레포트 위치 구하기
        Vector3 teleportpos = TelePortPoint + new Vector3(posx, posy, 0);
        Vector3 isnewpos = teleportpos;

        //  만약 현재 있는 위치랑 새로운 텔레포트 위치가 같으면 
        if (transform.position == isnewpos)
        { StartCoroutine(TelePort()); } // 새로운 위치 구하기
        // 같지 않으면 계산한 위치로 이동
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
        // 플레이어 현재 위치에 장판 공격 한개 생성하는 보스 기본 공격
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
            // 투사체를 전방향으로 발사하되 약간 듬성듬성하게
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

            //  상하좌우 4방향으로 3갈래로 나가는 투사체를 각도를 더해주면서 발사
            case 1:
                for (int k = 0; k < 20; k++) //총 20줄을 발사할건데
                {
                    float plusAngle2 = k * 3; // 1줄 쏘면 다음 줄은 3도 만큼 더해진 방향으로 쏠거다.

                    for (int j = 0; j < 4; j++) // 그 20번을 4번(4방향으로) 쏠것이다.
                    {
                        float other = j * 90f; // j에 90도를 곱해주면 상하좌우 방향이 나오게 됨.
                        for (int i = -1; i < 2; i++) // i가 -1,0,1에 대하여
                        {
                            float plusAngle = i * 15f + other; //15도씩 곱해줌으로서 -15도, 0도, +15도 -> 3갈래로 나가게 할것이다.

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

            // 상하좌우 4방향으로 한줄기로 나가는 투사체를 각도를 더해주면서 발사
            case 2:
                for (int i = 0; i < 60; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        float plusAngle = i * 10f + (j * 90); // i*10 ->다음 줄이 나갈 방향의 추가 각도, j*90 -> 방향

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

            // 대규모 장판 공격
            case 3:
                for (int j = 0; j < 5; j++) // 5번
                {
                    for (int i = 0; i < 20; i++) //25번
                    {
                        // 플레이어 현재 위치 기준
                        // x 범위 얼마, y범위 얼마의 값을 랜덤으로 구해서
                        // 해당 위치에 뿌림
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

            //  패턴 1 + 패턴 2
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
                            float plusAngle = i * 10f + (j * 90); // i*10 ->다음 줄이 나갈 방향의 추가 각도, j*90 -> 방향

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

            //  발사된 투사체 몇초 후 플레이어 향해 날아감.
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
