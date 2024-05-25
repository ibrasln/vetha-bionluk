using System;
using System.Collections;
using Mission.Report;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Earth
{
    public class EarthMission : Mission
    {
        [Space(7)][Header("MISSION PROPERTIES")]
        [ReadOnly] public MissionPhase MissionPhase = MissionPhase.Grass;
        [ReadOnly] public bool CanAnimateSliders;
        [ReadOnly] public Slider CurrentSlider;
        [SerializeField] private DropdownReport rabbitReport;
        [SerializeField] private DropdownReport owlReport;
        
        [Space(7)]
        /// <summary>
        /// 0 -> Owl
        /// 1 -> Rabbit
        /// 2 -> Grass
        /// </summary>
        [SerializeField] private Slider[] sliders;
        
        /// <summary>
        /// 0 -> Owl
        /// 1 -> Rabbit
        /// 2 -> Grass
        /// </summary>
        [SerializeField] private UILineRenderer[] graphs;

        /// <summary>
        /// 0 -> Decrease
        /// 1 -> Increase
        /// 2 -> Zero
        /// </summary>
        [SerializeField] private PointsData[] pointsDatas;
        
        [Space(7)] [Header("SLIDER PROPERTIES")] 
        [SerializeField] private float animationDuration;
        [SerializeField] private Button continueButton;
        
        public void OnGraphsAnimated()
        {
            switch (MissionPhase)
            {
                case MissionPhase.Owl:
                    owlReport.Open();
                    break;
                case MissionPhase.Rabbit:
                    rabbitReport.Open();
                    break;
                case MissionPhase.Grass:
                    Report.Open();
                    break;
                case MissionPhase.Tutorial:
                    Planet.StartTutorial(Planet.CurrentTutorialIndex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void Update()
        {
            if (CanAnimateSliders) AnimateSliders();
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                SetGraphs();
                DrawGraphs();
            }
        }
        
        public void Initialize(float value)
        {
            SetCurrentSlider();
            SetGraphs();
            AnimateSlider(CurrentSlider, value);
        }

        private void SetGraphs()
        {
            switch (MissionPhase)
            {
                case MissionPhase.Owl:
                    graphs[0].SetPoints(pointsDatas[2].Points);
                    graphs[1].SetPoints(pointsDatas[1].Points);
                    graphs[2].SetPoints(pointsDatas[0].Points);
                    break;
                case MissionPhase.Rabbit:
                    graphs[0].SetPoints(pointsDatas[0].Points);
                    graphs[1].SetPoints(pointsDatas[2].Points);
                    graphs[2].SetPoints(pointsDatas[1].Points);
                    break;
                case MissionPhase.Grass:
                    graphs[0].SetPoints(pointsDatas[0].Points);
                    graphs[1].SetPoints(pointsDatas[0].Points);
                    graphs[2].SetPoints(pointsDatas[2].Points);
                    break;
                case MissionPhase.Tutorial:
                    graphs[0].SetPoints(pointsDatas[3].Points);
                    graphs[1].SetPoints(pointsDatas[4].Points);
                    graphs[2].SetPoints(pointsDatas[5].Points);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void DrawGraphs()
        {
            foreach (UILineRenderer graph in graphs)
            {
                graph.StartDrawing();
            }

            CanAnimateSliders = true;
        }

        public void AnimateSliders()
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                if (graphs[i].CurrentPointIndex >= graphs[i].Points.Length) break;
                UpdateSliderValue(sliders[i], graphs[i].Points[graphs[i].CurrentPointIndex].y);
            }
        }

        public void AnimateSlider(Slider slider, float value)
        {
            StartCoroutine(AnimateSliderRoutine(slider, value));
        }

        private IEnumerator AnimateSliderRoutine(Slider slider, float value)
        {
            float startValue = slider.value;
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / animationDuration);
                slider.value = Mathf.Lerp(startValue, value, t);

                yield return null;
            }

            slider.value = value;
            continueButton.interactable = true;
        }

        public void ResetSliderValues()
        {
            foreach (Slider slider in sliders)
            {
                UpdateSliderValue(slider, 50);
            }
        }

        
        private void UpdateSliderValue(Slider slider, float value)
        {
            slider.value = value;
        }

        private void SetCurrentSlider()
        {
            CurrentSlider = MissionPhase switch
            {
                MissionPhase.Owl => sliders[0],
                MissionPhase.Rabbit => sliders[1],
                MissionPhase.Grass => sliders[2],
                MissionPhase.Tutorial => sliders[0],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetMissionPhase(MissionPhase phase) => MissionPhase = phase;
    }
    
    public enum MissionPhase
    {
        Owl,
        Rabbit,
        Grass,
        Tutorial
    }
}