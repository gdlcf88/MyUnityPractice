using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Model
{
    class SocketMessage
    {
        [JsonProperty("class")]
        public string ClassName { get; set; }
        [JsonProperty("action")]
        public string ActionName { get; set; }
        [JsonProperty("data")]
        public MessageData Data { get; set; }

        public abstract class MessageData
        {
            [JsonProperty("playerId")]
            public int PlayerId { get; set; }
            [JsonProperty("roomId")]
            public int RoomId { get; set; }
        }

        public class PlayerEnterMD : MessageData
        {
        }

        public class SendMyStateMD : MessageData
        {
            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}
