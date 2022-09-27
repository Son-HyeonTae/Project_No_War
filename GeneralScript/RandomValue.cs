using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* %비율로 참/거짓을 반환하는 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class RandomValue : Singleton<RandomValue>
{
    private bool Success = false;


    /**
    * %비율로 참/거짓을 반환하는 함수
    * 
    * @param float Percent
    * @return bool Success
    * @최종 수정일 - 2022-08-25::15:14
    */
    public bool Random(float Percent)
    {
        if(Percent < 0.0001f)
        {
            Percent = 0.0001f;
        }

        Percent /= 100;

        int RandAccuracy = 10000000;
        float RandHitRange = Percent * RandAccuracy;
        int Rand = UnityEngine.Random.Range(1, RandAccuracy + 1);
        if (Rand <= RandHitRange)
        {
            Success = true;
        }
        return Success;
    }
}
