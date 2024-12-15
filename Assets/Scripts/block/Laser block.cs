using UnityEngine;
using System;

public class Laserblock : MonoBehaviour
{
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayLength, layerMask);
                if (hit.collider != null)
                {
                    // �浹 �������� ���� �׸� (����ĳ��Ʈ�� �ð��� Ȯ��)
                    Debug.DrawLine(transform.position, transform.position + dir * rayLength, new Color(1, 0, 0), 2f);
                    // �浹�� ������Ʈ�� �̸��� �α׷� ���
                    Debug.Log("Hit " + hit.collider.name + " at " + dir);
                }
            }
        }

    }
}
