using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FM.TreasureHunt.Web.ViewModels
{
    public class RegisterFindViewModel
    {
        [DisplayName("Reference Number")]
        [Required] public string ReferenceNumber { get; set; }
    }
}