using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    [SerializeField] private GameInput input;
    private void Update()
    {
        HandleMovement();
        handleInteractions();
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
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance))
        {
            Debug.Log(raycastHit.transform);
        }
        
    }
}

