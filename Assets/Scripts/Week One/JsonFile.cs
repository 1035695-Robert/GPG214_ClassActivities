using System.IO;
using UnityEngine;

public class JsonFile : MonoBehaviour
{ 
    public PlayerStats stats = new PlayerStats();

    public string playerJsonFileName = "Player.json";
    public string  folderPath = Application.streamingAssetsPath;
    private string fullFilePath = string.Empty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullFilePath = Path.Combine(folderPath, playerJsonFileName);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveJson();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadJson();
        }
    }
    void SaveJson()
    {
        //turn it into a jsonData 
        string jsonData = JsonUtility.ToJson(stats);

        File.WriteAllText(fullFilePath, jsonData);
    }
    void LoadJson()
    {
        if (File.Exists(fullFilePath))
        {
            string jsonData = File.ReadAllText(fullFilePath);

            stats = JsonUtility.FromJson<PlayerStats>(jsonData);

            if(stats != null) 
            {
                Debug.LogError("json Found, but cant convert to class");
                transform.position = stats.ReturnPlayerPosition();
            }
        }
        else
        {
            Debug.LogError("no player Found");
        }
    }
}
