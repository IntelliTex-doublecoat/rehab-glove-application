using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    public Image fillImage;  // 圆形填充的Image组件

    public float Progress
    {
        set { fillImage.fillAmount = value; }
    }
}
