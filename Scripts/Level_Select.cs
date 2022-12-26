using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Select : MonoBehaviour
{

    public bool canSelect = false;

    public Sprite levelBg;

    private Image image;

    public GameObject[] stars;

    private void Awake() {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 判断当前是否是第一关
        if(transform.parent.GetChild(0).name == gameObject.name) {
            // 则可以被选中
            canSelect = true;
        } else {
            // 如果非第一关且前一关的值是否大于0
            int preNum = int.Parse(gameObject.name.Substring(2)) - 1;
            if(PlayerPrefs.GetInt("level" + gameObject.name.Substring(0, 2) + preNum) > 0) {
                // 后续的关卡解锁
                canSelect =true;
            }
        }

        if(canSelect) {
            // 改变背景
            image.overrideSprite = levelBg;

            // 显示关卡名
            transform.Find("num").gameObject.SetActive(true);

            // 获取当前关卡的星星数
            int Count = PlayerPrefs.GetInt("level" + gameObject.name);
            for(int i = 0; i < Count; i++) {
                stars[i].SetActive(true);
            }
        }
    }

    public void Selected() {
        if(canSelect) {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            // 切换场景
            SceneManager.LoadScene(2);
        }
    }

}
