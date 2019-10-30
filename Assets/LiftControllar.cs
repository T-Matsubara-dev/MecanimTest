using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftControllar : MonoBehaviour {

    //オブジェクトの生成開始位置
    private Vector3 CenterPos;

    private Vector3 UpdatePos;

    private Rigidbody myRigidbody;

    private float UpDownLiftSpeed;

    // Use this for initialization
    void Start () {
        this.CenterPos = this.transform.position;
        this.UpdatePos = this.transform.position;
        if (this.tag == "SideLift")
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.05f, this.transform.position.y, this.transform.position.z);
        }
        else if (this.tag == "UpDownLift")
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
        }

        this.UpDownLiftSpeed = Random.Range(0.05f, 0.1f);

    }
	
	// Update is called once per frame
	void Update () {

        if(this.tag == "SideLift")
        {
            if (Mathf.Abs(this.CenterPos.x - this.transform.position.x) < 10.0f && this.transform.position.x - this.UpdatePos.x > 0)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x + 0.05f, this.transform.position.y, this.transform.position.z);
            }
            else if (Mathf.Abs(this.CenterPos.x - this.transform.position.x) < 10.0f && this.transform.position.x - this.UpdatePos.x < 0)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x - 0.05f, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x - this.CenterPos.x > 10.0f)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x - 0.05f, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x - this.CenterPos.x < -10.0f)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x + 0.05f, this.transform.position.y, this.transform.position.z);
            }
        }
        else if (this.tag == "UpDownLift")
        {
            if (Mathf.Abs(this.CenterPos.y - this.transform.position.y) < 15.0f && this.transform.position.y - this.UpdatePos.y > 0)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + this.UpDownLiftSpeed, this.transform.position.z);
            }
            else if (Mathf.Abs(this.CenterPos.y - this.transform.position.y) < 15.0f && this.transform.position.y - this.UpdatePos.y < 0)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - this.UpDownLiftSpeed, this.transform.position.z);
            }
            else if (this.transform.position.y - this.CenterPos.y > 15.0f)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - this.UpDownLiftSpeed, this.transform.position.z);
            }
            else if (this.transform.position.y - this.CenterPos.y < -15.0f)
            {
                this.UpdatePos = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + this.UpDownLiftSpeed, this.transform.position.z);
            }
        }


    }
}
