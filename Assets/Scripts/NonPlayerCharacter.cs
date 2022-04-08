using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [Header("会話イベント判定用")]
    public bool isTalking;         // true の場合は会話イベント中であるように扱う

    [Header("TalkWindow の使用許可")]
    public bool isFixedTalkWindowUsing;    // インスペクターより確認できるように public 修飾子とする。確認後は private に変更可能

    private UIManager uiManager;　　　　　 // UIManager スクリプトの情報を代入するための変数

    private DialogController dialogController;　　　　// DialogController スクリプトの情報を代入するための変数

    private Vector3 defaultPos;

    private Vector3 offsetPos;

    private EventData.EventType eventType = EventData.EventType.Talk;   // NPC とのイベントは会話イベントとして設定


    ////*  ここから変数の宣言を追加する  *////


    private PlayerController playerController;


    ////*  ここまで  *////


    [SerializeField, Header("NPC 会話イベントの通し番号")]
    private int npcTalkEventNo;　　　　　　　　　　　　　　　　　　　　　　// この番号と上記の EventType を使って、スクリプタブル・オブジェクト内から会話イベントのデータを取得します

    [SerializeField, Header("NPC 会話イベントのデータ")]
    private EventData eventData;


    void Start()
    {
        // 子オブジェクトにアタッチされている DialogController スクリプトを取得して変数に代入
        dialogController = GetComponentInChildren<DialogController>();

        defaultPos = dialogController.transform.position;

        offsetPos = new Vector3(dialogController.transform.position.x, dialogController.transform.position.y - 5.0f, dialogController.transform.position.z);

        // DataBaseManager に登録してあるスクリプタブル・オブジェクトを検索し、指定した通し番号の EventData を NPC 用の EventData として取得して代入
        eventData = DataBaseManager.instance.GetEventDataFromNPCEvent(npcTalkEventNo);
    }


    ////*  ここから処理を追加する  *////


    /// <summary>
    /// 会話開始
    /// </summary>
    /// <param name="playerPos"></param>
    /// <param name="playerController"></param>
    public void PlayTalk(Vector3 playerPos, PlayerController playerController)
    {     //  <=  ☆　第2引数を追加する

        if (this.playerController == null)
        {
            this.playerController = playerController;
        }


        ////*  ここまで  *////


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

        // 設定されている会話ウインドウの種類に合わせて開く会話ウインドウを分岐
        if (isFixedTalkWindowUsing)
        {


            ////*  ここから処理を修正する  *////


            // 固定型の会話ウインドウを表示する
            uiManager.OpenTalkWindow(eventData, this);  //　<=　第2引数を追加します。


            ////*  ここまで  *////


        }
        else
        {


            ////*  ここから処理を修正する  *////


            // 稼働型の会話イベントのウインドウを表示する
            StartCoroutine(dialogController.DisplayDialog(eventData, this));  //　<=　呼び出し方法を変更し、第2引数を追加します。


            ////*  ここまで  *////


        }
    }

    /// <summary>
    /// 会話終了
    /// </summary>
    public void StopTalk()
    {

        // 会話イベントをしていない状態にする
        isTalking = false;

        // 設定されている会話ウインドウの種類に合わせて会話イベントのウインドウを閉じる
        if (isFixedTalkWindowUsing)
        {
            uiManager.CloseTalkWindow();
        }
        else
        {
            dialogController.HideDialog();
        }


        ////*  ここから処理を追加する  *////


        // 会話終了状態にする
        playerController.IsTalking = false;


        ////*  ここまで  *////


    }

    /// <summary>
    /// 固定型会話ウインドウを利用するための設定
    /// </summary>
    /// <param name="uiManager"></param>
    public void SetUpFixedTalkWindow(UIManager uiManager)
    {
        this.uiManager = uiManager;

        isFixedTalkWindowUsing = true;
    }
}