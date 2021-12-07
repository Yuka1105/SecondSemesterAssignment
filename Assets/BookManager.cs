using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    GameObject RaycastManager;
    RaycastManager script;
    // Start is called before the first frame update
    void Start()
    {
     RaycastManager = GameObject.Find("RaycastManager"); 
     script = RaycastManager.GetComponent<RaycastManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        int RiceCounter = 0;
        Wrapper wrapper = new Wrapper();
        wrapper.List = new List<SaveData>();
        wrapper = script.Load();
        for(int i=0; i< wrapper.List.Count; i++ ){
            if(wrapper.List[i].food == "米"){
                RiceCounter ++;
            }
            //Debug.Log(wrapper.List[i].month + "月" + wrapper.List[i].day + "日" + wrapper.List[i].food + "を登録"　+ wrapper.List[i].color + "色" + wrapper.List[i].price + "円");
        }
        Debug.Log(RiceCounter);
        //Debug.Log(JsonUtility.ToJson(wrapper)); 
    }
}
