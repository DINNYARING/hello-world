using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    float h = 0f, v = 0f, r = 0f;
    public float moveSpeed = 20f;
    public float turnSpeed = 80f;

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (h * Vector3.right) + (v * Vector3.forward);
        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);   //noramlized = ¡§±‘»≠
        transform.Rotate(Vector3.up * r * Time.deltaTime * turnSpeed);
    }
}
