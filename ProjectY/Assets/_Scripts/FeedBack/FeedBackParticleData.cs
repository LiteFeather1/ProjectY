using UnityEngine;

namespace ProjectY
{
    [System.Serializable]
    public class FeedBackParticleData
    {
        [SerializeField] private Material _colour;
        [SerializeField] private int _amount;
        private int _finalAmount;

        public Vector3 Position { get; set; }
        public Color Colour { get => _colour.color; }
        public int Amount { get => _finalAmount; } 


        public void SetPosAndAmount(Vector3 pos, int amount)
        {
            Position = pos;
            _finalAmount = _amount + amount;
        }
    }
}