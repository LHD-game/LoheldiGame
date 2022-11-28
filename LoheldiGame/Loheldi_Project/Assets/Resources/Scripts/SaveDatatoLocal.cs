using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveDatatoLocal : MonoBehaviour
{
    [System.Serializable]
    public class LocalData //로컬에 저장될 데이터 모음입니다.
    {
        public static float LocalBGMVolume = 0.5f;
        public static float LocalSEVolume = 0.5f;
    }

    public LocalData localData;
    string GameDataFileName = ".json";

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadLocalData()
    {
        string FilePath = Application.persistentDataPath + GameDataFileName;
        if (File.Exists(FilePath))
        {
            Debug.Log("Loading LocalData");
            string FromJsonData = File.ReadAllText(FilePath);
            localData = JsonUtility.FromJson<LocalData>(FromJsonData);
        }
        else
        {
            Debug.Log("New LocalData");
            localData = new LocalData();
        }
    }
    public void SaveLocalData()
    {
        string ToJsonData = JsonUtility.ToJson(localData);
        string FilePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(FilePath, ToJsonData);
        Debug.Log("Save LocalData");
    }
    private void OnApplicationQuit()
    {
        SaveLocalData();
    }
}
