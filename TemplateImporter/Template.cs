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
        public Layout Layout { get; set; }

        [XmlElement("metadata")]
        [DataMember]
        public Metadata Metadata { get; set; }

        [XmlIgnore]
        [DataMember]
        public CSS[] Css { get; set; }

        [XmlIgnore]
        [DataMember]
        public string Background { get; set; }

        [XmlIgnore]
        [DataMember]
        public string Sessionid { get; set; }

        [XmlIgnore]
        [DataMember]
        public SessionState Sessionstate { get; set; }

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
        public MetadataItem[] Metadataitems { get; set; }
    }

    [DataContract]
    public class MetadataItem
    {
        [XmlAttribute("id")]
        [DataMember]
        public string Id { get; set; }

        [XmlElement("value")]
        [DataMember]
        public string Value { get; set; }
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
        public string Id { get; set; }

        [DataMember]
        public string Layoutid { get; set; }

        [XmlElement("layoutwidget",IsNullable=false)]
        [DataMember]
        public LayoutWidget Layoutwidget { get; set; }
    }

    [DataContract]
    public class LayoutWidget
    {
        [XmlElement("column")]
        [DataMember]
        public Column[] Columns { get; set; }

        [DataMember]
        public bool Custom { get; set; }

        [DataMember]
        public string Backgroundcolor { get; set; }

        [DataMember]
        public string Backgroundimageurl { get; set; }

        [DataMember]
        public string Backgroundimage { get; set; }

        [DataMember]
        public string Background_position { get; set; }

        [DataMember]
        public string Background_repeat { get; set; }

        [DataMember]
        public string Background_attachment { get; set; }
    }

    [DataContract]
    public class Column
    {
        [XmlAttribute("width")]
        [DataMember]
        public int Width { get; set; }

        [XmlElement("widget",IsNullable=false)]
        [DataMember]
        public Widget Widget { get; set; }
    }

    [DataContract]
    public class Widget
    {
        
        [XmlElement("type")]
        [DataMember]
        public string Type { get; set; }

        [XmlElement("sfID")]
        [DataMember]
        public string SfID { get; set; }

        [XmlElement("properties")]
        [DataMember]
        public Properties Properties { get; set; }

        [XmlElement("cssclass")]
        [DataMember]
        public string Cssclass { get; set; }
    }

    [DataContract]
    public class Properties
    {
        [XmlElement("text")]
        [DataMember]
        public string Text;

        [XmlElement("navigationtype")]
        [DataMember]
        public string Navigationtype;

        [XmlElement("filename")]
        [DataMember]
        public string Filename;

        [XmlElement("size")]
        [DataMember]
        public string Size;

        [XmlElement("navigationskin")]
        [DataMember]
        public string Navigationskin;

        [XmlIgnore]
        [DataMember]
        public string Url;
    }


    [DataContract]
    public class CSS
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string[] Properties { get; set; }

        [DataMember]
        public string[] Values { get; set; }
    }

    [DataContract]
    public class SessionState
    {
        [DataMember]
        public BackgroundState Backgroundstate { get; set; }

        [DataMember]
        public LayoutState Layoutstate { get; set; }

        [DataMember]
        public ContentState Contentstate { get; set; }

        [DataMember]
        public string[] Uploaded_images { get; set; }

        [DataMember]
        public string[] Uploaded_backgrounds { get; set; }

        [DataMember]
        public int Id { get; set; }
    }

    [DataContract]
    public class BackgroundState
    {
        [DataMember]
        public string Image {get; set;}
        [DataMember]
        public string Color {get; set;}
        [DataMember]
        public string Position {get; set;}
        [DataMember]
        public string Repeat {get; set;}
        [DataMember]
        public string Attachment {get; set;}
    }

    [DataContract]
    public class LayoutState
    {
        [DataMember]
        public string Position {get; set;}
        [DataMember]
        public string Width {get; set;}
        [DataMember]
        public string Margin_left {get; set;}
        [DataMember]
        public string Left {get; set;}
    }

    [DataContract]
    public class ContentState
    {
        [DataMember]
        public string Background {get; set;}

        [DataMember]
        public Textblocks Textblocks { get; set; }

        [DataMember]
        public string Navigationskin { get; set; }

        [DataMember]
        public string Wrapperclasses { get; set; }
    }

    [DataContract]
    public class Textblocks
    {
        [DataMember]
        public string Line_height { get; set; }
        [DataMember]
        public string Basestyle { get; set; }
        [DataMember]
        public string Font_size { get; set; }
        [DataMember]
        public Textblock Text { get; set; }
        [DataMember]
        public Textblock Quote { get; set; }
        [DataMember]
        public Textblock Heading { get; set; }
        [DataMember]
        public Textblock Link { get; set; }
    }

    [DataContract]
    public class Textblock
    {
        [DataMember]
        public string Font_family { get; set; }

        [DataMember]
        public string Color { get; set; }
    }

}
