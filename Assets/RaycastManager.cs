using System.Collections;
using System.Collections.Generic;//リストを使うとき
using UnityEngine;
using System; //DateTimeを使用する為追加。
using System.IO;//セーブとロード
using UnityEngine.SceneManagement;

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
    public int price;//値段
}
public class RaycastManager : MonoBehaviour
{
    private static RaycastManager instance = null;

    // GameControllerインスタンスのプロパティーは、実体が存在しないとき（＝初回参照時）実体を探して登録する
    public static RaycastManager Instance => instance
        ?? ( instance = GameObject.FindWithTag ( "RaycastManager" ).GetComponent<RaycastManager> () );
    void Awake()
    {
        // もしインスタンスが複数存在するなら、自らを破棄する
        if ( this != Instance )
        {
            Destroy ( this.gameObject );
            return;
        }

        // 唯一のインスタンスなら、シーン遷移しても残す
        DontDestroyOnLoad ( this.gameObject );
    }
    private void OnDestroy ()
    {
        // 破棄時に、登録した実体の解除を行う
        if ( this == Instance ) instance = null;
    }
    GameObject FoodProvider;
    FoodProvider script;
    //DateTimeを使うため変数を設定
    DateTime TodayNow;
    // Start is called before the first frame update
    void Start()
    {
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        wrapper = Load();
        for(int i=0; i< wrapper.List.Count; i++ ){
            Debug.Log(wrapper.List[i].month + "月" + wrapper.List[i].day + "日" + wrapper.List[i].food + "を登録"　+ wrapper.List[i].color + "色" + wrapper.List[i].price + "円");
        }
        Debug.Log(JsonUtility.ToJson(wrapper));
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
            if(datastr == ""){//ファイルにデータが何もなかった場合
                reader.Close();
                Wrapper w = new Wrapper();
                w.List = new List<SaveData>();
                SaveData s = new SaveData();
                w.List.Add(s);
                w.List[0].month = 0;
                w.List[0].day = 0;
                w.List[0].food = "";
                w.List[0].color = "";
                w.List[0].price = 0;
                return w; 
            }else{
                reader.Close();
                return JsonUtility.FromJson<Wrapper>(datastr);//json形式のデータ(datastr)をWrapperに変えてリターンしている。
            }
        }
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        SaveData saveData = new SaveData();
        wrapper.List.Add(saveData);
        wrapper.List[0].month = 0;
        wrapper.List[0].day = 0;
        wrapper.List[0].food = "";
        wrapper.List[0].color = "";
        wrapper.List[0].price = 0;
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
                if (tag == "red" || tag == "orange" || tag == "yellow" || tag == "green" || tag == "blue"|| tag == "purple" || tag == "pink" || tag == "brown" || tag == "black" || tag == "gray" || tag == "white")//食べ物にヒットした場合のみ
                {
                    FoodProvider = GameObject.Find("FoodProvider"); 
                    script = FoodProvider.GetComponent<FoodProvider>();
                    TodayNow = DateTime.Now; //時間を取得
                    SaveData saveData = new SaveData();
                    saveData.month = TodayNow.Month;
                    saveData.day = TodayNow.Day;
                    saveData.food = rayHit.collider.gameObject.name;
                    int length = saveData.food.Length;//(Clone)という文字を含めた、食べ物の名前の長さを読み取る
                    saveData.food = saveData.food.Remove(length-7);//lenght-7が食べ物の名前の長さ。末尾の(Clone)の文字を消して再代入
                    saveData.color = tag;
                    for(int i = 0; i < script.script.ObjCount; i++){
                        if(rayHit.collider.gameObject == script.food[i].insta_obj){
                            saveData.price = script.food[i].food_price;
                        }
                    }
                    Wrapper wrapper = new Wrapper();
                    wrapper.List = new List<SaveData>();
                    wrapper = Load();
                    wrapper.List.Add(saveData);
                    Save(wrapper);
                    Destroy(rayHit.collider.gameObject);//食べ物を消す
                }
                if(tag == "book"){
                    SceneManager.LoadScene("Book");
                }
            }
        }
    }
}
