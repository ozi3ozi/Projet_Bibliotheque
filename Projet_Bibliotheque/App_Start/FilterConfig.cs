﻿using System.Web;
using System.Web.Mvc;

namespace Projet_Bibliotheque
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
