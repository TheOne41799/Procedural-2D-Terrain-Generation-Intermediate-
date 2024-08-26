using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


[ExecuteInEditMode]
public class GroundGenerator2 : MonoBehaviour
{
    [SerializeField] private SpriteShapeController groundShapeController;

    [SerializeField, Range(5f, 100f)] private int levelLength = 25;
    [SerializeField, Range(10f, 50f)] private float baseWidthMultiplier = 15f;
    [SerializeField, Range(10f, 50f)] private float baseHeightMultiplier = 10f;
    [SerializeField, Range(0f, 1f)] private float curveSmoothness = 0.5f;
    [SerializeField] private float noiseStep = 0.1f;  // Adjusted step size
    [SerializeField] private float bottom = 10f;
    [SerializeField] private float difficultyIncrement = 0.1f;
    [SerializeField] private float frequency = 0.1f;  // Adjusted frequency for smoother curves
    [SerializeField] private float maxHeightVariance = 5f;

    private Vector3 lastPosition;

    private void OnValidate()
    {
        groundShapeController.spline.Clear();

        for (int i = 0; i < levelLength; i++) // Increment i by 1
        {
            float difficultyFactor = (float)i / levelLength;

            float dynamicHeightMultiplier = baseHeightMultiplier
                                            + (difficultyFactor * difficultyIncrement)
                                            + Random.Range(-maxHeightVariance, maxHeightVariance);

            float sineWave = Mathf.Sin(i * frequency) * dynamicHeightMultiplier;
            float noiseValue = Mathf.PerlinNoise(i * noiseStep, 0) * dynamicHeightMultiplier * 0.5f;

            float yPos = sineWave + noiseValue;

            lastPosition = transform.position + new Vector3(i * baseWidthMultiplier, yPos);

            groundShapeController.spline.InsertPointAt(i, lastPosition);

            if (i != 0 && i != levelLength - 1)
            {
                groundShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);

                groundShapeController.spline.SetLeftTangent(i, curveSmoothness * baseWidthMultiplier * Vector3.left);
                groundShapeController.spline.SetRightTangent(i, curveSmoothness * baseWidthMultiplier * Vector3.right);
            }
        }

        groundShapeController.spline.InsertPointAt(levelLength,
                                                   new Vector3(lastPosition.x, transform.position.y - bottom));

        groundShapeController.spline.InsertPointAt(levelLength + 1,
                                                   new Vector3(transform.position.x, transform.position.y - bottom));
    }



}
