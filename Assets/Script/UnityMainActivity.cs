using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UnityMainActivity : MonoBehaviour {
    [SerializeField]
    Button mMoveNextActivityButton;

    const string CLASS_CONTROLLER = "jchan1001.co.jp.unityplugin.Controller.UnityController";
    private static AndroidJavaObject activityContext = null;

    void Start() {
#if !UNITY_EDITOR && UNITY_ANDROID
        mMoveNextActivityButton.OnClickAsObservable().Subscribe(_ => {
            AndroidLog("d", "Unity Log", "Log from unity");
        });
#endif
    }

    void AndroidLog(string type, string tag, string msg) {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (AndroidJavaClass ajc = new AndroidJavaClass(CLASS_CONTROLLER))
        {
            if (ajc != null)
            {
                ajc.CallStatic("Log", type, tag, msg);
            }
        }
#endif
    }
}
