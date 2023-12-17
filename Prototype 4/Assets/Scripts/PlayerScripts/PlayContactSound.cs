using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class PlayContactSound : MonoBehaviour
{
    public AudioSource contactAudio;
    private float[][] soundTimeWindows =
    {
        new float[] { 0.0f, 0.25f },
        new float[] { 0.25f, 0.45f },
        new float[] { 0.45f, 0.7f },
        new float[] { 0.7f, 0.9f },
        new float[] { 0.9f, 1.1f },

        new float[] { 1.7f, 1.9f },
        new float[] { 1.9f, 2.1f },
        new float[] { 2.1f, 2.35f },
        new float[] { 2.35f, 2.55f },
        new float[] { 2.55f, 2.7f },

        new float[] { 3.4f, 3.74f },
        new float[] { 3.74f, 4.10f },
        new float[] { 4.10f, 4.48f },
        new float[] { 4.48f, 4.75f },

        new float[] { 5.7f, 5.95f },
        new float[] { 5.95f, 6.2f },
        new float[] { 6.2f, 6.45f },
        new float[] { 6.45f, 6.65f },

        new float[] { 7.6f, 7.8f },
        new float[] { 7.8f, 8.05f },
    };
    // Start is called before the first frame update
    void Start()
    {
        EventsHandler.playerContact += playAudio;
    }

    private void OnDestroy()
    {
        EventsHandler.playerContact -= playAudio;
    }

    private float[] RandomTimeWindow()
    {
        int randomIndex = Random.Range(0, soundTimeWindows.Length - 1);
        return new float[] { soundTimeWindows[randomIndex][0], soundTimeWindows[randomIndex][1] };
    }

    private void playAudio()
    {
        float[] timeWindow = RandomTimeWindow();
        contactAudio.time = timeWindow[0];
        contactAudio.Play();
        StartCoroutine(StopAudio(timeWindow[1]));
    }

    private IEnumerator StopAudio(float endTime)
    {
        bool audioStopped = false;
        while (!audioStopped)
        {
            if (contactAudio.time > endTime) 
            {
                contactAudio.Stop();
                audioStopped = true;
            }
            yield return null;
        }
    }
}
