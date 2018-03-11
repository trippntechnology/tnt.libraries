using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace TNT.Configuration
{
	/// <summary>
	/// Represents a xml section containing a serialized object of type T
	/// </summary>
	/// <typeparam name="T">Type of serialized object</typeparam>
	public class XmlSection<T> : BaseConfigurationSection where T : class, new()
	{
		/// <summary>
		/// Transform used to add namespace to type attribute
		/// </summary>
		private const string TYPE_TRANSFORM = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>

	<!-- identity template -->
	<xsl:template match='@*|node()'>
		<xsl:copy>
			<xsl:apply-templates select='@*|node()'/>
		</xsl:copy>
	</xsl:template>

	<xsl:template name='get-type'>
		<xsl:param name='string' />
		<xsl:choose>
			<xsl:when test=""contains($string, ',')"">
				<xsl:call-template name='get-type'>
					<xsl:with-param name='string' select=""substring-before($string, ',')"" />
				</xsl:call-template>
			</xsl:when>
			<xsl:when test=""contains($string, '.')"">
				<xsl:call-template name='get-type'>
					<xsl:with-param name='string' select=""substring-after($string, '.')"" />
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select='$string' />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match='node()[@type]'>
		<xsl:copy>
			<xsl:attribute name='type' namespace='http://www.w3.org/2001/XMLSchema-instance'>
				<xsl:call-template name='get-type'>
					<xsl:with-param name='string' select='@type' />
				</xsl:call-template>
			</xsl:attribute>
			<xsl:apply-templates select='@*|node()'/>
		</xsl:copy>
	</xsl:template>

</xsl:stylesheet>";

		private T DeserializedObject;

		/// <summary>
		/// Retrieves the serialized object described in the XML section
		/// </summary>
		/// <param name="sectionName">Section name</param>
		/// <returns>Object of type T</returns>
		public static T Deserialize(string sectionName, bool suppressException = true)
		{
			XmlSection<T> section = null;

			try
			{
				section = Create<XmlSection<T>>(sectionName);
			}
			catch
			{
				if (!suppressException)
				{
					throw;
				}
			}

			if (null == section)
			{
				return new T();
			}
			else
			{
				return section.DeserializedObject;
			}
		}

		#region Overrides

		/// <summary>
		/// Deserializes the XML section
		/// </summary>
		/// <param name="reader">Reader containing the xml within the section</param>
		protected override void DeserializeSection(XmlReader reader)
		{
			XmlDocument xmlSection = new XmlDocument();
			xmlSection.Load(reader);
			References references = new References();

			// Find References if exist
			XmlNode refNode = xmlSection.SelectSingleNode("//References");

			// Find serialized object
			XmlNode contentNode = xmlSection.SelectSingleNode(string.Format("/{0}/*[not(self::References)]", this.SectionInformation.SectionName));

			if (refNode != null)
			{
				references = Deserialize<References>(refNode.OuterXml, new Type[0]);
			}
			else
			{
				var attrs = contentNode.SelectNodes("//@type");

				foreach (XmlNode attr in attrs)
				{
					references.Add(new Reference() { Type = attr.Value });
				}
			}

			StringBuilder sb = new StringBuilder();
			XmlDocument xslDoc = new XmlDocument();
			xslDoc.LoadXml(TYPE_TRANSFORM);

			XmlDocument content = new XmlDocument();
			content.LoadXml(contentNode.OuterXml);

			XslCompiledTransform xsl = new XslCompiledTransform();
			xsl.Load(xslDoc);

			using (StringWriter sw = new StringWriter(sb))
			{
				xsl.Transform(content, null, sw);
			}

			content.LoadXml(sb.ToString());

			this.DeserializedObject = Deserialize<T>(content.OuterXml, references.Types);
		}

		private A Deserialize<A>(string content, Type[] types)
		{
			using (StringReader sr = new StringReader(content))
			using (XmlTextReader tr = new XmlTextReader(sr))
			{
				XmlSerializer deser = new XmlSerializer(typeof(A), types);
				return (A)deser.Deserialize(tr);
			}
		}

		#endregion
	}
}
