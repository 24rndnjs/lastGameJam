using UnityEngine;
using System;

public class Laserblock : MonoBehaviour
{
    public bool shouldRotate = false;
    public GameObject g;
    private int clearBlockCount; // 클리어 블록 개수를 저장할 변수
    void Awake()
    {
        // "Clear" 태그를 가진 모든 게임 오브젝트를 찾아서 그 개수를 셈
        clearBlockCount = GameObject.FindGameObjectsWithTag("Clear").Length;
        Debug.Log("Total clear blocks: " + clearBlockCount);
    }
    void Update()
    {
        float rayLength = 5.0f;  // 레이캐스트의 길이
        int layerMask = LayerMask.GetMask("sss");  // 레이캐스트가 반응할 레이어 마스크

        // 레이캐스트를 쏘는 방향 배열
        Vector3[] directions = {
            Vector3.down,  // 아래
            Vector3.up,    // 위
            Vector3.left,  // 왼쪽
            Vector3.right  // 오른쪽
        };

        if (Input.GetKeyDown(KeyCode.Space))
        {

            foreach (Vector3 dir in directions)
            {
                Vector3 checkDirection = shouldRotate ? Quaternion.Euler(0, 0, 45) * dir : dir;  // 조건에 따라 방향을 조절
                RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDirection, rayLength, layerMask);
                if (hit.collider != null)
                {
                    Debug.DrawLine(transform.position, transform.position + checkDirection * rayLength, new Color(1, 0, 0), 0.1f);
                    Debug.Log("Hit " + hit.collider.name + " at " + checkDirection);

                    if (hit.collider.name == "TargetBlock" && !shouldRotate)
                    {
                        shouldRotate = true;  // 첫 번째 블록에 충돌 시 회전 실시
                    }
                    else if (hit.collider.name == "TragetBlock2" && shouldRotate)
                    {
                        Debug.Log("Found TragetBlock2, resetting rotation.");
                        shouldRotate = false;  // 두 번째 블록에 충돌 시 회전 리셋
                    }
                    else if (hit.collider.CompareTag("Clear"))
                    {
                        // "Clear" 태그를 가진 블록의 콜라이더 비활성화 및 블록 개수 감소
                        hit.collider.enabled = false;
                        clearBlockCount--;
                        Debug.Log("Clear block hit. Remaining blocks: " + clearBlockCount);

                        // 모든 블록이 제거되었다면 클리어 화면 표시
                        if (clearBlockCount <= 0)
                        {
                            Debug.Log("All clear blocks removed. Level cleared!");
                            // 클리어 화면 로직을 여기에 구현 (예: 씬 전환, UI 업데이트 등)
                        }
                    }
                    else if (hit.collider.name == "Power")
                    {
                        Collider2D collider = g.GetComponent<Collider2D>();
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

