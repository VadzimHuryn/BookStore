using BookStore.Models.ViewModels;
using System.Collections.Generic;

namespace BookStore.Web.Client
{
    public static class UserStaticInfo
    {
        public static User CurrentUser { get; set; }
        public static bool IsLogged { get; set; }
        public static bool IsSeller { get; set; }
    }
}
