using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{ 
    public GameObject wallPrefub;
    public GameObject doorPrefub;
    public Transform labirintTran;

    public float wallWidth;
    public float wallHeight;
    void Awake()
    {
        
        Maze maze = new Maze(10, 10);
        int rn = Random.Range(0, 10);
        for (int i = 0; i < maze.cells.Length; i++)
        {
            for (int j = 0; j < maze.cells[i].Length; j++)
            {
                if (maze.cells[i][j].rightBorder)
                {
                    Vector3 pos = new Vector3((j+0.5f) * wallWidth, wallHeight/2, i * wallWidth);
                    Quaternion rot = Quaternion.LookRotation(Vector3.right);
                    GameObject prefub = Instantiate(wallPrefub, pos, rot);
                    prefub.name = "wall " + "right " + i + " " + j;
                    prefub.gameObject.transform.SetParent(labirintTran);
                }
                if (maze.cells[i][j].bottomBorder)
                {
                    Vector3 pos = new Vector3(j * wallWidth, wallHeight / 2, (i+0.5f) * wallWidth);
                    Quaternion rot = Quaternion.LookRotation(Vector3.back);
                    GameObject prefub = Instantiate(wallPrefub, pos, rot);
                    prefub.name = "wall " + "back " + i + " " + j;
                    prefub.gameObject.transform.SetParent(labirintTran);
                }
                if (maze.cells[i][j].leftBorder)
                {
                    Vector3 pos = new Vector3((j-0.5f) * wallWidth, wallHeight / 2, i * wallWidth);
                    Quaternion rot = Quaternion.LookRotation(Vector3.left);
                    GameObject prefub = Instantiate(wallPrefub, pos, rot);
                    prefub.name = "wall " + "left " + i + " " + j;
                    prefub.gameObject.transform.SetParent(labirintTran);
                }
                if (maze.cells[i][j].topBorder)
                {
                    Vector3 pos = new Vector3(j * wallWidth, wallHeight / 2, (i-0.5f) * wallWidth);
                    Quaternion rot = Quaternion.LookRotation(Vector3.forward);
                    GameObject prefub = Instantiate(
                       (i == 0 && j == rn) ? doorPrefub : wallPrefub, pos, rot);
                    prefub.name = "wall " + "forward " + i + " " + j;
                    prefub.gameObject.transform.SetParent(labirintTran);
                }
            }
        }


    }
}
