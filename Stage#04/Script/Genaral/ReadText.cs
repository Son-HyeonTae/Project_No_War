using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadText : Singleton<ReadText>
{
    private int Counter = 0;

    public string[] Read(string fileLocation)
    {
        List<string> lines = new List<string>();
        
        foreach(string line in File.ReadLines(fileLocation))
        {
            lines.Add(line);
            Counter++;
        }

        return lines.ToArray();
    }
}
