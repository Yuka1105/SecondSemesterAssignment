﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasContainer : MonoBehaviour
{
    private static CanvasContainer instance = null;
    public static CanvasContainer Instance => instance
        ?? ( instance = GameObject.FindWithTag ( "CanvasContainer" ).GetComponent<CanvasContainer> () );
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

}
