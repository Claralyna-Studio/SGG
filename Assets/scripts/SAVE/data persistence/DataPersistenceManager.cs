using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
public class DataPersistenceManager : MonoBehaviour
{
    [Header("debugging")]
    [SerializeField] private bool initializeDataIfNull = false;


    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than 1 Data Persistence Manager in this scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //TO DO - Load any saved data from a file using the data handler
        //this.gameData = dataHandler.Load();
        this.gameData = dataHandler.LoadDate();

        //start a new game if the data is null and we're configure to initialize data for debugging purposes
        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        //if no file was found, don't continue
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be loaded.");
            return;
        }

        //TO DO - push the Loaded data to all scripts that needs it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Loaded money = " + gameData.money + " crystal = " + gameData.crystal + " trayMax = " + gameData.trayMax + "\n" + gameData.pausedDate.PausedDate.ToString());
/*        for (int i = 0; i < gameData.waktu.Count; i++)a
        {
            Debug.Log($"\n{gameData.waktu[i].ToString()}");
        }*/
    }
    public void SaveGame()
    {
        //if we don't have any data to save
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be saved.");
            return;
        }

        //TO DO - pass the data to the other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        //TO DO - save that data to a file using the data handler
        //this.gameData.pausedDate.PausedDate = DateTime.Now;
        Debug.Log("Saved money = " + gameData.money + " crystal = " + gameData.crystal + " trayMax = " + gameData.trayMax + "\n" + gameData.pausedDate.PausedDate.ToString());
/*        for (int i = 0; i < gameData.waktu.Count; i++)
        {
            Debug.Log($"\n{gameData.waktu[i].ToString()}");
        }*/
        //dataHandler.Save(gameData);

        //modded
        dataHandler.SaveDate(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
