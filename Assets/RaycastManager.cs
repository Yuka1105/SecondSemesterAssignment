using System.Collections;
using System.Collections.Generic;//リストを使うとき
using UnityEngine;
using System; //DateTimeを使用する為追加。
using System.IO;//セーブとロード
using System.Text.Json;
using System.Text.Json.Serialization;


[SerializeField] List<SaveData> List = new List<SaveData>();//SaveDataを管理するリスト

[System.Serializable]
public class SaveData //セーブデータ用のクラス
{
    [JsonPropertyName("month")] public int Month{ get; set; }
    [JsonPropertyName("day")] public int Day { get; set; }
    [JsonPropertyName("food")] public string Food { get; set; }
    [JsonPropertyName("color")] public string Color { get; set; }
//     public int month;//買った月
//     public int day;//買った日
//     public string food;//買った食べ物
//     public string color;//買った食べ物の色
//     //価格、カテゴリも追加したい
}

public class RaycastManager : MonoBehaviour
{
    //DateTimeを使うため変数を設定
    DateTime TodayNow;
    // Start is called before the first frame update
    void Start()
    {
        List = Load();
        List.ForEach(data =>
        {
        Debug.Log(data.Month + "," + data.Day + "," + data.Food + "," + data.Color);
        });
    }

    public void Save(List<SaveData> List)
    {
        StreamWriter writer;
        string jsonstr = JsonSerializer.Serialize(List);
        writer = new StreamWriter(Application.dataPath + "/savedata.json",false);//trueだと追加で、falseだと上書き
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        Debug.Log(jsonstr);
    }

    public List<SaveData> Load(){
        if(File.Exists(Application.dataPath + "/savedata.json")){
            string datastr = "";//json形式のデータを格納するためのstring変数
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/savedata.json");
            datastr = reader.ReadToEnd();
            reader.Close();
            //return JsonUtility.FromJson<List<SaveData>>(datastr);//json形式のデータ(datastr)をList<SaveData>に変えてリターンしている。
            return JsonSerializer.Deserialize<List<SaveData>>(datastr);//json形式のデータ(datastr)をList<SaveData>に変えてリターンしている。
        }
        List<SaveData> list;
        list.Add(new SaveData { Month = 0, Day = 0, Food = " ", Color = " " });
        //SaveData saveData = new SaveData();
        // list.add(saveData);
        // list.get(0).month = 0;
        // list.get(0).day = 0;
        // list.get(0).food = "";
        // list.get(0).color = "";
        return list;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit = new RaycastHit();
            float maxDistance = 100f;
            bool isHit = Physics.Raycast(ray, out rayHit, maxDistance);
            if (isHit)
            {
                string tag = rayHit.collider.gameObject.tag;//ヒットしたオブジェクトのタグ名を取得
                Debug.Log(tag);
                if (tag == "red" || tag == "orange" || tag == "green")//食べ物にヒットした場合のみ
                {
                    TodayNow = DateTime.Now; //時間を取得
                    SaveData saveData = new SaveData();
                    //saveData.month = TodayNow.Month;
                    //saveData.day = TodayNow.Day;
                    // saveData.food = rayHit.collider.gameObject.name;
                    // int length = saveData.food.Length;//(Clone)という文字を含めた、食べ物の名前の長さを読み取る
                    // saveData.food = saveData.food.Remove(length-7);//lenght-7が食べ物の名前の長さ。末尾の(Clone)の文字を消して再代入
                    //saveData.color = tag;
                    string f_n = rayHit.collider.gameObject.name;
                    int length = f_n.Length;//(Clone)という文字を含めた、食べ物の名前の長さを読み取る
                    f_n = f_n.Remove(length-7);//lenght-7が食べ物の名前の長さ。末尾の(Clone)の文字を消して再代入

                    List.Add(new SaveData { Month = TodayNow.Month, Day =TodayNow.Day, Food = f_n, Color = tag });
                    //List.Add(saveData);
                    Save(List);
                    Destroy(rayHit.collider.gameObject);//食べ物を消す
                }
            }
        }
    }

    public void PushLoadButton(){//アプリを開いたらすぐ押す（仮）
        List = Load();
        List.ForEach(data =>
        {
        Debug.Log(data.Month + "," + data.Day + "," + data.Food + "," + data.Color);
        });
    }
}
