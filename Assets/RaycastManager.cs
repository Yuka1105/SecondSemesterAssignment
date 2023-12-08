using System.Collections;
using System.Collections.Generic; // リスト
using UnityEngine;
using System; // DateTime
using System.IO; // セーブとロード
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wrapper
{
   public List<SaveData> List;
}

[System.Serializable]
public class SaveData // セーブデータ用のクラス
{
    public int month; // 買った月
    public int day; // 買った日
    public string food; // 買った食べ物
    public string color; // 買った食べ物の色
    public int price; // 値段
}
public class RaycastManager : MonoBehaviour
{
    // 食べ物を掴んで放す
    Plane plane;   
    bool isGrabbing;
    Transform cube;
    GameObject movedFood;
    string tag = "";

    public bool ate = false; // 食べ物を食べたかどうか（CharacterAnimater.csで使用）
    private static RaycastManager instance = null;

    // GameControllerインスタンスのプロパティーは、実体が存在しないとき（＝初回参照時）実体を探して登録
    public static RaycastManager Instance => instance
        ?? ( instance = GameObject.FindWithTag ( "RaycastManager" ).GetComponent<RaycastManager> () );
    void Awake()
    {
        // インスタンスが複数存在するなら自らを破棄
        if ( this != Instance )
        {
            Destroy ( this.gameObject );
            return;
        }

        // 唯一のインスタンスならシーン遷移しても残す
        DontDestroyOnLoad ( this.gameObject );
    }
    private void OnDestroy ()
    {
        // 破棄時に、登録した実体の解除を行う
        if ( this == Instance ) instance = null;
    }
    GameObject FoodProvider;
    FoodProvider script;
    DateTime TodayNow;
    
    void Start()
    {
        // 食べ物を掴んで放す
        plane = new Plane(new Vector3(-3f,-4f,13f), new Vector3(0f,0f,13f),new Vector3(1f,2f,13f));

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
        writer = new StreamWriter(Application.dataPath + "/savedata.json",false); // trueだと追加でfalseだと上書き
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        Debug.Log(jsonstr);
    }

    public Wrapper Load(){
        if(File.Exists(Application.dataPath + "/savedata.json")){
            string datastr = ""; // json形式のデータを格納
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/savedata.json");
            datastr = reader.ReadToEnd();
            // ファイルにデータが何もなかった場合
            if(datastr == ""){
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
                // json形式のデータ(datastr)をWrapperに変えて返す
                return JsonUtility.FromJson<Wrapper>(datastr);
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

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit = new RaycastHit();
            float maxDistance = 10000f;
            bool isHit = Physics.Raycast(ray, out rayHit, maxDistance);
            if (isHit)
            {
                FoodProvider = GameObject.Find("FoodProvider"); 
                script = FoodProvider.GetComponent<FoodProvider>();
                tag = rayHit.collider.gameObject.tag; // ヒットしたオブジェクトのタグ名を取得
                movedFood = rayHit.collider.gameObject;
                if (tag == "red" || tag == "orange" || tag == "yellow" || tag == "green" || tag == "blue"|| tag == "purple" || tag == "pink" || tag == "brown" || tag == "black" || tag == "gray" || tag == "white")//食べ物にヒットした場合のみ
                {   
                    if(ate == false){
                        // 食べ物を掴んで放す
                        isGrabbing = true;
                        cube = rayHit.transform;
                    }
                }
                // 食べ物が画面上にない場合、食べている最中ではないときのみ図鑑とカレンダーを押すことができる
                if(script.food_num == 0 && ate == false){
                    if(tag == "book"){
                        SceneManager.LoadScene("Book");
                    }
                    if(tag == "calendar"){
                        SceneManager.LoadScene("Calendar");
                    }
                }
            }
        }
        // 食べ物を掴んで放す
        if (isGrabbing)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            plane.Raycast(ray, out rayDistance);
            cube.position = ray.GetPoint(rayDistance);

            // 食べ物を放したとき
            if (Input.GetMouseButtonUp(0))
            {
                // 画面外なら中央に食べ物を戻す
                if(movedFood.transform.position.x < -4 || movedFood.transform.position.x > 4 || movedFood.transform.position.y > -1.5  || movedFood.transform.position.y < -12.5){
                    cube.position =  new Vector3(-0f, -10f, 13f);
                }
                // 口のあたりに食べ物を話した場合、画面上から食べ物を消してセーブ
                if(movedFood.transform.position.x < 1.5 && movedFood.transform.position.x > -1.5 && movedFood.transform.position.y > -9  && movedFood.transform.position.y < -7){
                    ate = true;
                    TodayNow = DateTime.Now;
                    // セーブ
                    SaveData saveData = new SaveData();
                    saveData.month = TodayNow.Month;
                    saveData.day = TodayNow.Day;
                    saveData.food = movedFood.name;
                    int length = saveData.food.Length; // (Clone)という文字を含めた食べ物の名前の長さを読み取る
                    saveData.food = saveData.food.Remove(length-7); // lenght-7が食べ物の名前の長さ。末尾の(Clone)の文字を消して再代入
                    saveData.color = tag;
                    for(int i = 0; i < script.script.ObjCount; i++){
                        if(movedFood == script.food[i].insta_obj){
                            saveData.price = script.food[i].food_price;
                        }
                    }
                    Wrapper wrapper = new Wrapper();
                    wrapper.List = new List<SaveData>();
                    wrapper = Load();
                    wrapper.List.Add(saveData);
                    Save(wrapper);
                    script.food_num --;
                    Destroy(movedFood); // 食べ物を消す
                }
                isGrabbing = false;
            }
        }
    }
}
