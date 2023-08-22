using System;
using System.IO;
using System.Threading;
using UnityEngine;

public class Button_SynchronizeGameData : MonoBehaviour
{
    private string DataPath;

    private void Start ()
    {
        DataPath = $"{Application.persistentDataPath}/GameTempData.hoilai";
    }

    public async void Button_Click_SynchronizeGameData ()
    {
        try
        {
            var cts = new CancellationTokenSource();
            if (API.EncodingAction.GetNum(GameConfig.GetValue("UID", "playerinformation.xml")) > 10000)
            {
                var _loading = new ShowLoading("正在初始化");
                if (File.Exists(DataPath))
                    File.Delete(DataPath);
                if (SynchronizeGameData.CompressGameData())
                {
                    _loading.SetTitle("正在同步");
                    await SynchronizeGameData.DownloadGameDataAsync(GameConfig.GetValue("UID", "playerinformation.xml"), DataPath);
                    if (SynchronizeGameData.UpdateGameData(DataPath))
                    {
                        new PopNewMessage("同步成功");
                    }
                    else
                    {
                        SynchronizeGameData.CompressGameData();
                        await SynchronizeGameData.SendGameDataAsync(GameConfig.GetValue("UID", "playerinformation.xml"), DataPath);
                    }
                    _loading.KillLoading();
                }
                else
                {
                    _loading.KillLoading();
                    new PopNewMessage("无法确定的存档文件");
                }
                /*
                SynchronizeGameData.UpdateGameData($"{Application.persistentDataPath}/GameTempData.hoilai");
                */
            }
            else
            {
                new PopNewMessage("您尚未登录，或账号数据异常，请尝试重新登陆");
            }
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
        }
    }
}