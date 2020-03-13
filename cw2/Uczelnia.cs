using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class Uczelnia
    {
        [XmlAttribute(AttributeName = "createdAt")]
        public string createdAt;
        [XmlAttribute]
        public string author = "Piotr Dębowski";
        [XmlElement(ElementName = "studenci")]
        public HashSet<Student> studenci;
        [XmlElement(ElementName = "activeStudies")]
        public Dictionary<string, int> activeStudies;
    }
}
