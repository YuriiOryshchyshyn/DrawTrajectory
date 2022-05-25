using System;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int dotsNumber;
    [SerializeField] private GameObject dotParent;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private float dotSpacing;
    [SerializeField] [Range(0.01f, 0.3f)] private float dotMinScale;
    [SerializeField] [Range(0.01f, 0.8f)] private float dotMaxScale;

    private Transform[] dotsArray;

    private Vector2 position;

    private float timeStamp;

    private void Start()
    {
        Hide();
        PrepareDots();
    }

    private void PrepareDots()
    {
        dotsArray = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = dotMaxScale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsArray[i] = Instantiate(dotPrefab, dotParent.transform).transform;
            dotsArray[i].transform.localScale = Vector3.one * scale;

            if (scale > dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
    }

    public void UpdateDots(Vector3 ballPosition, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            position.x = (ballPosition.x + forceApplied.x * timeStamp);
            position.y = (ballPosition.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsArray[i].position = position;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotParent.SetActive(true);
    }

    public void Hide()
    {
        dotParent.SetActive(false);
    }
}
