using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{

    private int[] items = new int[3];

    public int objective;

    public int currentItem;

    [SerializeField]
    private GameObject puzzleManager;

    private int counter;



    void Start()
    {
        items[0] = 1;
        items[1] = 2;
        items[2] = 3;

        counter=0;
        currentItem = items[0];

        // objective = Random.Range(1, 3);
        objective = 2;

        // change();
        checkPuzzle();
    }



    public void change()
    {
              
        moveItem();
        checkPuzzle();

    }

    void checkPuzzle()
    {
        if (currentItem == objective)
        {
            puzzleSolved();
        }
        else
        {
            puzzleUnsolved();
        }
    }

    void puzzleSolved()
    {
        // Debug.Log("Palanca guay");
        puzzleManager.GetComponent<PuzzleManager>().correct();
    }

    void puzzleUnsolved()
    {
        puzzleManager.GetComponent<PuzzleManager>().incorrect();
    }

    void moveItem()
    {
        counter++;
        if (counter >= 3)
        {
            counter = 0;
        }


        currentItem = items[counter];
    }

}
