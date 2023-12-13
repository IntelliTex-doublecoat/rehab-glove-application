using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    public Image fillImage; 

    public float Progress
    {
        set { fillImage.fillAmount = value; }
    }
}
