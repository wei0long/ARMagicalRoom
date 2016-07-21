using UnityEngine;
using System.Collections;
public class DragObject : MonoBehaviour
{
    private Transform pickedObject = null;
    private Vector3 lastPlanePoint;

    private float x;
    private float y;
    // 移动加权，使移动与手指移动同步
    private float xSpeed = 2;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //创建一个平面
        Plane targetPlane = new Plane(transform.up, transform.position);
        Debug.Log("position=" + transform.position);
        foreach (Touch touch in Input.touches)
        {
            //获取摄像头近平面到屏幕触摸点的射线
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            //获取射线沿着plane的距离
            float dist = 0.0f;
            targetPlane.Raycast(ray, out dist);
            //获取沿着射线在距离dist位置的点
            Vector3 planePoint = ray.GetPoint(dist);
            Debug.Log("Point=" + planePoint);
            //按下手指触碰屏幕
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit = new RaycastHit();
                // 判断是否有碰撞到对象
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    pickedObject = hit.transform;
                    lastPlanePoint = planePoint;
                }
                else
                {
                    pickedObject = null;
                }

            }//选中模型后拖拽
            else if (touch.phase == TouchPhase.Moved)
            {
                if (pickedObject != null)
                {
                    // 设置移动位移
                    x = Input.GetAxis("Mouse X") * xSpeed;
                    pickedObject.position += new Vector3(x, 0, 0);
                    // 方法一
                    //pickedObject.position += planePoint - lastPlanePoint;
                    lastPlanePoint = planePoint;
                }
                //释放
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                pickedObject = null;
            }
        }
    }
}
