using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncountManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerController クラスに EncountManager クラスの情報を渡す
        playerController.SetUpPlayerController(this);    　//　<=　この処理にエラーが出ます
    }

    /// <summary>
    /// ランダムエンカウントの発生
    /// </summary>

    public void JudgeRandomEncount()
    {

        if (GameData.instance.isEncouting)
        {
            return;
        }

        int encountRate = Random.Range(0, GameData.instance.randomEncountRate);

        if (encountRate == 0)
        {
            Debug.Log("エンカウント : " + encountRate);

            GameData.instance.isEncouting = true;

            // TODO プレイヤーキャラの位置と方向の情報を保存


            // Battle シーンへ遷移
            SceneStateManager.instance.NextScene(SceneStateManager.SceneType.Battle);
        }
    }

    void Update()
    {

        // デバッグ用(GameData クラスの isDebug が true (インスペクター上ではチェックがオンの状態)の場合に Left Shift キーを押すことで動作する)
        if (Input.GetKeyDown(KeyCode.LeftShift) && GameData.instance.isDebug)
        {
            Debug.Log("エンカウント終了");

            GameData.instance.isEncouting = false;
        }
    }
}
