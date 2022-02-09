using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    [SerializeField] private GameObject buttonUnpressed;
    [SerializeField] private GameObject buttonPressed;
    //win effect
    [SerializeField] private GameObject winLine0;
    [SerializeField] private GameObject winLine1;
    [SerializeField] private GameObject winLine2;
    [SerializeField] private GameObject ray;
    [SerializeField] private GameObject firstPrize;
    [SerializeField] private GameObject secondPrize;
    //UI screen
    [SerializeField] private GameObject UIscene;
    [SerializeField] private GameObject prizeScreen;
    [SerializeField] private GameObject punishScreen;
    [SerializeField] private GameObject firstPrizeTag;
    [SerializeField] private GameObject secondPrizeTag;
    [SerializeField] private GameObject smallPrizeTag;
    [SerializeField] private GameObject punishTag;
    //special screen
    [SerializeField] private GameObject specialScene;
    [SerializeField] private GameObject hint1Text;
    [SerializeField] private GameObject hint2Text;
    [SerializeField] private GameObject hint3Text;
    private int hintCount = 0;
    //special image scene
    [SerializeField] private GameObject specialImage;
    [SerializeField] private GameObject firstParagraphText;
    [SerializeField] private GameObject secondParagraphText;
    [SerializeField] private GameObject finalText;
    [SerializeField] private GameObject psText;
    [SerializeField] private GameObject backButton;

    [SerializeField] private GameObject gameScene;

    public static GameManager instance = null;
    
    //the parameter is to control which line is about to stop
    private int isStart = 0;
    public int IsStart { get { return isStart; } }
    //the parameter is to control what prize the player wins
    private int isWin = 0;
    public int IsWin { get { return isWin; } }

    //the parameter is to check if there is a 4 in a row
    private int prize = 0;
    //the parameter is to check if there are more than 6 same icon on the screen
    private int totalIcon1 = 0;
    private int totalIcon2 = 0;
    //the parameter is to control the prize order: second prize/punishment> small prize/punishment
    private bool isStraight = false;
    //the parameter is to control the prize order: first prize> prize> punishment
    private int isPrize = 0;
    
    private int[,] iconArray = new int[,] { { 1, 2, 2, 3, 1, 1, 3, 2, 1, 2 }, { 1, 2, 1, 3, 2, 1, 3, 3, 1, 2 },
        { 1, 2, 1, 2, 3, 1, 3, 2, 1, 2 },{ 3, 2, 1, 1, 2, 2, 1, 2, 3, 2 } };
    private int[,] finalArray = new int[3, 4];

    private int count = 0;
    public Text monthText;
    public Text dateText;

    //check if the GameManager exsists
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //check if the serializefields have object or not
        Assert.IsNotNull(buttonUnpressed);
        Assert.IsNotNull(buttonPressed);
        Assert.IsNotNull(winLine0);
        Assert.IsNotNull(winLine1);
        Assert.IsNotNull(winLine2);
        Assert.IsNotNull(ray);
        Assert.IsNotNull(firstPrize);
        Assert.IsNotNull(secondPrize);
        Assert.IsNotNull(UIscene);
        Assert.IsNotNull(firstPrizeTag);
        Assert.IsNotNull(secondPrizeTag);
        Assert.IsNotNull(smallPrizeTag);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //win prize
        if (isStart == 5&&MoveIcon.isFinish==true)
        {
            putInArray(0, MoveIcon.line1Pos);
            putInArray(1, MoveIcon.line2Pos);
            putInArray(2, MoveIcon.line3Pos);
            putInArray(3, MoveIcon.line4Pos);
            score();
            isStart = 0;            
        }
    }

    //start to run
    public void startButton()   
    {        
        totalIcon1 = 0;
        totalIcon2 = 0;
        isStraight = false;        

        buttonUnpressed.SetActive(false);
        buttonPressed.SetActive(true);
        winLine0.SetActive(false);
        winLine1.SetActive(false);
        winLine2.SetActive(false);
        ray.SetActive(false);
        firstPrize.SetActive(false);
        secondPrize.SetActive(false);

        /*count += 1;                  
        if (count == 10)
        {
            specialScene.SetActive(true);
            gameScene.SetActive(false);
        }
        else
        {*/
            isStart = 1;
            StartCoroutine(delayFunc());
        //}
    }

    //back to the main screen
    public void homeButton()
    {
        firstPrizeTag.SetActive(false);
        secondPrizeTag.SetActive(false);
        smallPrizeTag.SetActive(false);
        punishTag.SetActive(false);
        UIscene.SetActive(false);
        prizeScreen.SetActive(false);
        punishScreen.SetActive(false);

        gameScene.SetActive(true);
        buttonUnpressed.SetActive(true);
        buttonPressed.SetActive(false);

        isWin = 0;
        isPrize = 0;
    }

    public void checkButton()
    {
        monthText= monthText.GetComponent<Text>();
        dateText= dateText.GetComponent<Text>();
        if (monthText.text == "10" && dateText.text == "10")
        {
            specialScene.SetActive(false);
            StartCoroutine(showSpecialImage());
        }
        else if(hintCount<3)
        {
            switch (hintCount)
            {
                case 0:
                    hint1Text.SetActive(true);
                    break;
                case 1:
                    hint1Text.SetActive(false);
                    hint2Text.SetActive(true);
                    break;
                case 2:
                    hint2Text.SetActive(false);
                    hint3Text.SetActive(true);
                    break;
            }
            hintCount += 1;
            
        }
    }

    public void returnButton()
    {
        specialImage.SetActive(false);
        buttonUnpressed.SetActive(true);
        buttonPressed.SetActive(false);
        gameScene.SetActive(true);
    }

    //stop running
    IEnumerator delayFunc()
    {
        //wait 1~2 second before stop the line
        int delayTime1 = Random.Range(1, 2);
        yield return new WaitForSeconds(delayTime1);
        isStart += 1;

        int delayTime2 = Random.Range(1, 2);
        yield return new WaitForSeconds(delayTime2);
        isStart += 1;

        int delayTime3 = Random.Range(1, 2);
        yield return new WaitForSeconds(delayTime3);
        isStart += 1;

        int delayTime4 = Random.Range(1, 2);
        yield return new WaitForSeconds(delayTime4);
        isStart += 1;        
    }

    //put the result into an array
    void putInArray(int row,int linePos)
    {
        for (int i = 2; i >= 0; i--)
        {
            finalArray[i, row] = iconArray[row, linePos];
            linePos += 1;
        }
    }

    //to check if the player wins or not
    void score()
    {

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //check if it is a 4 in a row
                if (j == 0)
                    prize = finalArray[i, 0];
                else if (finalArray[i, j] != prize)
                    prize = 0;                

                //count the amount for each icon
                switch (finalArray[i, j])
                {
                    case 1:
                        totalIcon1 += 1;
                        break;
                    case 2:
                        totalIcon2 += 1;
                        break;
                    case 3:
                        break;
                }
            }
           

            if (prize != 0)
            {
                //which line get win
                switch (i)
                {
                    case 0:
                        winLine0.SetActive(true);
                        break;
                    case 1:
                        winLine1.SetActive(true);
                        break;
                    case 2:
                        winLine2.SetActive(true);
                        break;
                }

                //which prize the player win,following the order
                if (prize == 3)
                {
                    Invoke("showMegaWin", 0.5f);
                    isPrize = 2;
                    isWin = 1;
                }
                else if (prize == 2)
                {
                    Invoke("showSecondPrize", 0.5f);
                    if(isPrize==0)
                        isWin = 22;
                }
                else if (prize == 1)
                {
                    Invoke("showSecondPrize", 0.5f);
                    if (isPrize < 2)
                    {
                        isPrize = 1;
                        isWin = 21;
                    }
                }
                isStraight = true;
            }            
        }

        //check the small prize
        if (isStraight == false)
        {
            if (totalIcon1 >= 6)
            {
                ray.SetActive(true);
                isWin = 31;
            }
            else if (totalIcon2 >= 6)
            {
                ray.SetActive(true);
                isWin = 32;
            }
        }

        //if the player win
        if (isWin != 0)
        {
            Invoke("showUIscreen", 1);
        }
        else
        {
            buttonUnpressed.SetActive(true);
            buttonPressed.SetActive(false);
        }               
    }

    void showMegaWin()
    {
        firstPrize.SetActive(true);
    }

    void showSecondPrize()
    {
        secondPrize.SetActive(true);
    }

    void showUIscreen()
    {
        gameScene.SetActive(false);
        UIscene.SetActive(true);

        switch (isWin)
        {
            case 1:
                prizeScreen.SetActive(true);
                firstPrizeTag.SetActive(true);
                break;
            case 21:
                prizeScreen.SetActive(true);
                secondPrizeTag.SetActive(true);
                break;
            case 22:
                punishScreen.SetActive(true);
                punishTag.SetActive(true);
                break;
            case 31:
                prizeScreen.SetActive(true);
                smallPrizeTag.SetActive(true);
                break;
            case 32:
                punishScreen.SetActive(true);
                punishTag.SetActive(true);
                break;
        }

        GameObject.Find("prizeText").GetComponent<ShowPrizeText>().showPrize();
    }

    IEnumerator showSpecialImage()
    {
        specialImage.SetActive(true);
        yield return new WaitForSeconds(2);
        firstParagraphText.SetActive(true);
        yield return new WaitForSeconds(4);
        secondParagraphText.SetActive(true);
        yield return new WaitForSeconds(4);
        finalText.SetActive(true);
        yield return new WaitForSeconds(3);
        psText.SetActive(true);
        yield return new WaitForSeconds(2);
        backButton.SetActive(true);
    }
}
