using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 소환즉시 그냥 바로 날아올 수 있게
    void Update()
    {
        transform.Translate(Vector3.back * 0.1f);


    }

    // 물체에 닿았을때 끝에 닿으면 삭제
    // 플레이어에 닿으면 플레이어를 삭제
    // MiniGamemanager의 ST 변수를 falas로 바꿔줌
    // 플레이어가 죽으면 RestartCanv 활성화
    
    private void OnTriggerEnter(Collider other) {


        if(other.gameObject.tag == "End")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            var mIniGamemanager = GameObject.Find("MiniGamemanager").GetComponent<MIniGamemanager>();
            mIniGamemanager.ReStartCanv.SetActive(true);
            mIniGamemanager.ST = false;
        }
    }

}
