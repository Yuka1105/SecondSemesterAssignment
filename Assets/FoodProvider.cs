using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Food{//上段のリストに追加された食材名と値段を格納するクラス
    public GameObject insta_obj;
    public int food_price;
}

public class FoodProvider : MonoBehaviour
{
    public int food_num = 0;//食べ物を食べ切るまで他のボタンを押せないようにするシステムを作るのに必要な変数
    public Food[] food = new Food[50];
    GameObject ButtonManager;
    public ButtonManager script;
    // Start is called before the first frame update
    void Start()
    {
     for(int i =0; i<50; i++){
         food[i].insta_obj = new GameObject();
         food[i].food_price = 0;
     }
     ButtonManager = GameObject.Find ("ButtonManager");   
    }

    // Update is called once per frame
    void Update()
    {
     script = ButtonManager.GetComponent<ButtonManager>();
     if(script.load_scene == true){
        for(int i = 0; i < script.ObjCount; i++){
            GameObject obj = (GameObject)Resources.Load(script.food_name_end[i]);
            //画面上にリストにある食べ物を出現させる
            food_num ++; //画面上にいくつ食べ物があるか数える
            food[i].insta_obj = Instantiate(obj, new Vector3(-2.0f + i * 1.2f, -10.0f, 13.0f), Quaternion.identity);
            food[i].food_price = script.food_price_end[i];
        }
        script.load_scene = false;
     }
     Debug.Log(food_num);
    }
    
}
