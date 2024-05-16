using System;
using UnityEngine;
using UnityEngine.UI;

namespace DragDrop
{
    public class DragDropItem : MonoBehaviour
    {
        protected Image image;
        public int ObjectId;

        private void Awake()
        {
            image = GetComponent<Image>();
        }
    }
}