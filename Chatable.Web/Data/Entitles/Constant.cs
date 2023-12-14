using Chatable.Web.Data.Entitles.Model;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Chatable.Web.Data.Entitles
{
    public static class Constant
    {
        public const string defaultImgMale = "images/default_male.jpg";
        public const string defaultImgFemale = "images/default_female.jpg";
        public const string defaultImgGroup = "images/default_group.jpg";
        public const string defaultBg = "images/pattern_bg.jpg";


        public static readonly IList<String> states = new ReadOnlyCollection<String>
        (new List<String>
        {
            "Alabama", "Alaska", "American Samoa"
        });

        

        public static readonly List<User> usersList = new List<User>
        {
        new User("nhubaole", " Lê Bảo Như","","2023-11-19 07:58:48.084+00"),
        new User("nhihuynh", " Lê Bảo Như","","2023-11-19 07:58:48.084+00"),
        };

        public static readonly User currentUser = new("BC", " BC", "", "2023-12-05 11:27:29.397+00");

       

    }

}
