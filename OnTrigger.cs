using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public GameObject Bteam;
    public Transform RteamSt;
    //OnTriggerEnter関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグ
        if (other.CompareTag("blue_attack"))
        {
            //オブジェクトの色を赤に変更する
            GetComponent<Renderer>().material.color = Color.blue;
            this.tag = "B_target";
        }

        if (other.CompareTag("red_attack"))
        {
            //オブジェクトの色を赤に変更する
            GetComponent<Renderer>().material.color = Color.red;
            this.tag = "R_target";
        }
    }

}