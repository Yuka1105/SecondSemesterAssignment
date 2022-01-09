using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //DateTimeを使用する為追加。

[System.Serializable]
public class TwoColor{//上位２色のためのクラス
    public string color;
    public float ratio;
    public UnityEngine.Color c;
}
public class CharacterManager : MonoBehaviour
{
    int month = 0;
    DateTime TodayNow;
    GameObject RaycastManager;
    RaycastManager script;
    Color[] color = new Color[11];
    TwoColor[] twocolor = new TwoColor[2];
    // Start is called before the first frame update
    void Start()
    {
     RaycastManager = GameObject.Find("RaycastManager"); 
     script = RaycastManager.GetComponent<RaycastManager>(); 

     TodayNow = DateTime.Now;
     month = TodayNow.Month;//今月の月の値を格納する。

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

     for(int i = 0; i < 2; i++){
         twocolor[i] = new TwoColor();
     }
    }
    

    // Update is called once per frame
    void Update()
    {
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        wrapper = script.Load();
        int sum = 0;
        for(int i = 0; i<wrapper.List.Count; i++){
                if(wrapper.List[i].month == month){//今月のデータのみ参照
                    for(int j = 0; j<11; j++){
                        if(wrapper.List[i].color == color[j].color){//例えばデータ内の食べ物が赤色だった場合colorクラスの赤のインスタンスの値段を増やす。
                            color[j].price += wrapper.List[i].price;
                            sum += wrapper.List[i].price;//値段の合計を増やす。
                        }
                    }
                }
        }
        int color_num = 0;//今月の色の数を数えるための変数
        for(int j = 0; j<11; j++){
            if(color[j].price >= 1){
                color_num++;
            }
        }
        Debug.Log("今月の色の数は" + color_num + "色です。");
        if(color_num == 0){//今月0色の場合
            //キャラクターの色を変更
            Renderer renderer = GetComponent<Renderer>();
            Material[] mats = renderer.materials;
            UnityEngine.Color c = new UnityEngine.Color(255f / 255f, 247f / 255f, 153f / 255f);
            mats[2].color = c;
            renderer.materials = mats;
        }
        else if(color_num == 1){//今月1色の場合
            //キャラクターの色を変更
            Renderer renderer = GetComponent<Renderer>();
            Material[] mats = renderer.materials;
            for(int i = 0; i<wrapper.List.Count; i++){
                if(wrapper.List[i].month == month){//今月のデータのみ参照
                    if(wrapper.List[i].color == "red"){
                        UnityEngine.Color c = new UnityEngine.Color(232f / 255f, 57f / 255f, 41f / 255f);;
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "orange"){
                        UnityEngine.Color c = new UnityEngine.Color(236f / 255f, 104f / 255f, 0f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "yellow"){
                        UnityEngine.Color c = new UnityEngine.Color(251f / 255f, 202f / 255f, 77f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "green"){
                        UnityEngine.Color c = new UnityEngine.Color(62f / 255f, 179f / 255f, 112f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "blue"){
                        UnityEngine.Color c = new UnityEngine.Color(56f / 255f, 161f / 255f, 219f / 255f);;
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "purple"){
                        UnityEngine.Color c = new UnityEngine.Color(112f / 255f, 88f / 255f, 163f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "pink"){
                        UnityEngine.Color c = new UnityEngine.Color(240f / 255f, 145f / 255f, 153f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "brown"){
                        UnityEngine.Color c = new UnityEngine.Color(171f / 255f, 105f / 255f, 83f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "black"){
                        UnityEngine.Color c = new UnityEngine.Color(89f / 255f, 88 / 255f, 87f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "gray"){
                        UnityEngine.Color c = new UnityEngine.Color(158f / 255f, 161f / 255f, 163f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                    else if(wrapper.List[i].color == "white"){
                        UnityEngine.Color c = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f);
                        mats[2].color = c;
                        renderer.materials = mats;
                    }
                }
            }
        }
        else{//今月2色以上ある場合
            //各色の割合を算出する。
            for(int j = 0; j<11; j++){
                color[j].ratio = (float)Math.Round(((float)color[j].price / (float)sum) * 100, 1, MidpointRounding.AwayFromZero);//それぞれの色の値段における割合。小数第二位で四捨五入
            }
            //rankという名前の配列に割合の値を移す。
            float[] rank = new float[11];
            for(int j = 0; j<11; j++){
                rank[j] = color[j].ratio;
            }
            //割合の大きい順に並べ替える。
            Array.Sort(rank);
            Array.Reverse(rank);
            for(int j = 0; j<11; j++){
                Debug.Log(rank[j]);
            }
            for(int i = 0; i<2; i++){//上位２色がどの色なのかを求める。
                for(int j =0; j<11; j++){
                    if(rank[i] == color[j].ratio){
                        //TwoColorクラスに上位2色の情報を格納
                        twocolor[i].color = color[j].color;
                        twocolor[i].ratio = color[j].ratio;
                    }
                }
            }
            for(int i = 0; i<2; i++){//RGBの色の情報をTwoColorオブジェクトに入れる。
                if(twocolor[i].color == "red"){
                    twocolor[i].c = new UnityEngine.Color(232f / 255f, 57f / 255f, 41f / 255f);
                }
                else if(twocolor[i].color == "orange"){
                    twocolor[i].c = new UnityEngine.Color(236f / 255f, 104f / 255f, 0f / 255f);
                }
                else if(twocolor[i].color == "yellow"){
                    twocolor[i].c = new UnityEngine.Color(251f / 255f, 202f / 255f, 77f / 255f);
                }
                else if(twocolor[i].color == "green"){
                    twocolor[i].c = new UnityEngine.Color(62f / 255f, 179f / 255f, 112f / 255f);
                }
                else if(twocolor[i].color == "blue"){
                    twocolor[i].c = new UnityEngine.Color(56f / 255f, 161f / 255f, 219f / 255f);;
                }
                else if(twocolor[i].color == "purple"){
                    twocolor[i].c = new UnityEngine.Color(112f / 255f, 88f / 255f, 163f / 255f);
                }
                else if(twocolor[i].color == "pink"){
                    twocolor[i].c = new UnityEngine.Color(240f / 255f, 145f / 255f, 153f / 255f);
                }
                else if(twocolor[i].color == "brown"){
                    twocolor[i].c = new UnityEngine.Color(171f / 255f, 105f / 255f, 83f / 255f);
                }
                else if(twocolor[i].color == "black"){
                    twocolor[i].c = new UnityEngine.Color(89f / 255f, 88 / 255f, 87f / 255f);
                }
                else if(twocolor[i].color == "gray"){
                    twocolor[i].c = new UnityEngine.Color(158f / 255f, 161f / 255f, 163f / 255f);
                }
                else if(twocolor[i].color == "white"){
                    twocolor[i].c = new UnityEngine.Color(255f / 255f, 255f / 255f, 255f / 255f);
                }
            }
            //キャラクターの色を変更
            Renderer renderer = GetComponent<Renderer>();
            Material[] mats = renderer.materials;
            //2色について、それぞれの割合から色の配分を決めて、色を足し合わせる。
            UnityEngine.Color c = twocolor[0].c * (twocolor[0].ratio/(twocolor[0].ratio + twocolor[1].ratio)) + twocolor[1].c * (twocolor[1].ratio/(twocolor[0].ratio + twocolor[1].ratio));
            mats[2].color = c;
            renderer.materials = mats;
        }
        for(int j = 0; j<11; j++){
            color[j].price = 0;//初期値に戻す
        }
    }
}
