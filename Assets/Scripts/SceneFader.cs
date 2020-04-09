using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public AnimationCurve fadeCurve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            // Scaling load time
            t -= Time.deltaTime * 2f;
            float a = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            // skip to the next frame
            yield return 0;
        }

        // Load the scene, do action ...
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 1f;
            float a = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
