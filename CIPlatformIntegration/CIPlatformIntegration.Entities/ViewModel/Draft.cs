using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CIPlatformIntegration.Entities.ViewModel
{
    public class Draft
    {
        [Required(ErrorMessage = "Please enter your title.")]
        public string title { get; set; }

        [Required(ErrorMessage = "Please enter your description.")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please enter your date.")]
        public string date { get; set; }

        [Required(ErrorMessage = "Please enter your Paths.")]
        public IEnumerable<string> Paths { get; set; }

        public string status { get; set; }

       


    }
}
