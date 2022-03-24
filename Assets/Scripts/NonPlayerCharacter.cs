using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [Header("会話イベント判定用")]
    public bool isTalking;         // true の場合は会話イベント中であるように扱う

    private DialogController dialogController;　　　// DialogController スクリプトの情報を代入するための変数

    private Vector3 defaultPos;

    private Vector3 offsetPos;

    void Start()
    {
        // 子オブジェクトにアタッチされている DialogController スクリプトを取得して変数に代入
        dialogController = GetComponentInChildren<DialogController>();

        defaultPos = dialogController.transform.position;
        offsetPos = new Vector3(defaultPos.x, defaultPos.y - 5.0f, defaultPos.z);
    }

    /// <summary>
    /// 会話開始
    /// </summary>
    public void PlayTalk(Vector3 playerPos)
    {

        // 会話イベントを行っている状態にする
        isTalking = true;

        // プレイヤーの位置を確認してウインドウを出す位置を決定する

        if (playerPos.y < transform.position.y)
        {
            dialogController.transform.position = offsetPos;
        }
        else
        {
            dialogController.transform.position = defaultPos;
        }

       

        // 会話イベントのウインドウを表示する
        dialogController.DisplayDialog();
        //Debug.Log("会話ウインドウを開く");

    }

    /// <summary>
    /// 会話終了
    /// </summary>
    public void StopTalk()
    {

        // 会話イベントをしていない状態にする
        isTalking = false;

        // 会話イベントのウインドウを閉じる

        dialogController.HideDialog();
        //Debug.Log("会話ウインドウを閉じる");
    }
}