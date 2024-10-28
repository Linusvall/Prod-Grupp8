using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logger 
{
    private static readonly Func<Logger> GetInstance = () =>
    {
        instance ??= new Logger();
        return instance;

    };

    private static Logger instance;

    readonly static string FileName = "gamelog.txt";
    private StreamWriter writer; 



    private Logger()
    {
        //writer = File.AppendText(MakeFilePath());
    }

    ~Logger()
    {
        writer?.Close(); 
    }
   

    public static void Log(string log)
    {
        string AdjusetLogText = DateTime.Now.ToString() + ": " + log;
        var file = File.AppendText(MakeFilePath());
        file.WriteLineAsync(AdjusetLogText);
        file.Close();
    }

    public static void LogBuffered()
    {

    }




    private static string MakeFilePath()
    {
        string path = Path.Combine("Assets/Resources/", FileName);
        return path;
    }

}
