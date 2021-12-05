using UnityEngine;
using UnityEngine.UI; //Textを使用する為追加。
using System; //DateTimeを使用する為追加。

public class DateManager : MonoBehaviour
{
    //テキストUIをドラッグ&ドロップ
    [SerializeField] Text date_text;
    DateTime TodayNow;
    // Update is called once per frame
    void Update()
    {
        //時間を取得
        TodayNow = DateTime.Now;
        //テキストUIに年・月・日を表示させる
        date_text.text = TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日";
    }
}
