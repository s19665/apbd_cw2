using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    class Student
    {
        [XmlElement(ElementName = "fname")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "lname")]
        public string SecondtName { get; set; }
        [XmlElement(ElementName = "ename")]
        public string Email { get; set; }
        [XmlElement(ElementName = "brithdate")]
        public DateTime Birthdate { get; set; }
        [XmlElement(ElementName = "motherName")]
        public string MotherName { get; set; }
        [XmlElement(ElementName = "fatherName")]
        public string FathersName { get; set; }
        [XmlElement(ElementName = "studies")]
        public Studies Studies { get; set; }
        [XmlAttribute(AttributeName = "indexNumber")]
        public string IndexNumber;
    }
}
