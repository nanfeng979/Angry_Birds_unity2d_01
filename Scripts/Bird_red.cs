using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 重置场景需要用到的

public class Bird_red : MonoBehaviour
{
    // 鼠标位置
    private Vector2 position;

    // 弹簧定点，即右侧弹弓
    public Transform targetPos;

    // 距离弹簧定点的最长距离
    public float maxDis = 2f;

    [HideInInspector]
    // 弹簧对象
    public SpringJoint2D sp;
    // 刚体对象 
    private Rigidbody2D rg;

    // 弹簧线组件
    public LineRenderer leftLine;
    public LineRenderer rightLine;

    // 左侧弹弓
    public Transform leftPos;

    // 死亡特效boom
    public GameObject boom;


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

        // 更新bird_red坐标位置
        transform.position = position;

        // 绘制弹簧线
        Line();
    }

    // 鼠标松开时
    private void OnMouseUp() {

        // 刚体停掉动力学，即启动物理引擎
        rg.isKinematic = false;

        // 停用弹簧组件
        Invoke("Fly", 0.1f);
    }

    private void Fly()
    {
        // 停用弹弓组件
        sp.enabled = false;

        // 一定时间后本体死亡
        Invoke("Die", 4f);
    }

    // 绘制弹簧线
    void Line()
    {
        // 弹簧线从弹弓右侧到bird_red连线
        rightLine.SetPosition(0, targetPos.position);
        rightLine.SetPosition(1, transform.position);

        // 弹簧线从弹弓左侧到bird_red连线
        leftLine.SetPosition(0, leftPos.position);
        leftLine.SetPosition(1, transform.position);
    }

    // 本体死亡
    void Die()
    {
        // 删除List的本体
        GameObjectManager.instance.bird_Reds.Remove(this);
        // 摧毁本体
        Destroy(gameObject);
        // 摧毁后产生爆炸特效
        Instantiate(boom, transform.position, Quaternion.identity);
        // 检查下一步动作
        GameObjectManager.instance.afterBirdFly();
    }
}
