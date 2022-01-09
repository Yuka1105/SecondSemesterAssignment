using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimater : MonoBehaviour
{
    GameObject RaycastManager;
    RaycastManager script;
    int velocity = -1;//首を傾ける方向（横振り）
    int velocity2 = 1;//首を傾ける方向（食べる）
    int count  = 0;//食べるアニメーションで何回頷いたか

    bool eating = false;
    // Start is called before the first frame update
    void Start()
    {
     RaycastManager = GameObject.Find("RaycastManager"); 
     script = RaycastManager.GetComponent<RaycastManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(script.ate == false){
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
        else if(script.ate == true){
            //食べるアニメーション
            float z = GetComponent<Transform>().rotation.z;
            this.transform.Rotate(new Vector3(0, 0, velocity2 * 1.20f));
            if(count == 0){
                if(z < 0.49f){ //初期値0.53から0.49まで減らす
                    velocity2 = -1;//なぜかこれで増える
                }
                else if(z > 0.58f){
                    velocity2 = 1;
                    count = 1;
                }
            }
            else if(count == 1){
                if(z < 0.45f){ //0.58から0.45まで減らす
                    velocity2 = -1;//なぜかこれで増える
                    count = 2;
                }
            }
            else if(count == 2){
                if(z > 0.57f){
                    //初期化
                    velocity2 = 1;
                    count = 0;
                    script.ate = false;
                }
            }
        }
    }
}
