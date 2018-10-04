using UnityEngine;
using UnityEngine.UI;
using UniRx;

// AndroidJavaObject -> Java class

public class UnityMainActivity : MonoBehaviour
{
    [SerializeField]
    Button mMoveNextActivityButton;

    private static AndroidJavaObject mActivityContext = null;
    const string CLASS_CONTROLLER = "jchan1001.co.jp.unityplugin.Controller.UnityController";
    private static AndroidJavaObject go = null;

    void Start()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        SetUnityControllerObject(CLASS_CONTROLLER);
        SetContext();
        mMoveNextActivityButton.OnClickAsObservable().Subscribe(_ => {
            go.Call("log", "Unity Log", "Log from unity");
            go.CallStatic("moveActivity", mActivityContext);
        });
#endif
    }

    // Get UnityController Android Object 
    // Todo return -> error 
    void SetUnityControllerObject(string packageName)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (AndroidJavaClass ajc = new AndroidJavaClass(packageName))
        {
            if (ajc != null)
            {
                go = ajc.CallStatic<AndroidJavaObject>("getUnityController");
            }
        }
#endif
    }

    void SetContext()
    {
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            mActivityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

    }
}