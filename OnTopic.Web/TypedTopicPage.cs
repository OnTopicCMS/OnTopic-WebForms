/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Web {

  /*============================================================================================================================
  | CLASS: TYPED TOPIC PAGE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a generic page class implementation which is aware of the strongly-typed Topic.
  /// </summary>
  /// <typeparam name="T" />
  public class TypedTopicPage<T> : TopicPage where T : Topic {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TypedTopicPage{T}"/> class.
    /// </summary>
    public TypedTopicPage() : base() { }

  } //Class
} //Namespace