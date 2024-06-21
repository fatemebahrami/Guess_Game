using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource[] A;
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI Scores;
    private int NumScores;
    public TextMeshProUGUI Timer;
    private float NumTimer = 5;
    public List<Image> Imgs;
    private Color Choise;

    //New
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;

    private Color[] colors = new Color[] {
        Color.green,
        Color.blue,
        Color.white,
        Color.red
    };
    void Start()
    {
        RandorColor();
    }

    void Update()
    {
        if (NumTimer < 0)
        {
            Timer.color = Color.red;
            Scores.color = Color.red;
            Timer.text = "Game";
            Scores.text = "Over";

            Invoke("Restart",2);
            return;
        }
        else if(NumScores >= 30)
        {
            Timer.color = Color.green;
            Scores.color = Color.green;
            Timer.text = "You";
            Scores.text = "Win";

            Invoke("Restart", 10);
            return;
        }
        else
        {
            Scores.text = "Score: " + NumScores.ToString();

            NumTimer -= Time.deltaTime;
        }

        Timer.text ="Timer: " + NumTimer.ToString("F2");

        //Windows
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            Selecting(Imgs[0]);

            RandorColor();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selecting(Imgs[1]);

            RandorColor();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Selecting(Imgs[2]);

            RandorColor();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Selecting(Imgs[3]);

            RandorColor();
        }

        //Android
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }

        //Exit
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    private void DetectSwipe()
    {
        Vector2 swipeVector = endTouchPosition - startTouchPosition;

        if (swipeVector.magnitude >= swipeThreshold)
        {
            float x = swipeVector.x;
            float y = swipeVector.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0)
                {
                    Selecting(Imgs[3]);

                    RandorColor();
                }
                else
                {
                    Selecting(Imgs[2]);

                    RandorColor();
                }
            }
            else
            {
                if (y > 0)
                {
                    Selecting(Imgs[1]);

                    RandorColor();
                }
                else
                {
                    Selecting(Imgs[0]);

                    RandorColor();
                }
            }
        }
    }
    private void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void RandorColor()
    {
        List<Color> colorList = new List<Color>(colors);
        ShuffleList(colorList);

        for (int i = 0; i < Imgs.Count; i++)
        {
            Imgs[i].color = colorList[i];
        }

        RandomText();
    }

    void RandomText()
    {
        int R = Random.Range(0, 5);

        if (R == 1)
        {
            textMeshProUGUI.text = "Green";
            Choise = Color.green;
        }

        else if (R == 2)
        {
            textMeshProUGUI.text = "Blue";
            Choise = Color.blue;
        }

        else if (R == 3)
        {
            textMeshProUGUI.text = "White";
            Choise = Color.white;
        }

        else if (R == 4)
        {
            textMeshProUGUI.text = "Red";
            Choise = Color.red;
        }
    }
    void ShuffleList(List<Color> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Color temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    public void Selecting(Image img)
    {
        if (img.color == Choise)
        {
            NumScores++;

            A[0].Play();

            if (NumScores <= 5)
            {
                NumTimer = 5;
            }
            else if (NumScores > 5 && NumScores < 10)
            {
                NumTimer = 4;
            }
            else if (NumScores >= 10 && NumScores < 15)
            {
                NumTimer = 3;
            }
            else if (NumScores >= 15 && NumScores < 20)
            {
                NumTimer = 2;
            }
            else if (NumScores >= 20 && NumScores < 30)
            {
                NumTimer = 1.5f;
            }
        }
        else
        {
            A[1].Play();
            if (NumScores > 0) NumScores--;
            NumTimer--;
        }
    }
}
