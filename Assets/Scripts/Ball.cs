using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    [SerializeField] float ballVel;
    [SerializeField, Min(1.1f)] float velocityMultiplier;

    Vector2 vell;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Throw();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        rb.simulated = true;
        Throw();
    }

    void Throw()
    {
        rb.AddForce(new Vector2(UnityEngine.Random.Range(-1f, 1f) * ballVel * Time.deltaTime, UnityEngine.Random.Range(-1f, 1f) * ballVel * Time.deltaTime));
        StartCoroutine(SetVelocity());
    }

    IEnumerator SetVelocity()
    {
        while (rb.velocity == Vector2.zero) yield return null;

        float a, b;
        if (rb.velocity.x < 0) a = Mathf.Max(-1, rb.velocity.x);
        else a = Mathf.Min(1, rb.velocity.x);
        if (rb.velocity.y < 0) b = Mathf.Max(-1, rb.velocity.y);
        else b = Mathf.Min(1, rb.velocity.y);

        rb.velocity = new Vector2(a, b);
        vell = rb.velocity;
    }

    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 refVec = Vector2.Reflect(vell, collision.contacts[0].normal);

        if (collision.transform.CompareTag("Player"))
            refVec *= velocityMultiplier;

        rb.velocity = refVec;
        vell = rb.velocity;

        if (collision.transform.CompareTag("PointWallP2"))
        {
            GameManager.Instance.PointHost();
            ResetPos();
        }

        if (collision.transform.CompareTag("PointWallP1"))
        {
            GameManager.Instance.PointP2();
            ResetPos();
        }
    }

    void ResetPos()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;

        Throw();
    }
}
