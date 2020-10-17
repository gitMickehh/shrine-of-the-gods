using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mummy : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D myBody;
    public float walkDistance = 4f;

    [Header("Rot")]
    public GameObject rot_VFX;
    public float rottingRange;
    public LayerMask infectionLayer;

    private void Start()
    {
        Seek();
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 rDirection = new Vector2();
        rDirection.x = Random.Range(-1.0f, 1.0f);
        rDirection.y = Random.Range(-1.0f, 1.0f);
        return rDirection;
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 newDirection = GetRandomDirection() * walkDistance;
        newDirection.x += transform.position.x;
        newDirection.y += transform.position.y;

        return newDirection;
    }

    private void FixedUpdate()
    {
        InfectRange();
    }

    private void Seek()
    {
        //myBody.velocity = GetRandomDirection() * Time.deltaTime * speed;
        myBody.DOMove(GetRandomPosition(), speed).SetEase(Ease.Linear).OnComplete(()=>Seek());
    }

    public void OnHitSomething()
    {
        Seek();
    }

    public void KillMummy()
    {
        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("I hit something");
    //    OnHitSomething();
    //}

    private void InfectRange()
    {
        var infectionRadius = new Vector2(transform.position.x , transform.position.y);
        Collider2D[] infectables = Physics2D.OverlapCircleAll(infectionRadius, rottingRange, infectionLayer);

        foreach (Collider2D infectable in infectables)
        {
            var collectable = infectable.GetComponent<Collectable>();
            if(collectable != null)
            {
                collectable.Rot(rot_VFX);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rottingRange);
    }
}
