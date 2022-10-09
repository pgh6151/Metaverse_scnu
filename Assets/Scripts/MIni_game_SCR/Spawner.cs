using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Rock;
    Vector3 RanSpawner;
    int routineControll =1;
    
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {   
        RanSpawner = new Vector3(Random.Range(-7, 4), 1.18f, 44.93f);


        if(MIniGamemanager.Instance.ST && routineControll == 1)
        {
            StartCoroutine("InstRock");
            routineControll--;
        }else if (MIniGamemanager.Instance.ST == false)
        {
            StopCoroutine("InstRock");
            routineControll = 1;
        }
    }

    IEnumerator InstRock()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(Rock, RanSpawner, gameObject.transform.rotation);
            
        }
        

    }
        
        
}
