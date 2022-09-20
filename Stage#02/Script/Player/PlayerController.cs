using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject stampAnimation;
    public  GameObject docSpawner;
    public  GameObject textRemains;
    private Animator   animator;

    [SerializeField]
    public int  count = 80;
    public bool isRed = false;

    void Update()
    {
        /**
         * Scene을 별도로 관리하는 클래스를 만들어
         * 해당 부분 주석 처리 후 새로 적용하였음
         * !해당 주석은 확인 후 제거 가능
         * 
         * 변경 사항 : 
         * 
         *   if (count <= 0) {
         *      textRemains.GetComponent<RemainText>().GameClear();
         *   }
         * 
         * -->Stage2ClearFlag.cs에서 확인 가능
         * 
         * 주요 사항 : GameManager.cs, CutSceneManager.cs에서 확인
         * 
         * 최종 수정일 : 2022-08-31::01:18
         * 최종 수정자 : 살메
         */


        if (Input.GetMouseButtonDown(0)) {
            Instantiate(stampAnimation, new Vector3(0, 0, 0), Quaternion.identity);
            isRed = false;
            count--;
            docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
        }
        else if (Input.GetMouseButtonDown(1)) {
            Instantiate(stampAnimation, new Vector3(0, 0, 0), Quaternion.identity);
            isRed = true;
            count--;
            docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();    
        }
    }
}