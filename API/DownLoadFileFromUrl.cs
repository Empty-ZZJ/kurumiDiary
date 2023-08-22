using UnityEngine;

public class DownLoadFileFromUrl : MonoBehaviour
{
    /*
    public string url;
    private DownloadRequest _request;
    public Text _download;
    public void StartDownLoad ()
    {
        StartCoroutine(Loading());
    }
    private IEnumerator Loading ()
    {
        var content = DownloadContent.Get(url, Application.persistentDataPath + "/APK.apk");
        _request = Downloader.DownloadAsync(content);
        while (!_request.isDone)
        {
            var downloadedBytes = Utility.FormatBytes(_request.downloadedBytes);
            var downloadSize = Utility.FormatBytes(_request.downloadSize);
            var bandwidth = Utility.FormatBytes(_request.bandwidth);

            var time = (_request.downloadSize - _request.downloadedBytes) * 1f / _request.bandwidth;
            _download.text = $"{Constants.Text.Loading}{downloadedBytes}/{downloadSize}, {bandwidth}/s -  £”‡£∫{time}s";
            start_code.countloading = (int)_request.progress;
            yield return null;
        }
        if (File.Exists(Application.persistentDataPath + "/APK.apk"))
        {

        }
        else
        {
            new PopNewMessage("œ¬‘ÿ◊ ‘¥ ß∞‹£°");
        }
    }
    */

}