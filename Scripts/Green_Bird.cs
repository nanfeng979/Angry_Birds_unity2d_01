using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Bird : Bird_red
{
    public override void useSkill()
    {
        base.useSkill();

        // 改变当前刚体速度的x轴方向
        Vector3 speed = rg.velocity;
        speed.x *= -1;
        rg.velocity = speed;

        // 改变当前对象的x轴方向
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;
        gameObject.transform.localScale = scale;

    }
}
