using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set; } //for singleton pattern > property, not field

    public event EventHandler<OnSelectedVisualChangedEventArgs> OnSelectedVisualChanged;
    public class OnSelectedVisualChangedEventArgs : EventArgs {
        public Objects_clear selectedObjects;
    }

    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private KeyInputs keyInputs;
    //[SerializeField] private LayerMask stairsLayerMask;

    private float moveSpeed;
    private bool isWalking;
    private Vector3 lastInteractDirection;
    private Objects_clear selectedObjects;

    AudioManager audioManager;
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("More than one player instance");
        }
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        keyInputs.OnInteractAction += KeyInputs_OnInteract_Action;
    }

    private void KeyInputs_OnInteract_Action(object sender, System.EventArgs e)
    {
        if (selectedObjects != null)
        {   
            if (selectedObjects.name == "Stairs_clear")
            {
                selectedObjects.InteractStairs();
            }
            else if (selectedObjects.name == "Wall_Window_clear")
            {
                selectedObjects.InteractWindow(1);
            }
            else if (selectedObjects.name == "Wall_Window_broken")
            {
                selectedObjects.InteractWindow(2);
            }
            else if (selectedObjects.name == "Chair_1" |  selectedObjects.name =="Chair_2") 
            {
                selectedObjects.InteractChair();
            }
            else if (selectedObjects.name == "Extinguisher" |  selectedObjects.name =="Hose") 
            {
                selectedObjects.InteractFireSafetyTool();
            }
            else if (selectedObjects.name == "Door_Closed_clear")
            {
                selectedObjects.InteractDoor(1);
            }
            else if (selectedObjects.name == "Door_Open_clear")
            {
                selectedObjects.InteractDoor(2);
            }
            else if (selectedObjects.name == "Plant_1" | selectedObjects.name == "Plant_2")
            {
                selectedObjects.InteractPlant();
            }
            else if (selectedObjects.name == "Alarm")
            {
                selectedObjects.InteractAlarm();
            }
            else if (selectedObjects.name == "Phone_intact")
            {
                selectedObjects.InteractPhone();
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
        Scream();
    }

    private void Scream()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlaySFX(audioManager.scream);
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = keyInputs.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 1f;
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, lastInteractDirection, out raycastHit, interactDistance)) // maybe use layer mask for all interactabe objects , stairsLayerMask
        {
            if (raycastHit.transform.TryGetComponent(out Objects_clear objects_clear)) 
            {
                // player hit interactable object
                if (objects_clear != selectedObjects)
                {
                    SetSelectedObjects(objects_clear); // if interactable object infront of player 
                                                            // then set it, else null
                }
            } else {
                SetSelectedObjects(null);
            }
        } else {
            SetSelectedObjects(null);
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        } else {
            moveSpeed = walkSpeed;
        }
        Vector2 inputVector = keyInputs.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerRadius = 0.3f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime; // so speed doesn't vary with framerate
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove) // if cannot move
        {
            // Attempt x direction
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            
            if (canMove)
            {
                moveDirection = moveDirX;
            } else {
                // x direction didn't work, Attempt z direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                
                if (canMove)
                {
                    moveDirection = moveDirZ;
                } else {
                    //Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero; //true if movedirection not zero

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed); //Slerp fo smooth rotation (start, target, quick))
    }

    // fct to avoid duplication
    private void SetSelectedObjects(Objects_clear selectedObjects) {
        this.selectedObjects = selectedObjects;

        OnSelectedVisualChanged?.Invoke(this, new OnSelectedVisualChangedEventArgs {
                        selectedObjects = selectedObjects
                    });
    }
}
