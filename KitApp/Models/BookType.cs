using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KitApp.Models
{
    public class BookType
    { 
        [Key] //primary key
        public int Id { get; set; }
        [Required(ErrorMessage ="Kitap Adı Boş Bırakılamaz")] //not null
        [MaxLength(30)]
        [DisplayName("Kitap Türü Adı")]
        public string Name { get; set; }
    }
}
