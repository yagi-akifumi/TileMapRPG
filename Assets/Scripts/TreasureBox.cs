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


    ////*  ここから処理を修正  *////


    //void Start() {
    //    SetUpTresureBox();　　　//　<=　外部のクラスより呼び出すように変更するので、Start メソッドでは実行しないようにする
    //}


    ////*  ここまで  *////


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


        ////*  ここから処理を修正  *////


        //isOpen = true;　　　　　　　//　<=　削除します

        SwitchStateTresureBox(true);


        ////*  ここまで  *////


        if (playerPos.y < transform.position.y)
        {
            dialogController.transform.position = offsetPos;
        }
        else
        {
            dialogController.transform.position = defaultPos;
        }

        // 探索イベント用の会話ウインドウを開く
        dialogController.DisplaySearchDialog(eventData, this);
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


    ////*  ここからメソッドを２つ追加  *////


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

            // ① 宝箱の画像を開封済にする場合
            spriteRenderer.sprite = eventData.eventSprite;

            // ② 宝箱自体を非表示にする場合
            this.gameObject.SetActive(false);
        }
    }


    ////*  ここまで  *////


}