using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //食材名と値段が書かれた小さいパネルを消すボタン関数
    public void DeleteButtonOnClick(){
        Destroy(this.gameObject);
    }
}
