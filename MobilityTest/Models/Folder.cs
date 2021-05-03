using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MobilityTest.Models
{
    public partial class Folder:Node
    {
        public Folder()
        {
            File = new HashSet<File>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual ICollection<File> File { get; set; }
    }
}
