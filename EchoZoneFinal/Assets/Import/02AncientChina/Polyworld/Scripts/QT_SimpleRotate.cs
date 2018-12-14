/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public class QT_SimpleRotate : MonoBehaviour {

    public bool XAxis, YAxis, ZAxis;
    
    public float Speed = 25f;
    private Vector3 rotationAxis = new Vector3(0, 0, 0);
    

	void Start () {

        if (XAxis)
            rotationAxis += new Vector3(1, 0, 0);
        if (YAxis)
            rotationAxis += new Vector3(0, 1, 0);
        if (ZAxis)
            rotationAxis += new Vector3(0, 0, 1);
	}

	void FixedUpdate()
	{

		if(rotationAxis!=Vector3.zero)    
			this.transform.Rotate((rotationAxis * Time.deltaTime)*Speed);
	}
	
    
}
