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

    // 获取boom特效对象
    public GameObject boom;

    // 获得分数对象
    public GameObject score;

    // 获得音效
    public AudioClip hurtAudio;
    public AudioClip dead;
    public AudioClip birdCollision;


    private void Awake() {
        // 拿到自身的精灵组件
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.relativeVelocity.magnitude);

        // 如果是碰到小鸟 // 因为障碍物也挂用了此函数，所以障碍物也能发出音效
        if(other.gameObject.tag == "Player")
        {
            // 播放音效
            GameObjectManager.instance.AudioPlay(birdCollision);
        }

        // 判断与碰撞对象之间的相对速度 relativeVelocity是相对速度，是一个向量，magnitude改成数值
        if(other.relativeVelocity.magnitude > maxSpeed)
        {
            // 超过maxSpeed时
            Dead();

        } else if(other.relativeVelocity.magnitude > minSpeed) {
            // 超过minSpeed时，变成受伤样式
            render.sprite = hurt;

            // 播放音效
            GameObjectManager.instance.AudioPlay(hurtAudio);
        }

    }

    void Dead() {
        // 播放音效
        GameObjectManager.instance.AudioPlay(dead);

        // 删除List的本体
        GameObjectManager.instance.pig_Greens.Remove(this);
        // 摧毁自身
        Destroy(gameObject);

        // 摧毁后产生爆炸特效
        Instantiate(boom, transform.position, Quaternion.identity);

        // 在头顶上显示分数，1.5s之后摧毁
        if(score)
        {
            GameObject scoreObject = Instantiate(score, transform.position + new Vector3(0, 0.7f, 0) , Quaternion.identity);
            Destroy(scoreObject, 1.5f);
        }
        
    }
}
