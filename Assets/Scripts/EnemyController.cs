using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    private Vector3[] paths;// 移動する各地点を代入するための配列

    private Animator anim;// Animator コンポーネントの取得用

    private Vector3 currentPos;// 敵キャラの現在の位置情報

    void Start()
    {
        // Animator コンポーネントを取得して anim 変数に代入
        TryGetComponent(out anim);

        // 移動する地点を取得
        paths = pathData.pathTranArray.Select(x=>x.position).ToArray();

        // 各地点に向けて移動
        transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear);
    }

    private void Update()
    {
        // 敵の進行方向を取得
        ChangeAnimeDirection();
    }

    /// <summary>
    /// 敵の進行方向を取得して、移動アニメと同期
    /// </summary>

    private void ChangeAnimeDirection()
    {
        if (transform.position.x < currentPos.x)
        {
            anim.SetFloat("Y", 0f);
            anim.SetFloat("X", -1.0f);

            Debug.Log("左方向");
        }
        else if(transform.position.y > currentPos.y)
        {
            anim.SetFloat("X", 0f);
            anim.SetFloat("Y", 1.0f);

            Debug.Log("上方向");
        }
        else if (transform.position.y < currentPos.y)
        {
            anim.SetFloat("X", 0f);
            anim.SetFloat("Y", -1.0f);

            Debug.Log("下方向");
        }
        else if (transform.position.x > currentPos.x)
        {
            anim.SetFloat("Y", 0f);
            anim.SetFloat("X", 1.0f);

            Debug.Log("右方向");
        }

        // 現在の位置情報を保持
        currentPos = transform.position;
    }

}

