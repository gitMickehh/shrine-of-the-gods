using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 movementDirection;
    float movementSpeed;

    [Header("Movement")]
    public float moveSpeed = 10f;
    [HideInInspector] public Vector2 direction;

    [Header("Interaction")]
    public Vector2 interactionOffset = new Vector2(0, 0.5f);
    public float interactionRange = 2f;
    public LayerMask interactablesLayer;
    private IInteractable currentInteractable;

    [Header("Animation")]
    public Animator myAnimator;

    private bool controlable;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        EnableControl();
    }

    public void EnableControl()
    {
        controlable = true;
    }

    public void Disablecontrol()
    {

        controlable = false;
        _rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (!controlable)
            return;

        IndicateInteractability();
        ProcessKeyboardInput();
        Move();
        Animate();
    }

    void ProcessKeyboardInput()
    {
        movementDirection = Vector2.zero;
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * moveSpeed;
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();


        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    void Move()
    {
        _rb.velocity = movementDirection * movementSpeed * moveSpeed;

        if (movementDirection != Vector2.zero)
        {
            direction = movementDirection;
        }
    }

    void Animate()
    {
        myAnimator.SetFloat("Horizontal", direction.x);
        myAnimator.SetFloat("Vertical", direction.y);
        myAnimator.SetFloat("Speed", movementSpeed);
    }

    void IndicateInteractability()
    {
        var interactable = GetNearestInteractable();

        if (interactable == null)
        {
            if (currentInteractable != null)
            {
                // Debug.Log("my current interactable isn't null!");
                currentInteractable.IndicateInteractionEnd();
                currentInteractable = null;
            }

            return;
        }
        else if (currentInteractable == null)
        {
            currentInteractable = interactable;
            currentInteractable.IndicateInteractionStart();
        }
        else if (currentInteractable != interactable)
        {
            currentInteractable.IndicateInteractionEnd();
            currentInteractable = interactable;
            currentInteractable.IndicateInteractionStart();
        }
    }

    void Interact()
    {
        // currentInteractable = GetNearestInteractable();
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            currentInteractable = null;
        }
    }

    IInteractable GetNearestInteractable()
    {
        var interactionCenter = new Vector2(transform.position.x + interactionOffset.x, transform.position.y + interactionOffset.y);
        Collider2D[] interactables = Physics2D.OverlapCircleAll(interactionCenter, interactionRange, interactablesLayer);

        if (interactables.Length == 0)
        {
            return null;
        }

        int indexOfClosest = 0;
        float minDistance = 0;

        for (int i = 0; i < interactables.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, interactables[i].transform.position);

            if (i == 0)
            {
                minDistance = distance;
            }
            else if (distance <= minDistance)
            {
                minDistance = distance;
                indexOfClosest = i;
            }
        }


        return interactables[indexOfClosest].GetComponent<IInteractable>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        var interactionCenter = new Vector2(transform.position.x + interactionOffset.x, transform.position.y + interactionOffset.y);
        Gizmos.DrawWireSphere(interactionCenter, interactionRange);
    }

}
