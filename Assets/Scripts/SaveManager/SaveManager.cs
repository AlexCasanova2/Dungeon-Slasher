using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData activeSave;

    public bool hasLoaded;

    public bool exists;


    private void Awake()
    {
        instance = this;
        Load();
        UpdateUI();

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            DeleteSavedData();
        }   
    }

    //Save the data
    public void Save()
    {
        //Ruta donde se almacenara la info
        string dataPath = Application.persistentDataPath;
        //Serializamos la clase SaveData en formato XML
        var serializer = new XmlSerializer(typeof(SaveData));
        //Creamos el archivo de guardado con la ruta
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Saved");
    }

    //Load the data
    public void Load()
    {
        //Ruta donde se almacena la info
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Loaded");

            hasLoaded = true;
        }
    }

    //Delete data
    public void DeleteSavedData()
    {
        //Ruta donde se almacena la info
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }
    }

    public void UpdateUI()
    {
        //Ruta donde se almacena la info
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            exists = true;
        }
        else
        {
            exists = false;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;
    public Vector3 respawnPosition;
    public bool doorOpen, destructedTiles;
    public int health, potions, gold, damage;
}
