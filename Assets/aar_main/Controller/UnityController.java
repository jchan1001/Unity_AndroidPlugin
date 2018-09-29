package jchan1001.co.jp.unityplugin.Controller;

import android.util.Log;

import static android.util.Log.println;

public class UnityController {

    public void startActivity() {

    }

    public static void Log(String type, String tag, String msg) {
        switch (type){
            case "d":
                println(Log.DEBUG, tag, msg);
                break;
            case "e":
                // error
                println(Log.ERROR, tag, msg);
                break;
            case "w":
                // warning
                println(Log.WARN, tag, msg);
                break;
            case "i":
                // information
                println(Log.INFO, tag, msg);
                break;
            case "v":
                // verbose
                println(Log.VERBOSE, tag, msg);
                break;
            default:
                println(Log.ASSERT, tag, msg);
        }
    }
}
