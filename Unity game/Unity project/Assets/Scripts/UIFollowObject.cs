using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowObject : MonoBehaviour
{
    public Transform targetObject;   // 要跟随的物体
    public Vector3 offset = new Vector3(0, 1, 0);  // UI元素相对于物体的偏移

    private RectTransform uiTransform;

    void Start()
    {
        uiTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetObject != null)
        {
            // 计算UI元素的目标位置
            Vector3 targetPosition = targetObject.position + offset;
            
            // 将3D世界坐标转换为屏幕空间坐标
            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPosition);
            
            // 更新UI元素的位置
            uiTransform.position = screenPos;
        }
    }
}
