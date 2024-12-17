using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Laserblock : MonoBehaviour
{
    public Sprite Image;
    public bool shouldRotate = false;
    public GameObject[] g;
    private int clearBlockCount;
    private LineRenderer lineRenderer;

    void Awake()
    {
        clearBlockCount = GameObject.FindGameObjectsWithTag("Clear").Length;
        Debug.Log("Total clear blocks: " + clearBlockCount);

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color.red;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float rayLength = 5.0f;
            int layerMask = LayerMask.GetMask("sss");
            Vector3[] directions = { Vector3.down, Vector3.up, Vector3.left, Vector3.right };

            foreach (Vector3 dir in directions)
            {
                Vector3 checkDirection = shouldRotate ? Quaternion.Euler(0, 0, 45) * dir : dir;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDirection, rayLength, layerMask);
                Vector3 endPosition = hit.collider ? new Vector3(hit.point.x, hit.point.y, 0) : transform.position + checkDirection * rayLength;

                StartCoroutine(ShowRay(transform.position, endPosition));

                if (hit.collider != null)
                {
                    Debug.Log("Hit " + hit.collider.name + " at " + checkDirection);

                    if (hit.collider.CompareTag("TargetBlock") && !shouldRotate)
                    {
                        shouldRotate = true;
                    }
                    else if (hit.collider.CompareTag ("TargetBlock") && shouldRotate)
                    {
                        Debug.Log("Found TragetBlock2, resetting rotation.");
                        shouldRotate = false;
                    }
                    else if (hit.collider.CompareTag("Clear"))
                    {
                        if (hit.collider != null && hit.collider.CompareTag("Clear")) // "Target"은 원하는 태그 이름
                        {
                            // 충돌한 오브젝트의 SpriteRenderer 컴포넌트를 찾고, 색상을 변경
                            SpriteRenderer spriteRenderer = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                            if (spriteRenderer != null)
                            {
                                spriteRenderer.sprite = Image; // 스프라이트 변경
                            }
                        }
                        hit.collider.enabled = false;
                        clearBlockCount--;

                        Debug.Log("Clear block hit. Remaining blocks: " + clearBlockCount);
                        if (clearBlockCount <= 0)
                        {
                            Debug.Log("All clear blocks removed. Level cleared!");
                            SceneManager.LoadScene("Clearscene");
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
}
