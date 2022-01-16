using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Color{//色別のためのクラス
    public string color;
    public int price;
    public float ratio;
    public int end_rank;
}
public class ColorRank{//色のランキングのためのクラス
    public string rank;
    public string color;//日本語
    public int price;
    public float ratio;
    public UnityEngine.Color c;
}

public class MeatFish{//肉/魚別のためのクラス
    public string meatfish;
    public int price;
    public float ratio;
    public UnityEngine.Color c;
    public int end_rank;
}

public class Happy{//果物・嗜好別のためのクラス
    public string happy;
    public int price;
    public float ratio;
    public UnityEngine.Color c;
    public int end_rank;
}

public class Category{//カテゴリ別のためのクラス
    public string category;
    public int price;
    public float ratio;
    public UnityEngine.Color c;
    public int end_rank;
}

public class GraphManager : MonoBehaviour
{
    GameObject RaycastManager;
    RaycastManager script;
    public Dropdown dropdown;
    public GameObject panel_color;
    public GameObject panel_happy;
    public GameObject panel_meatfish;
    public GameObject panel_category;
    List<int> month_value;
    string panel_kind;//今何のパネルを表示中か
    public GameObject color_button;
    int change_month;
    Color[] color = new Color[11];//今選択している月の色
    Color[] color2 = new Color[11];//前の月の色
    ColorRank[] color_rank = new ColorRank[11];
    MeatFish[] meatfish = new MeatFish[6];
    MeatFish[] meatfish2 = new MeatFish[6];
    string[] rank = new string[13];
    Happy[] happy = new Happy[8];
    Happy[] happy2 = new Happy[8];
    Category[] category = new Category[13];
    Category[] category2= new Category[13];
    bool push_button = false;//ボタンを押したかの判定。ボタンを押したタイミングでも表の表示を切り替えたい。
    public Sprite[] m_Sprite;

    // Start is called before the first frame update
    void Start()
    {
     RaycastManager = GameObject.Find("RaycastManager"); 
     script = RaycastManager.GetComponent<RaycastManager>();

     month_value = new List<int>();//月の値を保存するためのList

     //jsonデータ内にある月の値を取得
     Wrapper wrapper = new Wrapper();
     wrapper.List = new List<SaveData>();
     wrapper = script.Load();
     month_value.Add(wrapper.List[0].month);//１番目のデータの月の値をList型month_valueに保存する。
     for(int i=0; i< wrapper.List.Count-1; i++ ){
         if(wrapper.List[i].month != wrapper.List[i+1].month){
             month_value.Add(wrapper.List[i+1].month);//前と後の月の値が違った場合、後の月の値をmonthに追加する。
         }
        }
    //ドロップダウンリストに月を追加
     for(int i=0; i< month_value.Count; i++){
            dropdown.options.Add(new Dropdown.OptionData { text = month_value[i].ToString() + "月"});
     }
     dropdown.RefreshShownValue();
     //直近の月を初期値として設定しておく
     dropdown.value = month_value.Count - 1;

     //最初は色別のボタンを押しておく
     color_button.GetComponent<Button>().onClick.Invoke();
     change_month = 13;//13月はないから絶対実行される

     for(int i = 0; i < 11; i++){
         color[i] = new Color();
     }
     color[0].color = "red";
     color[1].color = "orange";
     color[2].color = "yellow";
     color[3].color = "green";
     color[4].color = "blue";
     color[5].color = "purple";
     color[6].color = "pink";
     color[7].color = "brown";
     color[8].color = "black";
     color[9].color = "gray";
     color[10].color = "white";

     for(int i = 0; i < 11; i++){
         color2[i] = new Color();
     }
     color2[0].color = "red";
     color2[1].color = "orange";
     color2[2].color = "yellow";
     color2[3].color = "green";
     color2[4].color = "blue";
     color2[5].color = "purple";
     color2[6].color = "pink";
     color2[7].color = "brown";
     color2[8].color = "black";
     color2[9].color = "gray";
     color2[10].color = "white";

     for(int i = 0; i < 11; i++){
         color_rank[i] = new ColorRank();
     }
     color_rank[0].rank = "first";
     color_rank[1].rank = "second";
     color_rank[2].rank = "third";
     color_rank[3].rank = "fourth";
     color_rank[4].rank = "fifth";
     color_rank[5].rank = "sixth";
     color_rank[6].rank = "seventh";
     color_rank[7].rank = "eighth";
     color_rank[8].rank = "ninth";
     color_rank[9].rank = "tenth";
     color_rank[10].rank = "eleventh";

     for(int i = 0; i < 6; i++){
         meatfish[i] = new MeatFish();
         meatfish2[i] = new MeatFish();
     }
     meatfish[0].meatfish = "イワシ";
     meatfish[1].meatfish = "タラ";
     meatfish[2].meatfish = "サケ";
     meatfish[3].meatfish = "牛肉";
     meatfish[4].meatfish = "豚肉";
     meatfish[5].meatfish = "鶏肉";

     meatfish2[0].meatfish = "イワシ";
     meatfish2[1].meatfish = "タラ";
     meatfish2[2].meatfish = "サケ";
     meatfish2[3].meatfish = "牛肉";
     meatfish2[4].meatfish = "豚肉";
     meatfish2[5].meatfish = "鶏肉";

     rank[0] = "first";
     rank[1] = "second";
     rank[2] = "third";
     rank[3] = "fourth";
     rank[4] = "fifth";
     rank[5] = "sixth";
     rank[6] = "seventh";
     rank[7] = "eighth";
     rank[8] = "ninth";
     rank[9] = "tenth";
     rank[10] = "eleventh";
     rank[11] = "twelfth";
     rank[12] = "thirteenth";

     for(int i = 0; i < 8; i++){
         happy[i] = new Happy();
         happy2[i] = new Happy();
     }
     happy[0].happy = "りんご";
     happy[1].happy = "みかん";
     happy[2].happy = "バナナ";
     happy[3].happy = "ぶどう";
     happy[4].happy = "いちご";
     happy[5].happy = "チョコレート";
     happy[6].happy = "クッキー";
     happy[7].happy = "ケーキ";

     happy2[0].happy = "りんご";
     happy2[1].happy = "みかん";
     happy2[2].happy = "バナナ";
     happy2[3].happy = "ぶどう";
     happy2[4].happy = "いちご";
     happy2[5].happy = "チョコレート";
     happy2[6].happy = "クッキー";
     happy2[7].happy = "ケーキ";

     for(int i = 0; i < 13; i++){
         category[i] = new Category();
         category2[i] = new Category();
     }
     category[0].category = "穀類";
     category[1].category = "肉類";
     category[2].category = "魚介類";
     category[3].category = "豆・豆食品";
     category[4].category = "卵";
     category[5].category = "牛乳・乳製品";
     category[6].category = "緑黄色野菜";
     category[7].category = "淡色野菜";
     category[8].category = "キノコ類";
     category[9].category = "イモ類";
     category[10].category = "海藻類";
     category[11].category = "果物";
     category[12].category = "嗜好品";

     category2[0].category = "穀類";
     category2[1].category = "肉類";
     category2[2].category = "魚介類";
     category2[3].category = "豆・豆食品";
     category2[4].category = "卵";
     category2[5].category = "牛乳・乳製品";
     category2[6].category = "緑黄色野菜";
     category2[7].category = "淡色野菜";
     category2[8].category = "キノコ類";
     category2[9].category = "イモ類";
     category2[10].category = "海藻類";
     category2[11].category = "果物";
     category2[12].category = "嗜好品";
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
     Debug.Log(month_value[dropdown.value] + "月を見ている！");//今選択している月を表示
     if(change_month != month_value[dropdown.value] || push_button == true){//ドロップダウンで違う月を選択したら（1回だけ実行される）
        if(panel_kind == "color"){//色別を見ている時
            Wrapper wrapper = new Wrapper();
            wrapper.List = new List<SaveData>();
            wrapper = script.Load();
            int sum = 0;//値段の合計
            for(int i = 0; i<wrapper.List.Count; i++){
                if(wrapper.List[i].month == month_value[dropdown.value]){//今選択している月のデータのみ参照
                    for(int j = 0; j<11; j++){
                        if(wrapper.List[i].color == color[j].color){//例えばデータ内の食べ物が赤色だった場合colorクラスの赤のインスタンスの値段を増やす。
                            color[j].price += wrapper.List[i].price;
                            sum += wrapper.List[i].price;//値段の合計を増やす。
                        }
                    }
                }
                else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] != 1) && (wrapper.List[i].month == (month_value[dropdown.value] - 1))){//今選択している月(1月以外)の前の月のデータのみ参照 ただしデータの一番最初の月は除く
                    for(int j = 0; j<11; j++){
                        if(wrapper.List[i].color == color2[j].color){
                            color2[j].price += wrapper.List[i].price;
                        }
                    }
                }
                else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] == 1) && (wrapper.List[i].month == 12)){//今選択している月が1月の場合は前の月として12月を参照　ただしデータの一番最初の月は除く
                    for(int j = 0; j<11; j++){
                        if(wrapper.List[i].color == color2[j].color){
                            color2[j].price += wrapper.List[i].price;
                        }
                    }
                }
            }
            //最終的な結果を表示
            float angle = 0.0f;//円グラフの回転角を増やすための変数
            for(int j = 0; j<11; j++){
                //割合を出す
                color[j].ratio = (float)Math.Round(((float)color[j].price / (float)sum) * 100, 1, MidpointRounding.AwayFromZero);//それぞれの色の値段における割合。小数第二位で四捨五入
            }
            //値段の大きい順に並べ替える。
            Array.Sort(color, (a, b) => b.price - a.price);
            //同率処理のための
            int end = 1;
            int jump = 0;
            color[0].end_rank = 1;
            for(int i =1; i<11; i++){
                if(color[i].price == color[i-1].price){
                    color[i].end_rank = end;
                    jump++;
                }
                else{
                    end = end  + jump + 1;
                    color[i].end_rank = end;
                    jump = 0;
                }
            }
            //ColorRankクラスのcolor_rankにランキング順の情報を代入し直す
            for(int j =0; j<11; j++){
                //英語から日本語表記に変換
                if(color[j].color == "red"){
                    color_rank[j].color = "赤";
                    color_rank[j].c = new UnityEngine.Color(232f / 255f, 57f / 255f, 41f / 255f);
                }
                else if(color[j].color == "orange"){
                    color_rank[j].color = "橙";
                    color_rank[j].c = new UnityEngine.Color(236f / 255f, 104f / 255f, 0f / 255f);
                }
                else if(color[j].color == "yellow"){
                    color_rank[j].color = "黄";
                    color_rank[j].c = new UnityEngine.Color(251f / 255f, 202f / 255f, 77f / 255f);
                }
                else if(color[j].color == "green"){
                    color_rank[j].color = "緑";
                    color_rank[j].c = new UnityEngine.Color(62f / 255f, 179f / 255f, 112f / 255f);
                }
                else if(color[j].color == "blue"){
                    color_rank[j].color = "青";
                    color_rank[j].c = new UnityEngine.Color(56f / 255f, 161f / 255f, 219f / 255f);
                }
                else if(color[j].color == "purple"){
                    color_rank[j].color = "紫";
                    color_rank[j].c = new UnityEngine.Color(112f / 255f, 88f / 255f, 163f / 255f);
                }
                else if(color[j].color == "pink"){
                    color_rank[j].color = "桃";
                    color_rank[j].c = new UnityEngine.Color(240f / 255f, 145f / 255f, 153f / 255f);
                }
                else if(color[j].color == "brown"){
                    color_rank[j].color = "茶";
                    color_rank[j].c = new UnityEngine.Color(171f / 255f, 105f / 255f, 83f / 255f);
                }
                else if(color[j].color == "black"){
                    color_rank[j].color = "黒";
                    color_rank[j].c = new UnityEngine.Color(89f / 255f, 88 / 255f, 87f / 255f);
                }
                else if(color[j].color == "gray"){
                    color_rank[j].color = "灰";
                    color_rank[j].c = new UnityEngine.Color(158f / 255f, 161f / 255f, 163f / 255f);
                }
                else if(color[j].color == "white"){
                    color_rank[j].color = "白";
                    color_rank[j].c = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f);
                }
                color_rank[j].price = color[j].price;
                color_rank[j].ratio = color[j].ratio;
            }
            for(int j = 0; j<11; j++){
                //円グラフ
                GameObject.Find("Image_" + color[j].color).GetComponent<Image>().fillAmount = color[j].ratio / 100.0f;
                Transform myTransform = GameObject.Find("Image_" + color[j].color).transform;
                Vector3 worldAngle = myTransform.eulerAngles;
                worldAngle.z = 360.0f * angle;//回転角を求める
                myTransform.eulerAngles = worldAngle; // 回転角度を設定
                angle -= color[j].ratio / 100.0f;
            }
            //表
            for(int j = 0; j<11; j++){
                // Debug.Log(color[j].color + "は合計" + color[j].price + "円。全体の" + color[j].ratio + "%");//結果
                GameObject.Find(color_rank[j].rank).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = (color_rank[j].color);
                GameObject.Find(color_rank[j].rank).transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = (color_rank[j].price).ToString() + "円";
                GameObject.Find(color_rank[j].rank).transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = (color_rank[j].ratio).ToString() + "%";
                GameObject.Find(color_rank[j].rank).transform.GetChild(0).GetComponent<Image>().color = color_rank[j].c;
                //同率処理：値段が一個前の順位のものと同じだった場合
                GameObject.Find(color_rank[j].rank).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = color[j].end_rank.ToString() + "位";
            }
            //updown表示
            if(wrapper.List[0].month == month_value[dropdown.value]){//最初の月を見ている場合
                for(int i =0; i<11; i++){
                    GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 0);
                }
            }
            if(wrapper.List[0].month != month_value[dropdown.value]){//最初以外の月を見ている場合
                Array.Sort(color2, (a, b) => b.price - a.price);
                //同率処理のための
                end = 1;
                jump = 0;
                color2[0].end_rank = 1;
                for(int i =1; i<11; i++){
                    if(color2[i].price == color2[i-1].price){
                        color2[i].end_rank = end;
                        jump++;
                    }
                    else{
                        end = end  + jump + 1;
                        color2[i].end_rank = end;
                        jump = 0;
                    }
                }
                //updown表示の計算
                for(int i =0; i<11; i++){
                    for(int j =0; j<11; j++){
                        if(color[i].color == color2[j].color){
                            if( color[i].end_rank < color2[j].end_rank ){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[0];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(color[i].end_rank == color2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[1];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(color[i].end_rank > color2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[2];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                        }
                    }
                }
            }
            angle = 0;//初期値に戻す
            for(int j = 0; j<11; j++){
                color[j].price = 0;//初期値に戻す
                color2[j].price = 0;//初期値に戻す
            }
        }
        if(panel_kind == "meatfish"){//肉/魚別を見ている時
            Wrapper wrapper = new Wrapper();
            wrapper.List = new List<SaveData>();
            wrapper = script.Load();
            int sum = 0;//値段の合計
            for(int i = 0; i<wrapper.List.Count; i++){
                if(wrapper.List[i].month == month_value[dropdown.value]){//今選択している月のデータのみ参照
                    for(int j = 0; j<6; j++){
                        if(wrapper.List[i].food == meatfish[j].meatfish){//例えばデータ内の食べ物がサケだった場合meatfishクラスのサケのインスタンスの値段を増やす。
                            meatfish[j].price += wrapper.List[i].price;
                            sum += wrapper.List[i].price;//値段の合計を増やす。
                        }
                    }
                }
                else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] != 1) && (wrapper.List[i].month == (month_value[dropdown.value] - 1))){//今選択している月(1月以外)の前の月のデータのみ参照 ただしデータの一番最初の月は除く
                    for(int j = 0; j<6; j++){
                        if(wrapper.List[i].food == meatfish2[j].meatfish){
                            meatfish2[j].price += wrapper.List[i].price;
                        }
                    }
                }
                else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] == 1) && (wrapper.List[i].month == 12)){//今選択している月が1月の場合は前の月として12月を参照　ただしデータの一番最初の月は除く
                    for(int j = 0; j<6; j++){
                        if(wrapper.List[i].food == meatfish2[j].meatfish){
                            meatfish2[j].price += wrapper.List[i].price;
                        }
                    }
                }
            }
            //最終的な結果を表示
            float angle = 0.0f;//円グラフの回転角を増やすための変数
            for(int j = 0; j<6; j++){
                //割合を出す
                meatfish[j].ratio = (float)Math.Round(((float)meatfish[j].price / (float)sum) * 100, 1, MidpointRounding.AwayFromZero);//それぞれの食べ物の値段における割合。小数第二位で四捨五入
            }
            //割合の大きい順に並べ替える。
            Array.Sort(meatfish, (a, b) => b.price - a.price);
            //同率処理のための
            int end = 1;
            int jump = 0;
            meatfish[0].end_rank = 1;
            for(int i =1; i<6; i++){
                if(meatfish[i].price == meatfish[i-1].price){
                    meatfish[i].end_rank = end;
                    jump++;
                }
                else{
                    end = end  + jump + 1;
                    meatfish[i].end_rank = end;
                    jump = 0;
                }
            }
            //MeatFishクラスのmeatfishにパネルの色情報を代入し直す
            for(int j =0; j<6; j++){
                if(meatfish[j].meatfish == "イワシ"){
                    meatfish[j].c = new UnityEngine.Color(131f / 255f, 204f / 255f, 210f / 255f);
                }
                else if(meatfish[j].meatfish == "タラ"){
                    meatfish[j].c = new UnityEngine.Color(132f / 255f, 185f / 255f, 203f / 255f);
                }
                else if(meatfish[j].meatfish == "サケ"){
                    meatfish[j].c = new UnityEngine.Color(132f / 255f, 162f / 255f, 212f / 255f);
                }
                else if(meatfish[j].meatfish == "牛肉"){
                    meatfish[j].c = new UnityEngine.Color(223f / 255f, 113f / 255f, 99f / 255f);
                }
                else if(meatfish[j].meatfish == "豚肉"){
                    meatfish[j].c = new UnityEngine.Color(224f / 255f, 129f / 255f, 94f / 255f);
                }
                else if(meatfish[j].meatfish == "鶏肉"){
                    meatfish[j].c = new UnityEngine.Color(235f / 255f, 155f / 255f, 111f / 255f);
                }
            }
            for(int j = 0; j<6; j++){
                //円グラフ
                GameObject.Find("Image_" + meatfish[j].meatfish).GetComponent<Image>().fillAmount = meatfish[j].ratio / 100.0f;
                Transform myTransform = GameObject.Find("Image_" + meatfish[j].meatfish).transform;
                Vector3 worldAngle = myTransform.eulerAngles;
                worldAngle.z = 360.0f * angle;//回転角を求める
                myTransform.eulerAngles = worldAngle; // 回転角度を設定
                angle -= meatfish[j].ratio / 100.0f;
            }
            for(int j = 0; j<6; j++){
                //表
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = (meatfish[j].meatfish);
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = (meatfish[j].price).ToString() + "円";
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = (meatfish[j].ratio).ToString() + "%";
                GameObject.Find(rank[j]).transform.GetChild(0).GetComponent<Image>().color = meatfish[j].c;
                //同率処理：値段が一個前の順位のものと同じだった場合
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = meatfish[j].end_rank.ToString() + "位";
            }
            //updown表示
            if(wrapper.List[0].month == month_value[dropdown.value]){//最初の月を見ている場合
                for(int i =0; i<6; i++){
                    GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 0);
                }
            }
            if(wrapper.List[0].month != month_value[dropdown.value]){//最初以外の月を見ている場合
                Array.Sort(meatfish2, (a, b) => b.price - a.price);
                //同率処理のための
                end = 1;
                jump = 0;
                meatfish2[0].end_rank = 1;
                for(int i =1; i<6; i++){
                    if(meatfish2[i].price == meatfish2[i-1].price){
                        meatfish2[i].end_rank = end;
                        jump++;
                    }
                    else{
                        end = end  + jump + 1;
                        meatfish2[i].end_rank = end;
                        jump = 0;
                    }
                }
                //updown表示の計算
                for(int i =0; i<6; i++){
                    for(int j =0; j<6; j++){
                        if(meatfish[i].meatfish == meatfish2[j].meatfish){
                            if( meatfish[i].end_rank < meatfish2[j].end_rank ){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[0];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(meatfish[i].end_rank == meatfish2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[1];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(meatfish[i].end_rank > meatfish2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[2];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                        }
                    }
                }
            }
            angle = 0;//初期値に戻す
            for(int j = 0; j<6; j++){
                meatfish[j].price = 0;//初期値に戻す
                meatfish2[j].price = 0;//初期値に戻す
            }
        }
        if(panel_kind == "happy"){//果物・嗜好別を見ている時
            Wrapper wrapper = new Wrapper();
            wrapper.List = new List<SaveData>();
            wrapper = script.Load();
            int sum = 0;//値段の合計
            for(int i = 0; i<wrapper.List.Count; i++){
                if(wrapper.List[i].month == month_value[dropdown.value]){//今選択している月のデータのみ参照
                    for(int j = 0; j<8; j++){
                        if(wrapper.List[i].food == happy[j].happy){//例えばデータ内の食べ物が赤色だった場合colorクラスの赤のインスタンスの値段を増やす。
                            happy[j].price += wrapper.List[i].price;
                            sum += wrapper.List[i].price;//値段の合計を増やす。
                        }
                    }
                }
                else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] != 1) && (wrapper.List[i].month == (month_value[dropdown.value] - 1))){//今選択している月(1月以外)の前の月のデータのみ参照 ただしデータの一番最初の月は除く
                    for(int j = 0; j<8; j++){
                        if(wrapper.List[i].food == happy2[j].happy){
                            happy2[j].price += wrapper.List[i].price;
                        }
                    }
                }
                else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] == 1) && (wrapper.List[i].month == 12)){//今選択している月が1月の場合は前の月として12月を参照　ただしデータの一番最初の月は除く
                    for(int j = 0; j<8; j++){
                        if(wrapper.List[i].food == happy2[j].happy){
                            happy2[j].price += wrapper.List[i].price;
                        }
                    }
                }
            }
            //最終的な結果を表示
            float angle = 0.0f;//円グラフの回転角を増やすための変数
            for(int j = 0; j<8; j++){
                //割合を出す
                happy[j].ratio = (float)Math.Round(((float)happy[j].price / (float)sum) * 100, 1, MidpointRounding.AwayFromZero);//それぞれの食べ物の値段における割合。小数第二位で四捨五入
            }
            //割合の大きい順に並べ替える。
            Array.Sort(happy, (a, b) => b.price - a.price);
            //同率処理のための
            int end = 1;
            int jump = 0;
            happy[0].end_rank = 1;
            for(int i =1; i<8; i++){
                if(happy[i].price == happy[i-1].price){
                    happy[i].end_rank = end;
                    jump++;
                }
                else{
                    end = end  + jump + 1;
                    happy[i].end_rank = end;
                    jump = 0;
                }
            }
            //MeatFishクラスのmeatfishにパネルの色情報を代入し直す
            for(int j =0; j<8; j++){
                if(happy[j].happy == "りんご"){
                    happy[j].c = new UnityEngine.Color(235f / 255f, 98f / 255f, 56f / 255f);
                }
                else if(happy[j].happy == "みかん"){
                     happy[j].c = new UnityEngine.Color(240f / 255f, 131f / 255f, 0f / 255f);
                }
                else if(happy[j].happy == "バナナ"){
                    happy[j].c = new UnityEngine.Color(252f / 255f, 200f / 255f, 0f / 255f);
                }
                else if(happy[j].happy == "ぶどう"){
                    happy[j].c = new UnityEngine.Color(162f / 255f, 87f / 255f, 104f / 255f);
                }
                else if(happy[j].happy == "いちご"){
                    happy[j].c = new UnityEngine.Color(235f / 255f, 110f / 255f, 165f / 255f);
                }
                else if(happy[j].happy == "チョコレート"){
                    happy[j].c = new UnityEngine.Color(159f / 255f, 111f / 255f, 85f / 255f);
                }
                else if(happy[j].happy == "クッキー"){
                    happy[j].c = new UnityEngine.Color(248f / 255f, 184f / 255f, 98f / 255f);
                }
                else if(happy[j].happy == "ケーキ"){
                    happy[j].c = new UnityEngine.Color(248f / 255f, 229f / 255f, 140f / 255f);
                }
            }
            for(int j = 0; j<8; j++){
                //円グラフ
                GameObject.Find("Image_" + happy[j].happy).GetComponent<Image>().fillAmount = happy[j].ratio / 100.0f;
                Transform myTransform = GameObject.Find("Image_" + happy[j].happy).transform;
                Vector3 worldAngle = myTransform.eulerAngles;
                worldAngle.z = 360.0f * angle;//回転角を求める
                myTransform.eulerAngles = worldAngle; // 回転角度を設定
                angle -= happy[j].ratio / 100.0f;
            }
            for(int j = 0; j<8; j++){
                //表
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = (happy[j].happy);
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = (happy[j].price).ToString() + "円";
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = (happy[j].ratio).ToString() + "%";
                GameObject.Find(rank[j]).transform.GetChild(0).GetComponent<Image>().color = happy[j].c;
                 //同率処理：値段が一個前の順位のものと同じだった場合
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = happy[j].end_rank.ToString() + "位";
                if(happy[j].happy == "チョコレート"){
                    GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize = 25;
                }
            }
            //updown表示
            if(wrapper.List[0].month == month_value[dropdown.value]){//最初の月を見ている場合
                for(int i =0; i<8; i++){
                    GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 0);
                }
            }
            if(wrapper.List[0].month != month_value[dropdown.value]){//最初以外の月を見ている場合
                Array.Sort(happy2, (a, b) => b.price - a.price);
                //同率処理のための
                end = 1;
                jump = 0;
                happy2[0].end_rank = 1;
                for(int i =1; i<8; i++){
                    if(happy2[i].price == happy2[i-1].price){
                        happy2[i].end_rank = end;
                        jump++;
                    }
                    else{
                        end = end  + jump + 1;
                        happy2[i].end_rank = end;
                        jump = 0;
                    }
                }
                //updown表示の計算
                for(int i =0; i<8; i++){
                    for(int j =0; j<8; j++){
                        if(happy[i].happy == happy2[j].happy){
                            if( happy[i].end_rank < happy2[j].end_rank ){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[0];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(happy[i].end_rank == happy2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[1];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(happy[i].end_rank > happy2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[2];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                        }
                    }
                }
            }
            angle = 0;//初期値に戻す
            for(int j = 0; j<8; j++){
                happy[j].price = 0;//初期値に戻す
                happy2[j].price = 0;//初期値に戻す
            }
            
        }
        if(panel_kind == "category"){//カテゴリ別を見ている時
            Wrapper wrapper = new Wrapper();
            wrapper.List = new List<SaveData>();
            wrapper = script.Load();
            int sum = 0;//値段の合計
            for(int i = 0; i<wrapper.List.Count; i++){
                if(wrapper.List[i].month == month_value[dropdown.value]){//今選択している月のデータのみ参照
                    if(wrapper.List[i].food == "米" || wrapper.List[i].food == "パン" || wrapper.List[i].food == "麺"　|| wrapper.List[i].food == "餅"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "穀類"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "牛肉" || wrapper.List[i].food == "豚肉" || wrapper.List[i].food == "鶏肉"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "肉類"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "イワシ" || wrapper.List[i].food == "タラ" || wrapper.List[i].food == "サケ"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "魚介類"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "大豆" || wrapper.List[i].food == "豆腐" || wrapper.List[i].food == "納豆"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "豆・豆食品"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "卵"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "卵"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "牛乳" || wrapper.List[i].food == "チーズ" || wrapper.List[i].food == "ヨーグルト"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "牛乳・乳製品"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "トマト" || wrapper.List[i].food == "にんじん" || wrapper.List[i].food == "ブロッコリー"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "緑黄色野菜"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "白菜" || wrapper.List[i].food == "玉ねぎ" || wrapper.List[i].food == "キャベツ"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "淡色野菜"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "しめじ" || wrapper.List[i].food == "マイタケ" || wrapper.List[i].food == "えのき"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "キノコ類"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "ジャガイモ" || wrapper.List[i].food == "さつまいも" || wrapper.List[i].food == "こんにゃく"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "イモ類"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "ワカメ" || wrapper.List[i].food == "ヒジキ"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "海藻類"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "りんご" || wrapper.List[i].food == "みかん"|| wrapper.List[i].food == "バナナ" || wrapper.List[i].food == "ぶどう" ||wrapper.List[i].food == "いちご"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "果物"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "チョコレート" || wrapper.List[i].food == "クッキー"|| wrapper.List[i].food == "ケーキ"){
                        for(int j = 0; j < 13; j++){
                            if(category[j].category == "嗜好品"){
                                category[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    sum += wrapper.List[i].price;//値段の合計を増やす。
                }else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] != 1) && (wrapper.List[i].month == (month_value[dropdown.value] - 1))){//今選択している月(1月以外)の前の月のデータのみ参照 ただしデータの一番最初の月は除く
                    if(wrapper.List[i].food == "米" || wrapper.List[i].food == "パン" || wrapper.List[i].food == "麺"　|| wrapper.List[i].food == "餅"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "穀類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "牛肉" || wrapper.List[i].food == "豚肉" || wrapper.List[i].food == "鶏肉"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "肉類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "イワシ" || wrapper.List[i].food == "タラ" || wrapper.List[i].food == "サケ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "魚介類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "大豆" || wrapper.List[i].food == "豆腐" || wrapper.List[i].food == "納豆"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "豆・豆食品"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "卵"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "卵"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "牛乳" || wrapper.List[i].food == "チーズ" || wrapper.List[i].food == "ヨーグルト"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "牛乳・乳製品"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "トマト" || wrapper.List[i].food == "にんじん" || wrapper.List[i].food == "ブロッコリー"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "緑黄色野菜"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "白菜" || wrapper.List[i].food == "玉ねぎ" || wrapper.List[i].food == "キャベツ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "淡色野菜"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "しめじ" || wrapper.List[i].food == "マイタケ" || wrapper.List[i].food == "えのき"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "キノコ類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "ジャガイモ" || wrapper.List[i].food == "さつまいも" || wrapper.List[i].food == "こんにゃく"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "イモ類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "ワカメ" || wrapper.List[i].food == "ヒジキ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "海藻類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "りんご" || wrapper.List[i].food == "みかん"|| wrapper.List[i].food == "バナナ" || wrapper.List[i].food == "ぶどう" ||wrapper.List[i].food == "いちご"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "果物"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "チョコレート" || wrapper.List[i].food == "クッキー"|| wrapper.List[i].food == "ケーキ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "嗜好品"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                }
                else if((wrapper.List[0].month != month_value[dropdown.value]) && (month_value[dropdown.value] == 1) && (wrapper.List[i].month == 12)){//今選択している月が1月の場合は前の月として12月を参照　ただしデータの一番最初の月は除く
                    if(wrapper.List[i].food == "米" || wrapper.List[i].food == "パン" || wrapper.List[i].food == "麺"　|| wrapper.List[i].food == "餅"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "穀類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "牛肉" || wrapper.List[i].food == "豚肉" || wrapper.List[i].food == "鶏肉"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "肉類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "イワシ" || wrapper.List[i].food == "タラ" || wrapper.List[i].food == "サケ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "魚介類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "大豆" || wrapper.List[i].food == "豆腐" || wrapper.List[i].food == "納豆"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "豆・豆食品"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "卵"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "卵"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "牛乳" || wrapper.List[i].food == "チーズ" || wrapper.List[i].food == "ヨーグルト"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "牛乳・乳製品"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "トマト" || wrapper.List[i].food == "にんじん" || wrapper.List[i].food == "ブロッコリー"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "緑黄色野菜"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "白菜" || wrapper.List[i].food == "玉ねぎ" || wrapper.List[i].food == "キャベツ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "淡色野菜"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "しめじ" || wrapper.List[i].food == "マイタケ" || wrapper.List[i].food == "えのき"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "キノコ類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "ジャガイモ" || wrapper.List[i].food == "さつまいも" || wrapper.List[i].food == "こんにゃく"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "イモ類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "ワカメ" || wrapper.List[i].food == "ヒジキ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "海藻類"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "りんご" || wrapper.List[i].food == "みかん"|| wrapper.List[i].food == "バナナ" || wrapper.List[i].food == "ぶどう" ||wrapper.List[i].food == "いちご"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "果物"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                    else if(wrapper.List[i].food == "チョコレート" || wrapper.List[i].food == "クッキー"|| wrapper.List[i].food == "ケーキ"){
                        for(int j = 0; j < 13; j++){
                            if(category2[j].category == "嗜好品"){
                                category2[j].price += wrapper.List[i].price;
                            }
                        }
                    }
                }
            }
            //最終的な結果を表示
            float angle = 0.0f;//円グラフの回転角を増やすための変数
            for(int j = 0; j<13; j++){
                //割合を出す
                category[j].ratio = (float)Math.Round(((float)category[j].price / (float)sum) * 100, 1, MidpointRounding.AwayFromZero);//それぞれの食べ物の値段における割合。小数第二位で四捨五入
            }
            //割合の大きい順に並べ替える。
            Array.Sort(category, (a, b) => b.price - a.price);
            //同率処理のための
            int end = 1;
            int jump = 0;
            category[0].end_rank = 1;
            for(int i =1; i<13; i++){
                if(category[i].price == category[i-1].price){
                    category[i].end_rank = end;
                    jump++;
                }
                else{
                    end = end  + jump + 1;
                    category[i].end_rank = end;
                    jump = 0;
                }
            }
            //MeatFishクラスのmeatfishにパネルの色情報を代入し直す
            for(int j =0; j<13; j++){
                if(category[j].category == "穀類"){
                    category[j].c = new UnityEngine.Color(242f / 255f, 201f / 255f, 172f / 255f);
                }
                else if(category[j].category == "肉類"){
                     category[j].c = new UnityEngine.Color(238f / 255f, 121f / 255f, 72f / 255f);
                }
                else if(category[j].category == "魚介類"){
                    category[j].c = new UnityEngine.Color(89f / 255f, 185f / 255f, 198f / 255f);
                }
                else if(category[j].category == "豆・豆食品"){
                    category[j].c = new UnityEngine.Color(237f / 255f, 211f / 255f, 161f / 255f);
                }
                else if(category[j].category == "卵"){
                    category[j].c = new UnityEngine.Color(255f / 255f, 219f / 255f, 79f / 255f);
                }
                else if(category[j].category == "牛乳・乳製品"){
                    category[j].c = new UnityEngine.Color(251f / 255f, 250f / 255f, 245f / 255f);
                }
                else if(category[j].category == "緑黄色野菜"){
                    category[j].c = new UnityEngine.Color(220f / 255f, 203f / 255f, 24f / 255f);
                }
                else if(category[j].category == "淡色野菜"){
                    category[j].c = new UnityEngine.Color(216f / 255f, 230f / 255f, 152f / 255f);
                }
                else if(category[j].category == "キノコ類"){
                    category[j].c = new UnityEngine.Color(180f / 255f, 134f / 255f, 107f / 255f);
                }
                else if(category[j].category == "イモ類"){
                    category[j].c = new UnityEngine.Color(215f / 255f, 169f / 255f, 140f / 255f);
                }
                else if(category[j].category == "海藻類"){
                    category[j].c = new UnityEngine.Color(105f / 255f, 176f / 255f, 118f / 255f);
                }
                else if(category[j].category == "果物"){
                    category[j].c = new UnityEngine.Color(248f / 255f, 184f / 255f, 98f / 255f);
                }
                else if(category[j].category == "嗜好品"){
                    category[j].c = new UnityEngine.Color(242f / 255f, 160f / 255f, 161f / 255f);
                }
            }
            for(int j = 0; j<13; j++){
                //円グラフ
                GameObject.Find("Image_" + category[j].category).GetComponent<Image>().fillAmount = category[j].ratio / 100.0f;
                Transform myTransform = GameObject.Find("Image_" + category[j].category).transform;
                Vector3 worldAngle = myTransform.eulerAngles;
                worldAngle.z = 360.0f * angle;//回転角を求める
                myTransform.eulerAngles = worldAngle; // 回転角度を設定
                angle -= category[j].ratio / 100.0f;
            }
            for(int j = 0; j<13; j++){
                //表
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = (category[j].category);
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = (category[j].price).ToString() + "円";
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = (category[j].ratio).ToString() + "%";
                GameObject.Find(rank[j]).transform.GetChild(0).GetComponent<Image>().color = category[j].c;
                //同率処理：値段が一個前の順位のものと同じだった場合
                GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = category[j].end_rank.ToString() + "位";
                if(j > 2){//1,2,3位
                    if(category[j].category == "牛乳・乳製品"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize = 25;
                    }
                    else if(category[j].category == "豆・豆食品"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize =30;
                    }
                    else if(category[j].category == "緑黄色野菜"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize = 30;
                    }
                    else if(category[j].category == "淡色野菜"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize = 37;
                    }
                    else if(category[j].category == "キノコ類"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize = 37;
                    }
                }
                else{//4位〜
                    if(category[j].category == "牛乳・乳製品"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize = 25;
                    }
                    else if(category[j].category == "豆・豆食品"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize =30;
                    }
                    else if(category[j].category == "緑黄色野菜"){
                        GameObject.Find(rank[j]).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().fontSize = 30;
                    }
                }
            }
             //updown表示
            if(wrapper.List[0].month == month_value[dropdown.value]){//最初の月を見ている場合
                for(int i =0; i<13; i++){
                    GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 0);
                }
            }
            if(wrapper.List[0].month != month_value[dropdown.value]){//最初以外の月を見ている場合
                Array.Sort(category2, (a, b) => b.price - a.price);
                //同率処理のための
                end = 1;
                jump = 0;
                category2[0].end_rank = 1;
                for(int i =1; i<13; i++){
                    if(category2[i].price == category2[i-1].price){
                        category2[i].end_rank = end;
                        jump++;
                    }
                    else{
                        end = end  + jump + 1;
                        category2[i].end_rank = end;
                        jump = 0;
                    }
                }
                //updown表示の計算
                for(int i =0; i<13; i++){
                    for(int j =0; j<13; j++){
                        if(category[i].category == category2[j].category){
                            if( category[i].end_rank < category2[j].end_rank ){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[0];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(category[i].end_rank == category2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[1];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                            else if(category[i].end_rank > category2[j].end_rank){
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().sprite = m_Sprite[2];
                                GameObject.Find(rank[i]).transform.GetChild(0).transform.GetChild(5).GetComponent<Image>().color = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f, 1);
                            }
                        }
                    }
                }
            }
            angle = 0;//初期値に戻す
            for(int j = 0; j<13; j++){
                category[j].price = 0;//初期値に戻す
                category2[j].price = 0;//初期値に戻す
            }
        }
        
        push_button = false;
     }
     
     change_month = month_value[dropdown.value];
     
    }
    public void Buttons(string buttons){
        switch(buttons){
            case "color":
                panel_color.SetActive(true);
                panel_happy.SetActive(false);
                panel_meatfish.SetActive(false);
                panel_category.SetActive(false);
                panel_kind = "color";
                push_button = true;
                break;
            case "happy":
                panel_color.SetActive(false);
                panel_happy.SetActive(true);
                panel_meatfish.SetActive(false);
                panel_category.SetActive(false);
                panel_kind = "happy";
                push_button = true;
                break;
            case "meatfish":
                panel_color.SetActive(false);
                panel_happy.SetActive(false);
                panel_meatfish.SetActive(true);
                panel_category.SetActive(false);
                panel_kind = "meatfish";
                push_button = true;
                break;
            case "category":
                panel_color.SetActive(false);
                panel_happy.SetActive(false);
                panel_meatfish.SetActive(false);
                panel_category.SetActive(true);
                panel_kind = "category";
                push_button = true;
                break;
    
            default:
                break;
        }
    }
    public void ReturnOnClick(){
        SceneManager.LoadScene("Home");
    }
}
