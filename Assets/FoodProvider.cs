using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 上段リストに追加された食べ物の名前と値段を格納するクラス
[System.Serializable]
public class Food{
    public GameObject insta_obj;
    public int food_price;
}

public class FoodProvider : MonoBehaviour
{
    public int food_num = 0; // 画面上の食べ物の数
    public Food[] food = new Food[50];
    GameObject ButtonManager;
    public ButtonManager script;
    
    void Start()
    {
     for(int i =0; i<50; i++){
         food[i].insta_obj = new GameObject();
         food[i].food_price = 0;
     }
     ButtonManager = GameObject.Find ("ButtonManager");   
    }

    void Update()
    {
     script = ButtonManager.GetComponent<ButtonManager>();
     if(script.load_scene == true){
        for(int i = 0; i < script.ObjCount; i++){
            // 上段リストの食べ物を出現させる
            GameObject obj = (GameObject)Resources.Load(script.food_name_end[i]);
            food_num ++;
            food[i].insta_obj = Instantiate(obj, new Vector3(-3.0f + i * 1.5f, -10.0f, 13.0f), Quaternion.identity);
            food[i].food_price = script.food_price_end[i];
        }
        script.load_scene = false;
     }
     Debug.Log(food_num);
    }
    
}
