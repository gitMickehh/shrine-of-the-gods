using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public bool active = true;
    public Transform target;
    [Range(0, 20)] public float smoothSpeed = 5f;

    public Vector3 Offset;
    private Vector3 currentOffset;
    private Vector2 lastPlayerDirection;
    public S_Vector2 playerDirection;
    public Vector2 threhsold;

    [Header("Bounds")]
    public bool haveBounds;
    public Vector2 minBounds;
    public Vector2 maxBounds;


    private void Start()
    {
        playerDirection.onValueChanged += ChangeOffset;

        lastPlayerDirection = playerDirection.Value;
        currentOffset = Offset;
    }

    private Vector3 CheckBounds(Vector3 desiredPosition)
    {
        if (desiredPosition.x <= minBounds.x)
        {
            desiredPosition.x = minBounds.x;
        }
        else if (desiredPosition.x >= maxBounds.x)
        {
            desiredPosition.x = maxBounds.x;
        }

        if (desiredPosition.y <= minBounds.y)
        {
            desiredPosition.y = minBounds.y;
        }
        else if (desiredPosition.y >= maxBounds.y)
        {
            desiredPosition.y = maxBounds.y;
        }

        return desiredPosition;
    }

    private Vector3 CalculateThreshold(Vector3 desiredPosition, Vector3 smoothPosition)
    {
        var difference = desiredPosition - smoothPosition;

        if (Mathf.Abs(difference.x) <= threhsold.x)
        {
            smoothPosition.x = desiredPosition.x;
        }
        if (Mathf.Abs(difference.y) <= threhsold.y)
        {
            smoothPosition.y = desiredPosition.y;
        }

        return smoothPosition;
    }

    private void ChangeOffset()
    {
        if (playerDirection.Value != lastPlayerDirection)
        {
            lastPlayerDirection = playerDirection.Value;

            currentOffset.x = Offset.x * lastPlayerDirection.x;


            currentOffset.y = Offset.y * lastPlayerDirection.y;
        }
    }

    private void FixedUpdate()
    {
        if (active)
            FollowTarget();
    }

    public void StartFollowing(Transform targetToFollow)
    {
        target = targetToFollow;
        active = true;
    }

    public void StopFollowing()
    {
        active = false;
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = target.position + currentOffset;

        if (haveBounds)
            desiredPosition = CheckBounds(desiredPosition);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = CalculateThreshold(desiredPosition, smoothPosition);
    }

    [ContextMenu("Move Camera to Position")]
    public void MoveCameraToPlace()
    {
        transform.position = target.position + Offset;
    }

    public void OnDrawGizmosSelected()
    {
        //Draw bounds

        if (haveBounds)
        {
            Gizmos.color = Color.red;

            var p1 = new Vector2(minBounds.x, maxBounds.y);
            var p2 = new Vector2(maxBounds.x, minBounds.y);

            Gizmos.DrawLine(minBounds, p1);
            Gizmos.DrawLine(p1, maxBounds);
            Gizmos.DrawLine(maxBounds, p2);
            Gizmos.DrawLine(p2, minBounds);
        }
    }
}
