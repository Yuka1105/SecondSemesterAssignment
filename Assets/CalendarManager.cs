using UnityEngine;
using UnityEngine.UI; // Text
using System; // DateTime

public class CalendarManager : MonoBehaviour
{
    [SerializeField] Text month_text;
    [SerializeField] Text day_text;
    DateTime TodayNow;

    void Update()
    {
        // 時間を取得
        TodayNow = DateTime.Now;
        // テキストUIに年・月・日を表示させる
        month_text.text = TodayNow.Month.ToString();
        day_text.text = TodayNow.Day.ToString();
    }
}
