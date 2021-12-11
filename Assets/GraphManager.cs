using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour
{
    GameObject RaycastManager;
    RaycastManager script;
    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
     RaycastManager = GameObject.Find("RaycastManager"); 
     script = RaycastManager.GetComponent<RaycastManager>();

     List<int> month_value = new List<int>();//月の値を保存するためのList

     //jsonデータ内にある月の値を取得
     Wrapper wrapper = new Wrapper();
     wrapper.List = new List<SaveData>();
     wrapper = script.Load();
     month_value.Add(wrapper.List[0].month);//１番目のデータの月の値をList型month_valueに保存する。
     for(int i=0; i< wrapper.List.Count-1; i++ ){
         if(wrapper.List[i].month != wrapper.List[i+1].month){
             month_value.Add(wrapper.List[i+1].month);//前と後の月の値が違った場合、後の月の値をmonthに追加する。
         }
        }
    //ドロップダウンリストに月を追加
     for(int i=0; i< month_value.Count; i++){
            dropdown.options.Add(new Dropdown.OptionData { text = month_value[i].ToString() + "月"});
     }
     dropdown.RefreshShownValue();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
