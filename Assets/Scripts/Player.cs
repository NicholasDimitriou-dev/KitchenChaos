using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitcehnObjectParent
{
    public event EventHandler OnPickup;
    private static Player instance;
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> 
        OnSelectedCounterChange;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    [SerializeField] private GameInput input;
    private Vector3 lastInteactDir;
    [SerializeField] private LayerMask countersLayerMask;
    private BaseCounter selectedCounter;
    [SerializeField] private Transform location;
    private KitchenObject kitchenObject;
    private KitchenObjectCO kitchenObjectCo;

    private void Update()
    {
        HandleMovement();
        handleInteractions();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Player");
        }
        Instance = this;
    }

    private void Start()
    {
        input.OnInteractAction += GameInput_OnInteractAction;
        input.OnInteractAltAction += GameInput_OnInteractAltAction;
    }

    private void GameInput_OnInteractAltAction(object sender, System.EventArgs e)
    {
        if (!GameHandler.Instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null) {
            selectedCounter.InteractAlt(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameHandler.Instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = input.GetMovemenetVectorNormailized();
        Vector3 moveDir =  new Vector3(inputVector.x, 0f, inputVector.y);
        float playerSize = 0.7f;
        float playerHieght = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position+ Vector3.up * playerHieght,playerSize, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position+ Vector3.up * playerHieght,playerSize, moveDirX, moveDistance);
            if (canMove) {
                moveDir = moveDirX;
            }else {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position+ Vector3.up * playerHieght,playerSize, moveDirZ, moveDistance);
                if (canMove) {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove) {
            transform.position += moveSpeed * Time.deltaTime * moveDir;
        }
        
        isWalking = (moveDir != Vector3.zero);
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir,rotateSpeed * Time.deltaTime);
    }

    private void handleInteractions()
    {
        Vector2 inputVector = input.GetMovemenetVectorNormailized();
        Vector3 moveDir =  new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteactDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteactDir, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }else {
            SetSelectedCounter(null);
        }
        
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
    public Transform GetKitchObjectFollowTransfrom()
    {
        return location;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        this.kitchenObject = ko;
        if (kitchenObject != null)
        {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
        this.kitchenObjectCo = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

