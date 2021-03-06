/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Configuration;
using System.Globalization;

namespace OnTopic.Web.Configuration {

  /*============================================================================================================================
  | CLASS: EDITOR ELEMENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a custom <see cref="ConfigurationElement"/> which represents the (default: OnTopic) editor configuration.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     Permits an application to define whether or not an editor is enabled and, if so, under which conditions it should
  ///     be used.
  ///   </para>
  ///   <para>
  ///     Child elements allow definition of preview and authorization capabilities.Adapted from the Ignia Localization
  ///     library; in the future, these libraries may(and should) share custom configuration classes.
  ///   </para>
  /// </remarks>
  public class EditorElement : ConfigurationElement {

    /*==========================================================================================================================
    | PROPRTY: ENABLED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets whether the (CMS) editor is enabled as defined by the configuration attribute.
    /// </summary>
    [ConfigurationProperty("enabled", DefaultValue = "True", IsRequired = false)]
    public bool Enabled => Convert.ToBoolean(this["enabled"], CultureInfo.InvariantCulture);

    /*==========================================================================================================================
    | PROPRTY: LOCATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the website location of the (CMS) editor as defined by the configuration attribute.
    /// </summary>
    [ConfigurationProperty("location", IsRequired=false)]
    public string Location => this["source"] as string;

    /*==========================================================================================================================
    | ELEMENT: ADMIN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the admin element, which describes administrative rights on the system.
    /// </summary>
    [ConfigurationProperty("admin")]
    public SourceElement Admin => this["admin"] as SourceElement;

  } //Class
} //Namespace