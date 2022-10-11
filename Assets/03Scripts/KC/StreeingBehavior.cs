using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreeingBehavior : MonoBehaviour
{
    public float maxSpeed;
    public float Speed;

    Vector3 force;
    Vector3 TargetPosition;
    Vector3 ThisPosition;

    //wander
    public float WanderJitter;
    public float WanderRadius;
    private Vector3 WanderTarget;
    private Vector3 Target;
    
    private Rigidbody2D rigidbody2D;
    private bool stop;

    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        float theta = Mathf.Deg2Rad * 360f;
        WanderTarget = new Vector3(WanderRadius * Mathf.Cos(theta), WanderRadius * Mathf.Sin(theta), 0);
        stop = false;

        StartCoroutine(GoORStop());
    }

    void Update()
    {
        ThisPosition = transform.position;

        if (!stop)
        {
            force = Wander();
            rigidbody2D.AddForce(force);
        }
        else 
        {
            force = new Vector3(0,0,0);
            rigidbody2D.AddForce(force);
        }
    }

    /// <summary>
    /// Seek
    /// </summary>
    Vector3 Seek(Vector3 TargetPos)
    {
        //  (타겟위치 - 내 위치)를 정규화 = 방향
        Vector2 DF = (TargetPos - ThisPosition).normalized * maxSpeed;
        Vector2 SF = DF - rigidbody2D.velocity;
        return SF;
    }

    /// <summary>
    /// Wander
    /// </summary>
    Vector3 Wander()
    {
        float JitterThisTimeSlice = WanderJitter * Time.deltaTime;

        WanderTarget += new Vector3(rand() * JitterThisTimeSlice,
                                    rand() * JitterThisTimeSlice, 0);

        WanderTarget.Normalize();

        WanderTarget *= WanderRadius;

        Target = Matrix4x4.Rotate(transform.rotation) * WanderTarget;
        Target += transform.position;

        return Seek(Target);
    }
    public float rand()
    {
        float ran = Random.Range(-1f, 1f);
        return ran;
    }

    IEnumerator GoORStop()
    {
        yield return new WaitForSeconds(3f);
        stop = true;

        yield return new WaitForSeconds(3f);
        stop = false;

        StartCoroutine(GoORStop());
    }
}