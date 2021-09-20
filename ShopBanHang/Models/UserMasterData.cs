using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBanHang.Context
{
    public partial class UserMasterData
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "pass rỗng")]
        public string Password { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string Date { get; set; }
    }
}