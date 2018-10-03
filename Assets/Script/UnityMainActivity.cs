using UnityEngine;
using UnityEngine.UI;
using UniRx;

// AndroidJavaObject -> Java class

public class UnityMainActivity : MonoBehaviour {
    [SerializeField]
    Button mMoveNextActivityButton;

    const string CLASS_CONTROLLER = "jchan1001.co.jp.unityplugin.Controller.UnityController";
    private static AndroidJavaObject go = null;

    void Start() {
#if !UNITY_EDITOR && UNITY_ANDROID
        SetUnityControllerObject(CLASS_CONTROLLER);
        mMoveNextActivityButton.OnClickAsObservable().Subscribe(_ => {
            go.Call("log", "Unity Log", "Log from unity");
        });
#endif
    }

    // Get UnityController Android Object 
    // Todo return -> error 
    void SetUnityControllerObject(string packageName) {
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
}
