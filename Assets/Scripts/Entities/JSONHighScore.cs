using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class JSONHighScore
    {
        public class SaveData
        {
            public List<InfoHighScore> SaveInfoHighScore { get; set; }
        }

        public class InfoHighScore {
            public string playerName { get; set; }
            public string musicName { get; set; }
            public string highScore { get; set; }
        }
    }
}
