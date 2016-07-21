using UnityEngine;
using Vuforia; //DefaulTrackableEventHandler.cs中定义了一个命名空间Vuforia，这里用using来调用
using System.Collections;


public class position : MonoBehaviour , ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
     public Transform target;//创建一个目标物体，可以在脚本附给物体之后，会在脚本下产生一个target，将对应物体拖入到框中
    Vector3 origposition = Vector3.zero;
    Vector3 cameraposition = new Vector3(0, 0, 0);
    
    Vector3 save_Position;
    Quaternion save_Rotation;
    //用代码法来实现脱卡，看与打勾法不同imagtarget位置会不会变
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
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
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
        save_Rotation = target.transform.localRotation;
        //target.rotation = Quaternion.Euler(Vector3.zero);
        Debug.Log("找到");
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
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        //添加start
        if (firstfound == true)
        {
            // target.parent = Camera.main.transform;
            // target.localPosition = cameraposition;
            target.transform.position = this.transform.position;
            target.transform.localRotation = save_Rotation;
            Debug.Log("消失");

        }
        //添加end

      
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

}

