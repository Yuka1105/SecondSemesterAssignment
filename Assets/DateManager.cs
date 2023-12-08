using UnityEngine;
using UnityEngine.UI; // Text
using System; // DateTime

public class DateManager : MonoBehaviour
{
    [SerializeField] Text date_text;
    DateTime TodayNow;

    void Update()
    {
        // 時間を取得
        TodayNow = DateTime.Now;
        // テキストUIに年・月・日を表示
        date_text.text = TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日";
    }
}
