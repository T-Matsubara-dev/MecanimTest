using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {

    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;

    //ジャンプするための力
    private float upForce = 110.0f;


    private float inputHorizontal;
    private float inputVertical;
    private bool  inputSpace;
    private float moveSpeed = 5.0f;

    private int layerMask = 1 << 8;


    // Use this for initialization
    void Start () {
        
        //接地判定用のマスクを設定
        layerMask = ~layerMask;

        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputSpace = Input.GetKeyDown(KeyCode.Space);
     
    }

    void FixedUpdate()
    {
        
        
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

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        myRigidbody.velocity = moveForward * moveSpeed + new Vector3(0, myRigidbody.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }


        // ジャンプしていない時にスペースが押されたらジャンプする（追加）
        if (inputSpace && Physics.CheckSphere(this.transform.position,0.2f,layerMask))
        {
            

            //ジャンプアニメを再生（追加）
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える（追加）
            //this.myAnimator.SetFloat("JumpHeight", 1.0f);
            this.myRigidbody.AddForce(this.transform.up * this.upForce * (Mathf.Abs(inputVertical) * 8.0f));

        }
        else
        {
            this.myAnimator.SetBool("Jump", false);
        }        
    }
}
