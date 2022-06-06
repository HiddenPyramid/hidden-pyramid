using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDeactivator : MonoBehaviour
{
    public GameObject dummy;
    
    private void Start() {
        dummy.SetActive(false);
    }
}
