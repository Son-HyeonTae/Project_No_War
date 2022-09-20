using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileSpawner : MonoBehaviour
{
    // 생성할 타일의 prefab
    [SerializeField] private GameObject tilePrefab;

    // txt 파일 읽고 타일 생성.
    private void Awake()
    {
    
        FileStream tileData = new FileStream("Assets/Stage#5/TileData.txt", FileMode.Open);
        StreamReader reader = new StreamReader(tileData);

        string stringLine = reader.ReadLine();
        string [] stringValue;
        Vector2 tilePos;

        while(stringLine != null)
        {
            stringValue = stringLine.Split(" ");
            // 타일 위치 저장.
            tilePos = new Vector2(float.Parse(stringValue[1]), float.Parse(stringValue[2]));
            // 타일 생성
            GameObject instance = Instantiate(tilePrefab, tilePos, Quaternion.identity);
            Tile tile = instance.GetComponent<Tile>();
            // 타일 타입 지정
            if(stringValue[0] == "0") // obstacle
            {
                tile.tileType = Tile.Type.Obstacle;
            }
            else  // bullet
            {
                tile.tileType = Tile.Type.Bullet;
                // 색 빨강으로 변경.
                SpriteRenderer tileRenderer = instance.GetComponent<SpriteRenderer>();
                tileRenderer.color = Color.red;
            }
            Debug.Log(tile.tileType);

            stringLine = reader.ReadLine();

        }

        reader.Close();

    }

}
