using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScaleOnClick : MonoBehaviour
{
    public Vector3 normalScale = new Vector3(0.27f, 0.27f, 0.27f);
    public Vector3 pressedScale = new Vector3(0.29f, 0.29f, 0.29f);
    public float scaleDuration = 0.1f; // Duration of scale effect

    private Button button;
    private bool isScaling = false;

    void Start()
    {
        button = GetComponent<Button>();

        // Add listener to button click event
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (!isScaling)
        {
            StartCoroutine(ScaleButton());
        }
    }

    private System.Collections.IEnumerator ScaleButton()
    {
        isScaling = true;

        // Smoothly scale up to pressedScale
        float elapsedTime = 0;
        Vector3 startingScale = transform.localScale;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startingScale, pressedScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = pressedScale;

        // Wait for a moment
        yield return new WaitForSeconds(0.1f);

        // Smoothly scale back down to normalScale
        elapsedTime = 0;
        startingScale = transform.localScale;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startingScale, normalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = normalScale;

        isScaling = false;
    }
}