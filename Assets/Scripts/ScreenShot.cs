using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private int _index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot($"Screenshot{_index}.png");
            _index++;
        }
    }
}