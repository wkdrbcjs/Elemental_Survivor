using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject RangeBack;
    [SerializeField]
    GameObject RangeIn;
    public GameObject Attack;
    Vector2 scalevec;
    Vector2 scalevec2;
    float scalevecf;
    public int damage;
    public int type;
    public float time;

    /// <summary>
    /// Type == 1 : ���� ���� ����
    /// Type == 2 : �̳�Ÿ�츣�� �� ����
    /// Type == 3 : ����� ���� ����
    /// Type == 4 : ���� ����� ���� (��ü ����)
    /// Type == 5 : ���� ����� ���� (�Ϲ� ����)
    /// </summary>
    /// <returns></returns>

    void Start()
    {
        //RangeBack = GetComponentsInChildren<GameObject>()[0];
        //RangeIn = GetComponentsInChildren<GameObject>()[1];
        scalevec = new Vector3(RangeBack.transform.localScale.x/ (time), RangeBack.transform.localScale.y/ (time), 0) * Time.deltaTime;

        //  ���� ���� ����
        if (type == 4 || type == 5)
        {
            scalevec = RangeBack.transform.TransformDirection(Vector2.right) * RangeBack.transform.localScale.x / (time) * Time.deltaTime;
            scalevecf = RangeBack.transform.localScale.x / (time) * Time.deltaTime;
        }
        StartCoroutine(RangeOn());
    }

    void Update()
    {}

    IEnumerator RangeOn()
    {
        if (RangeBack.transform.localScale.x > RangeIn.transform.localScale.x)
        {
            if (type == 4 || type == 5)
            {
                RangeIn.transform.position += (Vector3)scalevec/2;
                RangeIn.transform.localScale += new Vector3(scalevecf, 0,0);
                yield return new WaitForSeconds(0f);
                StartCoroutine(RangeOn());
            }

            if (type <= 3)  //  ���� ���� ����, �̳�Ÿ�츣�� �� ����, ����� ���� ����
            {
                RangeIn.transform.localScale += (Vector3)scalevec;
                yield return new WaitForSeconds(0f);
                StartCoroutine(RangeOn());
            }
        }
        else
        {
            if (type == 1)  // ���� ���� ����
            {
                yield return new WaitForSeconds(0.1f);
                float effectScale = RangeBack.transform.localScale.x;
                GameObject go = Instantiate(Attack, transform);
                go.transform.localScale = new Vector3(effectScale, effectScale, effectScale);
                GetComponent<CapsuleCollider2D>().enabled = true;
                //Destroy(RangeIn);
                //Destroy(RangeBack);
                RangeIn.SetActive(false);
                RangeBack.SetActive(false);
                yield return new WaitForSeconds(0.3f);

                GetComponent<CapsuleCollider2D>().enabled = false;

                yield return new WaitForSeconds(1f);
                Destroy(this.gameObject);
            }

            if (type == 2)  //  �̳�Ÿ�츣�� �� ����
            {
                GameObject go = Instantiate(Attack, transform.position,Quaternion.identity);
                go.transform.SetParent(transform, true);
                //Destroy(RangeIn);
                //Destroy(RangeBack);
                RangeIn.SetActive(false);
                RangeBack.SetActive(false);
                yield return new WaitForSeconds(6f);
                Destroy(go);
                Destroy(this.gameObject);
            }

            if (type == 3)
            {
                GetComponent<CapsuleCollider2D>().enabled = true;
                RangeIn.SetActive(false);
                RangeBack.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                Destroy(this.gameObject);

            }

            if (type == 4)  //  ���� ����� ����(��ü ����)
            {
                yield return new WaitForSeconds(0.2f);
                GetComponent<BoxCollider2D>().enabled = true;
                yield return new WaitForSeconds(0.1f);
                Destroy(this.gameObject);
            }
            if (type == 5)  //  ���� ����� ����(�Ϲ� ����)
            {
                yield return new WaitForSeconds(0.15f);
                GetComponent<BoxCollider2D>().enabled = true;
                yield return new WaitForSeconds(0.1f);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerCtrl>().EnemyCollised(damage);
            if (type == 3)
            {
                other.GetComponent<PlayerCtrl>().PlayerOperation();
            }

            if(GetComponent<CapsuleCollider2D>() != null)
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
            if (GetComponent<BoxCollider2D>() != null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
