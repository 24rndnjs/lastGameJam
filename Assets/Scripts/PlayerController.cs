using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
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
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.up;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3.down;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3.right;

        if (direction != Vector3.zero)
        {
            targetPosition = transform.position + direction;
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
        }
    }
}
