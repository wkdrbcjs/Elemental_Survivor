using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCtrl : MonoBehaviour
{
    private GameObject PC;

    public float Speed;

    private SpriteRenderer SprRend;
    private Animator anim;
    private Vector2 DF = new Vector2(0, 0);
    private Vector2 CV = new Vector2(0, 0);
    private Vector2 SF = new Vector2(0, 0);
    private Vector2 Vec = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.Find("PetFos");
        SprRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DF = new Vector2(PC.transform.position.x - this.transform.position.x, PC.transform.position.y - this.transform.position.y);

        SF = (DF - CV) * 0.1f;
        Vec = Vec + SF;
        CV = Vec;
        this.transform.Translate(Vec * Speed * Time.deltaTime, Space.World);

        

        if (PC.transform.position.x > transform.position.x)//ÁÂ¿ì¹ÝÀü
        {
                SprRend.flipX = true;
        }
        else
        {
                SprRend.flipX = false;
        }
    }
    public void PetAttack()
    {
        anim.SetTrigger("Attack");
    }
}
