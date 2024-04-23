using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class CPanelUsersViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        public int CurrentPageNumber { get; set; }

        public int CountOfUsers { get; set; }
    }
}