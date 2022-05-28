using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameSaver : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;

    private void Start()
    {
        LoadNameIfPossible();
        input.onEndEdit.AddListener(SaveName);
    }

    private void SaveName(string name) {
        SaveContainer.SaveName(name);
    }

    private void LoadNameIfPossible()
    {
        Debug.Log("prova");
        string name = SaveContainer.LoadName();
        if (name != null && name.Length != 0) input.text = name;
        Debug.Log("resultat: "+name);
    }
}  
