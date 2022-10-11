using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    public List<GameObject> FoundEnemys;
    public GameObject Enemy;  

    public List<GameObject> FoundObjects;

    public AnimationCurve curve;
  


    public float Range;

    public GameObject MeteorEndPos;
    public GameObject SmallMeteor;

    public AudioClip Snd_FireExplosion;


    Vector2 mouse;
    Vector3 endPos;
    Vector3[] m_points = new Vector3[4];

    private float m_timerMax = 0;
    private float m_timerCurrent = 0;
    private float m_speed;
    Vector3 temp;

    bool isImpact;
    private void Start()
    {
        //_skillManagement = GameObject.Find("SkillManagement").GetComponent<SkillManagement>();     

        target = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;

        ItemInfoInit(3);


        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        //System.Array.Resize(ref monster, penetration + 100);
        System.Array.Resize(ref monster, penetration + 3000);

        endPos = NearestGameObject();
        Init(transform, endPos, projectileSpeed, 50);
        GameObject endPosEffect = Instantiate(MeteorEndPos, endPos, Quaternion.identity);
        endPosEffect.transform.localScale = new Vector3(projectileScale, projectileScale, 1);
        _SM.SoundPlay(_SM.Fire_MeteorStart);
    }  

    private void FixedUpdate() 
    {
        if (m_timerCurrent > m_timerMax)
        {
            if (Enemy != null && !hit)
            {
                if (Enemy.GetComponent<EnemyCtrl>().isDead == false)
                {
                    //Destroy(endPos);
                    //Monster.GetComponent<MonsterAI>().Hit(this.gameObject, attackDamage, knockBack);
                    //hit = true;
                }

            }
            
            isImpact = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            GetComponent<Animator>().SetTrigger("Impact");
            
            return;
        }

        if (Enemy != null)
        {
            if (Enemy.GetComponent<EnemyCtrl>().isDead == false)
            {
                //m_points[2] = Monster.transform.position;
            }
            else
            {
                //GetComponent<Animator>().SetTrigger("Impact");
                //m_points[0] = transform.position;
                //m_timerCurrent = 0;
                //NearestGameObject();
            }
        }


        m_timerCurrent += Time.deltaTime * m_speed;

        float dy = transform.position.y - temp.y;
        float dx = transform.position.x - temp.x;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        if(!isImpact)
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree+90);

        temp = transform.position;

        transform.position = new Vector3(
            CubicBezierCurve(m_points[0].x, m_points[1].x, m_points[2].x),
            CubicBezierCurve(m_points[0].y, m_points[1].y, m_points[2].y),
            CubicBezierCurve(m_points[0].z, m_points[1].z, m_points[2].z)
        );
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*if (hit == false)
        {
            //hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                    int index = System.Array.IndexOf(monster, collision.gameObject);
                    if (index == -1)
                    {
                        monster[a] = collision.gameObject;
                        a += 1;
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                }
            }
            hit = false;
        }*/
        EnemyHit(collision);
    }

    Vector3 NearestGameObject()
    {
        Vector3 mPosition = Input.mousePosition;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        FoundEnemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        shortDis = Vector2.Distance(gameObject.transform.position, FoundEnemys[0].transform.position);
        Enemy = FoundEnemys[0];
        foreach (GameObject found in FoundEnemys)
        {
            float Distance = Vector3.Distance(new Vector3(target.x + Random.Range(-Range, Range), target.y + Random.Range(-Range, Range), 0), found.transform.position);

            if (Distance < shortDis)
            {
                shortDis = Distance;
                Enemy = found;
            }
        }
        target = new Vector3(target.x + Random.Range(-Range, Range), target.y + Random.Range(-Range, Range), 0);
        return target;
    }

    IEnumerator Destroythis()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
    IEnumerator SmallMeteorAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.05f);
            _SM.SoundPlay(_SM.Fire_MeteorStart);
            Instantiate(SmallMeteor, transform.position, Quaternion.identity);
        }
            
    }
    public void Init(Transform _startTr, Vector3 _endTr, float _speed, float _newPointDistanceFromStartTr)
    {
        m_speed = _speed;

        m_timerMax = Random.Range(2.5f, 3.5f);

        m_points[0] = _startTr.position;


            m_points[1] = _startTr.position +
            (_newPointDistanceFromStartTr * Random.Range(-0.3f, 0.3f) * _startTr.right) +
            (_newPointDistanceFromStartTr * Random.Range(1.0f, 2.0f) * _startTr.up) +
            (_newPointDistanceFromStartTr * Random.Range(0.0f, 0.0f) * _startTr.forward);

        //else
        //{
        //    m_points[1] = _startTr.position +
        //    (_newPointDistanceFromStartTr * Random.Range(0.0f, 0.0f) * _startTr.right) +
        //    (_newPointDistanceFromStartTr * Random.Range(1.0f, 2.0f) * _startTr.up) +
        //    (_newPointDistanceFromStartTr * Random.Range(0.0f, 0.0f) * _startTr.forward);
        //}

        m_points[2] = _endTr;

        transform.position = _startTr.position;
    }
    private float CubicBezierCurve(float a, float b, float c)
    {

        float t = m_timerCurrent / m_timerMax;

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);

        return Mathf.Lerp(ab, bc, t);
    }

    void CollisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        _SM.SoundPlay(_SM.Fire_MeteorEnd);
        StartCoroutine(SmallMeteorAttack());
    }
    void CollisionOff()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
