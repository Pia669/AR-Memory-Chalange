using System;
using Unity.VisualScripting;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DetectObject(UnityEngine.InputSystem.InputAction action)
    {
        Ray ray = mainCamera.ScreenPointToRay(action.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) 
        {
            bool wasTurned = false;
            if (hit.collider != null)
            {
                Debug.Log("Hit:" + hit.collider.tag);
                wasTurned = hit.collider.GetComponent<CardScript>().flipCard();
            }

            if (wasTurned)
            {

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
}
