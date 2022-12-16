using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Bird : Bird_red
{
    public override void useSkill()
    {
        base.useSkill();

        // 获取当前刚体的速度
        Vector3 speed = rg.velocity;

        // 改变当前刚体速度的x轴方向
        speed.x *= -1;
        rg.velocity = speed;

    }
}
