using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.DTOs
{
    public class RateDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public byte StarsCount { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string PublishDate { get; set; }

        public string UserFullName { get; set; }
        public string UserId { get; set; }
        public string UserImageSrc { get; set; }
    }
}