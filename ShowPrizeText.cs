using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPrizeText : MonoBehaviour
{  
    //second prize and first prize have limitation
    private int secondPrizeCount=0;
    private int firstPrizeCount=0;
    private string prizeText = "";

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {      
    }

    public void showPrize()
    {        
        int prizeRan = Random.Range(1, 20);

        switch (GameManager.instance.IsWin)
        {
            case 0:
                break;
            //small prize
            case 31:
                if (prizeRan <= 10)
                    prizeText = "搥背券一次";                    
                else if (prizeRan > 14)
                    prizeText = "笑話券一張";
                else
                    prizeText = "洗碗券一次";
                break;
            //small punishment
            case 32:
                if (prizeRan <= 2)
                    prizeText = "倒垃圾";
                else if (prizeRan > 10)
                    prizeText = "收拾";
                else
                    prizeText = "按摩";  
                break;
            //second prize
            case 21:
                if (secondPrizeCount < 10)
                {
                    if (prizeRan <= 10)
                        prizeText = "好食券1000元";
                    else
                        prizeText = "甜點券一張";
                    secondPrizeCount += 1;
                }
                else
                    prizeText = "很抱歉，獎品已全數換光";
                break;
            //big punishment
            case 22:
                if (prizeRan <= 7)
                    prizeText = "掃地";
                else if (prizeRan > 8)
                    prizeText = "掃廁所";
                else
                    prizeText = "恭喜，中了低於1%的終極懲罰!";    
                break;
            //first prize
            case 1:
                if (firstPrizeCount < 2)
                {
                    prizeText = "今年是五倍之五倍券 5000円";                    
                    firstPrizeCount += 1;
                }
                else
                    prizeText = "很抱歉，獎品已全數換光";                
                break;            
        }
        //print the text out
        GetComponent<Text>().text = prizeText;
    }
}
