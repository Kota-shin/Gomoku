using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //ターン表示の定型文
    //private const string whiteTurn = "白のターン";
    //private const string blackTurn = "黒のターン";

    //プレファブ
    public GameObject whiteStone;
    public GameObject blackStone;

    //ターンテキスト
    public Text turn;

    void Start()
    {
        //ターン表示テキスト
        turn.text = "白のターン";

        //カメラ情報を取得
        camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();
        //配列を初期化
        InitializeArray();

        //デバッグ用メソッド
        DebugArray();
    }

    
    void Update()
    {
        //碁石がそろっているか確認
        if(CheckStone(WHITE) || CheckStone(BLACK))
        {
            return;
        }

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

                        //turnを交代
                        turn.text = "黒のターン";
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

                        //turnを交代
                        turn.text = "白のターン";
                    }
                }
            }
        }
    }

    //配列情報を初期化する
    private void InitializeArray()
    {
        //for文を利用して配列にアクセスする
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //配列を空（値を０）にする
                squares[i, j] = EMPTY;
            }
        }
    }

    //配列情報を確認する（デバッグ用）
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

    //private void InitializeArray()
    //{
    //    for(int i = 0; i< 10; i++)
    //    {
    //        for(int j =0; j < 10; j++)
    //        {
    //            squares[i, j] = EMPTY;
    //        }
    //    }
    //}

    //private void DebugArray()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        for (int j = 0; j < 10; j++)
    //        {
    //            Debug.Log("(i,j) = (" + i + "," + j + ") = " + squares[i, j]);
    //        }
    //    }
    //}

    private bool CheckStone(int color)
    {
        //碁石の数をカウントする
        int count = 0;

        //横向き
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //squaresの値が空のとき
                if (squares[i, j] == EMPTY || squares[i, j] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    //countにsquaresの値を格納する
                    count++;
                }

                //countの値が5になったとき
                if (count == 5)
                {
                    //白のとき
                    if (color == WHITE)
                    {
                        //勝利テキスト
                        turn.text = "白の勝ち！！！";
                    }
                    //黒のとき
                    else
                    {
                        //勝利テキスト
                        turn.text = "黒の勝ち！！！";
                    }

                    return true;
                }
            }
        }

        //countの値を初期化
        count = 0;

        //縦向き
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //squaresの値が空のとき
                if (squares[j, i] == EMPTY || squares[j, i] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    //countにsquaresの値を格納する
                    count++;
                }

                //countの値が5になったとき
                if (count == 5)
                {
                    //白のとき
                    if (color == WHITE)
                    {
                        //勝利テキスト
                        turn.text = "白の勝ち！！！";
                    }
                    //黒のとき
                    else
                    {
                        //勝利テキスト
                        turn.text = "黒の勝ち！！！";
                    }

                    return true;
                }
            }
        }

        //countの値を初期化
        count = 0;

        //斜め(右上がり)
        for (int i = 0; i < 10; i++)
        {
            //上移動用
            int up = 0;

            for (int j = i; j < 10; j++)
            {
                //squaresの値が空のとき
                if (squares[j, up] == EMPTY || squares[j, up] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    count++;
                }

                //countの値が5になったとき
                if (count == 5)
                {
                    //白のとき
                    if (color == WHITE)
                    {
                        //勝利テキスト
                        turn.text = "白の勝ち！！！";
                    }
                    //黒のとき
                    else
                    {
                        //勝利テキスト
                        turn.text = "黒の勝ち！！！";
                    }

                    return true;
                }

                up++;
            }
        }

        //countの値を初期化
        count = 0;

        //斜め(右下がり)
        for (int i = 0; i < 10; i++)
        {
            //下移動用
            int down = 9;

            for (int j = i; j < 10; j++)
            {
                //squaresの値が空のとき
                if (squares[j, down] == EMPTY || squares[j, down] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    count++;
                }

                //countの値が5になったとき
                if (count == 5)
                {
                    //白のとき
                    if (color == WHITE)
                    {
                        //勝利テキスト
                        turn.text = "白の勝ち！！！";
                    }
                    //黒のとき
                    else
                    {
                        //勝利テキスト
                        turn.text = "黒の勝ち！！！";
                    }

                    return true;
                }

                down--;
            }
        }

        //まだ判定がついていないとき
        return false;
    }

    //５こ連続で碁石が置かれているか確認する
    //private bool CheckStone(int color)
    //{
    //    //碁石の数をカウント
    //    int count = 0;

    //    //横向き
    //    for (int i = 0; i < 10; i++)
    //    {
    //        for(int j = 0; j < 10; j++)
    //        {
    //            //squaresの値が空のとき
    //            if(squares[i,j] == EMPTY || squares[i,j] != color)
    //            {
    //                //countを初期化する
    //                count = 0;
    //            }
    //            else
    //            {
    //                //countにsquaresの値を格納する
    //                count++;
    //            }

    //            //countの値が５になった時
    //            if (count == 5)
    //            {

    //                //白の時
    //                if (color == WHITE)
    //                {
    //                    Debug.Log("白の勝ち！！！");
    //                }
    //                //黒の時
    //                else
    //                {
    //                    Debug.Log("黒の勝ち！！！");
    //                }
    //                return true;
    //            }
    //        }
    //    }
    //    //countの値を初期化
    //    count = 0;

    //    //縦向き
    //    for (int i = 0; i < 10; i++)
    //    {
    //        for (int j = 0; j < 10; j++)
    //        {
    //            //squaresの値が空のとき
    //            if (squares[j, i] == EMPTY || squares[j, i] != color)
    //            {
    //                //countを初期化する
    //                count = 0;
    //            }
    //            else
    //            {
    //                //countにsquaresの値を格納する
    //                count++;
    //            }

    //            //countの値が５になった時
    //            if (count == 5)
    //            {

    //                //白の時
    //                if (color == WHITE)
    //                {
    //                    Debug.Log("白の勝ち！！！");
    //                }
    //                //黒の時
    //                else
    //                {
    //                    Debug.Log("黒の勝ち！！！");
    //                }
    //                return true;
    //            }
    //        }
    //    }
    //    //countの値を初期化
    //    count = 0;

    //    斜め（右上がり）
    //    for (int i = 0; i < 10; i++)
    //    {
    //        //上に移動用
    //        int up = 0;

    //        for (int j = i; j < 10; j++)
    //        {
    //            //squaresの値が空のとき
    //            if (squares[j, up] == EMPTY || squares[j, up] != color)
    //            {
    //                //countを初期化する
    //                count = 0;
    //            }
    //            else
    //            {
    //                //countにsquaresの値を格納する
    //                count++;
    //            }

    //            //countの値が５になった時
    //            if (count == 5)
    //            {

    //                //白の時
    //                if (color == WHITE)
    //                {
    //                    Debug.Log("白の勝ち！！！");
    //                }
    //                //黒の時
    //                else
    //                {
    //                    Debug.Log("黒の勝ち！！！");
    //                }
    //                return true;
    //            }
    //            up++;
    //        }
    //    }
    //    //countの値を初期化
    //    count = 0;

    //    //斜め（右下がり）
    //    for (int i = 0; i < 10; i++)
    //    {
    //        //下に移動用
    //        int down = 9;

    //        for (int j = i; j < 10; j++)
    //        {
    //            //squaresの値が空のとき
    //            if (squares[j, down] == EMPTY || squares[j, down] != color)
    //            {
    //                //countを初期化する
    //                count = 0;
    //            }
    //            else
    //            {
    //                //countにsquaresの値を格納する
    //                count++;
    //            }

    //            //countの値が５になった時
    //            if (count == 5)
    //            {

    //                //白の時
    //                if (color == WHITE)
    //                {
    //                    Debug.Log("白の勝ち！！！");
    //                }
    //                //黒の時
    //                else
    //                {
    //                    Debug.Log("黒の勝ち！！！");
    //                }
    //                return true;
    //            }
    //            down--;
    //        }
    //    }
    //    //まだ判定がついていないとき
    //    return false;
    //}
}
