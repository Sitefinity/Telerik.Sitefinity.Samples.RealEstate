using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Sitefinity.Utilities.Zip;


namespace TemplateImporter
{



    [XmlRoot("template")]
    [DataContract]
    public partial class Template 
    {

        [XmlElement("layout")]
        [DataMember]
        public Layout layout { get; set; }

        [XmlElement("metadata")]
        [DataMember]
        public Metadata metadata { get; set; }

        [XmlIgnore]
        [DataMember]
        public CSS[] css { get; set; }

        [XmlIgnore]
        [DataMember]
        public string background { get; set; }

        [XmlIgnore]
        [DataMember]
        public string sessionid { get; set; }

        [XmlIgnore]
        [DataMember]
        public SessionState sessionstate { get; set; }


        

        /// <summary>
        /// Serializes the templalte object to XML and returns the memory stream.
        /// </summary>
        /// <returns></returns>
        public MemoryStream SerializeToXML()
        {

            

            MemoryStream stream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(Template));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);

            xmlTextWriter.Formatting = Formatting.Indented;

            xs.Serialize(xmlTextWriter, this);

            stream = (MemoryStream)xmlTextWriter.BaseStream;

            return stream;
        }

        public void SerializeToFile(string filepath)
        {
            FileStream writer = new FileStream(filepath, FileMode.Create);

            DataContractSerializer ser =
                new DataContractSerializer(typeof(Template));
            
            ser.WriteObject(writer, this);
            writer.Close();
        }

         /// <summary>
        /// Creates a template from an already exported xml file
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static Template CreateFromStream(Stream stream)
        {
            if (stream == null)
                return null;
            
            XmlSerializer serializer = new XmlSerializer(typeof(Template));
            StreamReader reader = new StreamReader(stream);

            object obj = serializer.Deserialize(reader);
            Template deserializedTemplate = (Template)obj;
            reader.Close();

            return deserializedTemplate;
        }

    }


    [DataContract]
    public class Metadata
    {
        [XmlElement("meta")]
        [DataMember]
        public MetadataItem[] metadataitems { get; set; }
    }

    [DataContract]
    public class MetadataItem
    {
        [XmlAttribute("id")]
        [DataMember]
        public string id { get; set; }

        [XmlElement("value")]
        [DataMember]
        public string value { get; set; }
    }


    [DataContract]
    public class Layout
    {
        [XmlElement("placeholder")]
        [DataMember]
        public Placeholder[] placeholders { get; set; }
    }

    [DataContract]
    public class Placeholder
    {

        [XmlAttribute("id")]
        [DataMember]
        public string id { get; set; }


        [DataMember]
        public string layoutid { get; set; }

        [XmlElement("layoutwidget",IsNullable=false)]
        [DataMember]
        public LayoutWidget layoutwidget { get; set; }
    }

    [DataContract]
    public class LayoutWidget
    {
        [XmlElement("column")]
        [DataMember]
        public Column[] columns { get; set; }

        [DataMember]
        public bool custom { get; set; }

        [DataMember]
        public string backgroundcolor { get; set; }

        [DataMember]
        public string backgroundimageurl { get; set; }

        [DataMember]
        public string backgroundimage { get; set; }

        [DataMember]
        public string background_position { get; set; }

        [DataMember]
        public string background_repeat { get; set; }

        [DataMember]
        public string background_attachment { get; set; }

    }

    [DataContract]
    public class Column
    {
        [XmlAttribute("width")]
        [DataMember]
        public int width { get; set; }

        [XmlElement("widget",IsNullable=false)]
        [DataMember]
        public Widget widget { get; set; }
    }

    [DataContract]
    public class Widget
    {
        
        [XmlElement("type")]
        [DataMember]
        public string type { get; set; }

        [XmlElement("sfID")]
        [DataMember]
        public string sfID { get; set; }

        [XmlElement("properties")]
        [DataMember]
        public Properties properties { get; set; }

        [XmlElement("cssclass")]
        [DataMember]
        public string cssclass { get; set; }
    }

    [DataContract]
    public class Properties
    {
        [XmlElement("text")]
        [DataMember]
        public string text;

        [XmlElement("navigationtype")]
        [DataMember]
        public string navigationtype;

        [XmlElement("filename")]
        [DataMember]
        public string filename;

        [XmlElement("size")]
        [DataMember]
        public string size;

        [XmlElement("navigationskin")]
        [DataMember]
        public string navigationskin;

        [XmlIgnore]
        [DataMember]
        public string url;
    }


    [DataContract]
    public class CSS
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string[] properties { get; set; }

        [DataMember]
        public string[] values { get; set; }
    }

    [DataContract]
    public class SessionState
    {
        [DataMember]
        public BackgroundState backgroundstate { get; set; }

        [DataMember]
        public LayoutState layoutstate { get; set; }

        [DataMember]
        public ContentState contentstate { get; set; }

        [DataMember]
        public string[] uploaded_images { get; set; }

        [DataMember]
        public string[] uploaded_backgrounds { get; set; }

        [DataMember]
        public int id { get; set; }
    }

    [DataContract]
    public class BackgroundState
    {
        [DataMember]
        public string image {get; set;}
        [DataMember]
        public string color {get; set;}
        [DataMember]
        public string position {get; set;}
        [DataMember]
        public string repeat {get; set;}
        [DataMember]
        public string attachment {get; set;}
    }

    [DataContract]
    public class LayoutState
    {
        [DataMember]
        public string position {get; set;}
        [DataMember]
        public string width {get; set;}
        [DataMember]
        public string margin_left {get; set;}
        [DataMember]
        public string left {get; set;}
    }

    [DataContract]
    public class ContentState
    {
        [DataMember]
        public string background {get; set;}

        [DataMember]
        public Textblocks textblocks { get; set; }

        [DataMember]
        public string navigationskin { get; set; }

        [DataMember]
        public string wrapperclasses { get; set; }
    }

    [DataContract]
    public class Textblocks
    {
        [DataMember]
        public string line_height { get; set; }
        [DataMember]
        public string basestyle { get; set; }
        [DataMember]
        public string font_size { get; set; }
        [DataMember]
        public Textblock text { get; set; }
        [DataMember]
        public Textblock quote { get; set; }
        [DataMember]
        public Textblock heading { get; set; }
        [DataMember]
        public Textblock link { get; set; }
    }

    [DataContract]
    public class Textblock
    {
        [DataMember]
        public string font_family { get; set; }

        [DataMember]
        public string color { get; set; }
    }

}
