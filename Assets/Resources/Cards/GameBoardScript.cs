using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

public class GameBoardScript : MonoBehaviour
{
    int[] ROW_NUMBERS = { 0, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4 };
    int[] COL_NUMBERS = { 0, 2, 2, 3, 3, 4, 4, 5, 4, 5, 5 };

    public const int CARD_SIDE = 5;
    public const int CARD_SPACING = 1;

    public GameObject card;
    public int numberOfPairs = 10;

    private CardControl cardControl;
    private Camera mainCamera;
    private GameObject[] cards;

    private int score = 0;
    private CardScript firstCard = null;
    private CardScript secondCard = null;
    private bool isChecking = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI endGameText; 
    public int gameTime = 60; 
    private Coroutine timerCoroutine;

    private void Awake()
    {
        cardControl = new CardControl();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        cardControl.Enable();
    }

    private void OnDisable()
    {
        cardControl.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardControl.Touch.Tap.started += _ => DetectObject(cardControl.Touch.Position);
        cardControl.Touch.DebugClick.started += _ => DetectObject(cardControl.Touch.DebugPos);
        SpawnCards();
        AssingCardsPairs();

        timerCoroutine = StartCoroutine(CountdownTimer()); 
    }

    IEnumerator CountdownTimer()
    {
        int remainingTime = gameTime;

        while (remainingTime > 0)
        {
            timerText.text = "Time: " + remainingTime +"s";
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        timerText.text = "Time: 0s";

        
        if (score == numberOfPairs) 
        {
            endGameText.text = "YOU WIN";
        }
        else
        {
            endGameText.text = "YOU LOSE";
        }

        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DetectObject(UnityEngine.InputSystem.InputAction action)
    {
        if (isChecking) return;

        Ray ray = mainCamera.ScreenPointToRay(action.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) 
        {
            CardScript clicked = hit.collider.GetComponent<CardScript>();


            if (clicked == null) return;

            // Tá istá karta druhý krát
            if (clicked == firstCard) return;
            
            // Už je otočená
            if (!clicked.flipCard()) return;

            if (firstCard == null)
            {
                firstCard = clicked;
            }
            else
            {
                secondCard = clicked;
                StartCoroutine(CheckMatch());
            }
            
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

        Vector3 topRight = transform.position - new Vector3(
            -(COL_NUMBERS[numberOfPairs] * CARD_SIDE + (COL_NUMBERS[numberOfPairs] - 1) * CARD_SPACING) / 2,
            0,
            -(ROW_NUMBERS[numberOfPairs] * CARD_SIDE + (ROW_NUMBERS[numberOfPairs] - 1) * CARD_SPACING) / 2
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
                    transform.rotation
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

    IEnumerator CheckMatch()
    {
        isChecking = true;

        if (firstCard.pairNumber == secondCard.pairNumber)
        {
            score++;
            UpdateScoreUI();
            if(score == numberOfPairs)
            {
                endGameText.text = "YOU WIN";
                StopCoroutine(timerCoroutine);
                this.enabled = false;
            }
        }
        else
        {
            Debug.Log("starting");
            yield return new WaitForSeconds(1);
            Debug.Log("ending");
            firstCard.returnCard();
            secondCard.returnCard();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
