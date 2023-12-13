using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image moveImg;           // hand gesture
    public Image rabImg;           // rabbit
    public Image arrow;           // arrow


    public Sprite[] moveSeq;  // move pic sequence
    public Sprite[] rabSeq;  // rab move
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
