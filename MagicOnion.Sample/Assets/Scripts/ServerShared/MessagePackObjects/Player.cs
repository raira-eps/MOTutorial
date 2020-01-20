using MessagePack;
using UnityEngine;

namespace MagicOnionSample.Shared.MessagePackObjects
{
    [MessagePackObject()]
    public struct Player
    {
        [Key(0)]
        public string Name { get; set; }
        [Key(1)]
        public Vector3 Position { get; set; }
        [Key(2)]
        public Quaternion Rotation { get; set; }
        
        public Player(string name, Vector3 position, Quaternion rotation)
        {
             Name = name;
            Position = position;
            Rotation = rotation;
        }
    }
}
