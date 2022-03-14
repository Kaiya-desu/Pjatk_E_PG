using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    [SerializeField] private GameData data;
    private GameDataToSave actualSaveDataXD;
    private void Awake()
    {
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private GameManager gameManager; 

    //buttons:
    public void Save()
    {
        actualSaveDataXD = data.saveData;

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream stream = new FileStream(Application.dataPath + "/../save.bin", FileMode.Create);
        formatter.Serialize(stream, actualSaveDataXD);
        stream.Close();
    }

    public void Load()
    {
        this.gameManager = GetComponent<GameManager>();

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream stream = new FileStream(Application.dataPath + "/../save.bin", FileMode.Open);

        GameDataToSave tmp = formatter.Deserialize(stream) as GameDataToSave;
        if (tmp != null)
        {
            actualSaveDataXD = tmp;
        }
        stream.Close();

        gameManager.LoadData(actualSaveDataXD);


    }

    BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();
        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        formatter.SurrogateSelector = selector;

        return formatter;
    }

    public void Reset()
    {
        SceneManager.LoadScene("Game");
    }
}
