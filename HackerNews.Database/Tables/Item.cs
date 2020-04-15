namespace HackerNews.Database.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        public bool? Deleted { get; set; }

        public string Type { get; set; }

        public string Author { get; set; }

        public long? Time { get; set; }

        public string Text { get; set; }

        public bool? Dead { get; set; }

        public long? Parent { get; set; }

        public long? Poll { get; set; }

        public string Kids { get; set; }

        public string URL { get; set; }

        public long? Score { get; set; }

        public string Title { get; set; }

        public string Parts { get; set; }

        public long? Descendants { get; set; }
    }
}
