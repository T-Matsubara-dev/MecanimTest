using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : Enemy {

    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //オブジェクトに力を加えるためRigidbody
    private Rigidbody myRigidbody;

    //オブジェクトの生成開始位置(海に落ちた際に再生成するために使用)
    private Vector3 StartPos;

    //向きを変えるまでに歩く距離の基準とするポジション
    private Vector3 UpdatePos;

    //一定時間止まってしまった場合の処理のために、向き反転からの時間経過を格納する
    private float TimeCount;

    public KiwiController(string name) : base(name)
    {

    }

    public override void Attack()
    {
        
    }

    /// <summary>
    /// オブジェクトを移動させる際にコールする関数。
    /// 現状は前進処理のみを記載。
    /// </summary>
    public override void Move()
    {
        //前進する力を加える
        this.myRigidbody.AddForce(this.transform.forward * 13.5f);
        
    }

    // Use this for initialization
    void Start () {
        //Rigidbodyコンポーネントを取得
        this.myRigidbody= GetComponent<Rigidbody>();
        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        //オブジェクトを歩くアニメーションに遷移
        this.myAnimator.SetTrigger("walk");

        //開始位置と経過時間の初期化
        this.StartPos   = this.transform.position;
        this.UpdatePos  = this.transform.position;
        this.TimeCount  = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        //向き反転からの経過時間をカウント
        this.TimeCount += Time.deltaTime;


        //海に落ちてしまった場合は、初期位置に移動する。
        if (this.transform.position.y < this.StartPos.y - 5.0f)
        {
            this.transform.position = StartPos;
            this.TimeCount          = 0.0f;

        }

        //一定時間経過、あるいは、一定距離歩いた場合に、オブジェクトの向きを変える
        else if ((Vector3.Distance(this.UpdatePos, this.transform.position) > 3.0f) || this.TimeCount > 5.0f)
        {

            //回転を開始する角度を設定
            this.transform.Rotate(0, Random.Range(0, 360), 0);
            this.UpdatePos = this.transform.position;
            this.TimeCount = 0.0f;

        }

        //移動する
        else
        {
            this.Move();
        }
    }
}
