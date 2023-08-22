using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class Music
{
    public struct ConstPostAddress
    {
        public const string PostAddress = "http://music.163.com/api/search/get/web?csrf_token=hlpretag=&hlposttag=&s={ËÑË÷ÄÚÈÝ}&type=1&offset=0&total=true&limit=20";
        public const string MusicID_Address = "http://music.163.com/song/media/outer/url?id=34367845.mp3";
    }

    public struct MusicMessage
    {
        public string MusicName;
        public string MusicSinger;
        public string MusicID;
    }

    public async Task<List<MusicMessage>> MusicSearch (string musicName)
    {
        string apiUrl = $"http://music.163.com/api/search/get/web?csrf_token=&s={musicName}&type=1&offset=0&total=true&limit=20";

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(apiUrl);
            var resultJson = await response.Content.ReadAsStringAsync();
            var resultWrapper = JsonUtility.FromJson<ResultWrapper>(resultJson);
            List<MusicMessage> ReturnMessage = new List<MusicMessage>();
            foreach (var song in resultWrapper.result.songs)
            {
                MusicMessage temp = new MusicMessage();
                temp.MusicID = song.id.ToString();
                temp.MusicName = song.name;
                temp.MusicSinger = song.artists[0].name;
                ReturnMessage.Add(temp);
            }

            return ReturnMessage;
        }
    }

    [System.Serializable]
    public class ResultWrapper
    {
        public ResultData result;
    }

    [System.Serializable]
    public class ResultData
    {
        public int songCount;
        public SongData[] songs;
    }

    [System.Serializable]
    public class SongData
    {
        public int id;
        public string name;
        public ArtistData[] artists;
    }

    [System.Serializable]
    public class ArtistData
    {
        public string name;
    }
}