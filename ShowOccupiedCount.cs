using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOccupiedCount : MonoBehaviour
{
    public int occupiedCount; // Zmienna do wy�wietlenia w inspektorze
    public int change;
    public int rememberChange;
    public bool startShoot;
   // public bool playerTurn;
    private void Update()
    {
        occupiedCount = DropMyBoats.occupiedCount; // Przypisz warto�� zmienn� statyczn�
        // PermamentlyOccupiedCount = DropMyBoats
        change = DropMyBoats.changeA;
        rememberChange = DropMyBoats.rememberChange;
        startShoot = DropMyBoats.startShoot;
       // playerTurn = Clicks.playerTurn;
    }
}
