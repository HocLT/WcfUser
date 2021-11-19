using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfUser
{
    [DataContract]
    public class UserContract
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Fullname { get; set; }
        [DataMember]
        public string Birthday { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public int? Role { get; set; }
    }
}