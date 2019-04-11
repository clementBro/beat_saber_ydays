using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Entities
{
    public class JsonEntities
    {
        /// <summary>
        /// Données à sauvegarder
        /// </summary>
        public class DataToSave
        {
            public int BPM { get; set; }
            public float Time { get; set; }
            public List<NoteToSave> NotesToSave { get; set; }
        }

        /// <summary>
        /// Note à sauvegarder
        /// </summary>
        public class NoteToSave
        {
            public float PositionX { get; set; }
            public float PositionY { get; set; }
            public float PositionZ { get; set; }

            public float RotationZ { get; set; }
        }
    }
}
