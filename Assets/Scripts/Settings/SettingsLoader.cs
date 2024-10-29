




using System;
using System.IO;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{

    [SerializeField]
    SettingsObject defaultSettings;

    SettingsObject loadedSettings;

    readonly string FileName = "settings.json";
    public static Func<SettingsLoader> GetInstance = () => null;


    private void Awake()
    {
        if(GetInstance() != null)
        {
            Destroy(gameObject);
            return;
        }

        GetInstance = () => this; 

        // checks that we have a settings file
        if (!File.Exists(MakeFilePath()))
        {
            if(defaultSettings == null)
            {
                defaultSettings = new ();
            }
            WriteFile(JsonUtility.ToJson(defaultSettings));
        }
        string content = GetJsonContent(MakeFilePath());
        loadedSettings = new();
        JsonUtility.FromJsonOverwrite(content, loadedSettings);
        DontDestroyOnLoad(this.gameObject);
        Logger.Log("Test");
        Logger.Log("Test2");

    }

    public SettingsObject GetSettings()
    {
        if (loadedSettings == null)
        {
            return defaultSettings;
        }
        return loadedSettings;
    }


    private string MakeFilePath()
    {
        string path = Path.Combine("Assets/Resources/", FileName);

        return path; 
    }

    private void WriteFile(string content)
    {
        StreamWriter writer = new (MakeFilePath(), false);
        writer.Write(content);
        writer.Close();
    }

    private string GetJsonContent(string path)
    {
        StreamReader reader = new (path);
        string returnString = reader.ReadToEnd();
        reader.Close();
        Debug.Log("Test");
        return returnString;   
    }

}
