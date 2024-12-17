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
        gridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        if (gridGame == null)
        {
            gridGame = GameObject.FindObjectOfType<GridManager>();
        }

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!isMoving)
        {
            HandleMovement();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleMovement()
    {
        Vector2Int newPosition = gridPosition;

        if (Input.GetKey(KeyCode.W))
        {
            for (int y = gridPosition.y + 1; y < gridGame.gridHeight; y++)
            {
                if (!gridGame.IsValidMove(new Vector2Int(gridPosition.x, y)))
                    break;
                newPosition.y = y;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            for (int y = gridPosition.y - 1; y >= 0; y--)
            {
                if (!gridGame.IsValidMove(new Vector2Int(gridPosition.x, y)))
                    break;
                newPosition.y = y;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            for (int x = gridPosition.x - 1; x >= 0; x--)
            {
                if (!gridGame.IsValidMove(new Vector2Int(x, gridPosition.y)))
                    break;
                newPosition.x = x;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            for (int x = gridPosition.x + 1; x < gridGame.gridWidth; x++)
            {
                if (!gridGame.IsValidMove(new Vector2Int(x, gridPosition.y)))
                    break;
                newPosition.x = x;
            }
        }

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
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Clear"))
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
            gridPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
    }
}
