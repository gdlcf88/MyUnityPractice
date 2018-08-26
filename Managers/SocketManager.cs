using Assets.Scripts.MatePlayer;
using Assets.Scripts.Model;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

namespace Assets.Scripts.Managers
{
    public class SocketManager : MonoBehaviour {

        public float TimeBetweenSendMyState = 0.015f;

        private Button _btn;
        private int _myPlayerId;
        private GameObject _player;
        private GameObject _matePlayer;
        private GameObject _matePlayerGunBarrelEnd;
        private WebSocket _ws;
        private float _timer;

        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(OnClick);
        }

        private void Update()
        {
            if (!_player) return;
            _timer += Time.deltaTime;
            if (!(_timer > TimeBetweenSendMyState)) return;
            _timer = 0f;
            SendMyState();
        }

        public void OnClick ()
        {
            foreach (var tmp in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (tmp.GetComponent("MatePlayerHealth"))
                {
                    _matePlayer = tmp;
                    _matePlayerGunBarrelEnd = GameObject.Find("MatePlayerGunBarrelEnd");
                }
                else if (tmp.GetComponent("PlayerHealth"))
                {
                    _player = tmp;
                }
            }
            _myPlayerId = int.Parse(GameObject.FindGameObjectWithTag("MyPlayerId").GetComponent<InputField>().text);
            _ws = new WebSocket("ws://s.bphots.com:9501");
            _ws.OnMessage += OnMessage;
            _ws.Connect();

            var obj = new SocketMessage
            {
                ClassName = "Room",
                ActionName = "playerEnter",
                Data = new SocketMessage.PlayerEnterMD
                {
                    PlayerId = 1,
                    RoomId = 1000
                }
            };
            _ws.SendAsync(JsonConvert.SerializeObject(obj), null);
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            var data = JsonConvert.DeserializeObject<PlayerState>(e.Data);
            var playerId = data.PlayerId;
            var position = data.Position;
            var rotation = data.Rotation;
            var isShooting = data.IsShooting;
            if (playerId != _myPlayerId)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        var movement = _matePlayer.GetComponent<MatePlayerMovement>();
                        var shooting = _matePlayerGunBarrelEnd.GetComponent<MatePlayerShooting>();
                        movement.TargetPosition.Set(position[0], position[1], position[2]);
                        movement.Rotation.Set(rotation[0], rotation[1], rotation[2], rotation[3]);
                        shooting.IsShooting = isShooting;
                        Debug.Log(data.IsShooting);
                    }
                );
            }
        }

        private void SendMyState()
        {
            var state = new PlayerState()
            {
                PlayerId = _myPlayerId,
                Position = new float[] {
                    _player.transform.position.x,
                    _player.transform.position.y,
                    _player.transform.position.z

                },
                Rotation = new float[]
                {
                    _player.transform.rotation.x,
                    _player.transform.rotation.y,
                    _player.transform.rotation.z,
                    _player.transform.rotation.w
                },
                IsShooting = Input.GetButton("Fire1")
            };
            var msg = new SocketMessage
            {
                ClassName = "Room",
                ActionName = "sendToRoom",
                Data = new SocketMessage.SendMyStateMD
                {
                    PlayerId = _myPlayerId,
                    RoomId = 1000,
                    Message = JsonConvert.SerializeObject(state),
                }
            };
            var sendContent = JsonConvert.SerializeObject(msg);
            _ws.Send(sendContent);
        }
    }
}
