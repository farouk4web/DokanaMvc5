using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.DTOs
{
    public class FavoriteDto
    {
        [Required]
        public int ProductId { get; set; }
    }
}