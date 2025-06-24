using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Carmover : MonoBehaviour
{
[SerializeField] private float carSpeed;
KarakterKontrol karakterKontrol;
    void Start()
    {
        karakterKontrol= FindObjectOfType<KarakterKontrol>();
    }
    void Update()
    {
        if(karakterKontrol.isAlive){
        transform.Translate(0,0,carSpeed);

        }
    }

}
