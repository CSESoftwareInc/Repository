using CSESoftware.Core.Entity;
using System;

namespace CSESoftware.Repository.Models
{
    public class BatchQueries
    {
        public Guid BatchIdentifier { get; set; }
        public object RecordId { get; set; }
    }
}