using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static Vector3 CameraStartPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // ボタンをクリックするとBattleSceneに移動します
    public void TPSButtonClicked()
    {
       
       CameraStartPos = new Vector3(-6, 15, 0);
       SceneManager.LoadScene("GameScene");

    }
    public void FPSButtonClicked()
    {

        CameraStartPos = new Vector3(2, 14, 0);
        SceneManager.LoadScene("GameScene");

    }



    

    public static Vector3 GetCameraStartPos()
    {
        return CameraStartPos;
    }
}
