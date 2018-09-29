using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UnityMainActivity : MonoBehaviour {
    const string PACKAGE_NAME_CONTROLLER = "jchan1001.co.jp.unityplugin.Controller";
    #if !UNITY_EDITOR && UNITY_ANDROID
        private static AndroidJavaObject activityContext = null;
    #endif

    [SerializeField]
    Button mMoveNextActivityButton;

	// Use this for initialization
	void Start () {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
#endif

        mMoveNextActivityButton.OnClickAsObservable().Subscribe(_ => {
#if !UNITY_EDITOR && UNITY_ANDROID
            GetAndroidClass(PACKAGE_NAME_CONTROLLER);
#endif
        });
    }

    void MoveActivity(string activityName) {

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
}
