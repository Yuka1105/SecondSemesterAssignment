using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
  private static InputManager instance = null;
  public static InputManager Instance => instance
        ?? ( instance = GameObject.FindWithTag ( "InputManager" ).GetComponent<InputManager> () );
    // 値段入力
    public InputField inputField;
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
    void Start()
    {
      inputField = inputField.GetComponent<InputField> ();
    }
}
