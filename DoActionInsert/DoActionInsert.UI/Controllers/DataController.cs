namespace DoActionInsert.UI.Controllers
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using DoActionInsert.UI.Models;
    using Incoding.MvcContrib;

    #endregion

    public class DataController : IncControllerBase
    {
        #region Http action

        [HttpGet]
        public ActionResult Contact()
        {
            return IncView(GetContacts().FirstOrDefault());
        }

        [HttpGet]
        public ActionResult FetchComplex()
        {
            return IncJson(new ComplexVm
                               {
                                       Contacts = GetContacts(), 
                                       News = new List<NewsVm>
                                                  {
                                                          new NewsVm { CreateDt = DateTime.Now.AddDays(-5).ToShortDateString(), Content = "Old news" }, 
                                                          new NewsVm { CreateDt = DateTime.Now.AddDays(5).ToShortDateString(), Content = "Future news" }, 
                                                  }
                               });
        }

        [HttpGet]
        public ActionResult FetchContactById(string id)
        {
            return IncJson(GetContacts().First(r => r.Id == id));
        }

        [HttpGet]
        public ActionResult FetchContacts()
        {
            return IncJson(GetContacts());
        }

        [HttpPost]
        public ActionResult PostContact(ContactVm contactVm)
        {
            return IncView(contactVm);
        }

        [HttpGet]
        public ActionResult RedirectTo(string url)
        {
            return IncRedirect(url);
        }

        #endregion

        static List<ContactVm> GetContacts()
        {
            return new List<ContactVm>
                       {
                               new ContactVm { Id = "1", First = "Vlad", Last = "Popov", City = "Taganrog" }, 
                               new ContactVm { Id = "2", First = "Gena", Last = "Ivanov", City = "Rostov" }, 
                               new ContactVm { Id = "3", First = "Anton", Last = "Belov", City = "St" }, 
                               new ContactVm { Id = "4", First = "Victor", Last = "Karpatovv", City = "St" }
                       };
        }
    }
}