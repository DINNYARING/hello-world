using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    private Transform tr;
    public float Height = 4.0f;
    public float Distance = 5.0f;
    public float moveDanping = 15f;     // 이동 계수 : 카메라 흔들림을 부드럽게 해주기 위한 계수
    public float rotateDamping = 10f;   // 회전 계수
    public float targetOffset = 2.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()   // ─ 카메라가 플레이어보다 뒤늦게 따라와야 하기 때문에 처리가 지연되어야 한다. (플레이어의 반응에 대한 상호작용)
    {   //      ┌ 카메라 위치를 타겟 뒤, 위에 배치
        var camPos = target.position - (target.forward * Distance) + (target.up * Height);
        // └ var 대신 Vector3로 적을 수 있다. (var가 알아서 Vector3로 값을 받는다.) 
        tr.position = Vector3.Lerp(tr.position, camPos, Time.deltaTime * moveDanping);
        //                      └ 선형 보간 함수(Linear Interpolation)
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        //                      └ 회전 보간 함수(Linear Interpolation)
        tr.LookAt(target.position + (target.up * targetOffset));
    }
}
