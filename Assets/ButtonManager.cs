using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool load_scene = false;

    public int ObjCount = 0;
    public GameObject content;

    public GameObject panel_category;

    //カテゴリパネルボタン
    public GameObject panel_koku;
    public GameObject panel_niku;
    public GameObject panel_gyokai;
    public GameObject panel_mame;
    public GameObject panel_tamago;
    public GameObject panel_gyunyu;
    public GameObject panel_ryoku;
    public GameObject panel_tan;
    public GameObject panel_kinoko;
    public GameObject panel_imo;
    public GameObject panel_kaiso;
    public GameObject panel_kudamono;
    public GameObject panel_shiko;

    public GameObject panel_price;

    GameObject panel;
    public string food_name = "nothing";

    public GameObject panel_food_price;

    //最終的にリストに追加されている食材の名前を入れる配列
    public string[] food_name_end = new string[50];

    GameObject InputManager;
    InputManager script;

    public GameObject canvas;

    // GameControllerインスタンスの実体
    private static ButtonManager instance = null;

    // GameControllerインスタンスのプロパティーは、実体が存在しないとき（＝初回参照時）実体を探して登録する
    public static ButtonManager Instance => instance
        ?? ( instance = GameObject.FindWithTag ( "ButtonManager" ).GetComponent<ButtonManager> () );
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
    void Update(){
      InputManager = GameObject.Find("InputManager");
      script = InputManager.GetComponent<InputManager>();
    }
    public void Button(string button){
        switch(button){
            //カテゴリー14個分のボタン
            case "koku":
                panel_category.SetActive(false);
                panel_koku.SetActive(true);
                panel = panel_koku;
                break;
            case "niku":
                panel_category.SetActive(false);
                panel_niku.SetActive(true);
                panel = panel_niku;
                break;
            case "gyokai":
                panel_category.SetActive(false);
                panel_gyokai.SetActive(true);
                panel = panel_gyokai;
                break;
            case "mame":
                panel_category.SetActive(false);
                panel_mame.SetActive(true);
                panel = panel_mame;
                break;
            case "tamago":
                panel_category.SetActive(false);
                panel_tamago.SetActive(true);
                panel = panel_tamago;
                break;
            case "gyunyu":
                panel_category.SetActive(false);
                panel_gyunyu.SetActive(true);
                panel = panel_gyunyu;
                break;
            case "ryoku":
                panel_category.SetActive(false);
                panel_ryoku.SetActive(true);
                panel = panel_ryoku;
                break;
            case "tan":
                panel_category.SetActive(false);
                panel_tan.SetActive(true);
                panel = panel_tan;
                break;
            case "kinoko":
                panel_category.SetActive(false);
                panel_kinoko.SetActive(true);
                panel = panel_kinoko;
                break;
            case "imo":
                panel_category.SetActive(false);
                panel_imo.SetActive(true);
                panel = panel_imo;
                break;
            case "kaiso":
                panel_category.SetActive(false);
                panel_kaiso.SetActive(true);
                panel = panel_kaiso;
                break;
            case "kudamono":
                panel_category.SetActive(false);
                panel_kudamono.SetActive(true);
                panel = panel_kudamono;
                break;
            case "shiko":
                panel_category.SetActive(false);
                panel_shiko.SetActive(true);
                panel = panel_shiko;
                break;
            
            //カテゴリーそれぞれに対しての食材ボタン
            case "koku_kome":
                food_name = "米";
                panel_price.SetActive(true);
                break;
            case "koku_pan":
                food_name = "パン";
                panel_price.SetActive(true);
                break;
            case "koku_men":
                food_name = "麺";
                panel_price.SetActive(true);
                break;
            case "koku_mochi":
                food_name = "餅";
                panel_price.SetActive(true);
                break;
            case "niku_ushi":
                food_name = "牛肉";
                panel_price.SetActive(true);
                break;
            case "niku_buta":
                food_name = "豚肉";
                panel_price.SetActive(true);
                break;
            case "niku_tori":
                food_name = "鶏肉";
                panel_price.SetActive(true);
                break;
            case "gyokai_iwashi":
                food_name = "イワシ";
                panel_price.SetActive(true);
                break;
            case "gyokai_tara":
                food_name = "タラ";
                panel_price.SetActive(true);
                break;
            case "gyokai_sake":
                food_name = "サケ";
                panel_price.SetActive(true);
                break;
            case "mame_daizu":
                food_name = "大豆";
                panel_price.SetActive(true);
                break;
            case "mame_tofu":
                food_name = "豆腐";
                panel_price.SetActive(true);
                break;
            case "mame_natto":
                food_name = "納豆";
                panel_price.SetActive(true);
                break;
            case "tamago_tamago":
                food_name = "卵";
                panel_price.SetActive(true);
                break;
            case "gyunyu_gyunyu":
                food_name = "牛乳";
                panel_price.SetActive(true);
                break;
            case "gyunyu_chizu":
                food_name = "チーズ";
                panel_price.SetActive(true);
                break;
            case "gyunyu_yoguruto":
                food_name = "ヨーグルト";
                panel_price.SetActive(true);
                break;
            case "ryoku_tomato":
                food_name = "トマト";
                panel_price.SetActive(true);
                break;
            case "ryoku_ninjin":
                food_name = "にんじん";
                panel_price.SetActive(true);
                break;
            case "ryoku_burokkori":
                food_name = "ブロッコリー";
                panel_price.SetActive(true);
                break;
            case "tan_hakusai":
                food_name = "白菜";
                panel_price.SetActive(true);
                break;
            case "tan_tamanegi":
                food_name = "玉ねぎ";
                panel_price.SetActive(true);
                break;
            case "kinoko_shimeji":
                food_name = "しめじ";
                panel_price.SetActive(true);
                break;
            case "kinoko_maitake":
                food_name = "マイタケ";
                panel_price.SetActive(true);
                break;
            case "kinoko_enoki":
                food_name = "えのき";
                panel_price.SetActive(true);
                break;
            case "tan_kyabetsu":
                food_name = "キャベツ";
                panel_price.SetActive(true);
                break;
            case "imo_jagaimo":
                food_name = "ジャガイモ";
                panel_price.SetActive(true);
                break;
            case "imo_satsumaimo":
                food_name = "さつまいも";
                panel_price.SetActive(true);
                break;
            case "imo_konnyaku":
                food_name = "こんにゃく";
                panel_price.SetActive(true);
                break;
            case "kaiso_wakame":
                food_name = "ワカメ";
                panel_price.SetActive(true);
                break;
            case "kaiso_hijiki":
                food_name = "ヒジキ";
                panel_price.SetActive(true);
                break;
            case "kudamono_ringo":
                food_name = "りんご";
                panel_price.SetActive(true);
                break;
            case "kudamono_mikan":
                food_name = "みかん";
                panel_price.SetActive(true);
                break;
            case "kudamono_banana":
                food_name = "バナナ";
                panel_price.SetActive(true);
                break;
            case "kudamono_budo":
                food_name = "ぶどう";
                panel_price.SetActive(true);
                break;
            case "kudamono_ichigo":
                food_name = "いちご";
                panel_price.SetActive(true);
                break;
            case "shiko_choko":
                food_name = "チョコレート";
                panel_price.SetActive(true);
                break;
            case "shiko_cookie":
                food_name = "クッキー";
                panel_price.SetActive(true);
                break;
            case "shiko_cake":
                food_name = "ケーキ";
                panel_price.SetActive(true);
                break;
            case "shiko_alcohol":
                food_name = "アルコール";
                panel_price.SetActive(true);
                break;
            //値段入力OKボタン
            case "ok":
                if(int.Parse(script.inputField.text) >= 1 && int.Parse(script.inputField.text) <= 50000){//値段が1円以上なら実行する
                    //小さいパネルを上部のリストに追加
                    GameObject cloneObject =Instantiate(panel_food_price, new Vector3( -1.0f, 0.0f, 0.0f), Quaternion.identity);
                    cloneObject.transform.parent = content.transform;
                    cloneObject.name = food_name;
                    Text food_text = cloneObject.transform.GetChild(0).GetComponent<Text>();
                    food_text.text = food_name;
                    Text price_text = cloneObject.transform.GetChild(1).GetComponent<Text>();
                    price_text.text = script.inputField.text + "円";
                    //値段の入力フォームの値を削除
                    script.inputField.text="";
                    panel_category.SetActive(true);
                    panel.SetActive(false);
                    panel_price.SetActive(false);
                }
                else{
                    Debug.Log("正しく値段を入力してください");
                }
                break;

            case "cancel":
                script.inputField.text="";
                panel_category.SetActive(true);
                panel.SetActive(false);
                panel_price.SetActive(false);
                break;
            
            case "confirm":
                //追加されている食材の数を数える
                ObjCount = content.transform.childCount;
                for(int i = 0; i < ObjCount; i++){
                    food_name_end[i] = content.transform.GetChild(i).gameObject.name;//配列に食材名を保存
                }
                SceneManager.LoadScene("Home");
                canvas.SetActive(false);
                load_scene = true;
                foreach(Transform child in content.transform){
                    Destroy(child.gameObject);
                }
                break;
                
            case "return":
                SceneManager.LoadScene("Home");
                canvas.SetActive(false);
                foreach(Transform child in content.transform){
                    Destroy(child.gameObject);
                }
                break;

            default:
                break;
        }
    }
}
