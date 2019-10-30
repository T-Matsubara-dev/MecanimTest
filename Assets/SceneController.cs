using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //（追加）

public class SceneController : MonoBehaviour {

    //ゲーム終了時に表示するテキスト（追加）
    private GameObject stateText;

    // Use this for initialization
    void Start () {
        //シーン中のstateTextオブジェクトを取得（追加）
        this.stateText = GameObject.Find("GameOverText");
    }
	
	// Update is called once per frame
	void Update () {
		if (this.stateText.GetComponent<Text>().text.Length != 0)
        {
            // クリックされたらシーンをロードする（追加）
            if (Input.GetMouseButtonDown(0))
            {
                //GameSceneを読み込む（追加）
                SceneManager.LoadScene("StartScene");
            }
        }

    }  
}
