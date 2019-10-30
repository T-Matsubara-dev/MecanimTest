using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;

    //ジャンプするための力
    private float upForce = 110.0f;

    //ユーザ入力を受け取るプロパティ
    private float inputHorizontal;
    private float inputVertical;
    private bool  inputSpace;

    //オブジェクトの速度
    private float moveSpeed = 5.0f;

    //接地判定用のマスク
    private int layerMask = 1 << 8;

    //ゲームオーバー時のオフセット時間
    private float TimeCount;

    //ゲーム終了判定フラグ
    private bool Endflag = false;
    private bool Clearflag = false;

    //ゲーム終了時に表示するテキスト（追加）
    private GameObject stateText;

    //スコアを表示するテキスト（追加）
    private GameObject scoreText;

    //スコア
    private int score = 0;

    private Vector3 normalVector = Vector3.zero;


    // Use this for initialization
    void Start () {
        
        //接地判定用のマスクを設定
        layerMask = ~layerMask;

        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();
        //シーン中のstateTextオブジェクトを取得（追加）
        this.stateText = GameObject.Find("GameOverText");

        //ゲーム終了までの時間カウントを初期化
        this.TimeCount = 0.0f;

        //シーン中のscoreTextオブジェクトを取得（追加）
        this.scoreText = GameObject.Find("PointText");

    }
	
	// Update is called once per frame
	void Update () {

        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputSpace = Input.GetKeyDown(KeyCode.Space);

        if ( this.Endflag == true)
        {
            
            //パーティクルを再生（追加）
            GetComponent<ParticleSystem>().Play();
            this.stateText.GetComponent<Text>().text = "GAME OVER\n左クリックでリスタート";
            myRigidbody.velocity = new Vector3(0, 0, 0); 
            this.TimeCount += Time.deltaTime;
           
        }

        else if( this.Clearflag == true)
        {
            this.stateText.GetComponent<Text>().text = "GAME CLEAR\n左クリックでリスタート";
            myRigidbody.velocity = new Vector3(0, 0, 0);

        }

        if (this.TimeCount > 0.5f)
        {
            Destroy(this.gameObject);
        }    

    }

    void FixedUpdate()
    {

        //ゲーム終了判定
        if (this.transform.position.y < 2.0f)
        {
            this.Endflag = true;
        }


        //ユーザ入力によりアニメーションを変化
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            this.myAnimator.SetFloat("Speed", 1);
        }
        else
        {
            this.myAnimator.SetFloat("Speed", 0);
        }

        

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        Vector3 onPlane = Vector3.ProjectOnPlane(moveForward, normalVector);

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        myRigidbody.velocity = onPlane * moveSpeed + new Vector3(0, myRigidbody.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (onPlane != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(onPlane);
        }


        // ジャンプしていない時にスペースが押されたらジャンプする
        if (inputSpace && Physics.CheckSphere(this.transform.position,0.2f,layerMask))
        {

            //ジャンプアニメを再生（追加）
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える（追加）
            this.myRigidbody.AddForce(this.transform.up * this.upForce * (Mathf.Abs(inputVertical) * 8.0f));

        }
        else
        {
            this.myAnimator.SetBool("Jump", false);
        }


        if (this.transform.position.y < 2.0f || this.Endflag == true)
        {
            myRigidbody.velocity = new Vector3(0, 0, 0);          
        }

        
    }


    void OnTriggerEnter(Collider other)
    {
        //コインに衝突した場合
        if (other.gameObject.tag == "CoinTag")
        {

            //パーティクルを再生（追加）
            GetComponent<ParticleSystem>().Play();

            // スコアを加算(追加)
            this.score += 50;

            //ScoreText獲得した点数を表示(追加)
            this.scoreText.GetComponent<Text>().text = "Point" + String.Format("{0,7}",this.score.ToString()) + " pt";


            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Column" || other.gameObject.tag == "Grass" || other.gameObject.tag == "Road" || other.gameObject.tag == "Block")
        {
            normalVector = other.contacts[0].normal;
        }

        //敵に衝突した場合
        else if (other.gameObject.tag == "Eggy" || other.gameObject.tag == "Kiwi")
        {
            if (this.gameObject.transform.position.y > other.gameObject.transform.position.y + 1.0f)
            {

                Destroy(other.gameObject);
            }
            else
            {
                this.Endflag = true;
            }   
        }

        else if (other.gameObject.tag == "GoalSphere")
        {
            // スコアを加算(追加)
            this.score += 5000;

            //ScoreText獲得した点数を表示(追加)
            this.scoreText.GetComponent<Text>().text = "Point" + String.Format("{0,7}", this.score.ToString()) + " pt";

            this.Clearflag = true;
        }
        else if (other.gameObject.tag == "GoalBar")
        {
            // スコアを加算(追加)
            this.score += (int)(this.transform.position.y * 100.0f);

            //ScoreText獲得した点数を表示(追加)
            this.scoreText.GetComponent<Text>().text = "Point" + String.Format("{0,7}", this.score.ToString()) + " pt";

            this.Clearflag = true;
        }
    }
}
