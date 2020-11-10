using UnityEngine;
using System.Collections;

namespace Assets.Code
{
    //Class describing !basic! enemy properties
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private string enemyName = "Enemy!";
        public string EnemyName
        {
            get { return enemyName; }
        }


        private void Awake()
        {
            EntityRegister.Enemies.Add(this);
        }

        private void OnDestroy()
        {
            EntityRegister.Enemies.Remove(this);
        }
    }
}