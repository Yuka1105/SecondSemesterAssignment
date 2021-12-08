using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{//食材名、入力回数、最後に買った日付を格納するクラス
    public string food;
    public int times;
    public int month;
    public int day;
}

public class BookManager : MonoBehaviour
{
    GameObject RaycastManager;
    RaycastManager script;
    Item[] item;
    // Start is called before the first frame update
    void Start()
    {
     item = new Item[39];//食材の種類分だけItemインスタンスを作る。
     item[0].food = "米";
     item[1].food = "パン";
     item[2].food = "麺";
     item[3].food = "餅";
     item[4].food = "牛肉";
     item[5].food = "豚肉";
     item[6].food = "鶏肉";
     item[7].food = "イワシ";
     item[8].food = "タラ";
     item[9].food = "サケ";
     item[10].food = "大豆";
     item[11].food = "豆腐";
     item[12].food = "納豆";
     item[13].food = "卵";
     item[14].food = "牛乳";
     item[15].food = "チーズ";
     item[16].food = "ヨーグルト";
     item[17].food = "トマト";
     item[18].food = "にんじん";
     item[19].food = "ブロッコリー";
     item[20].food = "白菜";
     item[21].food = "玉ねぎ";
     item[22].food = "キャベツ";
     item[23].food = "しめじ";
     item[24].food = "マイタケ";
     item[25].food = "えのき";
     item[26].food = "ジャガイモ";
     item[27].food = "さつまいも";
     item[28].food = "こんにゃく";
     item[29].food = "ワカメ";
     item[30].food = "ヒジキ";
     item[31].food = "りんご";
     item[32].food = "みかん";
     item[33].food = "バナナ";
     item[34].food = "ぶどう";
     item[35].food = "いちご";
     item[36].food = "チョコレート";
     item[37].food = "クッキー";
     item[38].food = "ケーキ";
     for(int i =0; i<39; i++){
         item[i].times = 0;
         item[i].month = 0;
         item[i].day = 0;
     }
     RaycastManager = GameObject.Find("RaycastManager"); 
     script = RaycastManager.GetComponent<RaycastManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        wrapper = script.Load();
        for(int i=0; i< wrapper.List.Count; i++ ){
            for(int j=0; j<39; j++){
                Debug.Log(wrapper.List[i].food);
                Debug.Log(item[j].food);
                if(wrapper.List[i].food == item[j].food){
                    item[j].times ++;//セーブしたリストの食材名がitem[]の食材名と一致したら入力回数を表すtimes変数を1増やす。
                    item[j].month = wrapper.List[i].month; //データの月も格納(最後に入力した月に最終的に上書きされる。)
                    item[j].day = wrapper.List[i].day; //データの月も格納(最後に入力した日に最終的に上書きされる。)
                }
            }
        }
        for(int i=0; i<39; i++){
            if(item[i].times > 0){//もし一回でも入力されていれば
                Debug.Log(item[i].food + "を" + item[i].times + "回入力。最後に買った日は" + item[i].month + "月" + item[i].day + "日");
            }
             item[i].times = 0; //Updateで永遠に増えてしまわないように最後に0に戻しておく。
        }
    }
}
