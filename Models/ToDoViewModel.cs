using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public partial class ToDoViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Completed { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool? Status { get; set; }
    }
}
