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


    ////*  ここから修正・追加  *////


    //[SerializeField]
    //private string titleName = "dog";　　　　　//　<=　EventData から情報を受け取りますので不要になります

    //[SerializeField]
    //private string dialog = "ワンワン!";　　 　//　<=　EventData から情報を受け取りますので不要になります

    [SerializeField]　　　　　　　　　　　　　　 //　<=　代入されたか確認できるようにインスペクターに表示する。確認が終了したら、SerializeField属性 は削除してもよいです
    private EventData eventData;             //　<=  NonPlayerCharacter スクリプトから EventData の情報がメソッドの引数を通じて届きますので、それを代入するための変数


    ////*  ここまで  *////


    void Start()
    {
        SetUpDialog();
    }

    /// <summary>
    /// ダイアログの設定
    /// </summary>
    private void SetUpDialog()
    {
        canvasGroup.alpha = 0.0f;


        ////*  ここから修正  *////


        //txtTitleName.text = titleName;　　　//　<=　情報を受け取ってから表示する内容を設定しますので、ここはコメントアウトしてください。

        // EventData を初期化
        eventData = null;


        ////*  ここまで  *////


    }


    ////*  ①～④の処理を修正・追加  *////


    /// <summary>
    /// ダイアログの表示
    /// </summary>
    public void DisplayDialog(EventData eventData)
    {　　　//　①　引数を追加します

        if (this.eventData == null)
        {　　　　　　　　   　//　② if 文による制御を追加します
            this.eventData = eventData;
        }

        canvasGroup.DOFade(1.0f, 0.5f);　　　　　　   　　//　そのまま

        txtTitleName.text = this.eventData.title;　  　 　//　③ Title として表示するタイトル(NPC の名前)の内容を EventData の内容に変更します

        txtDialog.DOText(this.eventData.dialog, 1.0f).SetEase(Ease.Linear);  //　④  Dialog として表示するメッセージの内容を EventData の内容に変更します

        // TODO 画像データがある場合には、Image 型の変数を用意して eventData.eventSprite を代入する


        ////*  ここまで  *////


    }

    /// <summary>
    /// ダイアログの非表示
    /// </summary>
    public void HideDialog()
    {
        canvasGroup.DOFade(0.0f, 0.5f);

        txtDialog.text = "";
    }
}