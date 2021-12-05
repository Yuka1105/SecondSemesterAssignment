using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddButtonOnClick(){
       SceneManager.LoadScene("Add");
       GameObject g = GameObject.Find("CanvasContainer").transform.Find("Canvas").gameObject;
       g.SetActive(true);
    }
}
