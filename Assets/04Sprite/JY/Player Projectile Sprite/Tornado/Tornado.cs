using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Projectile
{
    //���� Ÿ�� ����//

    //public float duration;
    //public float cooltime;

    Vector3[] m_points = new Vector3[4];

    private float m_timerMax = 0;
    private float m_timerCurrent = 0;
    private float m_speed;

    public GameObject goal;
    //Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        ItemInfoInit(19);
        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        //System.Array.Resize(ref monster, 1000);
        System.Array.Resize(ref monster, penetration + 3000);

        goal.transform.position = GetRandomPosition();

        Init(transform, goal.transform, projectileSpeed, 100, 100);
        if(_SM.PlayingWind_TornadoSoundNum <= 1)
        {
            StartCoroutine(_SM.TornadoSoundNum());
            _SM.SoundPlay(_SM.Wind_Tornado);
        }
        
        StartCoroutine(DestroyObject()); 
        if(_skillManagement.ElementClass == ItemInfoSet.instance.Items[19].element.ToString())
        {
            StartCoroutine(ScaleGrowth());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timerCurrent > m_timerMax)
        {
            GetComponent<Animator>().SetTrigger("Off");
            return;
        }

        m_timerCurrent += Time.deltaTime * m_speed;

        transform.position = new Vector3(
            CubicBezierCurve(m_points[0].x, m_points[1].x, m_points[2].x, m_points[3].x),
            CubicBezierCurve(m_points[0].y, m_points[1].y, m_points[2].y, m_points[3].y),
            CubicBezierCurve(m_points[0].z, m_points[1].z, m_points[2].z, m_points[3].z)
        );
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (penetration > 0)
                {
                    int index = System.Array.IndexOf(monster, collision.gameObject);//���� �迭�� �ǰݵ� ���Ͱ� ����ִ� �� �˻�
                    if (index == -1)//�迭�� �ش� ���Ͱ� ���� ��
                    {
                        monster[a] = collision.gameObject; //�迭�� �ش� ���͸� ���� ����
                        a += 1;

                        StartCoroutine(TargetActive(a));
                        //Vector2 knockBackVec = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);

                        
                        //���� ��ũ��Ʈ�� �������� �˹�Ÿ��� �����ϸ� ���� �ǰ�ó�� ����//
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                        //DestroyEvent();
                    }
                }
            }
            hit = false;
        }*/
        EnemyHit(collision);
        StartCoroutine(TargetActive(monsterSize));
    }  


    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(baseDuration - 0.5f);
        GetComponent<Animator>().SetTrigger("Off");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    IEnumerator TargetActive(int a) // �ǰ� ������ �����ϴ� �Լ�
    {
        yield return new WaitForSeconds(baseCooltime);
        monster[a] = null; //���� �迭���� �ش� ���͸� ������ �ٽ� �ǰݵ� �� �ְ� ����
        
        //monster[a] = null; //���� �迭���� �ش� ���͸� ������ �ٽ� �ǰݵ� �� �ְ� ����
    }
    IEnumerator ScaleGrowth() // �ǰ� ������ �����ϴ� �Լ�
    {
        yield return new WaitForSeconds(0.1f);
        transform.localScale *= 1.03f;
        StartCoroutine(ScaleGrowth());
    }

    public void Init(Transform _startTr, Transform _endTr, float _speed, float _newPointDistanceFromStartTr, float _newPointDistanceFromEndTr)
    {
        m_speed = _speed;

        m_timerMax = baseDuration;

        m_points[0] = _startTr.position;

        m_points[1] = _startTr.position +
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * _startTr.right) +
            (_newPointDistanceFromStartTr * Random.Range(-0.15f, 1.0f) * _startTr.up) +
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, -0.8f) * _startTr.forward);

        m_points[2] = _endTr.position +
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.right) +
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.up) +
            (_newPointDistanceFromEndTr * Random.Range(0.8f, 1.0f) * _endTr.forward);

        m_points[3] = _endTr.position;

        transform.position = _startTr.position;
    }
    private float CubicBezierCurve(float a, float b, float c, float d)
    {

        float t = m_timerCurrent / m_timerMax;

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
    public Vector3 GetRandomPosition()
    {
        float radius = 100f;

        Vector3 SpawnerPosition = transform.position;

        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);


        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - anchorPosX, 2));

        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        float y = y_b + anchorPosY;

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }
   
}
