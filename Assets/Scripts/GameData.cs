using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameData : MonoBehaviour
{
    public static GameData instance;        // シングルトンデザインパターンのクラスにするための変数
    public int randomEncountRate;           // エンカウントの発生率
    public bool isEncouting;                // エンカウントしている状態かどうかの判定用。true の場合エンカウントしている状態
    public bool isDebug;                    // デバッグ用の変数。true ならば、エンカウントしている状態をリセットできる

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}