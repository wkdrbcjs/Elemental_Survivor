using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFontManage : MonoBehaviour
{
    public Text text;
    [SerializeField]
    private string type;
    private Color alpha;

    private float upSpeed;
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        
        transform.localScale = transform.localScale / transform.parent.gameObject.transform.localScale.x;

        if (type == "Enemy")
        {
            text.text = transform.parent.GetComponent<EnemyCtrl>().attackDamageForText.ToString();
            transform.Translate(Random.Range(-2, 2), Random.Range(-1, 1), 0);
            StartCoroutine(DamageFontAnimP());
        }
        if (type == "Player")
        {
            transform.Translate(Random.Range(-2, 2), Random.Range(-1, 1), 0);
            //text.text = transform.parent.GetComponent<PlayerCtrl>().attakcDamageForText;
            StartCoroutine(DamageFontAnimP());
        }
        if (type == "Level")
        {
            //text.text = transform.parent.GetComponent<PlayerCtrl>().attakcDamageForText;
            StartCoroutine(DamageFontAnimM());
        }
        if (type == "Buff")
        {
            //text.text = transform.parent.GetComponent<PlayerCtrl>().attakcDamageForText;
            StartCoroutine(DamageFontAnimB());
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * upSpeed * Time.deltaTime;
        transform.rotation = Quaternion.identity;
    }
    IEnumerator DamageFontAnimB()
    {
        upSpeed = 7.0f;
        for (int i = 0; i < 10; i++)
        {
            if (i < 3)
            {
                //transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                upSpeed -= 1f;
                yield return new WaitForSeconds(0.15f);
            }
            else
            {
                text.color -= new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.15f);
            }
        }
        Destroy(this.gameObject);
    }
    IEnumerator DamageFontAnimM()
    {
        upSpeed = 5.0f;
        for (int i = 0; i < 10; i++)
        {
            if (i < 3)
            {
                transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                upSpeed -= 1f;
                yield return new WaitForSeconds(0.15f);
            }
            else
            {
                text.color -= new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.15f);
            }
        }
        Destroy(this.gameObject);
    }
    IEnumerator DamageFontAnimP()
    {
        upSpeed = 30.0f;
        for (int i = 0; i < 10; i++)
        {
            upSpeed -= 6f;
            if (i < 3)
            {
                transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                text.color -= new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        Destroy(this.gameObject);
    }
}
