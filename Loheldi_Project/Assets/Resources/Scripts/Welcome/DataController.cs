using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get 
        {
            return _container; 
        }
    }

    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }
    public string gameDataFileName = "LocalSaveData.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + gameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");
            _gameData = new GameData();
        }
    }
    
    public void SaveGameData()
    {
        string ToJsonData=JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + gameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장완료");
    }


    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}