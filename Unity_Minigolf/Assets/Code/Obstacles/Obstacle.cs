using UnityEngine;

namespace Code.Obstacles
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Obstacle : MonoBehaviour
    {
        private Color _baseColor;
        void Awake()
        {
            _baseColor = gameObject.GetComponent<MeshRenderer>().material.color;
        }

        /// <summary>
        /// Makes sure that transparent obstacles are colored correctly when moved back into the object pool
        /// </summary>
        void OnDisable()
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", _baseColor);
        }
        
    }
}
