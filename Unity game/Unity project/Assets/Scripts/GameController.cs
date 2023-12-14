using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image moveImg;           // 手势动作图片
    public Image rabImg;           // 兔子动作图片
    public Image arrow;           // 箭头图片


    public Sprite[] moveSeq;  // move图片序列
    public Sprite[] rabSeq;  // rab动作图片序列
    public int currentSeqNum = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeImage()
    {
        currentSeqNum++;
        moveImg.sprite = moveSeq[currentSeqNum];
        rabImg.sprite = rabSeq[currentSeqNum];
    }
    public void HideImage()
    {
        moveImg.gameObject.SetActive(false);
        rabImg.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
    }
}
