using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIcon : MonoBehaviour
{
    [SerializeField] private GameObject Line1;
    [SerializeField] private GameObject Line2;
    [SerializeField] private GameObject Line3;
    [SerializeField] private GameObject Line4;
    [SerializeField] private GameObject Line5;
    [SerializeField] private GameObject Line6;
    [SerializeField] private GameObject Line7;
    [SerializeField] private GameObject Line8;

    //parameter of the movement
    private float objectSpeed = 0.8f;
    private float resetPosition = -16.0f;
    private float startPosition=9.6f;

    //use to count where the line is
    private int line1Count = 0;
    private int line2Count = 0;
    private int line3Count = 0;
    private int line4Count = 0;

    //use to count which icon is on the screen
    static public bool isFinish = false;
    static public int line1Pos = 1;
    static public int line2Pos = 1;
    static public int line3Pos = 1;
    static public int line4Pos = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //move the line seperately
        switch (GameManager.instance.IsStart)
        {
            case 0:                
                isFinish = false;
                break;
            case 1:
                move(Line1);                              
                move(Line2);
                move(Line3);
                move(Line4);
                move(Line5);
                move(Line6);
                move(Line7);
                move(Line8);
                line1Count += 1;
                line2Count += 1;
                line3Count += 1;
                line4Count += 1;
                break;
            case 2:
                move(Line2);
                move(Line3);
                move(Line4);
                move(Line6);
                move(Line7);
                move(Line8);
                finalMove(Line1, line1Count);
                line1Count = finalMove(Line5, line1Count);
                line1Pos += line1Count / 2 ;
                line1Pos %= 8;
                line1Count = 0;
                line2Count += 1;
                line3Count += 1;
                line4Count += 1;
                break;
            case 3:
                move(Line3);
                move(Line4);
                move(Line7);
                move(Line8);
                finalMove(Line2, line2Count);
                line2Count = finalMove(Line6, line2Count);
                line2Pos += line2Count / 2 ;
                line2Pos %= 8;
                line2Count = 0;
                line3Count += 1;
                line4Count += 1;
                break;
            case 4:
                move(Line4);
                move(Line8);
                finalMove(Line3, line3Count);
                line3Count = finalMove(Line7, line3Count);
                line3Pos += line3Count / 2 ;
                line3Pos %= 8;
                line3Count = 0;
                line4Count += 1;
                break;
            case 5:
                finalMove(Line4, line4Count);
                line4Count = finalMove(Line8, line4Count);
                line4Pos += line4Count / 2 % 8;
                line4Pos %= 8;
                line4Count = 0;
                isFinish = true;
                break;
        }
    }

    //move the line
    void move(GameObject runLine)
    {
        runLine.transform.Translate(Vector3.down * objectSpeed, Space.World);

        if (runLine.transform.localPosition.y <= resetPosition)
        {        
            Vector3 newPos = new Vector3(runLine.transform.position.x, startPosition, runLine.transform.position.z);                
            runLine.transform.position = newPos;            
        }
    }

    //stop the line at right place
    int finalMove(GameObject runLine,int lineCount)
    {
        if (lineCount % 2 != 0)
        {
            move(runLine);
            lineCount += 1;
        }
        return lineCount;
    }
}
