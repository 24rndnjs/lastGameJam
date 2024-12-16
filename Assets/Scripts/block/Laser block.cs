using UnityEngine;
using System;

public class Laserblock : MonoBehaviour
{
    public bool shouldRotate = false;
    public bool chek = false;
    void Update()
    {
        float rayLength = 5.0f;  // ����ĳ��Ʈ�� ����
        int layerMask = LayerMask.GetMask("sss");  // ����ĳ��Ʈ�� ������ ���̾� ����ũ

        // ����ĳ��Ʈ�� ��� ���� �迭
        Vector3[] directions = {
            Vector3.down,  // �Ʒ�
            Vector3.up,    // ��
            Vector3.left,  // ����
            Vector3.right  // ������
        };

        if (Input.GetKeyDown(KeyCode.Space))
        {

            foreach (Vector3 dir in directions)
            {
                Vector3 checkDirection = shouldRotate ? Quaternion.Euler(0, 0, 45) * dir : dir;  // ���ǿ� ���� ������ ����
                RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDirection, rayLength, layerMask);
                if (hit.collider != null)
                {
                    Debug.DrawLine(transform.position, transform.position + checkDirection * rayLength, new Color(1, 0, 0), 0.1f);
                    Debug.Log("Hit " + hit.collider.name + " at " + checkDirection);

                    if (hit.collider.name == "TargetBlock" && !shouldRotate)
                    {
                        shouldRotate = true;  // ù ��° ��Ͽ� �浹 �� ȸ�� �ǽ�
                    }
                    else if (hit.collider.name == "TragetBlock2" && shouldRotate)
                    {
                        Debug.Log("Found TragetBlock2, resetting rotation.");
                        shouldRotate = false;  // �� ��° ��Ͽ� �浹 �� ȸ�� ����
                    }
                }
            }

        }

    }
}

