using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DOffset : MonoBehaviour
{
    public Transform target;
    [Range(0, 20)] public float smoothSpeed = 5f;

    public Vector3 Offset;
    private Vector3 currentOffset;
    private Vector2 lastPlayerDirection;
    public S_Vector2 playerDirection;
    public Vector2 threhsold;


    private void Start()
    {
        playerDirection.onValueChanged += ChangeOffset;

        lastPlayerDirection = playerDirection.Value;
        currentOffset = Offset;
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
        Vector3 desiredPosition = target.position + currentOffset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = CalculateThreshold(desiredPosition, smoothPosition);
    }

    [ContextMenu("Move Camera to Position")]
    public void MoveCameraToPlace()
    {
        transform.position = target.position + Offset;
    }
}
