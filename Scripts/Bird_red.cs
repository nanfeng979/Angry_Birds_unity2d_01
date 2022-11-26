using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 重置场景需要用到的

public class Bird_red : MonoBehaviour
{
    // 鼠标位置
    private Vector2 position;

    // 弹簧定点
    public Transform targetPos;

    // 距离弹簧定点的最长距离
    public float maxDis = 2f;

    // 弹簧对象
    private SpringJoint2D sp;
    // 刚体对象 
    private Rigidbody2D rg;


    private void Awake() {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // 按下R键复原
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0); // 加载场景，0 是默认的第一个场景
            // Time.timeScale = 1; // 恢复正常时间
            return;
        }
    }

    private void OnMouseDown() {
        // 刚体开启动力学，即脱离物理引擎
        rg.isKinematic = true;
    }

    // 鼠标按住时
    private void OnMouseDrag() {
        // 将位置改成世界坐标轴
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 更新bird_red坐标
        transform.position = position;

        // bird_red 与 targetPos 之间的距离超过 maxDis
        if(Vector2.Distance(transform.position, targetPos.position) > maxDis)
        {
            // 向量单位化
            Vector3 pos = (transform.position - targetPos.position).normalized;
            position = pos * maxDis + targetPos.position;
        }

        transform.position = position;
    }

    // 鼠标松开时
    private void OnMouseUp() {
        

        // 刚体停掉动力学，即启动物理引擎
        rg.isKinematic = false;

        // 停用弹簧
        Invoke("Fly", 0.1f);
    }

    private void Fly()
    {
        sp.enabled = false;
    }

}
