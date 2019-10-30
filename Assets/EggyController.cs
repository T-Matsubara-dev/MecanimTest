using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggyController : Enemy {

    private float       CenterPos;
    private Rigidbody   myRigidbody;
    private float       Coffient;

    public EggyController(string name) : base(name)
    {
        
    }

    public override void Attack()
    {
        
    }

    public override void Move()
    {
        
        this.myRigidbody.AddForce(this.transform.up * ((CenterPos - this.transform.position.y) * this.Coffient + 120.0f));
        

    }

    // Use this for initialization
    void Start () {
        // コンストラクタに必要な処理を記述
        this.CenterPos = this.transform.position.y;
        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();

        this.Coffient = Random.Range(10.0f, 20.0f);
    }
	
	// Update is called once per frame
	void Update () {
        this.Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
