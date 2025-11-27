using System;
using UnityEngine;

public class GameBoardScript : MonoBehaviour
{
    int[] ROW_NUMBERS = { 0, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4 };
    int[] COL_NUMBERS = { 0, 2, 2, 3, 3, 4, 4, 5, 4, 5, 5 };

    public const int CARD_SIDE = 5;
    public const int CARD_SPACING = 2;

    public GameObject card;
    public int numberOfPairs = 10;

    private GameObject[] cards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cards = new GameObject[numberOfPairs*2];

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
                Debug.Log(cardsDone);
                if (cardsDone == numberOfPairs * 2)
                {
                    break;
                }

                cards[cardsDone] = Instantiate(
                    card, 
                    topRight - calcOffset(row, collum), 
                    transform.rotation
                    );

                cardsDone++;
            }
        }
    }

    Vector3 calcOffset(int row,  int col)
    {
        return new Vector3
            (
            col * (CARD_SIDE + CARD_SPACING),
            0,
            row * (CARD_SIDE + CARD_SPACING)
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
