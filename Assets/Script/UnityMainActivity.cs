using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UnityMainActivity : MonoBehaviour {
#if !UNITY_EDITOR && UNITY_ANDROID
    const string CLASS_CONTROLLER = "jchan1001.co.jp.unityplugin.Controller.UnityMainActivity";
    private static AndroidJavaObject activityContext = null;

    [SerializeField]
    Button mMoveNextActivityButton;

    // Use this for initialization
    void Start() {
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        mMoveNextActivityButton.OnClickAsObservable().Subscribe(_ => {
            AndroidLog("d", "Unity Log", "Hi There");
        });
    }

    AndroidJavaClass GetAndroidClass(string package) {
        using (AndroidJavaClass ajc = new AndroidJavaClass(package))
        {
            if (ajc != null)
            {
                return ajc;
            }
        }
        return null;
    }

    void AndroidLog(string type, string tag, string msg) {
        GetAndroidClass(CLASS_CONTROLLER).Call("Log", type, tag, msg);
    }
#endif
}
