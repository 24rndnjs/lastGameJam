using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2Int gridPosition;
    public GridManager gridGame;
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        targetPosition = transform.position;

        if (gridGame == null)
        {
            gridGame = GameObject.FindObjectOfType<GridManager>();
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleMovement()
    {
        if (isMoving) return;

        Vector2Int newPosition = gridPosition;

        if (Input.GetKeyDown(KeyCode.W)) newPosition.y = gridGame.gridHeight - 1;
        if (Input.GetKeyDown(KeyCode.S)) newPosition.y = 0;
        if (Input.GetKeyDown(KeyCode.A)) newPosition.x = 0;
        if (Input.GetKeyDown(KeyCode.D)) newPosition.x = gridGame.gridWidth - 1;

        if (gridGame.IsValidMove(newPosition))
        {
            gridPosition = newPosition;
            targetPosition = new Vector3(gridPosition.x, gridPosition.y, 0);
            isMoving = true;
        }
    }

    void MovePlayer()
    {
        if (isMoving)
        {
            Vector2 direction = (targetPosition - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                rb.linearVelocity = Vector2.zero;
                transform.position = targetPosition;
                isMoving = false;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
            targetPosition = transform.position;
            gridPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
    }
}
