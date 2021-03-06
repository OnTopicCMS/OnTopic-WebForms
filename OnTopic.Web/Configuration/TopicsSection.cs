/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Configuration;

namespace OnTopic.Web.Configuration {

  /*============================================================================================================================
  | CLASS: TOPICS SECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a customized version of the <see cref="ConfigurationSection"/> class in order to permit configuration of the
  ///   Topics data management classes via the application's web.config.
  /// </summary>
  public class TopicsSection : ConfigurationSection {

    /*==========================================================================================================================
    | FACTORY METHOD: GET CONFIG
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the <c>topics</c> element from the web.config as the configuration section.
    /// </summary>
    /// <returns>The <c>topics</c> section of the implementing client's configuration.</returns>
    public static TopicsSection GetConfig() => ConfigurationManager.GetSection("topics") as TopicsSection;

    /*==========================================================================================================================
    | ATTRIBUTE: ROOT TOPIC NAMESPACE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the root (parent) Topic expected to be defined for Topics data.
    /// </summary>
    [ConfigurationProperty("rootTopicNamespace", DefaultValue = "Root")]
    public string RootTopicNamespace {
      get => (string)this["rootTopicNamespace"];
      set => this["rootTopicNamespace"] = value;
    }

    /*==========================================================================================================================
    | ATTRIBUTE: TOPIC DELIMITER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the delimiter character expected to be used for separating Topics (Topic keys).
    /// </summary>
    [ConfigurationProperty("topicDelimiter", DefaultValue = "/")]
    public string TopicDelimiter {
      get => (string)this["topicDelimiter"];
      set => this["topicDelimiter"] = value;
    }

    /*==========================================================================================================================
    | ELEMENT: VERSIONING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the value of the <versioning /> element from the configuration.
    /// </summary>
    [ConfigurationProperty("versioning")]
    public VersioningElement Versioning => this["versioning"] as VersioningElement;

    /*==========================================================================================================================
    | ELEMENT: EDITOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the value of the <editor /> element from the configuration.
    /// </summary>
    [ConfigurationProperty("editor")]
    public EditorElement Editor => this["editor"] as EditorElement;

    /*==========================================================================================================================
    | ELEMENT: VIEWS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the value of the <view /> element from the configuration.
    /// </summary>
    [ConfigurationProperty("views")]
    public ViewsElement Views => this["views"] as ViewsElement;

    /*==========================================================================================================================
    | COLLECTION: PAGE TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the value of the <pageTypes /> element from the configuration.
    /// </summary>
    public PageTypeElementCollection GetPageTypes() => this["pageTypes"] as PageTypeElementCollection;

  } //Class
} //Namespace