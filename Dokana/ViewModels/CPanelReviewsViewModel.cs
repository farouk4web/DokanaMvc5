using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class CPanelReviewsViewModel
    {
        public IEnumerable<Review> Reviews { get; set; }

        public int CurrentPageNumber { get; set; }

        public int CountOfReviews { get; set; }
    }
}