using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    class Studies
    {
        [XmlElement(ElementName = "name")]
        public string StudiesName { get; set; }
        [XmlElement(ElementName = "mode")]
        public string StudiesMode { get; set; }
    }
}
