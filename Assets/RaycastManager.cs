using System.Collections;
using System.Collections.Generic;//リストを使うとき
using UnityEngine;
using System; //DateTimeを使用する為追加。
using System.IO;//セーブとロード

[System.Serializable]
public class Wrapper
{
   public List<SaveData> List;
}

[System.Serializable]
public class SaveData //セーブデータ用のクラス
{
    public int month;//買った月
    public int day;//買った日
    public string food;//買った食べ物
    public string color;//買った食べ物の色
    //価格、カテゴリも追加したい
}
public class RaycastManager : MonoBehaviour
{
    //DateTimeを使うため変数を設定
    DateTime TodayNow;
    // Start is called before the first frame update
    void Start()
    {
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        wrapper = Load();
        for(int i=0; i< wrapper.List.Count; i++ ){
            Debug.Log(wrapper.List[i].month);
            Debug.Log(wrapper.List[i].day);
            Debug.Log(wrapper.List[i].food);
            Debug.Log(wrapper.List[i].color);
        }
    }

    public void Save(Wrapper wrapper)
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(wrapper);
        writer = new StreamWriter(Application.dataPath + "/savedata.json",false);//trueだと追加で、falseだと上書き
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        Debug.Log(jsonstr);
    }

    public Wrapper Load(){
        if(File.Exists(Application.dataPath + "/savedata.json")){
            string datastr = "";//json形式のデータを格納するためのstring変数
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/savedata.json");
            datastr = reader.ReadToEnd();
            reader.Close();
            return JsonUtility.FromJson<Wrapper>(datastr);//json形式のデータ(datastr)をWrapperに変えてリターンしている。
        }
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        SaveData saveData = new SaveData();
        wrapper.List.Add(saveData);
        wrapper.List[0].month = 0;
        wrapper.List[0].day = 0;
        wrapper.List[0].food = "";
        wrapper.List[0].color = "";
        return wrapper;
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
                    saveData.month = TodayNow.Month;
                    saveData.day = TodayNow.Day;
                    saveData.food = rayHit.collider.gameObject.name;
                    int length = saveData.food.Length;//(Clone)という文字を含めた、食べ物の名前の長さを読み取る
                    saveData.food = saveData.food.Remove(length-7);//lenght-7が食べ物の名前の長さ。末尾の(Clone)の文字を消して再代入
                    saveData.color = tag;
                    Wrapper wrapper = new Wrapper();
                    wrapper.List = new List<SaveData>();
                    wrapper = Load();
                    wrapper.List.Add(saveData);
                    Save(wrapper);
                    Destroy(rayHit.collider.gameObject);//食べ物を消す
                }
            }
        }
    }

    public void PushLoadButton(){//アプリを開いたらすぐ押す（仮）
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        wrapper = Load();
        for(int i=0; i< wrapper.List.Count; i++ ){
            Debug.Log(wrapper.List[i].month);
            Debug.Log(wrapper.List[i].day);
            Debug.Log(wrapper.List[i].food);
            Debug.Log(wrapper.List[i].color);
        }
    }
}
