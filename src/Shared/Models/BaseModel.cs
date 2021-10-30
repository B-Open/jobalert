using System;

namespace Shared.Models
{
    public class BaseModel
    {
        /// <summary>
        /// The primary key of the database object
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Whether the item is deleted or not. 0 = active, 1 = deleted
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// The DateTime when the item was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The DateTime when the item was last updated
        /// </summary>
        public DateTime UpdatedOn { get; set; }
    }
}
