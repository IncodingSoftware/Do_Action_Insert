Do Action Insert
===

## Ajax content as html

    @(Html.When(JqueryBind.InitIncoding)
      .Do()
      .Ajax(options =>
                {
                    options.Url = Url.Action("Contact", "Data");
                    options.Type = HttpVerbs.Get;
                })
      .OnSuccess(dsl => dsl.Self().Core().Insert.Html())
      .AsHtmlAttributes()
      .ToDiv())    
      
## Ajax content as json

    @(Html.When(JqueryBind.InitIncoding)
      .Do()
      .AjaxGet(Url.Action("FetchContacts", "Data"))
      .OnSuccess(dsl =>
                     {
                         string asTmpl = Url.Dispatcher()
                                            .Model(new TableContactTmpl { IsShowInfo = true })
                                            .AsView("~/Views/Template/Table_Contact_Tmpl.cshtml");
                         dsl.Self().Core().Insert.WithTemplateByUrl(asTmpl).Html();
                     })
      .AsHtmlAttributes()
      .ToDiv())
      
## Insert generic 

    @(Html.When(JqueryBind.Click)
      .Do()
      .AjaxGet(Url.Action("FetchContactById", "Data", new { id = each.For(r => r.Id) }))
      .OnSuccess(dsl =>
                     {
               dsl.With(r => r.Name<ContactVm>(s => s.Last)).Core().Insert.For<ContactVm>(r => r.Last).Val();
               dsl.With(r => r.Name<ContactVm>(s => s.First)).Core().Insert.For<ContactVm>(r => r.First).Val();
               dsl.With(r => r.Name<ContactVm>(s => s.City)).Core().Insert.For<ContactVm>(r => r.City).Val();
                     })
      .AsHtmlAttributes()
      .ToButton("Show info"))
      
## Insert generic with template

    @(Html.When(JqueryBind.InitIncoding)
      .Do()
      .AjaxGet(Url.Action("FetchComplex", "Data"))
      .OnSuccess(dsl =>
                     {
         string urlTmplNews = Url.Dispatcher().AsView("~/Views/Template/Table_News_Tmpl.cshtml");
         dsl.WithId(newsContainerId).Core().Insert.For<ComplexVm>(r => r.News).WithTemplateByUrl(urlTmplNews).Html();
                          
         string urlTmplContact = Url.Dispatcher()
                                    .Model(new TableContactTmpl())
                                    .AsView("~/Views/Template/Table_Contact_Tmpl.cshtml");
         dsl.WithId(contactContainerId).Core().Insert.For<ComplexVm>(r => r.Contacts).WithTemplateByUrl(urlTmplContact).Html();
                     })
      .AsHtmlAttributes()
      .ToDiv())
      
      
## Submit ( bind on form )

    @using (Html.When(JqueryBind.Submit)
            .DoWithPreventDefault()
            .Submit()
            .OnSuccess(dsl => dsl.WithId(submitOnFormResultId).Core().Insert.Html())
            .AsHtmlAttributes(new { action = Url.Action("PostContact", "Data"), method = "POST" })
            .ToBeginTag(Html, HtmlTag.Form))
    {
    <div class="form-group">
        <label class="col-sm-2 control-label">First</label>
        <input type="text" name="First"/>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">Last</label>
        <input type="text" name="Last"/>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">City</label>
        <input type="text" name="City"/>
    </div>
    <input type="submit" value="Post"/>
    }
    
## Submit ( bind on input submit )

    @using (Html.BeginForm("PostContact", "Data"))
    {
    <div class="form-group">
        <label class="col-sm-2 control-label">First</label>
        <input type="text" name="First"/>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">Last</label>
        <input type="text" name="Last"/>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">City</label>
        <input type="text" name="City"/>
    </div>
    @(Html.When(JqueryBind.Click)
          .DoWithPreventDefault()
          .SubmitOn(selector => selector.Self().Closest(HtmlTag.Form))
          .OnSuccess(dsl => dsl.WithId(submitOnInputId).Core().Insert.Html())
          .AsHtmlAttributes()
          .ToInput(HtmlInputType.Submit, "Post"))
    }


                                                 
* [More sample](http://blog.incframework.com/ru/do-action-insert/)
