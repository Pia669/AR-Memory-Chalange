using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

public class GameBoardScript : MonoBehaviour
{
    readonly int[] ROW_NUMBERS = { 0, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4};
    readonly int[] COL_NUMBERS = { 0, 2, 2, 3, 3, 4, 4, 5, 4, 5, 5, 6, 6};

    public const int CARD_SIDE = 4;
    public const int CARD_SPACING = 1;

    public GameObject card;
    public int numberOfPairs = 10;

    private CardControl cardControl;
    private GameObject[] cards;

    private int score = 0;
    private CardScript firstCard = null;
    private CardScript secondCard = null;

    private bool isChecking = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI endGameText;
    public GameObject endScreen;
    public int gameTime = 60; 
    private Coroutine timerCoroutine;
    public Transform parent;

    private void Awake()
    {
        cardControl = new CardControl();
    }

    private void OnEnable()
    {
        cardControl.Enable();
    }

    private void OnDisable()
    {
        cardControl.Disable();
    }

    IEnumerator CountdownTimer()
    {
        int remainingTime = gameTime;

        while (remainingTime > 0)
        {
            timerText.text = "Time:\n" + remainingTime +"s";
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        timerText.text = "Time:\n0s";

        
        if (score == numberOfPairs) 
        {
            endGameText.text = "YOU WIN";
            endScreen.SetActive(true);
        }
        else
        {
            endGameText.text = "YOU LOSE";
            endScreen.SetActive(true);
        }

        this.enabled = false;
    }

    public bool DetectObject(Ray ray)
    {
        if (isChecking) return false;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) 
        {
            CardScript clicked = hit.collider.GetComponent<CardScript>();


            if (clicked == null) return false;

            // Tá istá karta druhý krát
            if (clicked == firstCard) return false;
            
            // Už je otočená
            if (!clicked.flipCard()) return false;

            if (firstCard == null)
            {
                firstCard = clicked;
                return false;
            }
            else
            {
                secondCard = clicked;
                CheckMatch();
            }
            
        }
        return true;
    }

    public void ReadyBoard()
    {
        numberOfPairs = GameSettings.Instance.GetNumberOfPairs();
        SpawnCards();
        AssingCardsPairs();

        if (GameSettings.Instance.gameMode == GameMode.Singleplayer)
        {
            timerCoroutine = StartCoroutine(CountdownTimer());
        }
    }

    void AssingCardsPairs()
    {
        int[] pairs = new int[numberOfPairs];
        for (int i = 0; i < numberOfPairs; i++)
        {
            pairs[i] = 2;
        }

        for (int c = 0;c < cards.Length; c++)
        {
            int pairNumber = UnityEngine.Random.Range(0, numberOfPairs);
            while (pairs[pairNumber] == 0)
            {
                pairNumber = UnityEngine.Random.Range(0, numberOfPairs);
            }
            pairs[pairNumber]-=1;
            cards[c].GetComponent<CardScript>().assassingPair(pairNumber);
        }
    }

    void SpawnCards()
    {
        cards = new GameObject[numberOfPairs * 2];

        Vector3 topRight = Vector3.zero - new Vector3(
            -((COL_NUMBERS[numberOfPairs] - 1) * (CARD_SIDE + CARD_SPACING)) / 2,
            0,
            -((ROW_NUMBERS[numberOfPairs] - 1) * (CARD_SIDE + CARD_SPACING)) / 2
            );

        int cardsDone = 0;

        for (int row = 0; row < ROW_NUMBERS[numberOfPairs]; row++)
        {
            for (int collum = 0; collum < COL_NUMBERS[numberOfPairs]; collum++)
            {
                
                if (cardsDone == numberOfPairs * 2)
                {
                    break;
                }

                cards[cardsDone] = Instantiate(
                    card,
                    topRight - CalcOffset(row, collum),
                    transform.rotation,
                    parent
                    );
            
                cardsDone++;
            }
        }
    }

    Vector3 CalcOffset(int row, int col)
    {
        return new Vector3
            (
            col * (CARD_SIDE + CARD_SPACING),
            0,
            row * (CARD_SIDE + CARD_SPACING)
            );
    }

    void CheckMatch()
    {
        if (firstCard.pairNumber == secondCard.pairNumber)
        {
            score++;
            UpdateScoreUI();

            if (score == numberOfPairs)
            {
                endGameText.text = "YOU WIN";
                StopCoroutine(timerCoroutine);
                this.enabled = false;
                endScreen.SetActive(true);
            }
        }

        StartCoroutine(DealayedCardReturn());
    }

    IEnumerator DealayedCardReturn()
    {
        isChecking = true;

        //Debug.Log("starting");
        yield return new WaitForSeconds(2);
        //Debug.Log("ending");
        firstCard.returnCard();
        secondCard.returnCard();

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
