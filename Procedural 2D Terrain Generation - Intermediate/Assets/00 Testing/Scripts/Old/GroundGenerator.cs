using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


[ExecuteInEditMode]
public class GroundGenerator : MonoBehaviour
{

    [SerializeField] private SpriteShapeController groundShapeController;

    [SerializeField, Range(5f, 100f)] private int levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float baseWidthMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float baseHeightMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float curveSmoothness = 0.5f;
    [SerializeField] private float noiseStep = 0.05f;  // Smaller step for smoother curves
    [SerializeField] private float bottom = 10f;
    [SerializeField] private float difficultyIncrement = 0.1f;  // How much to increase difficulty per segment
    [SerializeField] private float frequency = 0.1f;  // Controls the frequency of the sinusoidal curve

    private Vector3 lastPosition;

    private void OnValidate()
    {
        groundShapeController.spline.Clear();

        for (int i = 0; i < levelLength; i++)
        {
            // Gradually increase heightMultiplier while keeping variations smooth
            float difficultyFactor = (float)i / levelLength;
            float currentHeightMultiplier = baseHeightMultiplier + (difficultyFactor * difficultyIncrement);

            // Calculate the sinusoidal displacement for smoother curves
            float sineWave = Mathf.Sin(i * frequency) * currentHeightMultiplier;
            float noiseValue = Mathf.PerlinNoise(i * noiseStep, 0) * currentHeightMultiplier * 0.5f; // Smaller influence from noise

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

    /*[SerializeField] private SpriteShapeController groundShapeController;

    [SerializeField, Range(5f, 100f)] private int levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float widthMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float heightMultipler = 2f;
    [SerializeField, Range(0f, 1f)] private float curveSmoothness = 0.5f;
    [SerializeField] private float noiseStep = 0.5f;
    [SerializeField] private float bottom = 10f;

    private Vector3 lastPosition;


    private void OnValidate()
    {
        groundShapeController.spline.Clear();

        for (int i = 0; i < levelLength; i++)
        {
            lastPosition = transform.position + new Vector3(i * widthMultiplier,
                                                            Mathf.PerlinNoise(0, i * noiseStep) * heightMultipler);

            groundShapeController.spline.InsertPointAt(i, lastPosition);

            if (i != 0 && i != levelLength - 1)
            {
                groundShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);

                groundShapeController.spline.SetLeftTangent(i, curveSmoothness * widthMultiplier * Vector3.left);
                groundShapeController.spline.SetRightTangent(i, curveSmoothness * widthMultiplier * Vector3.right);
            }
        }

        groundShapeController.spline.InsertPointAt(levelLength,
                                                   new Vector3(lastPosition.x, transform.position.y - bottom));

        groundShapeController.spline.InsertPointAt(levelLength + 1,
                                                   new Vector3(transform.position.x, transform.position.y - bottom));
    }*/
}
