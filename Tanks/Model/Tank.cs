using System.Runtime.Serialization;


[DataContract]
    public class ServerTank
    {       
        [DataMember] public long ip { get; set; }
        [DataMember] public float x { get; set; }
        [DataMember] public float y { get; set; }
        [DataMember] public float _speed { get; set; }
    }





