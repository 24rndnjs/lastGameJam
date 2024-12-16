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
                Vector3 rotation = directions[0];
                Vector3 rotation1 = directions[1];
                Vector3 rotation2 = directions[2];
                Vector3 rotation3 = directions[3];
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayLength, layerMask);
                if (hit.collider != null)
                {
                     if (hit.collider.name == "TargetBlock")
                    {
                        Debug.Log("yjayuuiay");
                        rotation = Quaternion.Euler(0, 0, 45) * directions[0];  // 방향을 45도 회전
                        rotation1 = Quaternion.Euler(0, 0, 45) * directions[1];  // 방향을 45도 회전
                        rotation2 = Quaternion.Euler(0, 0, 45) * directions[2];  // 방향을 45도 회전
                        rotation3 = Quaternion.Euler(0, 0, 45) * directions[4];  // 방향을 45도 회전
            
                        hit = Physics2D.Raycast(transform.position, rotation, rayLength, layerMask);  // 새 방향으로 다시 레이캐스트 발사
                        hit = Physics2D.Raycast(transform.position, rotation1, rayLength, layerMask);  // 새 방향으로 다시 레이캐스트 발사
                        hit = Physics2D.Raycast(transform.position, rotation2, rayLength, layerMask);  // 새 방향으로 다시 레이캐스트 발사
                        hit = Physics2D.Raycast(transform.position, rotation3, rayLength, layerMask);  // 새 방향으로 다시 레이캐스트 발사
                        if (hit.collider != null)
                        {
                            Debug.DrawLine(transform.position, transform.position + rotation * rayLength, new Color(1, 0, 0), 0.1f);
                            Debug.Log("Re-Hit " + hit.collider.name + " at " + rotation);      
                            
                            Debug.DrawLine(transform.position, transform.position + rotation1 * rayLength, new Color(1, 0, 0), 0.1f);
                            Debug.Log("Re-Hit " + hit.collider.name + " at " + rotation1);

                            Debug.DrawLine(transform.position, transform.position + rotation2 * rayLength, new Color(1, 0, 0), 0.1f);
                            Debug.Log("Re-Hit " + hit.collider.name + " at " + rotation2);     
                            
                            Debug.DrawLine(transform.position, transform.position + rotation3 * rayLength, new Color(1, 0, 0), 0.1f);
                            Debug.Log("Re-Hit " + hit.collider.name + " at " + rotation3);
                        }
                        break;
                    }

                    // 충돌 지점까지 선을 그림 (레이캐스트의 시각적 확인)
                    Debug.DrawLine(transform.position, transform.position + dir * rayLength, new Color(1, 0, 0), 0.1f);
                    // 충돌한 오브젝트의 이름을 로그로 출력
                    Debug.Log("Hit " + hit.collider.name + " at " + dir);
                }


            }
        }


    }
}
