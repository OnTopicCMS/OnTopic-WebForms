/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Configuration;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;
using OnTopic.Internal.Diagnostics;
using OnTopic.Web.Configuration;

namespace OnTopic.Web {

  /*============================================================================================================================
  | CLASS: TOPICS ROUTE HANDLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides routing for any path that matches a path managed by the Topics database.
  /// </summary>
  /// <remarks>
  ///   If a match is found, then the user is routed to a template corresponding to the Topic's <see cref="Topic.ContentType"/>.
  ///   Otherwise, the originally-requested page is rendered (although this may yield a 404).
  /// </remarks>
  public class TopicsRouteHandler : IRouteHandler {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TopicsRouteHandler"/> class.
    /// </summary>
    public TopicsRouteHandler() { }

    /*==========================================================================================================================
    | PROPERTY: VIEWS PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the expected location for View files; attempts to retrieve the value from the <c><topics /></c> configuration
    ///   section, but defaults to ~/Common/Templates/.
    /// </summary>
    private static string ViewsPath {
      get {
        var viewsPath           = "~/Common/Templates/";
        var topicsSection       = (TopicsSection)ConfigurationManager.GetSection("topics");
        if (topicsSection != null && topicsSection.Views != null && !String.IsNullOrEmpty(topicsSection.Views.Path)) {
          viewsPath             = topicsSection.Views.Path;
        }
        return viewsPath;
      }
    }

    /*==========================================================================================================================
    | GET HTTP HANDLER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Evaluates the <see cref="RouteData"/> to identify the most appropriate HTTP Handler to use, then returns an instance
    ///   of that handler.
    /// </summary>
    /// <param name="requestContext">The HTTP request context.</param>
    /// <returns>An <see cref="IHttpHandler"/> instance.</returns>
    /// <requires description="The HTTP request context must be provided." exception="T:System.ArgumentNullException">
    ///   requestContext != null
    /// </requires>
    /// <exception cref="Exception">
    ///   The ContentType for the Topic <c>topic.UniqueKey</c> (<c>topic.Id</c>) is not set. Set the ContentType value of the
    ///   Topic based on the template that should be associated with it. E.g., a standard page will have the ContentType of
    ///   Page.
    /// </exception>
    public IHttpHandler GetHttpHandler(RequestContext requestContext) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(requestContext, nameof(requestContext));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var routeData             = requestContext.RouteData;
      var routingService        = new WebFormsTopicRoutingService(TopicRepository.DataProvider, requestContext, ViewsPath);
      var topic                 = routingService.GetCurrentTopic();
      var viewName              = routingService.View;

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate path
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (topic == null) {
        return BuildManager.CreateInstanceFromVirtualPath(ViewsPath, typeof(Page)) as IHttpHandler;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Set target path
      \-----------------------------------------------------------------------------------------------------------------------*/
      var targetPath            = ViewsPath + viewName + ".aspx";

      /*------------------------------------------------------------------------------------------------------------------------
      | Set route data
      \-----------------------------------------------------------------------------------------------------------------------*/
      routeData.Values          ["view"]                        = viewName;
      routeData.Values          ["targetPath"]                  = targetPath;
      routeData.Values          ["contentType"]                 = topic.ContentType;
      routeData.Values          ["directory"]                   = requestContext.HttpContext.Request.Url.AbsolutePath;
      routeData.Values          ["uniqueKey"]                   = topic.GetUniqueKey();

      /*------------------------------------------------------------------------------------------------------------------------
      | SET TARGET TYPES
      >-------------------------------------------------------------------------------------------------------------------------
      | ###REM JJC101713: Failed attempt to inject strongly typed Topic into page instantiation.  May revisit later.
      >-------------------------------------------------------------------------------------------------------------------------
      Type      topicPageType           = typeof(TypedTopicPage<>);
      Type      topicType               = topic.GetType();
      Type      typedPageType           = topicPageType.MakeGenericType(new Type[] {topicType});
      \-----------------------------------------------------------------------------------------------------------------------*/

      /*------------------------------------------------------------------------------------------------------------------------
      | Return page handler
      \-----------------------------------------------------------------------------------------------------------------------*/
      return BuildManager.CreateInstanceFromVirtualPath(targetPath, typeof(TopicPage)) as IHttpHandler;

    }

  } //Class
} //Namespace