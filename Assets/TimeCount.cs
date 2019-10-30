using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

    private float countTime = 0.0f;

    

    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        //ゲーム終了時に表示するテキスト（追加）
        GameObject stateText;
        //シーン中のstateTextオブジェクトを取得（追加）
        stateText = GameObject.Find("GameOverText");

        if (stateText.GetComponent<Text>().text == "")
        {            
            countTime += Time.deltaTime; //スタートしてからの秒数を格納
            this.GetComponent<Text>().text = ("Time Limit  " + (200.0f - countTime).ToString("F2") + " sec"); //小数2桁にして表示
        }
        else
        {
            return;
        }
        

    }
}
