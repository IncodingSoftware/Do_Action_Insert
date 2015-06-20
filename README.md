<strong>disclaimer</strong>: this article covers the most powerful links in IML, when <strong>action</strong> ( <strong>direct</strong>, <strong>ajax</strong>, <strong>submit</strong> ) is performed and findings (with or with no <a href="http://blog.incframework.com/client-template/"><strong>template</strong></a>) are in DOM-element. This method has appeared in different examples in other articles, but today I’m going to make a review of all possibilities of this links in the <strong>Incoding Framework </strong>context<strong> </strong>

Note: article code examples are available on <a href="https://github.com/IncodingSoftware/Do_Action_Insert"><em>GitHub</em></a><em>.</em>
<h1 style="text-align: center;">Do</h1>
The first unit of our links is Do. It may seem to be a “noise”. But when you understand all aspects of its work that influence on browser event service it will become clear that:
<ul>
	<li><strong>Do ( <a href="http://incframework.com/en-US/TutorialStructure#Do">sandbox</a> )</strong> - does not influence on behavior</li>
	<li><strong>DoWithPreventDefault ( <a href="http://incframework.com/en-US/TutorialStructure#Do_With_Prevent_Default">sandbox</a> )</strong> -<span style="line-height: 1.5;">cancels event, nullifying  it but does not stop continuing to spread the event.</span></li>
</ul>
Note: it is used for <em>Submit</em> form event to prevent a send with no <em>Ajax</em> and for <em>Anchor</em> to block transition on <em>Href</em>
<ul>
	<li><strong>DoWithStopPropagation – </strong>stops continuing event spreading (propagation) but does not cancel the event.</li>
</ul>
Note: is used for Submit event if an output agent is tied to <em>input (type submit).</em>
<ul>
	<li><strong>DoWithPreventDefaultAndStopPropagation</strong>  -  cancels and annuals event and stops continuing spreading (propagation) event.</li>
</ul>
<h1 style="text-align: center;">Action</h1>
The base element in work of our links is <strong>Action</strong>. It helps to get information using different ways, but all of them are united by the form that contains objects.
<h4 style="text-align: left;">Incoding Result</h4>
This is a successor from <strong>JsonResult, where on default included </strong><a href="http://stackoverflow.com/questions/8464677/why-is-jsonrequestbehavior-needed">JsonRequestBehavior.AllowGet</a> and prepared <strong>factory designers for creating a desired result type. </strong>
<ul>
	<li><strong>Success - </strong>json объект { data:setData, success:true, redirectTo:string.Empty }</li>
</ul>
<pre class="lang:c# decode:true">IncodingResult.Success(someData);</pre>
<em>примечание: someData может быть null, тогда результат просто сигнализирует об удачном завершении <strong>action</strong></em>
<ul>
	<li><strong> Error</strong>  - json object{ data:setData, success:false, redirectTo:string.Empty }</li>
</ul>
<pre class="lang:c# decode:true">IncodingResult.Error(someData);</pre>
Note: <em>someData could be null. In this case the result just gives a signal about successful </em><strong><em>action</em></strong><em> completion. </em>
<ul>
	<li> <strong>RedirectTo</strong> - json object{ data:null, success:true, redirectTo:someUrl }</li>
</ul>
<pre class="lang:c# decode:true">IncodingResult.RedirectTo(url)</pre>
Note: if after realization of any <em>( Ajax, Submit or etc )</em><em> </em><strong><em>action  as the result bring back </em></strong><em>redirectTo, window.location = url actuates on the client</em>

<em>Each type-result puts back call </em><em>(On callback ). There is a comparison table below</em>

[table caption="Compare incoding result"]
Result,<a href="http://incframework.com/en-US/TutorialStructure#On_Success">On Success</a>,<a href="http://incframework.com/en-US/TutorialStructure#On_Complete">On Complete</a>,<a href="http://incframework.com/en-US/TutorialStructure#On_Error">On Error</a>,<a href="http://incframework.com/en-US/TutorialStructure#On_Begin">On Begin</a>,<a href="http://incframework.com/en-US/TutorialStructure#On_Break">On Break</a>
IncodingResult.Success,Yes,Yes,No,Before Action,No
IncodingResult.Error,No,Yes,Yes,Before Action,No
IncodingResult.RedirectTo,No,No,No,Before Action,No
[/table]
<h5></h5>
[wpspoiler name="Integration with Controller" ]

Factory methods  <strong>IncodingResult</strong>  are low-level, therefore it’s better to use methods from basic IncControllerBase class
<ul>
	<li><strong>IncJson</strong>– is an alternative to <strong>Success</strong></li>
</ul>
<pre class="lang:c# decode:true">[HttpGet]
public ActionResult FetchContacts()
{
    return IncJson(GetContacts());
}</pre>
<ul>
	<li><strong>IncView</strong> - realize render <strong>View</strong> in line and brings back through  <strong>IncodingResult.Success</strong></li>
</ul>
<pre class="lang:c# decode:true">[HttpGet]
public ActionResult Contact()
{            
    return IncView(GetContacts().FirstOrDefault());
}</pre>
<em>Note: IncView works by an <a href="http://stackoverflow.com/questions/483091/render-a-view-as-a-string">algorithm </a></em>
<ul>
	<li><strong>IncPartialView</strong>–is an analog to  <strong>IncView</strong>, but with a chance to show the way to  <strong>View</strong></li>
</ul>
<pre class="lang:c# decode:true">[HttpGet]
public ActionResult Contact()
{            
    return IncPartialView("NewViewName",GetContacts().FirstOrDefault());
}</pre>
<ul>
	<li><strong>IncRedirect –  </strong>is an alternative to  <strong>RedirectTo</strong></li>
</ul>
<pre class="lang:c# decode:true">[HttpGet]
public ActionResult RedirectTo(string url)
{
    return IncRedirect(url);
}</pre>
<ul>
	<li><strong>IncRedirectToAction – </strong>redirect on the concrete  <strong>Action</strong> in <strong>Controller</strong></li>
</ul>
<pre class="lang:c# decode:true">[HttpGet]
public ActionResult RedirectTo()
{
    return IncRedirectToAction("Action","Controller");
}</pre>
[/wpspoiler]

<em><strong>How to get data?</strong></em>
<ul>
	<li><strong>Direct (</strong><strong> </strong><strong><a href="http://incframework.com/en-US/TutorialStructure#Direct">sandbox</a></strong><strong> </strong><strong>) –</strong> is more often used as free action, but supports a possibility to inform</li>
</ul>
<pre class="lang:c# decode:true">Html.When(JqueryBind.InitIncoding)
    .Do()
    .Direct(IncodingResult.Success(@&lt;span&gt; Direct content &lt;/span&gt;))
    .OnSuccess(dsl =&gt; // some code) .AsHtmlAttributes()
    .ToDiv()</pre>
<em>Note: Error, Redirect could be used as data</em>

[wpspoiler name="Scenario" ]
<h5>Data as Model</h5>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.InitIncoding)
      .Do()
      .Direct(IncodingResult.Success(Model.Contacts))
      .OnSuccess(dsl =&gt;dsl.Self().Core().Insert.WithTemplateByUrl(urlTemplate).Html())
      .AsHtmlAttributes()
      .ToDiv())</pre>
<em>Note: script is used if client template, but data are got</em><em> </em><strong><em>Ajax</em></strong>
<h5><strong>Static Content Setting in </strong></h5>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.InitIncoding)
      .Do()
      .Direct(IncodingResult.Success(@&lt;span&gt; Static content &lt;/span&gt;))
      .OnSuccess(dsl =&gt; dsl.Self().Core().Insert.Html())
      .AsHtmlAttributes()
      .ToButton("Insert html"))</pre>
<em> Note: the script is used, if it is necessary to set in data, that are permanent (empty line setting in, adding form elements,  etc.)</em>

[/wpspoiler]
<ul>
	<li><strong>Ajax ( <a href="http://incframework.com/en-US/TutorialStructure#Ajax">sandbox</a> ) -</strong> request to the server</li>
</ul>
<pre class="lang:c# decode:true">Html.When(JqueryBind.InitIncoding)
    .Do()
    .Ajax(options =&gt;
       {
         options.Url = Url.Action("Contact", "Data");
         options.Type = HttpVerbs.Get;
        })
     .OnSuccess(dsl =&gt; // some code)
     .AsHtmlAttributes()
     .ToDiv()</pre>
<em>Note: </em><strong><em>AjaxGet</em></strong><em>, <strong>AjaxPost</strong>  is “named” version of Ajax</em>
<ul>
	<li><strong>Submit On ( </strong><a href="http://incframework.com/en-US/TutorialStructure#Submit"><strong>sandbox</strong></a><strong>) – </strong>send mentioned  <strong>form </strong>through <strong>ajax </strong>( requires connection to  <a href="http://malsup.com/jquery/form/">jquery form</a> )</li>
</ul>
<pre class="lang:c# decode:true">@using (Html.BeginForm("PostContact", "Data"))
{
  &lt;input type="text" name="Value"&gt;
  @(Html.When(JqueryBind.Click)
        .DoWithStopPropagation()
        .SubmitOn(r=&gt; r.Self().Closest(HtmlTag.Form))
        .OnSuccess(dsl =&gt; // some code)
        .AsHtmlAttributes()
        .ToInput(HtmlInputType.Submit,"Post" ))
}</pre>
Note: this example shows a script, when Submit is realized on behalf of button. In this case as event service we choose <em>Stop Propagation to stop event progress “Click” above (up to form)</em>
<ul>
	<li><strong>Submit  – </strong>is similar to  <strong>SubmitOn, </strong>but has no possibility to indicate<strong>form</strong>, and always sends a nearest (closest ) <strong>form</strong> in the hierarchy Dom elements or <a href="http://blog.incframework.com/power-selector/">self</a>  if it is <strong>form</strong></li>
</ul>
<pre class="lang:c# decode:true">@using (Html.When(JqueryBind.Submit)
            .DoWithPreventDefault()
            .Submit()
            .OnSuccess(dsl =&gt; // some code)
            .AsHtmlAttributes(new { action = Url.Action("PostContact", "Data"),method="POST" })
            .ToBeginTag(Html, HtmlTag.Form))
{
 &lt;input type="text" name="Value"&gt;
 &lt;input type="submit" value="Post"&gt;
}</pre>
<em>Note: example shows a script, when form intercepts “Submit” event and further realizes Submit action, that’s why as event service we choose Prevent Default to annual form send by standard browser methods </em>
<ul>
	<li><strong>Events (</strong><a href="http://incframework.com/en-US/TutorialStructure#Event"><strong>sandbox </strong></a><strong>) – getting data from event, realization consists of two elements</strong>:
<ul>
	<li><strong>The source– </strong>“throws” data through trigger</li>
</ul>
</li>
</ul>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.InitIncoding)
      .Do()
      .AjaxGet(Url.Action("Contact", "Data"))
      .OnSuccess(dsl =&gt; dsl.WithId(eventContainerId).Core().Trigger.Incoding())
      .AsHtmlAttributes()
      .ToDiv())</pre>
<em>Note: any of them  could be used as Action</em>
<ul>
<ul>
	<li><strong>The receiver –</strong>the element, which the <strong>invoke is</strong> directed at.</li>
</ul>
</ul>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.None)
      .Do()
      .Event()
      .OnSuccess(dsl =&gt; dsl.Self().Core().Insert.Html())
      .AsHtmlAttributes(new { id = eventContainerId })
      .ToDiv())</pre>
<em>Note: work with data is constricted as with other </em><strong><em>Action</em></strong>

<em>Events by itself has no sense, but together  with </em><strong>Where allows  building up  complex  scripts, e.g. chat window</strong>

[wpspoiler name="Sample" ]

<strong>The term: </strong>on a single page N-th amount of chat windows could be opened. They are tied up with chosen contact, where messages are shown.

<strong>Decision 1:</strong> each window on the base of its Id (or other concrete) requests messages on Ajax
<pre class="lang:c# decode:true">@{
 Func&lt;string,MvcHtmlString&gt; createWindow = (id) =&gt;
   {
       return Html.When(JqueryBind.None)
                  .Do()
                  .AjaxGet(Url.Action("FetchContacts", "Data",new {Id = id}))
                  .OnSuccess(dsl =&gt;
                                 {
                                     string urlTemplate = Url.Dispatcher().Model(new TableContactTmpl { IsShowInfo = false })
                                                             .AsView("~/Views/Template/Table_Contact_Tmpl.cshtml");
                                     dsl.Self().Core().Insert.WithTemplateByUrl(urlTemplate).Html();
                                 })
                  .AsHtmlAttributes(new { @class="window col-lg-2" })
                  .ToDiv();
   };
}
@createWindow("1")
@createWindow("2")
@createWindow("3")
@createWindow("4")</pre>
<strong>Problem: </strong>as the number of  windows increases, the number of  requests on the server would grow, which may  result in:
<ul>
	<li>Overfill maximum limit of browser requests and there will be a delay between requests</li>
	<li>Load the asp.net pool</li>
</ul>
<strong>Decision 2</strong>: the  previous solution variant had a problem with a huge amount of data sources, therefore a slight  modernization of code adding one more element, one  can do with a single request for any amount of windows
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.InitIncoding)
      .Do()
      .AjaxGet(Url.Action("FetchContacts", "Data"))
      .OnSuccess(dsl =&gt; dsl.WithClass("window").Core().Trigger.Incoding())
      .AsHtmlAttributes()
      .ToDiv())

@{
 Func&lt;string, MvcHtmlString&gt; createWindow = (id) =&gt;
   {
       return Html.When(JqueryBind.None)
                  .Do()
                  .Event()
                  .Where(r =&gt; r.Id == id)
                  .OnSuccess(dsl =&gt;
                                 {
                                     string urlTemplate = Url.Dispatcher().Model(new TableContactTmpl { IsShowInfo = false })
                                                             .AsView("~/Views/Template/Table_Contact_Tmpl.cshtml");
                                     dsl.Self().Core().Insert.WithTemplateByUrl(urlTemplate).Html();
                                 })
                  .AsHtmlAttributes(new { @class="window col-lg-2" })
                  .ToDiv();
   };
}
@createWindow("1")
@createWindow("2")
@createWindow("3")
@createWindow("4")</pre>
<em>Note: now there is only one request, but we „add“ data to all elements with “window” class, but windows knows which Id should be used for  filtering.  </em>
<p style="text-align: center;"><strong>Структурное изображение решения</strong></p>
<img class="aligncenter" src="http://content.screencast.com/users/VladA4/folders/Snagit/media/ab708dda-6fb3-4415-9bf5-39309e87caaa/01.28.2014-21.42.png" alt="" width="592" height="423" />

[/wpspoiler]
<ul>
	<li><strong>Ajax Hash</strong>– <strong>is</strong> an analog of Submit, not for  <strong>form</strong>, but for</li>
</ul>
Example with no <strong>Ajax Hash</strong>
<pre class="lang:c# decode:true">AjaxGet(Url.Action("Action", "Controller",new { 
                        Id = Selector.Incoding.HashQueryString("id"),
                        First = Selector.Incoding.HashQueryString("first"),
                        Last = Selector.Incoding.HashQueryString("last"),
                                              }))</pre>
<em>Note: all parameters are obtained from hash, but we must indicate each of them </em>

Example with <strong>Ajax Hash</strong>
<pre class="lang:c# decode:true">AjaxHashGet(Url.Action("Action","Controller"))</pre>
<em>Note: now the example works similar to  </em><strong><em>Submit form</em></strong><em> ( serialization </em><strong><em>hash</em></strong><em> ), but collect parameters from <strong>hash</strong></em>
<h3 style="text-align: left;"> Ajax Vs Submit Or Ajax Vs Hash</h3>
All “action”, except <strong>Direct</strong> and <strong>Evetns</strong> , works with <strong>ajax, that’s why the question could come up: in what case which to use </strong>
<ul>
	<li>If you fill N-th amount of input, put in <strong>form</strong> and further <strong>action</strong><strong>Submit</strong></li>
	<li>If search parameters are kept in hash, use <strong>action AjaxHash</strong></li>
	<li>In all other cases could be realized with <strong>action</strong><strong>Ajax</strong></li>
</ul>
[wpspoiler name="Examples of scripts for Ajax" ]
<h5><strong>Status change</strong></h5>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.Click)
      .Do()
      .AjaxPost(Url.Action("Change","Status",new {New = Status.Approve}))
      .OnSuccess(dsl =&gt; //some code)
      .AsHtmlAttributes()
      .ToButton("Change"))</pre>
<h5><strong> </strong><strong>Loading</strong><strong> drop down</strong></h5>
<pre class="lang:c# decode:true">@Html.DropDownList("test", 
                   new SelectList(new[] { }),
                   Html.When(JqueryBind.InitIncoding)
                       .Do()
                       .AjaxGet(Url.Action("Fetch", "Country"))
                       .OnSuccess(dsl =&gt; //some code )
                       .AsHtmlAttributes())</pre>
And many other tasks (content for dialog, record deletion)

[/wpspoiler]
<h1 style="text-align: center;">  Insert</h1>
This is one of a set of executables that are available under <strong>On callback,</strong> that is why this is not an obligatory element of a line, but most of scripts are aimed to insert findings in Dom element. “Insert” repeatedly figured in examples from other articles, but now we are going to examine the work of this executable better
<h4 style="text-align: left;">Data</h4>
<ul>
	<li><strong>Content - </strong> <strong>controller</strong> brings back <strong>IncView</strong> or <strong>IncPartialView</strong></li>
</ul>
[wpspoiler name="Sample" ]
<h5>CodeController</h5>
<pre class="lang:c# decode:true">[HttpGet]
public ActionResult Contact()
{            
    return IncView(new ContactVm());
}</pre>
<em>Note: IncView will look for Contact.cshtml in file View/$Controller$ </em>
<h5> CodeView</h5>
<pre class="lang:c# decode:true">Html.When(JqueryBind.InitIncoding)
    .Do()
    .AjaxGet(Url.Action("Contact", "Data"))
    .OnSuccess(dsl =&gt; dsl.Self().Core().Insert.Html())
    .AsHtmlAttributes()
    .ToDiv()</pre>
<h5><strong>Alternative variant on MVD without Controller</strong></h5>
<pre class="lang:c# decode:true">Html.When(JqueryBind.InitIncoding)
    .Do()
    .AjaxGet(Url.Dispatcher().Model(new ContactVm()).AsView("~/Views/Template/Contact.cshtml"))
    .OnSuccess(dsl =&gt; dsl.Self().Core().Insert.Html())
    .AsHtmlAttributes()
    .ToDiv()</pre>
<em> </em>

<em>Note: realization on mvd dispense writing of</em><em> </em><strong><em>controller</em></strong>

[/wpspoiler]
<ul>
	<li><strong>Object</strong> -  <strong>controller </strong>brings back IncJson</li>
</ul>
[wpspoiler name="Пример" ]
<h5>Code Controller</h5>
<pre class="lang:c# decode:true">[HttpGet]
public ActionResult FetchContact()
{
    return IncJson(new ContactVm());
}</pre>
<h5>Code View</h5>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.InitIncoding)
      .Do()
      .AjaxGet(Url.Action("FetchContact", "Data"))
      .OnSuccess(dsl =&gt; dsl.Self().Core().Insert.WithTemplateByUrl(urlTemplate).Html())
      .AsHtmlAttributes()
      .ToDiv())</pre>
<em>note:  urlTemplate must return </em><em> </em><strong><em>IncView</em></strong>
<h5><strong>Alternative variant on MVD without Controller</strong></h5>
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.InitIncoding)
      .Do()
      .AjaxGet(Url.Dispatcher().Query(new GetContactQuery()).AsJson())
      .OnSuccess(dsl =&gt; dsl.Self().Core().Insert.WithTemplateByUrl(urlTemplate).Html())
      .AsHtmlAttributes()
      .ToDiv())</pre>
[/wpspoiler]
<h4> Methods</h4>
<ul>
	<li><a href="http://api.jquery.com/after/">After</a></li>
	<li><a href="http://api.jquery.com/html/">Html</a></li>
	<li><a href="http://api.jquery.com/prepend/">Prepend</a></li>
	<li><a href="http://api.jquery.com/text/">Text</a></li>
	<li><a href="http://api.jquery.com/val/">Val</a></li>
	<li><a href="http://api.jquery.com/before/">Before</a></li>
	<li><a href="http://api.jquery.com/append/">Append</a></li>
</ul>
<em>Note: each method has its analog in jquery</em>
<h4>Complex Model</h4>
All examples were constructed on one model, but there are could be other scripts when findings are “container”. To this effect in Insert is a chance to show with which part of model works through<strong>.</strong>
<pre class="lang:c# decode:true">dsl.WithId(newsContainerId).Core().Insert.For&lt;ContactVM&gt;(r =&gt; r.News).WithTemplateByUrl(urlTemplate)).Html();</pre>
<em>Note: as T for For must be showed a model that is in data </em>

[wpspoiler name="Examples of scripts " ]

<em>Note: each method has its analog in jquery</em>

<strong>Condition</strong>: by Id records get its model and update form fields

<strong>Decision 1</strong>:  after loading to update the hole form, built it again, but sometimes this can complicate this task, of form has extra elements.

<strong>Decision 2:</strong> after loading to update only necessary fields
<pre class="lang:c# decode:true">@(Html.When(JqueryBind.Click)
      .Do()
      .AjaxGet(Url.Action("FetchContactById", "Data", new { id = each.For(r =&gt; r.Id) }))
      .OnSuccess(dsl =&gt;
               {
                dsl.With(r =&gt; r.Name(s =&gt; s.Last)).Core().Insert.For&lt;ContactVm&gt;(r =&gt; r.Last).Val();
                dsl.With(r =&gt; r.Name(s =&gt; s.First)).Core().Insert.For&lt;ContactVm&gt;(r =&gt; r.First).Val(); 
                dsl.With(r =&gt; r.Name(s =&gt; s.City)).Core().Insert.For&lt;ContactVm&gt;(r =&gt; r.City).Val(); 
               })
      .AsHtmlAttributes()
      .ToButton("Show info"))</pre>
<em>Note: you can use reflection and build Insert.For dynamically</em>
<h6><strong>One request, a lot of models </strong></h6>
<strong>Condition</strong>:  update N-th amount of elements by different data

<strong>Decision 1</strong>: each element makes a request of its data, but it leads to increase load in server and slow page loading because of large amount of isochronic

<em>Note: partially this problem could be solved by Ajax Asyc in false installation. In this case a page would be loaded by pieces. That </em><em>is </em><em>also </em><em>permissible</em>

<strong>Decision 2: </strong>one element load all data than through For “pointwise” to update
<pre class="lang:c# decode:true">Html.When(JqueryBind.InitIncoding)
    .Do()
    .AjaxGet(Url.Action("FetchComplex", "Data"))
    .OnSuccess(dsl =&gt;
                   {
                       dsl.WithId(newsContainerId).Core().Insert.For&lt;ComplexVm&gt;(r =&gt; r.News).WithTemplateByUrl(urlTemplateNews).Html();
                       dsl.WithId(contactContainerId).Core().Insert.For&lt;ComplexVm&gt;(r =&gt; r.Contacts).WithTemplateByUrl(urlTemplateContacts).Html();
                   })
    .AsHtmlAttributes()
    .ToDiv()</pre>
<em>Note: for each</em><strong><em> For</em></strong><em> you can use its  </em><strong><em>template</em></strong>

[/wpspoiler]
<h5>Insert For Vs Trigger For</h5>
Method of Executor ( executable ) Trigger looks like Form ethos, but it coupled with  <strong>action events </strong> and let delegate data to other elements but don’t make actions. Example:
<h5>Insert For ( <a href="http://incframework.com/en-US/TutorialExecutable#Insert_Generic">sandbox</a> )</h5>
<pre class="lang:c# decode:true">dsl.WithId(newsContainerId).Core().Insert.For&lt;ComplexVm&gt;(r =&gt; r.News).WithTemplateByUrl(urlTemplateNews).Html();</pre>
<h5> Trigger For ( <a href="http://incframework.com/en-US/TutorialExecutable#Trigger_For">sandbox </a>)</h5>
<ul>
	<li>The source</li>
</ul>
<pre class="lang:c# decode:true">dsl.WithId(newsContainerId).Core().Trigger.For&lt;ComplexVM&gt;(r =&gt; r.News).Incoding();</pre>
<ul>
	<li> The receiver</li>
</ul>
<pre class="lang:c# decode:true">Html.When(JqueryBind.None)
    .Do()
    .Events()
    .OnSuccess(dsl =&gt; dsl.WithId(newsContainerId).Core().Insert.WithTemplateByUrl(urlTemplateNews).Html())
    .AsHtmlAttributes()
    .ToDiv()</pre>
<em>Note: realization with Events needs extra element</em>

Decision with  <strong>Trigger</strong> looks more complicated, therefore if you need only put different parts of one model it would be better to use <strong>Insert For, but possibility of </strong> <strong>Where for</strong> <strong>Events</strong>  lets realize scripts that couldn’t be solved with  <strong>Insert</strong>
<h2 style="text-align: center;">Conclusion</h2>
This is the 1<sup>st</sup> part of planed cycle of articles about different links, but in fact it’s rather difficult to examine all possible tasks because IML is not so flexible. But this fact let us group in a completely different variations for complex scripts

I’ve picked out fundamental:
<ul>
	<li>Do, Action, Insert</li>
	<li>When, Trigger</li>
	<li>Submit Form</li>
	<li>Search hash</li>
</ul>
In the nearest future I’ll publish separate articles about each script.

<strong>P.S.</strong> if somebody has a script that he wants to be examined, how to solve in Incoding Framework, leave it in comments and I’ll make an article.
