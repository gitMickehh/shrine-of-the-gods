using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class IdleSeeker : MonoBehaviour
{
    // [System.Serializable]public struct targetSet{
    //     public List<Transform> targets;
    // }
    [Header("References")]
    public Rigidbody2D myBody;
    private bool isIdle;
    public bool interruptable = true;
    public bool loopPath = true;
    public Action pathIsFinished;

    [Header("Seeking Settings")]
    public float timeToReachTarget = 3f;
    public float maxTimeToReachTarget = 6f;
    public float stoppingTime = 2f;
    public float maxStoppingTime = 3f;

    [Header("Targets")]
    // public List<targetSet> targetSets;
    public List<Transform> targets;
    private int currentTarget;
    private DG.Tweening.Core.TweenerCore<Vector2, Vector2, DG.Tweening.Plugins.Options.VectorOptions> moveTween;

    public void PauseSeeker(bool pauseState)
    {
        if (pauseState)
        {
            isIdle = false;
            StopAllCoroutines();

            if (moveTween != null)
                moveTween.Kill();
        }
        else
        {
            if (isIdle)
                return;

            isIdle = true;
            MoveToNext();
        }
    }

    public void InterruptSeeker(bool interruptState)
    {
        if (!interruptable)
            return;

        //for example when you start a conversation with them
        if (interruptState)
        {
            isIdle = false;
            StopAllCoroutines();

            if (moveTween != null)
                moveTween.Kill();
        }
        else
        {
            if(isIdle)
                return;
                
            isIdle = true;
            MoveToNext();
        }

    }

    public void StartSeeker()
    {
        isIdle = true;
        currentTarget = UnityEngine.Random.Range(0,targets.Count);
        MoveToNext();
    }

    private void SeekTarget(Vector3 targetPosition)
    {
        float r = UnityEngine.Random.Range(timeToReachTarget,maxTimeToReachTarget);
        moveTween = myBody.DOMove(targetPosition, r).OnComplete(StandInStop);
    }

    private void MoveToNext()
    {
        if(!isIdle)
            return;

        currentTarget++;

        if (currentTarget >= targets.Count)
        {
            if(!loopPath)
            {
                pathIsFinished.Invoke();
                return;
            }

            currentTarget = 0;
        }

        SeekTarget(targets[currentTarget].position);
    }

    private void StandInStop()
    {
        StartCoroutine(StandingInStop());
    }

    IEnumerator StandingInStop()
    {
        float r = UnityEngine.Random.Range(stoppingTime, maxStoppingTime);
        yield return new WaitForSeconds(r);
        MoveToNext();
    }


    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < targets.Count; i++)
        {
            if(i == 0)
            {
                Gizmos.DrawLine(transform.position, targets[i].position);    
                
            }
            else
            {
                Gizmos.DrawLine(targets[i-1].position, targets[i].position);    
            }
        }    
    }
}
