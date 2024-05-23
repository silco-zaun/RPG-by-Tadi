using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBarController : MonoBehaviour
{
    [SerializeField] private Transform bar;
    [SerializeField] private SpriteRenderer barSpriteRenderer;

    private const float MAX_VALUE = 1.0f;


    public void SetBar(float newValue)
    {
        bar.transform.localScale = new Vector3(newValue, 1f);
    }

    public IEnumerator SetBarRoutine(float newValue)
    {
        float curValue = bar.transform.localScale.x;
        float changeAmount = curValue - newValue;

        while (curValue - newValue > Mathf.Epsilon)
        {
            curValue -= changeAmount * Time.deltaTime;
            bar.transform.localScale = new Vector3(curValue, 1f);
            yield return null;
        }

        bar.transform.localScale = new Vector3(newValue, 1f);
    }

    public void SetColor(Color color)
    {
        barSpriteRenderer.color = color;
    }
}
