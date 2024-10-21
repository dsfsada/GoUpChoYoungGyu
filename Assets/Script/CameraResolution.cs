using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public Transform target; // 플레이어를 가리키는 변수
    public Transform loopWall; // 플레이어가 부딪히면 돌아가는 벽
    public Vector3 offset; // 플레이어와 카메라 사이의 거리를 제어하는 변수

    //private bool shouldFollow = false; // 카메라가 따라가야 하는지 여부를 나타내는 변수

    /*    void FixedUpdate()
        {
            // 플레이어가 목표 지점에 도달했는지 확인
            if (!shouldFollow && target.position.x >= 2.55f && target.position.x <= loopWall.position.x - 3.5f) // target.position.x는 플레이어의 x 좌표를 나타냄
            {
                shouldFollow = true; // 플레이어가 목표 지점에 도달했을 때 카메라가 따라가도록 설정
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
                transform.position = target.position + offset; // 플레이어와 offset을 더해 카메라의 원하는 위치를 계산
            }
        }*/

    void FixedUpdate()
    {
        // 플레이어가 목표 지점에 도달했는지 확인
        if (target.position.x >= loopWall.position.x - 3.0f) // target.position.x는 플레이어의 x 좌표를 나타냄
        {
            transform.position = new Vector3(loopWall.position.x - 2.5f, -2, -10);  // -2.5
        }
        else if (target.position.x <= 2.55f)
        {
            transform.position = new Vector3(3, -2, -10);
        }
    }
}