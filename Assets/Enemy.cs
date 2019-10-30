using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private string name;  // 名前

    public Enemy(string name)
    {
        this.name = name;
    }

    // 攻撃時アクション関数
    public abstract void Attack();

    // 動作時アクション関数
    public abstract void Move();


    public string GetName()
    {
        return this.name;
    }

}
