using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EventDataSO",menuName ="Create EventDataSO")]
public class EventDataSO : ScriptableObject
{
    public List<EventData> eventDataList = new List<EventData>();// 複数の EventData を１つの変数内でまとめて管理を行う
}
