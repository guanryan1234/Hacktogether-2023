using System.Runtime.Serialization;

namespace GraphPOCBlazor.Models
{
    [DataContract]
    public class User
    {
        [DataMember(Name ="displayName", IsRequired = true)]
        public string DisplayName { get; set; }
    }
}
