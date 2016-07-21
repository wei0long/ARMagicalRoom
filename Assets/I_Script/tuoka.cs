using UnityEngine;
using Vuforia;//DefaulTrackableEventHandler.cs中定义了一个命名空间Vuforia，这里用using来调用

public class tuoka : MonoBehaviour , ITrackableEventHandler {


    private TrackableBehaviour mTrackableBehaviour;


    public Transform target;//创建一个目标物体，可以在脚本附给物体之后，会在脚本下产生一个target，将对应物体拖入到框中
   public Vector3 origposition = Vector3.zero;
 public   Vector3 cameraposition = new Vector3(0,0, 0);
    public GameObject UI;  //用来设置UI的显示与隐藏

    public GameObject teapot;
    // 记录模型位姿
    private Vector3 save_Position;
    private Quaternion save_Rotation;

    bool firstfound = false;
    // Use this for initialization
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    

    

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(    TrackableBehaviour.Status previousStatus,  TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

   


 


    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

//添加start     //脱卡设置摄像头代码
        firstfound = true;
        target.parent = this.transform;
   //  target.position = origposition;
        target.rotation = Quaternion.Euler(Vector3.zero);
        save_Position = teapot.transform.localPosition;
        save_Rotation = teapot.transform.localRotation;
        
        Debug.Log("找到");
        //添加end

        //添加start    //UI显示
        UI.SetActive(true);
        //添加end
       
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        //添加start
        if (firstfound == true)
        {
            // target.parent = Camera.main.transform;
            // target.localPosition = cameraposition;
            Debug.Log("消失");

        }
        //添加end

        //添加start
        UI.SetActive(false);
        //添加end

        teapot.transform.localRotation = save_Rotation;
        teapot.transform.localPosition = save_Position;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

}

