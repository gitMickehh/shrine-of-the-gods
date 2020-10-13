using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Birds : MonoBehaviour
{
    public Collider2D myCollider;
    public Rigidbody2D myRB;

    [Header("Birds Settings")]
    public float range;
    public float flyTime = 2f;
    public Ease flyingEasing = Ease.OutQuad;
    public bool randomDirectin = true;
    public Vector2 specificDireciton;

    [Header("Birds Sprites")]
    public SpriteRenderer[] sprites;
    public Sprite standing;
    public Sprite flying;

    public void ChangeSprites(Sprite sprite)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sprite = sprite;
        }
    }

    public void OnGetDisturbed()
    {
        myCollider.enabled = false;

        ChangeSprites(flying);

        Vector2 direction;

        if(randomDirectin)
        {
            float rX = Random.Range(-1.0f,1.0f);
            float rY = Random.Range(-1.0f,1.0f);

            direction = new Vector2(rX,rY);
        }
        else
        {
            direction = specificDireciton.normalized;
        }   

        direction *= range;
        direction.x += transform.position.x;
        direction.y += transform.position.y;
        
        myRB.DOMove(direction,flyTime).SetEase(flyingEasing).OnComplete(
            ()=>{ myCollider.enabled = true;
                ChangeSprites(standing);
            }
        );
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.cyan;
        Vector2 direction = transform.position;
        direction.x += specificDireciton.normalized.x;
        direction.y += specificDireciton.normalized.y;

        Gizmos.DrawLine(transform.position,direction);    
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
