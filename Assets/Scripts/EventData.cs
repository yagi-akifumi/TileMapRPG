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

    [Multiline]
    public string dialog;// NPC のメッセージ、対象物のメッセージ、など
    public Sprite eventSprite;// イベントの画像データ

    public ItemName eventItemName; // イベントで獲得できるアイテム
    public int eventItemCount;     // イベントで獲得できる個数

    // TODO そのほかに追加する場合には以下に補記する
}
