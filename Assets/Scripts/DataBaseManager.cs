using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;


    [SerializeField]
    private EventDataSO eventDataSO;

    private void Awake()
    {
        if(instance==null)
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
        foreach(EventData eventData in eventDataSO.eventDataList)
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
}
