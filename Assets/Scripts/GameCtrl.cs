using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    //プライぺート変数
    //10*10のint型2次元配列
    private int[,] squares = new int[10, 10];
    //EMPTY=0、WHITE=0、BLACK=0;
    private const int EMPTY = 0;
    private const int WHITE = 1;
    private const int BLACK = -1;

    void Start()
    {
        //配列を初期化
        InitializeArray();

        //デバッグ用メソッド
        DebugArray();
    }

    
    void Update()
    {
        
    }

    private void InitializeArray()
    {
        for(int i = 0; i< 10; i++)
        {
            for(int j =0; j < 10; j++)
            {
                squares[i, j] = EMPTY;
            }
        }
    }

    private void DebugArray()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Debug.Log("(i,j) = (" + i + "," + j + ") = " + squares[i, j]);
            }
        }
    }
}
