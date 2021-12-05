using UnityEngine;
using UnityEngine.UI; //Textを使用する為追加。
using System; //DateTimeを使用する為追加。

public class CalendarManager : MonoBehaviour
{
    //テキストUIをドラッグ&ドロップ
    [SerializeField] Text month_text;
    [SerializeField] Text day_text;
    //DateTimeを使うため変数を設定
    DateTime TodayNow;
    // Update is called once per frame
    void Update()
    {
        //時間を取得
        TodayNow = DateTime.Now;
        //テキストUIに年・月・日を表示させる
        month_text.text = TodayNow.Month.ToString();
        day_text.text = TodayNow.Day.ToString();
    }
}
