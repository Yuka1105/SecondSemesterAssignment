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
}

public class MeatFish{//肉/魚別のためのクラス
    public string meatfish;
    public int price;
    public float ratio;
}

public class GraphManager : MonoBehaviour
{
    GameObject RaycastManager;
    RaycastManager script;
    public Dropdown dropdown;
    public GameObject panel_color;
    public GameObject panel_category;
    public GameObject panel_meatfish;
    List<int> month_value;
    string panel_kind;//今何のパネルを表示中か
    public GameObject color_button;
    int change_month;
    Color[] color = new Color[11];
    MeatFish[] meatfish = new MeatFish[6];
    bool push_button = false;//ボタンを押したかの判定。ボタンを押したタイミングでも表の表示を切り替えたい。

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

     for(int i = 0; i < 6; i++){
         meatfish[i] = new MeatFish();
     }
     meatfish[0].meatfish = "イワシ";
     meatfish[1].meatfish = "タラ";
     meatfish[2].meatfish = "サケ";
     meatfish[3].meatfish = "牛肉";
     meatfish[4].meatfish = "豚肉";
     meatfish[5].meatfish = "鶏肉";
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
            }
            //最終的な結果を表示
            float angle = 0.0f;//円グラフの回転角を増やすための変数
            for(int j = 0; j<11; j++){
                //表
                color[j].ratio = (float)Math.Round(((float)color[j].price / (float)sum) * 100, 1, MidpointRounding.AwayFromZero);//それぞれの色の値段における割合。小数第二位で四捨五入
                Debug.Log(color[j].color + "は合計" + color[j].price + "円。全体の" + color[j].ratio + "%");//結果
                GameObject.Find(color[j].color).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = (color[j].price).ToString() + "円";
                GameObject.Find(color[j].color).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = (color[j].ratio).ToString() + "%";

                //円グラフ
                GameObject.Find("Image_" + color[j].color).GetComponent<Image>().fillAmount = color[j].ratio / 100.0f;
                Transform myTransform = GameObject.Find("Image_" + color[j].color).transform;
                Vector3 worldAngle = myTransform.eulerAngles;
                worldAngle.z = 360.0f * angle;//回転角を求める
                myTransform.eulerAngles = worldAngle; // 回転角度を設定
                angle -= color[j].ratio / 100.0f;
            }
            angle = 0;//初期値に戻す
            for(int j = 0; j<11; j++){
                color[j].price = 0;//初期値に戻す
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
            }
            //最終的な結果を表示
            float angle = 0.0f;//円グラフの回転角を増やすための変数
            for(int j = 0; j<6; j++){
                //表
                meatfish[j].ratio = (float)Math.Round(((float)meatfish[j].price / (float)sum) * 100, 1, MidpointRounding.AwayFromZero);//それぞれの食べ物の値段における割合。小数第二位で四捨五入
                Debug.Log(meatfish[j].meatfish + "は合計" + meatfish[j].price + "円。全体の" + meatfish[j].ratio + "%");//結果
                GameObject.Find(meatfish[j].meatfish).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = (meatfish[j].price).ToString() + "円";
                GameObject.Find(meatfish[j].meatfish).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = (meatfish[j].ratio).ToString() + "%";

                //円グラフ
                GameObject.Find("Image_" + meatfish[j].meatfish).GetComponent<Image>().fillAmount = meatfish[j].ratio / 100.0f;
                Transform myTransform = GameObject.Find("Image_" + meatfish[j].meatfish).transform;
                Vector3 worldAngle = myTransform.eulerAngles;
                worldAngle.z = 360.0f * angle;//回転角を求める
                myTransform.eulerAngles = worldAngle; // 回転角度を設定
                angle -= meatfish[j].ratio / 100.0f;
            }
            angle = 0;//初期値に戻す
            for(int j = 0; j<6; j++){
                meatfish[j].price = 0;//初期値に戻す
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
                panel_category.SetActive(false);
                panel_meatfish.SetActive(false);
                panel_kind = "color";
                push_button = true;
                break;
            case "category":
                panel_color.SetActive(false);
                panel_category.SetActive(true);
                panel_meatfish.SetActive(false);
                panel_kind = "category";
                push_button = true;
                break;
            case "meatfish":
                panel_color.SetActive(false);
                panel_category.SetActive(false);
                panel_meatfish.SetActive(true);
                panel_kind = "meatfish";
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
