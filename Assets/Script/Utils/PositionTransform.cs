using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class PositionTransform
    {
        public static Vector3 ScreenToWorld(Vector3 mousepos,Transform targetTransform) {
            //先计算相机到目标的向量
            Vector3 dir = targetTransform.position - Camera.main.transform.position;
            //计算投影
            Vector3 normardir = Vector3.Project(dir, Camera.main.transform.forward);
            //计算是节点，需要知道处置屏幕的投影距离
            Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, normardir.magnitude));
            return worldpos;
        }
        public static float UpdateZ(float y)
        {
            return -2+(y+3)/20.0f;
        }
    }
}
