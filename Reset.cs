using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject winImage;
    public GameObject loseImage;

    void Update()
    {
        // Sprawdź, czy naciśnięto przycisk myszy
        if (Input.GetMouseButtonDown(0))
        {
            // Pobierz pozycję kliknięcia
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // Sprawdź, czy kliknięty obiekt to obiekt resetu
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Resetuj ustawienia gry
                ResetGame();
                
            }
        }
    }

    void ResetGame()
    {
        // Wyłącz obiekty "win" i "lose"

        // Załaduj ponownie scenę, aby zresetować wszystko
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Clicks.enemyHits = 0;
        Clicks.playerHits = 0;
        winImage.SetActive(false);
        loseImage.SetActive(false);


        Clicks.playerTurn = true;

        Clicks.xShoot = -1;
        Clicks.yShoot = -1;

        Clicks.shootEnemyAgain = false;


        Boats.prevOccupied = 0;


        DropMyBoats.occupiedCount = 0;

        DropMyBoats.changeA = 0;

        DropMyBoats.rememberChange = 0;
        DropMyBoats.maxPermanentlyOccupied = 23;
        DropMyBoats.countPermanentlyOccupied = 0;
        DropMyBoats.startShoot = false;
        EnemyBoats.end = false;
}
}
