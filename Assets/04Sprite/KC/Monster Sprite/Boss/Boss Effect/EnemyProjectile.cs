using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject TraceTarget;
    private Rigidbody Rigid;

    private int angleNum;

    public int damge = 10;
    public int Type;
    public float Speed;

    private Vector3 toPcVec;
    private bool isTrace;
    public bool On;

    Vector3 force;
    Vector3 TargetPosition;
    Vector3 ThisPosition;

    Vector3 DF;
    void Start()
    {
        TraceTarget = PlayerCtrl.playerInstance;
        Rigid = GetComponent<Rigidbody>();
        force = new Vector3(0, 0, 0);
        isTrace = false;
        
        DF = (TraceTarget.transform.position - transform.position).normalized * 500f;

        if (Type != 4)  //  type = 4 미노타우르스 회전 투사체 - Minotaur_AttackTile 프리팹 -> BoxCollider2D 자체를 안가지고 있음 -> 빨간줄 나옴
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            ThisPosition = transform.position;
            TargetPosition = TraceTarget.transform.position;

            //  어느 정도의 힘을 줄 것인지
            //force = (TargetPosition - ThisPosition).normalized * 0.01f;
        }
        if (Type == 6)
        {
            StartCoroutine(FadeIn());
            DF = (transform.parent.transform.position - transform.position).normalized * Speed;
        }

        if (Type == 2)  //  보스 특수 공격 투사체
        {
            StartCoroutine(StopAndReturnTile());
        }
        if (Type == 7)
        {
            transform.Rotate(0, 0, -50, Space.Self);
            StartCoroutine(AngleCtrl());
        }

        StartCoroutine(Destroythis());
    }

    void FixedUpdate()
    {
        TileMove();
    }

    void TileMove()
    {
        if(Type == 0|| Type == 1|| Type == 2)
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        if (Type == 6)
        {
            if (On)
                transform.Translate(Vector2.right * Speed * Time.deltaTime);
            return;
        }

        if (Type == 5)
        {
            if (!isTrace)
            {
                transform.Translate(Vector2.right * Speed * Time.deltaTime);
                StartCoroutine(delay(0.3f));
            }
            else
            {
                transform.position -= toPcVec.normalized * Speed * Time.deltaTime;
            }
        }

        //  미노타우로스 투사체
        if (Type == 4)
        {
            // z축 회전
            transform.Rotate(new Vector3(0, 0, 200f * Time.deltaTime));

            //  조종행동 Seek 위해서 
            //  자신 위치와 쫒을 타겟 위치 갱신
            ThisPosition = transform.position;
            TargetPosition = TraceTarget.transform.position;

            //  어느 정도의 힘을 줄 것인지
            //force = (TargetPosition - ThisPosition).normalized * 0.1f;

            // Seek 쓰기 싫으면 아래 /**/ 열어주고 Rigid.AddForce(Seek(TargetPosition)) 부분 주석처리하면 됨.
            toPcVec = new Vector3
                   (transform.position.x - TraceTarget.transform.position.x,
                     transform.position.y - TraceTarget.transform.position.y,
                    0);
            transform.position -= toPcVec.normalized * Speed * Time.deltaTime;

            //Rigid.AddForce(Seek(TargetPosition));

        }

        //  셰이드 공격 투사체
        if(Type == 7)
        {
            Wander();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!other.gameObject.GetComponent<PlayerCtrl>().isRoll)
            {
                if (!(Type == 50))
                {
                    other.gameObject.GetComponent<PlayerCtrl>().EnemyCollised(damge);
                }
                if (Type == 0 || Type == 7)
                {
                    Destroy(this.gameObject);
                }
                else if (Type ==4 || Type ==5 || Type == 6)
                { }
                else
                {
                    Speed = 0f;
                    GetComponent<Animator>().SetTrigger("End");
                }
            }
        }
        else if (other.tag == "PlayerAttack")
        {
            //Destroy(this.gameObject);
        }
    }
    IEnumerator FadeIn()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 50; i++)
        {
            spr.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator Destroythis()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }

    IEnumerator StopAndReturnTile()
    {
        yield return new WaitForSeconds(Random.Range(1.5f,2.0f));
        float temp = Speed * 2f;
        Speed = 0;

        yield return new WaitForSeconds(0.5f);

        toPcVec = new Vector3
                   (transform.position.x - TraceTarget.transform.position.x,
                     transform.position.y - TraceTarget.transform.position.y,
                    0);

        yield return new WaitForSeconds(0.1f);
        Speed = temp;

        //  Tile 방향 전환
        float angle = Mathf.Atan2(TraceTarget.transform.position.y - transform.position.y, TraceTarget.transform.position.x- transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);

        //  isTrace가 true로 되는 순간 
        //  update에 의해 플레이어 향해 날아감.
        isTrace = true;
    }

    Vector3 Seek(Vector3 TargetPos)
    {
        Vector3 DF = (TargetPos - ThisPosition).normalized * Speed;
        Vector3 SF = DF - Rigid.velocity;
        return SF;

        //DF = ((DF.normalized + (TargetPosition - ThisPosition).normalized * 0.03f) * 20f);
        //Vector3 SF = DF + ((TargetPosition - ThisPosition).normalized*1f); 
        //return DF;
    }

    IEnumerator delay(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Speed = 0;
    }

    void over()
    {
        Destroy(this.gameObject);
    }
    
    void Wander()
    {
        if(angleNum == 1)
        {
            transform.Rotate(0, 0, 2, Space.Self);
        }
        else if (angleNum == 0)
        {
            transform.Rotate(0, 0, -2, Space.Self);
        }

        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }
  
    IEnumerator AngleCtrl()
    {
        angleNum = 1;
        yield return new WaitForSeconds(1f);
        angleNum = 0;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AngleCtrl());
    }
}
