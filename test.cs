using UnityEngine;

public class test : MonoBehaviour
{
    private ShowLoading loading;
    public void Start ()
    {

    }
    public void 压缩文件 ()
    {
        SynchronizeGameData.CompressGameData();
    }

    public void 更新数据 ()
    {
        SynchronizeGameData.UpdateGameData($"{Application.persistentDataPath}/GameTempData.hoilai");
    }

    public async void 上传到服务器 ()
    {
        await SynchronizeGameData.SendGameDataAsync("10001", $"{Application.persistentDataPath}/GameTempData.hoilai");
    }

    public async void 下载到本地 ()
    {
        await SynchronizeGameData.DownloadGameDataAsync("10001", $"{Application.persistentDataPath}/GameTempData.hoilai");
    }

    public void 显示加载界面2秒 ()
    {
        loading = new ShowLoading("加载中");
        Invoke("_kill", 2);
    }

    private void _kill ()
    {
        loading.KillLoading();
    }

    public void 获取友邻圈数据 ()
    {
    }
}