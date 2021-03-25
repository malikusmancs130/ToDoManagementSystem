using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    [ModelMetadataType(typeof(ToDoMetaData))]
    public partial class ToDoViewModel
    {
    }

    public class ToDoMetaData
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Available quantity")]
        public int AvailableQuantity { get; set; }
    }
}
