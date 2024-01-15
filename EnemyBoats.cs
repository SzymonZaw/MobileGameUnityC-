using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoats : MonoBehaviour
{
    public int boardSize = 10; // Rozmiar planszy (10x10)
   // public GameObject[] ships; // Tablica prefabrykatów statków (1x5, 1x4, 1x3, 1x2)

    public int[,] board; // Tablica reprezentuj¹ca planszê, gdzie 0 oznacza wolne pole, a 1 oznacza pole zajête przez statek

    public GameObject winImage;
    public GameObject loseImage;

    public static bool end = false;

    private void Start()
    {
        InitializeBoard();
        PlaceShips();
        LogShipCoordinates();
    }

    private void Update()
    {
        if (Clicks.enemyHits == 23)
        {
            end = true;
            loseImage.SetActive(true);
        }
        else if (Clicks.playerHits == 23)
        {
            end = true;
            winImage.SetActive(true);
        }
    }

    void LogShipCoordinates()
    {
        Debug.Log("Wspó³rzêdne zajêtych pól przez statki:");
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i, j] == 1)
                {
                    Debug.Log("Statek na wspó³rzêdnych: X = " + i + ", Y = " + j);
                }
            }
        }
    }

    void InitializeBoard()
    {
        board = new int[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i, j] = 0; // Inicjalizacja planszy jako pustej
            }
        }
    }

    void PlaceShips()
    {
        // Losowe u³o¿enie statków 1x5
        PlaceShip(5);

        // Losowe u³o¿enie statków 1x4 (2 razy)
        PlaceShip(4);
        PlaceShip(4);

        // Losowe u³o¿enie statków 1x3 (2 razy)
        PlaceShip(3);
        PlaceShip(3);

        // Losowe u³o¿enie statków 1x2 (2 razy)
        PlaceShip(2);
        PlaceShip(2);
    }

    void PlaceShip(int length)
    {
        bool shipPlaced = false;

        while (!shipPlaced)
        {
            int x = Random.Range(0, boardSize);
            int y = Random.Range(0, boardSize);
            int direction = Random.Range(0, 2); // 0 oznacza poziomo, 1 oznacza pionowo

            // Sprawdzenie, czy statek mo¿na umieœciæ w wylosowanym miejscu
            if (CanPlaceShip(x, y, direction, length))
            {
                // Umieœæ statek na planszy
                for (int i = 0; i < length; i++)
                {
                    if (direction == 0)
                        board[x + i, y] = 1;
                    else
                        board[x, y + i] = 1;
                }
                shipPlaced = true;
            }
        }
    }

    bool CanPlaceShip(int x, int y, int direction, int length)
    {
        if (direction == 0)
        {
            // SprawdŸ, czy statek mieœci siê w granicach planszy
            if (x + length > boardSize)
                return false;

            // SprawdŸ, czy ¿adne pole nie jest ju¿ zajête przez inny statek
            for (int i = 0; i < length; i++)
            {
                if (board[x + i, y] != 0)
                    return false;

                // SprawdŸ, czy pole i jego otoczenie nie jest zajête przez inny statek
                if ((x + i > 0 && board[x + i - 1, y] != 0) ||
                    (x + i < boardSize - 1 && board[x + i + 1, y] != 0) ||
                    (y > 0 && board[x + i, y - 1] != 0) ||
                    (y < boardSize - 1 && board[x + i, y + 1] != 0))
                {
                    return false;
                }
            }
        }
        else
        {
            // SprawdŸ, czy statek mieœci siê w granicach planszy
            if (y + length > boardSize)
                return false;

            // SprawdŸ, czy ¿adne pole nie jest ju¿ zajête przez inny statek
            for (int i = 0; i < length; i++)
            {
                if (board[x, y + i] != 0)
                    return false;

                // SprawdŸ, czy pole i jego otoczenie nie jest zajête przez inny statek
                if ((x > 0 && board[x - 1, y + i] != 0) ||
                    (x < boardSize - 1 && board[x + 1, y + i] != 0) ||
                    (y + i > 0 && board[x, y + i - 1] != 0) ||
                    (y + i < boardSize - 1 && board[x, y + i + 1] != 0))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
