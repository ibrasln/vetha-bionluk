using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class EarthTest : MonoBehaviour
    {
        public Slider owlSlider;
        public Slider rabbitSlider;
        public Slider grassSlider;
        public Image owlGraph;
        public Image rabbitGraph;
        public Image grassGraph;
        public Button continueButton;
        public float animationDuration = 5f; // Duration for the slider animation
        public int graphPoints = 100;
        public float graphUpdateInterval = 0.05f; // Time between graph updates during animation

        private float[] owlPopulations;
        private float[] rabbitPopulations;
        private float[] grassPopulations;

        void Start()
        {
            owlPopulations = new float[graphPoints];
            rabbitPopulations = new float[graphPoints];
            grassPopulations = new float[graphPoints];

            continueButton.gameObject.SetActive(false);

            StartCoroutine(AnimateSliders());
        }

        IEnumerator AnimateSliders()
        {
            float elapsedTime = 0f;
            while (elapsedTime < animationDuration)
            {
                float owlValue = Mathf.Lerp(owlSlider.minValue, owlSlider.maxValue, elapsedTime / animationDuration);
                float rabbitValue = Mathf.Lerp(rabbitSlider.minValue, rabbitSlider.maxValue, elapsedTime / animationDuration);
                float grassValue = Mathf.Lerp(grassSlider.minValue, grassSlider.maxValue, elapsedTime / animationDuration);

                owlSlider.value = owlValue;
                rabbitSlider.value = rabbitValue;
                grassSlider.value = grassValue;

                UpdatePopulationArrays(owlValue, rabbitValue, grassValue);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            owlSlider.value = owlSlider.maxValue;
            rabbitSlider.value = rabbitSlider.maxValue;
            grassSlider.value = grassSlider.maxValue;

            UpdatePopulationArrays(owlSlider.value, rabbitSlider.value, grassSlider.value);
            continueButton.gameObject.SetActive(true);
        }

        void UpdatePopulationArrays(float owlValue, float rabbitValue, float grassValue)
        {
            ShiftArray(owlPopulations, owlValue);
            ShiftArray(rabbitPopulations, rabbitValue);
            ShiftArray(grassPopulations, grassValue);
        }

        void ShiftArray(float[] array, float newValue)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
            array[array.Length - 1] = newValue;
        }

        public void OnContinueButtonPressed()
        {
            continueButton.gameObject.SetActive(false);
            StartCoroutine(AnimateGraphs());
        }

        IEnumerator AnimateGraphs()
        {
            for (int i = 0; i < graphPoints; i++)
            {
                UpdateGraph(owlGraph, owlPopulations, i);
                UpdateGraph(rabbitGraph, rabbitPopulations, i);
                UpdateGraph(grassGraph, grassPopulations, i);

                yield return new WaitForSeconds(graphUpdateInterval);
            }
        }

        void UpdateGraph(Image image, float[] values, int currentPoint)
        {
            Texture2D texture = new Texture2D(graphPoints, 1, TextureFormat.RGBA32, false);
            for (int i = 0; i <= currentPoint; i++)
            {
                Color color = new Color(values[i], values[i], values[i]);
                texture.SetPixel(i, 0, color);
            }
            texture.Apply();

            Sprite graphSprite = Sprite.Create(texture, new Rect(0, 0, graphPoints, 1), new Vector2(0.5f, 0.5f));
            image.sprite = graphSprite;
        }
    }
}
