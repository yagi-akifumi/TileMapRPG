using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [Header("会話イベント判定用")]
    public bool isTalking;         // true の場合は会話イベント中であるように扱う

    private DialogController dialogController;　　　　// DialogController スクリプトの情報を代入するための変数

    private Vector3 defaultPos;

    private Vector3 offsetPos;


    ////*  ここから変数を追加  *////


    private EventData.EventType eventType = EventData.EventType.Talk;　　　// NPC とのイベントは会話イベントとして設定

    [SerializeField, Header("NPC 会話イベントの通し番号")]
    private int npcTalkEventNo;　　　　　　　　　　　　　　　　　　　　　　// この番号と上記の EventType を使って、スクリプタブル・オブジェクト内から会話イベントのデータを取得します

    [SerializeField, Header("NPC 会話イベントのデータ")]
    private EventData eventData;


    ////*  ここまで  *////


    void Start()
    {
        // 子オブジェクトにアタッチされている DialogController スクリプトを取得して変数に代入
        dialogController = GetComponentInChildren<DialogController>();

        defaultPos = dialogController.transform.position;

        offsetPos = new Vector3(dialogController.transform.position.x, dialogController.transform.position.y - 5.0f, dialogController.transform.position.z);


        ////*  ここから処理を追加  *////


        // DataBaseManager に登録してあるスクリプタブル・オブジェクトを検索し、指定した通し番号の EventData を NPC 用の EventData として取得して代入
        eventData = DataBaseManager.instance.GetEventDataFromNPCEvent(npcTalkEventNo);


        ////*  ここまで  *////


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


        ////*  ここから処理を修正  *////


        // 会話イベントのウインドウを表示する
        dialogController.DisplayDialog(eventData);    //  <=  引数を追加します


        ////*  ここまで  *////


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
    }
}
