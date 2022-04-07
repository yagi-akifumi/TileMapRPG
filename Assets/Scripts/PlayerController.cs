using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("移動速度")]
    public float moveSpeed;

    private Rigidbody2D rb;                      // コンポーネントの取得用

    private float horizontal;                    // x 軸(水平・横)方向の入力の値の代入用
    private float vertical;                      // y 軸(垂直・縦)方向の入力の値の代入用

    private Animator anim;　　　　　　　　　　　 // コンポーネントの取得用

    private Vector2 lookDirection = new Vector2(0, -1.0f);   // キャラの向きの情報の設定用

    private bool isTalking;                      // 会話イベント中かどうかの判定用。true の場合には会話イベント中

    private EncountManager encountManager;       // EncountManager クラスの情報を代入するための変数 


    ////*  ここから変数の宣言を追加  *////


    [SerializeField]
    private OperationStatusWindow operationStatusWindow = null;                     // アイテムインベントリーウインドウの参照用

    private string[] actionlayerMasks = new string[2] { "NPC", "TresureBox" };　　　// LayerMask の設定

    public bool IsTalking　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 // isTalking のプロパティ
    {
        set
        {
            isTalking = value;
        }

        get
        {
            return isTalking;
        }
    }


    ////*  ここまで  *////


    void Start()
    {

        // このスクリプトがアタッチされているゲームオブジェクトにアタッチされているコンポーネントの中から、<指定>したコンポーネントの情報を取得して、左辺に用意した変数に代入
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        // エンカウント後の場合　=>　初めてゲームを実行した場合には実行されない
        if (GameData.instance.isEncouting)
        {

            // Transform で記録すると、前のシーンの PlayerController を参照しているため、シーン遷移時に PlayerController がなくてエラーで落ちる
            // Vector3 型ならば問題なし。これで、前の位置に戻れる
            transform.position = GameData.instance.GetCurrentPlayerPos();

            // エンカウント発生中の状態を解除して、再度、エンカウント発生できる状態に戻す
            GameData.instance.isEncouting = false;

            // 向いていた向きの状態を取得し、アニメの向きも再現する
            lookDirection = GameData.instance.GetCurrentLookDirection();
            anim.SetFloat("Look X", lookDirection.x);
            anim.SetFloat("Look Y", lookDirection.y);
        }
    }

    void Update()
    {


        ////*  ここから処理を追加  *////

        // TODO ここがうまくいかない

        // ステータス画面表示中は操作できない
        //if (operationStatusWindow.propertyWindow.activeSelf)
        //{
        //return;
        //}


        ////*  ここまで  *////


        // アクション
        Action();

        // 会話中は移動のキー入力受付を行わない
        if (isTalking)
        {
            return;
        }

        // InputManager の Horizontal に登録してあるキーが入力されたら、水平(横)方向の入力値として代入
        horizontal = Input.GetAxis("Horizontal");

        // InputManager の Vertical に登録してあるキーが入力されたら、水平(横)方向の入力値として代入
        vertical = Input.GetAxis("Vertical");

        // キャラの向いている方向と移動アニメの同期
        SyncMoveAnimation();
    }

    void FixedUpdate()
    {


        ////*  ここから処理を追加  *////

        // TODO TileMapRPGの18と19の実装が完了したら動かす。

        // ステータス画面表示中は操作できない
        //if (operationStatusWindow.propertyWindow.activeSelf)
        //{
        //rb.velocity = Vector2.zero;
        //return;
        //}


        ////*  ここまで  *////


        // 移動
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {

        // velocity(速度)に新しい値を代入して、ゲームオブジェクトを移動させる
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        // プレイヤーの magnitude(ベクトルの長さ) が 0.5 よりも大きく(移動しているとき)、encountManager 変数に EncountManager の情報が代入されている場合
        if (rb.velocity.magnitude > 0.5f && encountManager)
        {

            // ランダムエンカウントが発生するか判定
            encountManager.JudgeRandomEncount();
        }
    }

    /// <summary>
    /// キャラの向いている方向と移動アニメの同期
    /// </summary>
    private void SyncMoveAnimation()
    {

        // 移動のキー入力値を代入
        Vector2 move = new Vector2(horizontal, vertical);

        // いずれかのキー入力があるか確認
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {

            // 向いている方向を更新
            lookDirection.Set(move.x, move.y);

            // 正規化
            lookDirection.Normalize();
        }

        // キー入力の値と Blend Tree で設定した移動アニメ用の値を確認し、移動アニメを再生
        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);

        // 停止アニメーション用
        anim.SetFloat("Speed", move.magnitude);
    }

    /// <summary>
    /// 行動ボタンを押した際の処理
    /// </summary>
    private void Action()
    {

        if (Input.GetButtonDown("Action"))
        {

            ////*  ここから処理を変更  *////

            // actionlayerMasks 変数に代入されている複数の文字列の Layer を判定対象とする　<=　NPC レイヤーのゲームオブジェクトだけではなく、TreasureBox レイヤーのゲームオブジェクトにも反応する
            RaycastHit2D hit = Physics2D.Raycast(rb.position, lookDirection, 1.0f, LayerMask.GetMask(actionlayerMasks));    //　<=　複数の Layer を判定するために、GetMask メソッドの引数を変更します


            ////*  ここまで  *////


            // Scene ビューにて Ray の可視化
            Debug.DrawRay(rb.position, lookDirection, Color.blue, 1.0f);

            // Ray によって hit 変数にコライダーを持つゲームオブジェクトの情報が取得出来ている場合
            if (hit.collider != null)
            {

                // そのゲームオブジェクトにアタッチされている NonPlayerCharacter クラスが取得できた場合
                if (hit.collider.TryGetComponent(out NonPlayerCharacter npc))
                {

                    // 取得した NonPlayerCharacter クラスを持つゲームオブジェクトと会話中ではない場合
                    if (!npc.isTalking)
                    {

                        // NPC との会話イベントを発生させる
                        npc.PlayTalk(transform.position);

                        // Player を会話イベント中の状態にする
                        isTalking = true;

                        // 取得した NonPlayerCharacter クラスを持つゲームオブジェクトと会話中である場合
                    }
                    else
                    {

                        // NPC との会話イベントを終了する
                        npc.StopTalk();

                        // Player を会話イベントをしていない状態にする
                        isTalking = false;
                    }
                }


                ////*  ここから処理を追加  *////


                else if (hit.collider.TryGetComponent(out TreasureBox treasureBox))
                {
                    if (!treasureBox.isOpen)
                    {
                        treasureBox.OpenTresureBox(transform.position, this);
                        isTalking = true;
                    }
                    else
                    {
                        treasureBox.CloseTreasureBox();
                        isTalking = false;
                    }
                }


                ////*  ここまで  *////


            }
        }
    }

    /// <summary>
    /// PlayerController に必要な外部のクラス情報を設定
    /// </summary>
    /// <param name="encountManager"></param>
    public void SetUpPlayerController(EncountManager encountManager)
    {
        this.encountManager = encountManager;
    }

    /// <summary>
    /// 向いている方向を戻す
    /// </summary>
    /// <returns></returns>
    public Vector2 GetLookDirection()
    {

        // 現在のキャラの向いている方向の情報を渡す
        return lookDirection;
    }
}