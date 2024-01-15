using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boats : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    public int count;
    public static int prevOccupied = 0;
    private int Answer;
    public bool isPermamentlySet = false;

    private DropMyBoats[] dropMyBoats;

    void Start()
    {
        dropMyBoats = GetComponentsInChildren<DropMyBoats>();
    }

    void OnMouseDown()
    {
        if(isPermamentlySet == false)
        {
            offset = gameObject.transform.position - GetInputWorldPosition();
            isDragging = true;
            initialRotation = transform.rotation;
            initialPosition = transform.position;
        }   
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (IsShipPlacementValid())
        {
            PlaceShipOnBoard();
            DropMyBoats.changeA += count;
            isPermamentlySet = true;
        }
        else if(isPermamentlySet == false)
        {
            // Jeœli umieszczenie jest nieprawid³owe, wróæ do poprzedniej pozycji
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 targetPosition = GetInputWorldPosition() + offset;
            transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);
        }

        if (isDragging && Input.GetMouseButtonDown(1))
        {
            isDragging = false;
            RotateBy90Degrees();
        }

        
    }

    private void RotateBy90Degrees()
    {
        Vector3 currentPosition = transform.position;
        transform.Rotate(new Vector3(0, 0, 90));
        transform.position = currentPosition;
    }

    private Vector3 GetInputWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    bool IsShipPlacementValid()
    {
        Answer = DropMyBoats.occupiedCount - prevOccupied;
        prevOccupied += count;

        bool allFieldsAvailable = true;
        foreach (var drop in dropMyBoats)
        {
            if (drop.isPermanentlyOccupied)
            {
                allFieldsAvailable = false;
                //prevOccupied -= count;
                break;
            }
        }

        bool check = count == Answer && allFieldsAvailable;

        if(check == false)
        {
            prevOccupied -= count;
        }
        return check;
    }

    void PlaceShipOnBoard()
    {
        foreach (var drop in dropMyBoats)
        {
            drop.SetOccupied(true);
            drop.SetPermanentlyOccupied(true); // Oznacz pole jako trwale zajête
        }
    }
}