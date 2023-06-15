using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IdataPersistence> dataPersistencesObjects;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {

        if (this.gameData == null) {
            NewGame();
        }

        foreach (IdataPersistence dataPersistence in dataPersistencesObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach(IdataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IdataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IdataPersistence> dataPersistenceObjects = 
            FindObjectsOfType<MonoBehaviour>().OfType<IdataPersistence>();

        return new List<IdataPersistence>(dataPersistenceObjects);
    }

}
