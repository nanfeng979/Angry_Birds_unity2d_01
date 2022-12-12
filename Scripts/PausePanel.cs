using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject button;


    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void Retry()
    {
        // 恢复
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    // 点击Pause按钮
    public void Pause()
    {
        // 显示自己
        this.gameObject.SetActive(true);
        // 播放Pause动画
        anim.SetBool("isPause", true);
        // 隐藏按钮
        button.SetActive(false);
        
    }

    // Pause动画播放完调用
    public void PauseAnimEnd()
    {
        // 暂停
        Time.timeScale = 0;
    }

    // 点击了Resume按钮
    public void Resume()
    {
        // 恢复
        Time.timeScale = 1;
        // 播放resume动画
        anim.SetBool("isPause", false);

    }

    // Resume动画播放完调用
    public void ResumeAnimEnd()
    {
        // 显示按钮
        button.SetActive(true);
        // 隐藏自己
        this.gameObject.SetActive(false);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }
}
