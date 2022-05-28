using System;
using System.IO;
using UnityEngine;

public class SaveManager
{
    public static readonly string SAVE_FOLDER = IsMobile() ? Application.persistentDataPath + "/Saves" : Application.dataPath + "/Saves";
    //public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves";

    public static void Initialise()
    {
        if (!Directory.Exists(SAVE_FOLDER))
            Directory.CreateDirectory(SAVE_FOLDER);
    }

    public static void Save(string saveString, int saveNumber)
    {
        File.WriteAllText(SAVE_FOLDER + "/save" + saveNumber + ".txt", saveString);
    }

    public static string Load(int saveNumber)
    {
        if (File.Exists(SAVE_FOLDER + "/save" + saveNumber + ".txt"))
            return File.ReadAllText(SAVE_FOLDER + "/save" + saveNumber + ".txt");
        return null;
    }

    private static bool IsMobile()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }

    /* #region GetSaveIndexes */
    public static int[] GetSaveIndexes()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        int[] indexes = new int[saveFiles.Length];
        int i = 0;
        foreach(FileInfo f in saveFiles)
        {
            indexes[i] = GetSaveIndex(f.ToString());
            i++;
        }
        return indexes;
    }

    private static int GetSaveIndex(string fileName)
    {
        // saveX.txt
        // 0123_XXXX
        int result; Int32.TryParse(fileName.Substring(4, 1), out result);
        return result;
    }
    /* #endregion */
}
