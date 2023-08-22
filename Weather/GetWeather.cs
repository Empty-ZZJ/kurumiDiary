using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetWeather : MonoBehaviour
{
    private const string API_KEY = "SU-eRf5LJT202xXW4";
    private const string API_URL = "https://api.seniverse.com/v3/weather/now.json?key={0}&location={1}&language=zh-Hans&unit=c";
    public WeatherData weatherData;

    private void Start ()
    {
    }

    public void GetWeatherState (string city)
    {
        StartCoroutine(FetchWeatherData(city));
    }

    private IEnumerator FetchWeatherData (string city)
    {
        string url = string.Format(API_URL, API_KEY, city);
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log(json);
            weatherData = JsonUtility.FromJson<WeatherData>(json);
            Debug.Log(weatherData.results[0].city.name);
            Debug.Log(weatherData.results[0].now.temperature);
            Debug.Log(weatherData.results[0].now.text);
        }
    }

    [System.Serializable]
    public class WeatherData
    {
        public List<Result> results;
    }

    [System.Serializable]
    public class Result
    {
        public Location city;
        public Now now;
    }

    [System.Serializable]
    public class Location
    {
        public string name;
    }

    [System.Serializable]
    public class Now
    {
        public string text;
        public string temperature;
    }
}