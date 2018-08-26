using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PlayerState
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }

        [JsonProperty("position")]
        public float[] Position { get; set; }

        [JsonProperty("rotation")]
        public float[] Rotation { get; set; }

        [JsonProperty("isShooting")]
        public bool IsShooting { get; set; }
    }
}
