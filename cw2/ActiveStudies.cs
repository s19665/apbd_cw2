using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class ActiveStudies
    {
        [XmlAttribute(AttributeName = "name")]
        public string id;
        [XmlAttribute(AttributeName = "numberOfStudents")]
        public int value;
    }
}
