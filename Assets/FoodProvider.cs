using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProvider : MonoBehaviour
{
    GameObject[] insta_obj = new GameObject[50];
    GameObject ButtonManager;
    ButtonManager script;
    // Start is called before the first frame update
    void Start()
    {
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
            insta_obj[i] = Instantiate(obj, new Vector3(-1.0f + i * 2.0f, 0.0f, 0.0f), Quaternion.identity);
        }
        script.load_scene = false;
     }
    }
}
