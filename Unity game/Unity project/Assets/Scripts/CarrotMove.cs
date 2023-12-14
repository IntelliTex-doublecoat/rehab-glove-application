using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMove : MonoBehaviour
{
    public float amplitude = 1.0f;  // 移动的幅度
    public float speed = 1.0f;      // 移动的速度

    private Vector3 startPos;       // 初始位置

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;  // 记录初始位置
    }

    // Update is called once per frame
    void Update()
    {
        // 计算新的Y坐标，基于正弦函数的周期性变化，但只在正值范围内
        float newY = startPos.y + Mathf.Abs(Mathf.Sin(Time.time * speed)) * amplitude;

        // 更新物体的位置
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    
    }
}
