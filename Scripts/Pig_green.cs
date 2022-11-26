using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_green : MonoBehaviour
{
    // 碰撞对象的最大/最小速度
    public float maxSpeed = 8;
    public float minSpeed = 4f;

    // 获取精灵组件
    private SpriteRenderer render;

    // 外部获取精灵图
    public Sprite hurt;

    private void Awake() {
        // 拿到自身的精灵组件
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.relativeVelocity.magnitude);
        // 判断与碰撞对象之间的相对速度
        // relativeVelocity是相对速度，是一个向量，magnitude改成数值
        if(other.relativeVelocity.magnitude > maxSpeed)
        {
            // 超过maxSpeed时，摧毁自身
            Destroy(gameObject);

        } else if(other.relativeVelocity.magnitude > minSpeed) {
            // 超过minSpeed时，变成受伤样式
            render.sprite = hurt;
        }

    }
}
