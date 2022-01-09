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
    // Start is called before the first frame update
    void Start()
    {
        FoodProvider = GameObject.Find("FoodProvider"); 
        script = FoodProvider.GetComponent<FoodProvider>(); 

        RaycastManager = GameObject.Find("RaycastManager"); 
        script2 = RaycastManager.GetComponent<RaycastManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddButtonOnClick(){
        if(script.food_num == 0 && script2.ate == false){//食べ物が画面上にない場合、食べている最中ではないのみ、Addボタンを押せる
            SceneManager.LoadScene("Add");
            GameObject g = GameObject.Find("CanvasContainer").transform.Find("Canvas").gameObject;
            g.SetActive(true);
        }
    }
}
