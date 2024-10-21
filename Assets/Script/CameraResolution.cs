using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public Transform target; // �÷��̾ ����Ű�� ����
    public Transform loopWall; // �÷��̾ �ε����� ���ư��� ��
    public Vector3 offset; // �÷��̾�� ī�޶� ������ �Ÿ��� �����ϴ� ����

    //private bool shouldFollow = false; // ī�޶� ���󰡾� �ϴ��� ���θ� ��Ÿ���� ����

    /*    void FixedUpdate()
        {
            // �÷��̾ ��ǥ ������ �����ߴ��� Ȯ��
            if (!shouldFollow && target.position.x >= 2.55f && target.position.x <= loopWall.position.x - 3.5f) // target.position.x�� �÷��̾��� x ��ǥ�� ��Ÿ��
            {
                shouldFollow = true; // �÷��̾ ��ǥ ������ �������� �� ī�޶� ���󰡵��� ����
            }
            else
            {
                shouldFollow = false;
            }

            if(target.position.x <= -0.5)
            {
                transform.position = new Vector3(3, -2, -10);
            }

            if (shouldFollow)
            {
                transform.position = target.position + offset; // �÷��̾�� offset�� ���� ī�޶��� ���ϴ� ��ġ�� ���
            }
        }*/

    void FixedUpdate()
    {
        // �÷��̾ ��ǥ ������ �����ߴ��� Ȯ��
        if (target.position.x >= loopWall.position.x - 3.0f) // target.position.x�� �÷��̾��� x ��ǥ�� ��Ÿ��
        {
            transform.position = new Vector3(loopWall.position.x - 2.5f, -2, -10);  // -2.5
        }
        else if (target.position.x <= 2.55f)
        {
            transform.position = new Vector3(3, -2, -10);
        }
    }
}