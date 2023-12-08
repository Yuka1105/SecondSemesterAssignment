using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasContainer : MonoBehaviour
{
    private static CanvasContainer instance = null;
    public static CanvasContainer Instance => instance
        ?? ( instance = GameObject.FindWithTag ( "CanvasContainer" ).GetComponent<CanvasContainer> () );
    void Awake()
    {
        // インスタンスが複数存在するなら自らを破棄
        if ( this != Instance )
        {
            Destroy ( this.gameObject );
            return;
        }
        // 唯一のインスタンスならシーン遷移しても残す
        DontDestroyOnLoad ( this.gameObject );
    }

    private void OnDestroy ()
    {
        // 破棄時に、登録した実体の解除を行う
        if ( this == Instance ) instance = null;
    }
}
