using System;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 同步游戏数据
/// </summary>
public static class SynchronizeGameData
{
    /// <summary>
    /// 将当前账户的游戏数据打包成一个hoilai文件
    /// </summary>
    /// <returns></returns>
    public static bool CompressGameData ()
    {
        try
        {
            string dataPath = Application.persistentDataPath;
            string zipFilePath = Path.Combine(dataPath, "GameTempData.hoilai");

            // 获取.dataPath目录下所有的.xml文件
            string[] xmlFiles = Directory.GetFiles(dataPath, "*.xml");

            // 如果没有找到任何.xml文件，无需进行压缩
            if (xmlFiles.Length == 0)
            {
                Debug.Log("未找到压缩文件");
                return false;
            }

            using (FileStream zipToCreate = new FileStream(zipFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                {
                    foreach (string xmlFilePath in xmlFiles)
                    {
                        // 将每个.xml文件添加到压缩包中
                        string xmlFileName = Path.GetFileName(xmlFilePath);
                        archive.CreateEntryFromFile(xmlFilePath, xmlFileName);
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            return false;
        }
    }

    public static long GetFileSize (string sFullName)
    {
        long lSize = 0;
        if (File.Exists(sFullName))
            lSize = new FileInfo(sFullName).Length;
        return lSize;
    }

    /// <summary>
    /// 将一个hoilai文件同步到本地
    /// </summary>
    /// <returns></returns>
    public static bool UpdateGameData (string zipFilePath)
    {
        try
        {
            Debug.Log(GetFileSize(zipFilePath));
            if (GetFileSize(zipFilePath) <= 0)
            {
                return false;
            }
            string dataPath = Application.persistentDataPath;

            // 删除 Application.persistentDataPath 下的所有.xml文件
            string[] xmlFiles = Directory.GetFiles(dataPath, "*.xml");
            foreach (string xmlFilePath in xmlFiles)
            {
                File.Delete(xmlFilePath);
            }

            using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string entryFileName = entry.Name;
                        string entryFullPath = Path.Combine(dataPath, entryFileName);

                        // 解压文件
                        entry.ExtractToFile(entryFullPath, true);
                    }
                }
            }
            // 删除压缩包中没有的.xml文件
            xmlFiles = Directory.GetFiles(dataPath, "*.xml");
            foreach (string xmlFilePath in xmlFiles)
            {
                string fileName = Path.GetFileName(xmlFilePath);
                if (!File.Exists(Path.Combine(dataPath, fileName)))
                {
                    File.Delete(xmlFilePath);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static async Task SendGameDataAsync (string uid, string filePath)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(Server.IP, Server.GameDataPort);
                //await client.ConnectAsync("127.0.0.1", Server.GameDataPort);
                using (NetworkStream stream = client.GetStream())
                {
                    // 发送请求类型（长度为5）
                    byte[] requestTypeBytes = System.Text.Encoding.UTF8.GetBytes("UPLOAD");
                    Array.Resize(ref requestTypeBytes, 6); // 调整为指定的长度
                    await stream.WriteAsync(requestTypeBytes, 0, requestTypeBytes.Length);

                    // 发送UID
                    byte[] uidBytes = System.Text.Encoding.UTF8.GetBytes(uid);
                    Array.Resize(ref uidBytes, 10);
                    await stream.WriteAsync(uidBytes, 0, uidBytes.Length);

                    // 发送文件数据
                    using (FileStream fileStream = File.OpenRead(filePath))
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                    Debug.Log("文件上传完成！");
                }
            }
        }
        catch (Exception e)
        {
            // 处理异常
            Debug.Log("上传过程中出现异常：" + e.Message);
        }
    }

    public static async Task DownloadGameDataAsync (string uid, string savePath)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(Server.IP, Server.GameDataPort);
                //await client.ConnectAsync("127.0.0.1", Server.GameDataPort);
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] requestTypeBytes = System.Text.Encoding.UTF8.GetBytes("GETDAT");
                    Array.Resize(ref requestTypeBytes, 6); // 调整为指定的长度
                    await stream.WriteAsync(requestTypeBytes, 0, requestTypeBytes.Length);

                    // 发送UID
                    byte[] uidBytes = System.Text.Encoding.UTF8.GetBytes(uid);
                    Array.Resize(ref uidBytes, 10);
                    await stream.WriteAsync(uidBytes, 0, uidBytes.Length);

                    // 将服务器响应保存到本地文件
                    using (FileStream fileStream = File.Create(savePath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    Console.WriteLine($"已成功下载数据并保存到：{savePath}");
                }
            }
        }
        catch (Exception e)
        {
            // 处理异常
            Console.WriteLine("下载数据过程中出现异常：" + e.Message);
        }
    }
}