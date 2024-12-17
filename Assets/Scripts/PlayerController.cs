using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector2Int gridPosition;
    public GridManager gridGame;
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rb;

    public Sprite Image;
    public bool shouldRotate = false;
    public GameObject[] g;
    private int clearBlockCount;

    public int Count = 0;

    
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
       
        clearBlockCount = GameObject.FindGameObjectsWithTag("Clear").Length;
        Debug.Log("Total clear blocks: " + clearBlockCount);
    }

    void Update()
    {
        if (!isMoving)
        {
            HandleMovement();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
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
            Count++;
            for (int y = gridPosition.y + 1; y < gridGame.gridHeight; y++)
            {
                if (!gridGame.IsValidMove(new Vector2Int(gridPosition.x, y)))
                    break;
                newPosition.y = y;
            }
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            Count++;
            for (int y = gridPosition.y - 1; y >= 0; y--)
            {
                if (!gridGame.IsValidMove(new Vector2Int(gridPosition.x, y)))
                    break;
                newPosition.y = y;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            Count++;
            for (int x = gridPosition.x - 1; x >= 0; x--)
            {
                if (!gridGame.IsValidMove(new Vector2Int(x, gridPosition.y)))
                    break;
                newPosition.x = x;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            Count++;
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

    void FireLaser()
    {
        float rayLength = 5.0f;
        int layerMask = LayerMask.GetMask("sss");
        Vector3[] directions = { Vector3.down, Vector3.up, Vector3.left, Vector3.right };
        Count++;
        foreach (Vector3 dir in directions)
        {
            Vector3 checkDirection = shouldRotate ? Quaternion.Euler(0, 0, 45) * dir : dir;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDirection, rayLength, layerMask);
            Vector3 endPosition = hit.collider ? new Vector3(hit.point.x, hit.point.y, 0) : transform.position + checkDirection * rayLength;

            StartCoroutine(ShowRay(transform.position, endPosition));

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("TargetBlock") && !shouldRotate)
                {
                    shouldRotate = true;
                }
                else if (hit.collider.CompareTag("TargetBlock") && shouldRotate)
                {
                    shouldRotate = false;
                }
                else if (hit.collider.CompareTag("Clear"))
                {
                    SpriteRenderer spriteRenderer = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = Image;
                    }

                    hit.collider.enabled = false;
                    clearBlockCount--;

                    if (clearBlockCount <= 0)
                    {
                        total();
                    }
                }
                else if (hit.collider.name == "Power")
                {
                    foreach (GameObject obj in g)
                    {
                        Collider2D collider = obj.GetComponent<Collider2D>();
                        if (collider != null)
                        {
                            collider.isTrigger = !collider.isTrigger;
                        }
                    }
                }
            }
        }
    }

    IEnumerator ShowRay(Vector3 start, Vector3 end)
    {
        LineRenderer lr = new GameObject("TemporaryLine").AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default")) { color = Color.blue };
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.enabled = true;

        yield return new WaitForSeconds(0.1f);
        Destroy(lr.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Clear") || collision.gameObject.CompareTag("glass") || collision.gameObject.CompareTag("TargetBlock"))
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
            gridPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
    }
    public void total()
    {
        if(gridGame.GuideClear<=Count)
        {
            SceneManager.LoadScene("GuideClear");
        }
        else if(gridGame.GuideClear>Count)
        {
            SceneManager.LoadScene("Clearscene");
        }
    }
}
