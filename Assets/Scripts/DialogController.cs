using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private Text txtDialog = null;

    [SerializeField]
    private Text txtTitleName = null;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private EventData eventData;


    ////*  ここから変数を追加  *////


    private bool isTalk;                                 //  会話中である場合 true になる、会話状態を表現する変数

    private NonPlayerCharacter nonPlayerCharacter;

    private float wordSpeed = 0.1f;             // 1文字当たりの表示速度。小さいほど早く表示される


    ////*  ここまで  *////


    void Start()
    {
        SetUpDialog();
    }

    /// <summary>
    /// ダイアログの設定
    /// </summary>
    public void SetUpDialog()
    {
        canvasGroup.alpha = 0.0f;

        // EventData を初期化
        eventData = null;
    }


    ////*  ここから処理を修正・追加  *////


    /// <summary>
    /// ダイアログの表示
    /// </summary>
    public IEnumerator DisplayDialog(EventData eventData, NonPlayerCharacter nonPlayerCharacter)
    {   //  <=  戻り値を修正してコルーチン・メソッドに変更する。また、第2引数を追加する

        if (this.nonPlayerCharacter == null)
        {　　　　　　　　　　　　//　if 文節を３行分追加します
            this.nonPlayerCharacter = nonPlayerCharacter;
        }

        if (this.eventData == null)
        {
            this.eventData = eventData;
        }

        canvasGroup.DOFade(1.0f, 0.5f);

        txtTitleName.text = this.eventData.title;

        // 会話イベント開始
        isTalk = true;　　　　　　　　　　　　　　　　　//　<=　追加します

        // メッセージ表示
        yield return StartCoroutine(PlayTalkEventProgress(this.eventData.dialogs));   //　<=　追加します

        // 会話イベント終了
        isTalk = false;                                 //　<=　追加します

        // Dialog を閉じる
        nonPlayerCharacter.StopTalk();                  //　<=　追加します

        //txtDialog.DOText(this.eventData.dialog, 1.0f).SetEase(Ease.Linear);    //  <=  複数のページになるので、１ページだけ表示の処理はコメントアウトする

        // TODO 画像データがある場合には、Image 型の変数を用意して eventData.eventSprite を代入する


        ////*  ここまで  *////


    }

    /// <summary>
    /// ダイアログの非表示
    /// </summary>
    public void HideDialog()
    {


        ////*  ここから処理を追加  *////


        if (isTalk)
        {
            return;
        }


        ////*  ここまで  *////


        canvasGroup.DOFade(0.0f, 0.5f);

        txtDialog.text = "";
    }

    /// <summary>
    /// 探索対象を獲得
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="treasureBox"></param>
    /// <returns></returns>
    public void DisplaySearchDialog(EventData eventData, TreasureBox treasureBox)
    {

        // 会話ウインドウを表示
        canvasGroup.DOFade(1.0f, 0.5f);

        // タイトルに探索物の名称を表示
        txtTitleName.text = eventData.title;

        // アイテム獲得
        GetEventItems(eventData);

        // 獲得した宝箱の番号を GameData に追加
        GameData.instance.AddSearchEventNum(treasureBox.treasureEventNo);

        // 獲得した宝箱の番号をセーブ
        GameData.instance.SaveSearchEventNum(treasureBox.treasureEventNo);

        // 所持しているアイテムのセーブ
        GameData.instance.SaveItemInventryDatas();

        // TODO お金や経験値のセーブ

    }


    /// <summary>
    /// アイテム獲得
    /// </summary>
    /// <param name="eventData"></param>
    private void GetEventItems(EventData eventData)
    {

        // 獲得したアイテムの名前と数を表示
        txtDialog.text = eventData.eventItemName.ToString() + " × " + eventData.eventItemCount + " 獲得";

        // GameData にデータを登録　=　これがアイテム獲得の実処理
        GameData.instance.AddItemInventryData(eventData.eventItemName, eventData.eventItemCount);
    }


    ////*  ここからメソッドを追加  *////


    /// <summary>
    /// 会話イベントのメッセージ再生とページ送り
    /// </summary>
    /// <param name="dialogs"></param>
    /// <returns></returns>
    private IEnumerator PlayTalkEventProgress(string[] dialogs)
    {

        bool isClick = false;

        // 複数のメッセージを順番に表示
        foreach (string dialog in dialogs)
        {
            Debug.Log(dialog);
            isClick = false;

            // １ページ分の文字を、１文字当たり 0.1 秒ずつかけて等速で表示。表示終了後、isClick を true にして文字が全文表示された状態にする
            txtDialog.DOText(dialog, dialog.Length * wordSpeed).SetEase(Ease.Linear).OnComplete(() => { isClick = true; });

            // １ページの文字が全文表示されている場合かつ、アクションボタンを押すと次のメッセージ表示。それまでは処理を中断して待機する
            yield return new WaitUntil(() => Input.GetButtonDown("Action") && isClick);

            // 次のページのために、現在表示されている文字を消去
            txtDialog.text = "";

            yield return null;
        }
    }


    ////*  ここまで  *////


}