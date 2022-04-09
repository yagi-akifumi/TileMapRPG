using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventData
{
    /// <summary>
    /// イベントの種類
    /// </summary>

    public enum EventType
    {
        Talk,
        Search,
    }

    public EventType eventType; //イベントの種類
    public int no; //通し番号
    public string title;//タイトル。NPCの名前、探す対象物の名前など

    [System.Serializable]
    public class EventDataDetail
    {
        public EventProgressType eventProgressType;

        [Multiline]
        public string[] dialogs;// NPC のメッセージ、対象物のメッセージ、など
        public Sprite eventSprite;// イベントの画像データ

        public ItemName eventItemName; // イベントで獲得できるアイテム
        public int eventItemCount;     // イベントで獲得できる個数
        internal object eventItemCounts;
    }

    public List<EventDataDetail> eventDataDetailsList = new List<EventDataDetail>();
    // TODO そのほかに追加する場合には以下に補記する
}