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

        if (Type != 4)  //  type = 4 �̳�Ÿ�츣�� ȸ�� ����ü - Minotaur_AttackTile ������ -> BoxCollider2D ��ü�� �Ȱ����� ���� -> ������ ����
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            ThisPosition = transform.position;
            TargetPosition = TraceTarget.transform.position;

            //  ��� ������ ���� �� ������
            //force = (TargetPosition - ThisPosition).normalized * 0.01f;
        }
        if (Type == 6)
        {
            StartCoroutine(FadeIn());
            DF = (transform.parent.transform.position - transform.position).normalized * Speed;
        }

        if (Type == 2)  //  ���� Ư�� ���� ����ü
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

        //  �̳�Ÿ��ν� ����ü
        if (Type == 4)
        {
            // z�� ȸ��
            transform.Rotate(new Vector3(0, 0, 200f * Time.deltaTime));

            //  �����ൿ Seek ���ؼ� 
            //  �ڽ� ��ġ�� �i�� Ÿ�� ��ġ ����
            ThisPosition = transform.position;
            TargetPosition = TraceTarget.transform.position;

            //  ��� ������ ���� �� ������
            //force = (TargetPosition - ThisPosition).normalized * 0.1f;

            // Seek ���� ������ �Ʒ� /**/ �����ְ� Rigid.AddForce(Seek(TargetPosition)) �κ� �ּ�ó���ϸ� ��.
            toPcVec = new Vector3
                   (transform.position.x - TraceTarget.transform.position.x,
                     transform.position.y - TraceTarget.transform.position.y,
                    0);
            transform.position -= toPcVec.normalized * Speed * Time.deltaTime;

            //Rigid.AddForce(Seek(TargetPosition));

        }

        //  ���̵� ���� ����ü
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

        //  Tile ���� ��ȯ
        float angle = Mathf.Atan2(TraceTarget.transform.position.y - transform.position.y, TraceTarget.transform.position.x- transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);

        //  isTrace�� true�� �Ǵ� ���� 
        //  update�� ���� �÷��̾� ���� ���ư�.
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
