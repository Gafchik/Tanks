using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Corsaries_by_VBUteamGKMI;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.Serialization;


    [DataContract]
    public class ServerTank
    {
        public ServerTank()
        {
            _speed = 7;
        }
        [DataMember] public long ip { get; set; }
        [DataMember] public float x { get; set; }
        [DataMember] public float y { get; set; }
        [DataMember] public float _speed { get; set; }
    }





