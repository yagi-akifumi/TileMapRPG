using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    [Header("宝箱イベント判定用")]
    public bool isOpen;

    [SerializeField]
    private DialogController dialogController;

    private Vector3 defaultPos;

    private Vector3 offsetPos;

    private EventData.EventType eventType = EventData.EventType.Search;

    [Header("宝箱イベントの通し番号")]
    public int treasureEventNo;

    [SerializeField, Header("宝箱イベントのデータ")]
    private EventData eventData;

    private SpriteRenderer spriteRenderer;

    private PlayerController playerController;


    /// <summary>
    /// 探索イベントの準備
    /// </summary>
    public void SetUpTresureBox()
    {
        isOpen = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogController = GetComponentInChildren<DialogController>();

        defaultPos = dialogController.transform.position;
        offsetPos = new Vector3(dialogController.transform.position.x, dialogController.transform.position.y - 5.0f, dialogController.transform.position.z);

        // 対象物 の EventData を取得
        eventData = DataBaseManager.instance.GetEventDataFromEvnetTypeAndEventNo(eventType, treasureEventNo);
    }

    /// <summary>
    /// 探索開始
    /// </summary>
    /// <param name="playerPos"></param>
    /// <param name="playerController"></param>
    public void OpenTresureBox(Vector3 playerPos, PlayerController playerController)
    {

        if (this.playerController == null)
        {
            this.playerController = playerController;
        }

        SwitchStateTresureBox(true);

        if (playerPos.y < transform.position.y)
        {
            dialogController.transform.position = offsetPos;
        }
        else
        {
            dialogController.transform.position = defaultPos;
        }


        ////*  ここから処理を修正  *////


        // 探索イベント用の会話ウインドウを開く
        //dialogController.DisplaySearchDialog(eventData, this);　　　//　<=　下記に変更します

        StartCoroutine(dialogController.DisplaySearchDialog(eventData, this));


        ////*  ここまで  *////


    }

    /// <summary>
    /// 探索終了
    /// </summary>
    public void CloseTreasureBox()
    {

        playerController.IsTalking = false;

        // 探索イベント用の会話ウインドウを閉じる
        dialogController.HideDialog();
    }

    /// <summary>
    /// 探索イベントの通し番号の取得
    /// </summary>
    /// <returns></returns>
    public int GetTresureEventNum()
    {
        return treasureEventNo;
    }

    /// <summary>
    /// 探索状態の切り替え(①か②は、いずれかを実装する)
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchStateTresureBox(bool isSwitch)
    {
        isOpen = isSwitch;

        if (isOpen)
        {


            ////*  ここから処理を修正  *////


            // ① 宝箱の画像を開封済にする場合
            spriteRenderer.sprite = eventData.eventDataDetailsList[0].eventSprite; //<= ☆①　右辺の参照する変数を変更します

            // ② 宝箱自体を非表示にする場合
            //this.gameObject.SetActive(false);   　//　<=　☆②　コルーチンを呼び出す処理を OpenTresureBox メソッド内に実装したため、こちらの処理は利用しないようにしてください。


            ////*  ここから処理を修正  *////


        }
    }
}