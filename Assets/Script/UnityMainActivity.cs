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
    private static AndroidJavaObject mUnityController = null;
    CompositeDisposable mDisposable = new CompositeDisposable();

    void Start()
    {
//#if !UNITY_EDITOR && UNITY_ANDROID
        // Set Unity Controller
        using (AndroidJavaClass ajc = new AndroidJavaClass(CLASS_CONTROLLER))
        {
            if (ajc != null)
            {
                mUnityController = ajc.CallStatic<AndroidJavaObject>("getUnityController");
            }
        }

        // Set Context
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            mActivityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        
        // Subscribe
        mMoveNextActivityButton.OnClickAsObservable().Subscribe(_ => {
            // Static method call
            mUnityController.CallStatic("log", AndroidUtils.LOG_TYPE_DEBUG, "Log from unity");

            mUnityController.Call("setTestInt", 10);
            mUnityController.Call("setTestString", "stringTest");
            string[] testArr = new string[3];
            testArr[0] = "0";
            testArr[1] = "1";
            testArr[2] = "2";
            // Todo Edd send error
            mUnityController.Call("setTestStringArr", (object) testArr);

            mUnityController.Call("moveActivity", mActivityContext);
        }).AddTo(mDisposable);
//#endif
    }

    private void OnDestroy()
    {
        mDisposable.Dispose();
    }
}