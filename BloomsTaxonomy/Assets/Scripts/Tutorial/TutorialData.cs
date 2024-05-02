using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME")]
    public class TutorialData : ScriptableObject
    {
        public Sprite RobotSprite;
        public string Instruction;
    }
}