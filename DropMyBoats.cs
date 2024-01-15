using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMyBoats : MonoBehaviour
{
    public int x;
    public int y;

    public bool isOccupied = false;
    public bool isPermanentlyOccupied = false;
    public bool isShooted = false;

    public GameObject occupiedImage;
    public GameObject shootedImage;
    public GameObject missImage;

    public static int occupiedCount = 0;

    public static int changeA = 0;

    public static int rememberChange = 0;
    public static int maxPermanentlyOccupied = 23;
    public static int countPermanentlyOccupied = 0;
    public static bool startShoot = false;

    private Clicks clicksScript; // Referencja do skryptu Clicks



    private void Awake()
    {
        if (isOccupied)
        {
            occupiedCount++;
        }
    }

    // Inicjalizacja referencji do skryptu Clicks
    private void Start()
    {
        clicksScript = FindObjectOfType<Clicks>();
    }

    private void Update()
    {
        if (changeA != rememberChange && isOccupied && !isPermanentlyOccupied)
        {
            SetPermanentlyOccupied(true);
            rememberChange += 1;
            countPermanentlyOccupied += 1;
            if (countPermanentlyOccupied == maxPermanentlyOccupied)
            {
                Debug.Log("Udało się!!!");
                startShoot = true;
            }
        }

        if (!Clicks.playerTurn && startShoot && Clicks.xShoot == x && Clicks.yShoot == y && !isShooted)
        {
            isShooted = true;
            Debug.Log("Strzelono w x: " + Clicks.xShoot);
            Debug.Log("Strzelono w y: " + Clicks.yShoot);
            if (isOccupied)
            {
                // Trafiono statek
                occupiedImage.SetActive(false);
                shootedImage.SetActive(true);
                Clicks.shootEnemyAgain = true;
            }
            else
            {
                // Pudło
                missImage.SetActive(true);
                Clicks.playerTurn = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MyShip") && !isPermanentlyOccupied)
        {
            SetOccupied(true);
            DisplayImage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MyShip") && !isPermanentlyOccupied)
        {
            SetOccupied(false);
            HideImage();
        }
    }

    public void SetOccupied(bool occupied)
    {
        if (isOccupied != occupied)
        {
            isOccupied = occupied;
            if (occupied)
            {
                occupiedCount++;
            }
            else
            {
                occupiedCount--;
            }
        }
    }

    public void SetPermanentlyOccupied(bool permanentlyOccupied)
    {
        isPermanentlyOccupied = permanentlyOccupied;
    }

    void DisplayImage()
    {
        if (occupiedImage != null)
        {
            occupiedImage.SetActive(true);
        }
    }

    void HideImage()
    {
        if (occupiedImage != null)
        {
            occupiedImage.SetActive(false);
        }
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }
}
