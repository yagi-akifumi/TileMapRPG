using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;　　　　　//　追加します
using System;　　　　　　　 //　追加します

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    [SerializeField]
    private EventDataSO eventDataSO;


    ////*  ここから変数の宣言を追加  *////


    [SerializeField]
    private ItemDataSO itemDataSO;    //　<=　ItemDataSO スクリプタブル・オブジェクトをアサインして登録するための変数


    ////*  ここまで  *////


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// NPC 用のデータから EventData を取得
    /// </summary>
    /// <param name="npcEvent"></param>
    /// <returns></returns>
    public EventData GetEventDataFromNPCEvent(int npcEventNo)
    {

        // EventDataSO スクリプタブル・オブジェクトの eventDatasList の中身(EventData)を１つずつ順番に取り出して、eventData 変数に代入
        foreach (EventData eventData in eventDataSO.eventDataList)
        {

            // eventData の情報を判定し、EventType が Talk かつ、引数で取得している npcEventNo と同じ場合
            if (eventData.eventType == EventData.EventType.Talk && eventData.no == npcEventNo)
            {

                // 該当する EvenData であると判定し、その情報を戻す
                return eventData;
            }
        }

        // 上記の処理の結果、該当する EvenData の情報がスクリプタブル・オブジェクト内にない場合には、null を戻す
        return null;
    }


    ////*  ここからメソッドを４つ追加  *////


    /// <summary>
    /// EventDataSO から指定した EventType と EventNo の EventData を照合して取得
    /// </summary>
    /// <param name="searchEventType"></param>
    /// <param name="searchEventNo"></param>
    /// <returns></returns>
    public EventData GetEventDataFromEvnetTypeAndEventNo(EventData.EventType searchEventType, int searchEventNo)
    {

        foreach (EventData eventData in eventDataSO.eventDataList)
        {
            if (eventData.eventType == searchEventType && eventData.no == searchEventNo)
            {
                return eventData;
            }
        }
        return null;
    }

    /// <summary>
    /// ItemNo から ItemData を取得
    /// </summary>
    /// <param name="itemNo"></param>
    /// <returns></returns>
    public ItemDataSO.ItemData GetItemDataFromItemNo(int itemNo)
    {

        // ItemDataSO スクリプタブル・オブジェクト内の ItemData の情報を１つずつ順番に取り出して itemData 変数に代入
        foreach (ItemDataSO.ItemData itemData in itemDataSO.itemDataList)
        {

            // 現在取り出している ItemData の itemNo 変数の値と引数で届いている itemNo の値が同じである場合
            if (itemData.itemNo == itemNo)
            {

                // 条件に合致したので、itemData の値を戻す
                return itemData;
            }
        }

        // 上記の検索結果、スクリプタブル・オブジェクト内に該当する情報がない場合、null を戻す
        return null;
    }

    /// <summary>
    /// ItemName から ItemData を取得
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public ItemDataSO.ItemData GetItemDataFromItemName(ItemName itemName)
    {

        // 上段にある GetItemDataFromItemNo メソッドと同じ内容の処理を、検索対象を itemName にして実行して取得
        return itemDataSO.itemDataList.FirstOrDefault(x => x.itemName == itemName);
    }

    /// <summary>
    /// ItemData の 最大要素数を取得
    /// </summary>
    /// <returns></returns>
    public int GetItemDataSOCount()
    {
        return itemDataSO.itemDataList.Count;
    }


    ////*  ここまで  *////


}