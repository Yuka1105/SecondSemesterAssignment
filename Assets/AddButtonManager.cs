using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddButtonManager : MonoBehaviour
{
    GameObject FoodProvider;
    FoodProvider script;

    GameObject RaycastManager;
    RaycastManager script2;
    
    void Start()
    {
        FoodProvider = GameObject.Find("FoodProvider"); 
        script = FoodProvider.GetComponent<FoodProvider>(); 

        RaycastManager = GameObject.Find("RaycastManager"); 
        script2 = RaycastManager.GetComponent<RaycastManager>();   
    }

    public void AddButtonOnClick(){
        // 食べ物が画面上にない場合、食べている最中ではないのみ、Addボタンを押せる
        if(script.food_num == 0 && script2.ate == false){
            SceneManager.LoadScene("Add");
            GameObject g = GameObject.Find("CanvasContainer").transform.Find("Canvas").gameObject;
            g.SetActive(true);
        }
    }
}
