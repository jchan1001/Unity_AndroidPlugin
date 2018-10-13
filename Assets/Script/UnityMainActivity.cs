using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UnityMainActivity : MonoBehaviour
{
    [SerializeField]
    Button mMoveNextActivityButton;

    private static AndroidJavaObject mActivityContext = null;
    const string CLASS_CONTROLLER = "jchan1001.co.jp.unityplugin.Controller.UnityController";
    const string FILE_PATH = "Profile";
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

            // none static method call
            mUnityController.Call("setInt", 10);
            mUnityController.Call("setString", "stringTest");

            string[] testArr = new string[3];
            testArr[0] = "one";
            testArr[1] = "two";
            testArr[2] = "three";
            mUnityController.Call("setStringArr", (object) testArr);

            // Get intent object
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            // set intent
            intentObject.Call<AndroidJavaObject>("putExtra", "testok", 100);

            Texture2D file = Resources.Load(FILE_PATH) as Texture2D;
            byte[] imageData = file.EncodeToPNG();
            mUnityController.Call("setImageData", imageData);

            mUnityController.Call("moveActivity", mActivityContext, intentObject);
        }).AddTo(mDisposable);

        PlayerPrefs.SetInt("Price", 10000);
        PlayerPrefs.Save();
        int price = PlayerPrefs.GetInt("Price");
        mUnityController.CallStatic("log", AndroidUtils.LOG_TYPE_DEBUG, "Log from unity PlayerPrefs Value : " + price);
        //#endif
    }

    private void OnDestroy()
    {
        mDisposable.Dispose();
    }

    // CallFromAndroid
    void ChangeButtonColorRed(string str)
    {
        mMoveNextActivityButton.GetComponent<Image>().color = Color.red;
    }

    // CallFromAndroid
    void ChangeButtonColorGreen(string str)
    {
        mMoveNextActivityButton.GetComponent<Image>().color = Color.green;
    }

    // true > background
    // false > foreground
    void OnApplicationPause(bool pauseStatus)
    {
        if (mUnityController != null)
        {
            mUnityController.CallStatic("log", AndroidUtils.LOG_TYPE_DEBUG, "pauseStatus : " + pauseStatus);
        }
    }
}
