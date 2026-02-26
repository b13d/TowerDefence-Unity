using System.Collections;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform[] waypoints;
    private int currentPointIndex = 0;
    private float timer = 0f;

    [SerializeField] private float pointsPerSecond = 1f; // Сколько точек добавлять

    void Start()
    {
        line = GetComponent<LineRenderer>();
        // SetColorLine();
        // Начинаем с 0 точек
        line.positionCount = 0;
    }

    void Update()
    {
        if (currentPointIndex >= waypoints.Length)
        {
            return; // Все точки уже нарисованы
        }

        timer += Time.deltaTime;

        // Добавляем точку каждые N секунд
        if (timer >= 1f / pointsPerSecond)
        {
            timer = 0f;

            // Добавляем следующую точку
            line.positionCount++;
            line.SetPosition(currentPointIndex, waypoints[currentPointIndex].position);
            currentPointIndex++;

            if (currentPointIndex >= waypoints.Length)
            {
                StartCoroutine(DeletePoints());
            }
        }
    }

    IEnumerator DeletePoints()
    {
        while (line.positionCount > 0)
        {
            yield return new WaitForSeconds(0.75f);

            line.positionCount--;
        }

        currentPointIndex = 0;
    }

    void SetColorLine()
    {
        LineRenderer lr = GetComponent<LineRenderer>();

        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.yellow, 0f),      // начало
                new GradientColorKey(new Color(255,69,0), 0.5f), // середина
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        lr.colorGradient = gradient;
    }
}