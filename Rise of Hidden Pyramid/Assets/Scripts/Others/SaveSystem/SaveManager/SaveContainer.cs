using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveContainer : MonoBehaviour
{
    private static SaveObject saveObject;
    private static int currentSaveIndex;
    public static SaveContainer instance;

    /* INSTRUCTIONS to add data to save
        1. Add parameter to SaveObject
        2. Create SaveX and LoadX methods
        3. Use SaveX or LoadX on whatever class
    */
    private void Awake() 
    {
        instance = this;
        SaveManager.Initialise();
        Load();
    }
    
    public static void Save()
    {
        string json = JsonUtility.ToJson(saveObject);
        SaveManager.Save(json, currentSaveIndex);
    }

    public static void Load()
    {
        string saveString = SaveManager.Load(currentSaveIndex);
        if (saveString != null)
        {
            saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        }
        else
        {
            // throw new NotFoundException("No save file found with that index.");
            saveObject = new SaveObject();
        } 
    }

    /* #region Parameter Saves and Loads */
    public static void SaveName(string name)
    {
        saveObject.playerName = name;
        Save();
    }

    public static string LoadName()
    {
        Load();
        return saveObject.playerName;
    }
    /* #endregion */

    /* #region Getters Setters */
    public static int GetCurrentSaveIndex()
    {
        return currentSaveIndex;
    }
    public static int[] GetSaveIndexes()
    {
        return SaveManager.GetSaveIndexes();
    }
    public static void SetCurrentSaveIndex(int index)
    {
        currentSaveIndex = index;
    }
    /* #endregion */
    private class SaveObject
    {
        public string playerName;
    }
}
