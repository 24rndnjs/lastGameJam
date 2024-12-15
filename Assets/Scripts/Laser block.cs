using UnityEngine;
using System;

public class Laserblock : MonoBehaviour
{
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayLength, layerMask);
                if (hit.collider != null)
                {
                    // 충돌 지점까지 선을 그림 (레이캐스트의 시각적 확인)
                    Debug.DrawLine(transform.position, transform.position + dir * rayLength, new Color(1, 0, 0), 2f);
                    // 충돌한 오브젝트의 이름을 로그로 출력
                    Debug.Log("Hit " + hit.collider.name + " at " + dir);
                }
            }
        }

    }
}
