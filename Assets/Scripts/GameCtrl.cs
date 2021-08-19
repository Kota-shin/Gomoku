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

    //現在のプレイヤー（初期プレイヤーは白）
    private int currentPlayer = WHITE;

    //カメラ情報
    private Camera camera_object;
    private RaycastHit hit;

    //プレファブ
    public GameObject whiteStone;
    public GameObject blackStone;

    void Start()
    {
        //カメラ情報を取得
        camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();
        //配列を初期化
        InitializeArray();

        //デバッグ用メソッド
        DebugArray();
    }

    
    void Update()
    {
        //マウスがクリックされたら
        if (Input.GetMouseButtonDown(0))
        {
            //マウスのポジションを取得してRayに代入
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition);

            //マウスのポジションからRayを投げて何かにあたったらhitにいれる　
            if(Physics.Raycast(ray,out hit))
            {
                //x、zの値を取得
                int x = (int)hit.collider.gameObject.transform.position.x;
                int z = (int)hit.collider.gameObject.transform.position.z;

                //マスが空のとき
                if(squares[z,x] == EMPTY)
                {
                    //白のターンのとき
                    if(currentPlayer == WHITE)
                    {
                        //Squaresの値を更新
                        squares[z, x] = WHITE;

                        //Stoneを出力
                        GameObject stone = Instantiate(whiteStone);
                        stone.transform.position = hit.collider.gameObject.transform.position;

                        //Playerを交代
                        currentPlayer = BLACK;
                    }
                    //黒のターンのとき
                    else if(currentPlayer == BLACK)
                    {
                        //Squaresの値を更新
                        squares[z, x] = BLACK;

                        //Stoneを出力
                        GameObject stone = Instantiate(blackStone);
                        stone.transform.position = hit.collider.gameObject.transform.position;

                        //Playerを交代
                        currentPlayer = WHITE;
                    }
                }
            }
        }
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
