using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/**
* 스크립트 내 파일 입출력을 위해 작성됨
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/

public class ReadText : Singleton<ReadText>
{
    private int Counter = 0;


    /**
    * 외부 경로의 텍스트 파일을 읽는 함수
    * 
    * @param        string fileLocation - 읽어들일 파일의 경로
    * @return       읽어들인 파일을 줄바다 배열로 저장 및 return
    * @Exception
    */
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
