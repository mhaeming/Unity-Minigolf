using TMPro;
using UnityEngine;

namespace Code
{
    public class ShowPriming : MonoBehaviour
    {
        // Start is called before the first frame update
        private bool _riskPrime;
        public TextMeshProUGUI priming;
        public Material brightMaterial;
        public Material darkMaterial;
   
        void Start()
        {
            _riskPrime = GameObject.Find("WorldManager").GetComponent<WorldManager>().riskPrime;
        
            if (_riskPrime)
            {
                // print the risky prime Text
                priming.text = "It’s a rainy day, and a very strong breeze catches hold of your round miniature golf ball figure as soon as you roll outside. You just had a hot tempered conversation with some colleagues at breakfast and decide to take a roll outside at the lane in order to get your head free. You don’t know the exact mapping of this route but are eager to take this challenge.";
                priming.color = Color.white;
                RenderSettings.skybox = darkMaterial;
            }
            else
            {
                // print the calm prime Text
                RenderSettings.skybox = brightMaterial;
                priming.text = "It’s a sunny day, a warm breeze is stroking over the pores of your miniature golf ball skin. You just had a very conversational and relaxed breakfast with your friends at the club house and decide to enjoy the weather and take a roll outside at the lane. You don’t know the exact mapping of this route but still feel motivated and ready to take this new challenge.";
            }
        }


    }
}
