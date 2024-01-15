using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicks : MonoBehaviour
{
    public GameObject shipObject; // Obiekt reprezentujący pole z polem statku
    public GameObject emptyObject; // Obiekt reprezentujący puste pole

    public int x1;
    public int y1;
    private int x;
    private int y;

    public EnemyBoats enemyBoats; // Referencja do skryptu EnemyBoats.cs
    public static bool playerTurn = true;

    public static int xShoot = -1;
    public static int yShoot = -1;

    private List<Vector2Int> shotPositions = new List<Vector2Int>();
    private List<int> allPositions = new List<int>();
    private System.Random random = new System.Random();

    public static bool shootEnemyAgain = false;

    private bool[,] playerShots = new bool[10, 10]; // Tablica do śledzenia strzałów gracza


    public static int playerHits = 0;
    public static int enemyHits = 0; // Liczba trafień przeciwnika

  //  public static bool playerTurnActive = true;

    private void Start()
    {
        // Znajdź skrypt EnemyBoats w scenie i zapisz referencję do niego
        enemyBoats = FindObjectOfType<EnemyBoats>();

        // Na początek gry dezaktywuj obiekt reprezentujący pole z polem statku
        shipObject.SetActive(false);
        emptyObject.SetActive(false);

        // Inicjalizuj listę wszystkich pozycji od 0 do 99
        for (int i = 0; i < 100; i++)
        {
            allPositions.Add(i);
        }
    }

    private void OnMouseDown()
    {
        if (DropMyBoats.startShoot && !EnemyBoats.end)
        {
            if (playerTurn || !shootEnemyAgain)
            {
                int clickedX = x1; // Pobierz współrzędną X pola, na które gracz kliknął
                int clickedY = y1; // Pobierz współrzędną Y pola, na które gracz kliknął

                if (!playerShots[clickedX, clickedY])
                {
                    if (IsShipCell())
                    {
                        // Jeśli na polu jest statek
                        shipObject.SetActive(true); // Aktywuj obiekt statku
                        emptyObject.SetActive(false); // Dezaktywuj obiekt pustego pola
                        playerHits++;
                    }
                    else
                    {
                        // Jeśli na polu nie ma statku
                        shipObject.SetActive(false); // Dezaktywuj obiekt statku
                        emptyObject.SetActive(true); // Aktywuj obiekt pustego pola
                        playerTurn = false;

                        playerShots[clickedX, clickedY] = true; // Oznacz pole jako trafione przez gracza

                        EnemyShoot();
                    }
                }
            }
            else if (shootEnemyAgain)
            {
                EnemyShoot(); // Wywołaj strzał przeciwnika
                enemyHits++;
                shootEnemyAgain = false;
                //playerTurnActive = true;
            }
        }

        
    }

    private void EnemyShoot()
    {
        if (allPositions.Count > 0)
        {
            int randomIndex = random.Next(0, allPositions.Count);
            int position = allPositions[randomIndex];
            xShoot = position / 10; // Oblicz x z pozycji
            yShoot = position % 10; // Oblicz y z pozycji
            allPositions.RemoveAt(randomIndex); // Usuń wylosowaną pozycję z listy
        }
    }

    private bool IsShipCell()
    {
        // Sprawdź, czy enemyBoats zostało zainicjowane
        if (enemyBoats == null)
        {
            Debug.LogError("EnemyBoats reference is not set.");
            return false;
        }

        x = x1;
        y = y1;

        // Dodaj warunek sprawdzający, czy enemyBoats.board zostało zainicjowane
        if (enemyBoats.board != null && x >= 0 && x < enemyBoats.board.GetLength(0) && y >= 0 && y < enemyBoats.board.GetLength(1))
        {
            return enemyBoats.board[x, y] == 1; // Sprawdzamy zawartość planszy
        }
        else
        {
            Debug.LogError("Invalid board coordinates.");
            return false;
        }
    }
}
