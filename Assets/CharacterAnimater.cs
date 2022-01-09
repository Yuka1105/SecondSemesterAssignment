using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimater : MonoBehaviour
{
    int velocity = -1;//首を傾ける方向  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //平常時に首を傾けるアニメーション
        float y = GetComponent<Transform>().rotation.y;
        this.transform.Rotate(new Vector3(0, velocity * 0.150f, 0));
        if(y < 0.43f){
            velocity = -1;
        }
        else if(y > 0.50f){
            velocity = 1;
        }
    
    }
}
