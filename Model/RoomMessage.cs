using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Model
{
    internal class RoomMessage
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
        [JsonProperty("position")]
        public float[] Position { get; set; }
    }
}
