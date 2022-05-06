using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject door;

    public int correctInputs = 0;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyDoor()
    {
        Debug.Log("Puzzle Solved!");
        Destroy(door);
    }

    public void correct()
    {
        correctInputs += 1;

        if (correctInputs == 2)
        {
            DestroyDoor();
        }
    }

    public void incorrect()
    {
        if (correctInputs > 0)
            correctInputs -= 1;
    }


}
