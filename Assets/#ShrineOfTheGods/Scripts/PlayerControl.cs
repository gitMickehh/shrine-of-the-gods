using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private bool controlable;

    Rigidbody2D _rb;
    Vector2 movementDirection;
    float movementSpeed;

    [Header("Movement")]
    public float moveSpeed = 10f;
    [HideInInspector] public Vector2 direction;
    public S_Vector2 playerAbsoluteDirection;

    [Header("Interaction")]
    public Vector2 interactionOffset = new Vector2(0, 0.5f);
    public float interactionRange = 2f;
    public LayerMask interactablesLayer;
    private IInteractable currentInteractable;
    public S_Inventory myInventory;

    [Header("Animation")]
    public Animator myAnimator;

    [Header("Eating")]
    public UnityEvent OnEat;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        myInventory.player = this;
    }

    private void OnDisable()
    {
        myInventory.player = null;
        myInventory.Value = null;
    }

    public void EnableControl()
    {
        //Debug.Log("Enable Controls");
        controlable = true;
    }

    public void Disablecontrol()
    {
        //Debug.Log("Disable Controls");
        controlable = false;
        direction = Vector2.zero;
        playerAbsoluteDirection.Value = direction;
        _rb.velocity = Vector2.zero;
        Animate();
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
        else if(Input.GetButtonDown("Eat"))
        {
            if(myInventory.Value != null)
            {
                OnEat.Invoke();
                myInventory.Value.EatItem();
            }
        }
        else if (Input.GetButtonDown("Drop"))
        {
            if(myInventory.Value != null)
                myInventory.Value.DropItem();
        }
    }

    void Move()
    {
        _rb.velocity = movementDirection * movementSpeed * moveSpeed;

        if (movementDirection != Vector2.zero)
        {
            direction = movementDirection;

            Vector2 absDirection = new Vector2(Mathf.Sign(direction.x), Mathf.Sign(direction.y));
            if(direction.x == 0)
            {
                absDirection.x = playerAbsoluteDirection.Value.x;
            }
            if (direction.y == 0)
            {
                absDirection.y = playerAbsoluteDirection.Value.y;
            }

            playerAbsoluteDirection.Value = absDirection;
        }
    }

    void Animate()
    {
        myAnimator.SetFloat("Horizontal", direction.x);
        myAnimator.SetFloat("Vertical", direction.y);
        myAnimator.SetFloat("Speed", movementSpeed);
    }

    public void PrayAnimation()
    {
        myAnimator.SetTrigger("Pray");
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

            //SetDirectionBetweenPlayerAndInteractable();
        }

    }

    //private void SetDirectionBetweenPlayerAndInteractable()
    //{
    //      //for camera smoothing and all :) but nvm
    //    Vector2 newDirection = currentInteractable.GetPosition() - direction;
    //    Vector2 absDirection = new Vector2(Mathf.Sign(newDirection.x), Mathf.Sign(newDirection.y));
    //    if (newDirection.x == 0)
    //    {
    //        absDirection.x = playerAbsoluteDirection.Value.x;
    //    }
    //    if (newDirection.y == 0)
    //    {
    //        absDirection.y = playerAbsoluteDirection.Value.y;
    //    }

    //    playerAbsoluteDirection.Value = absDirection;
    //}

    void Interact()
    {
        currentInteractable = GetNearestInteractable();
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
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
