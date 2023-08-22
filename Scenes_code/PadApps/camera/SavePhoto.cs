using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SavePhoto : MonoBehaviour
{
    private const string SCREENSHOT_DIR = "Menhera";
    private const string SCREENSHOT_EXT = ".png";
    public GameObject CameraUI;
    public void Save ()
    {

        StartCoroutine(CaptureScreenshot());
    }
    private IEnumerator CaptureScreenshot ()
    {
        // 等待一帧，以确保屏幕已经渲染完毕
        yield return new WaitForEndOfFrame();

        try
        {
            // 截屏并保存到本地
            Texture2D screenshotTex = ScreenCapture.CaptureScreenshotAsTexture();
            byte[] bytes = screenshotTex.EncodeToPNG();

            // 检查目录是否存在，如不存在则创建
            string screenshotPath = Path.Combine(GetScreenshotDir(), GetScreenshotName());
            string screenshotDir = Path.GetDirectoryName(screenshotPath);
            if (!Directory.Exists(screenshotDir))
            {
                Directory.CreateDirectory(screenshotDir);
            }

            File.WriteAllBytes(screenshotPath, bytes);
            string[] paths = new string[1];
            paths[0] = screenshotPath;
            ScanFile(paths);
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            HL.IO.HL_Log.Log($"{this.name}  {ex.Message}", "Error");
        }
        finally
        {

        }


    }


    private string GetScreenshotDir ()
    {
        string dcimPath = "/storage/emulated/0/DCIM/";

        // 检查 DCIM 文件夹是否存在，如不存在则创建之
        if (!Directory.Exists(dcimPath))
        {
            Directory.CreateDirectory(dcimPath);
        }

        // 拼接目标文件夹路径
        string screenshotDir = System.IO.Path.Combine(dcimPath, SCREENSHOT_DIR);

        if (!Directory.Exists(screenshotDir))
        {
            Directory.CreateDirectory(screenshotDir);
        }

        return screenshotDir;
    }

    private string GetScreenshotName ()
    {
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        return "screenshot_" + timestamp + SCREENSHOT_EXT;
    }

    private void ScanFile (string[] path)
    {
        using (AndroidJavaClass PlayerActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject playerActivity = PlayerActivity.GetStatic<AndroidJavaObject>("currentActivity");
            using (AndroidJavaObject Conn = new AndroidJavaObject("android.media.MediaScannerConnection", playerActivity, null))
            {
                Conn.CallStatic("scanFile", playerActivity, path, null, null);
            }
        }
    }
}