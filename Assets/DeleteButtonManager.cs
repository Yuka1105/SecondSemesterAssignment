using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButtonManager : MonoBehaviour
{
    // 食べ物の名前と値段が書かれた小さいパネルを消すボタン関数
    public void DeleteButtonOnClick(){
        Destroy(this.gameObject);
    }
}
