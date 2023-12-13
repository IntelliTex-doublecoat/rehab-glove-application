using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    private bool jumping = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float jumpStartTime;
    private float jumpDuration = 2f;
    // private float jumpHeight = 2f;
    private float rotationAmount = 360f;

      private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        // 检查触发器碰撞对象是否是胡萝卜
        if (other.CompareTag("carrot"))
        {
            // 销毁胡萝卜对象
            Destroy(other.gameObject);
        }
    }
}