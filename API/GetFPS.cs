using UnityEngine;
using UnityEngine.UI;

public class GetFPS : MonoBehaviour
{
    private float _updateInterval = 1f;//设定更新帧率的时间间隔为1秒
    private float _accum = .0f;//累积时间
    private int _frames = 0;//在_updateInterval时间内运行了多少帧
    private float _timeLeft;
    private string fpsFormat;

    private void Start ()
    {
        _timeLeft = _updateInterval;
    }

    private void Update ()
    {
        _timeLeft -= Time.deltaTime;
        //Time.timeScale可以控制Update 和LateUpdate 的执行速度,
        //Time.deltaTime是以秒计算，完成最后一帧的时间
        //相除即可得到相应的一帧所用的时间
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;//帧数

        if (_timeLeft <= 0)
        {
            float fps = _accum / _frames;
            //Debug.Log(_accum + "__" + _frames);
            fpsFormat = System.String.Format("{0:F2}", fps);//保留两位小数
            _timeLeft = _updateInterval;
            _accum = .0f;
            _frames = 0;
        }

        gameObject.GetComponent<Text>().text = "FPS:" + fpsFormat;
    }
}