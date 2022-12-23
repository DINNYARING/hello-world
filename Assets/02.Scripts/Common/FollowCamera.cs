using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    private Transform tr;
    public float Height = 4.0f;
    public float Distance = 5.0f;
    public float moveDanping = 15f;     // �̵� ��� : ī�޶� ��鸲�� �ε巴�� ���ֱ� ���� ���
    public float rotateDamping = 10f;   // ȸ�� ���
    public float targetOffset = 2.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()   // �� ī�޶� �÷��̾�� �ڴʰ� ����;� �ϱ� ������ ó���� �����Ǿ�� �Ѵ�. (�÷��̾��� ������ ���� ��ȣ�ۿ�)
    {   //      �� ī�޶� ��ġ�� Ÿ�� ��, ���� ��ġ
        var camPos = target.position - (target.forward * Distance) + (target.up * Height);
        // �� var ��� Vector3�� ���� �� �ִ�. (var�� �˾Ƽ� Vector3�� ���� �޴´�.) 
        tr.position = Vector3.Lerp(tr.position, camPos, Time.deltaTime * moveDanping);
        //                      �� ���� ���� �Լ�(Linear Interpolation)
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        //                      �� ȸ�� ���� �Լ�(Linear Interpolation)
        tr.LookAt(target.position + (target.up * targetOffset));
    }
}
