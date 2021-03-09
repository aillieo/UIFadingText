using AillieoUtils.UI;
using UnityEngine;

public class Sample : MonoBehaviour
{

    private BaseFadingText[] fadingTextComps;

    void Start()
    {
        fadingTextComps = GetComponentsInChildren<BaseFadingText>();
    }

    public float value = 0f;
    public float delta = 0.1f;

    private void Update()
    {
        if(fadingTextComps == null)
        {
            return;
        }

        value += delta * Time.deltaTime;

        if(value > 1)
        {
            value = 1;
            delta = -delta;
        }
        else if(value < 0)
        {
            value = 0;
            delta = -delta;
        }

        foreach(var f in fadingTextComps)
        {
            f.Value = value;
        }
    }
}
