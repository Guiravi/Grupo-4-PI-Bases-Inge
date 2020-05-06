using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// Esta clase correspone a la vista de MemberFaqs
/// </summary>
namespace TheCoffeePlace.Views
{
    public interface IViewMemberFaqs
    {
        string Category { get; set; }
        string Question { get; set; }
        string Answer { get; set; }
        Label Label { get; set; }
    }
}