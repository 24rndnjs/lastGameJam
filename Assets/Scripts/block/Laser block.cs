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
                        rotation = Quaternion.Euler(0, 0, 45) * directions[0];  // ������ 45�� ȸ��
                        rotation1 = Quaternion.Euler(0, 0, 45) * directions[1];  // ������ 45�� ȸ��
                        rotation2 = Quaternion.Euler(0, 0, 45) * directions[2];  // ������ 45�� ȸ��
                        rotation3 = Quaternion.Euler(0, 0, 45) * directions[4];  // ������ 45�� ȸ��
            
                        hit = Physics2D.Raycast(transform.position, rotation, rayLength, layerMask);  // �� �������� �ٽ� ����ĳ��Ʈ �߻�
                        hit = Physics2D.Raycast(transform.position, rotation1, rayLength, layerMask);  // �� �������� �ٽ� ����ĳ��Ʈ �߻�
                        hit = Physics2D.Raycast(transform.position, rotation2, rayLength, layerMask);  // �� �������� �ٽ� ����ĳ��Ʈ �߻�
                        hit = Physics2D.Raycast(transform.position, rotation3, rayLength, layerMask);  // �� �������� �ٽ� ����ĳ��Ʈ �߻�
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

                    // �浹 �������� ���� �׸� (����ĳ��Ʈ�� �ð��� Ȯ��)
                    Debug.DrawLine(transform.position, transform.position + dir * rayLength, new Color(1, 0, 0), 0.1f);
                    // �浹�� ������Ʈ�� �̸��� �α׷� ���
                    Debug.Log("Hit " + hit.collider.name + " at " + dir);
                }


            }
        }


    }
}
