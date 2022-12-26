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
    protected Rigidbody2D rg;

    // 弹簧线组件
    public LineRenderer leftLine;
    public LineRenderer rightLine;

    // 左侧弹弓
    public Transform leftPos;

    // 死亡特效boom
    public GameObject boom;

    // 是否允许被鼠标拖拽
    private bool canBeDrag = true;

    // 相机滑动速率
    public float smooth = 3;

    // 获得音效
    public AudioClip select;
    public AudioClip fly;

    // 是否能使用技能
    private bool canUseSkill = false;

    // 获取小鸟受伤样式
    public Sprite hurt;
    private SpriteRenderer hurtRender;

    private void Awake() {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        hurtRender = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        // 按下R键复原
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(2); // 加载场景，0 是默认的第一个场景
            // Time.timeScale = 1; // 恢复正常时间
            return;
        }
        
        // 清除星星
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), 0);
            return;
        }

        // 相机跟随
        // 小鸟的x轴位置
        float posX = transform.position.x;
        // 修改相机的位置  // Lerp()平滑函数；Clamp()区间函数
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z),
            smooth * Time.deltaTime);

        // 判断是能使用技能
        if(canUseSkill)
        {
            // 判断是否按下鼠标左键
            if(Input.GetMouseButtonDown(0))
            {
                useSkill();
            }
        }
    }
    
    // 碰到物体时
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            return;
        }
        canUseSkill = false;

        // 刚体x轴速度过小则不会受伤
        if(rg.velocity.x > 0.3f)
        {
            // 渲染小鸟受伤样式
            hurtRender.sprite = hurt;
        }
            
        
    }

    private void OnMouseDown() {
        // 刚体开启动力学，即脱离物理引擎
        rg.isKinematic = true;
    }

    // 鼠标按住时
    private void OnMouseDrag() {
        // 判断是否允许被拖拽
        if(!canBeDrag) return;

        // 播放音效
        GameObjectManager.instance.AudioPlay(select);

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

        // 停用弹簧线组件
        leftLine.enabled = false;
        rightLine.enabled = false;
    }

    // 小鸟飞行
    private void Fly()
    {
        // 正在飞
        canUseSkill = true;

        // 播放音效
        GameObjectManager.instance.AudioPlay(fly);

        // 禁止被拖拽
        canBeDrag = false;

        // 停用弹弓组件
        sp.enabled = false;

        // 一定时间后本体死亡
        Invoke("Die", 4f);
    }

    // 绘制弹簧线
    void Line()
    {
        // 启用弹簧线组件
        leftLine.enabled = true;
        rightLine.enabled = true;

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

    // 使用技能
    public virtual void useSkill()
    {
        Debug.Log("yeah");
        // 只能执行一次技能
        canUseSkill = false;
    }
}
